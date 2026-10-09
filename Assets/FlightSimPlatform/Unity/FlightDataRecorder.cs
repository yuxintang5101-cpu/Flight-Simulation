using System;
using System.Globalization;
using System.IO;
using System.Security;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using FlightSim.Platform.Integration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlightSim.Platform.Unity
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(0)]
    public sealed class FlightDataRecorder : MonoBehaviour, IFlightTelemetrySink
    {
        private const int TelemetryQueueCapacity = 2048;
        private const int EventQueueCapacity = 256;
        private const long DefaultTelemetryFileBytes = 256L * 1024L * 1024L;

        [SerializeField] private FlightSimulationHost simulationHost;
        [SerializeField] private bool autoStart = true;
        [SerializeField] private KeyCode toggleRecordingKey = KeyCode.F9;
        [SerializeField] private KeyCode markerKey = KeyCode.F10;
        [SerializeField] private long telemetryFileBytes = DefaultTelemetryFileBytes;
        [SerializeField, Min(0.1f)] private float flushIntervalSeconds = 1f;

        private readonly TelemetrySample[] telemetryQueue =
            new TelemetrySample[TelemetryQueueCapacity];
        private readonly PendingEvent[] eventQueue = new PendingEvent[EventQueueCapacity];
        private FlightLogWriter writer;
        private AircraftSystemsState latestSystems;
        private WarningState previousWarnings;
        private int telemetryQueueHead;
        private int telemetryQueueCount;
        private int eventQueueHead;
        private int eventQueueCount;
        private bool hasPreviousWarnings;
        private bool registered;
        private bool started;
        private int droppedTelemetrySamples;
        private int droppedEvents;
        private bool failureLogged;
        private float nextFlushTime;
        private FlightSimulationService registeredService;

        public bool IsRecording => writer != null;
        public string SessionDirectory { get; private set; } = string.Empty;
        public FlightSimulationHost SimulationHost => simulationHost;
        public event Action<bool> RecordingStateChanged;

        private void Start()
        {
            started = true;
            SubscribeToHost();
            RegisterWithHost();
            if (autoStart && registered)
                StartRecording();
        }

        private void OnEnable()
        {
            if (!started)
                return;

            SubscribeToHost();
            RegisterWithHost();
            if (autoStart && registered)
                StartRecording();
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleRecordingKey))
            {
                if (IsRecording)
                    StopRecording();
                else
                    StartRecording();
            }

            if (Input.GetKeyDown(markerKey) && IsRecording)
                AddUserMarker();

            if (!IsRecording)
                return;

            try
            {
                if (Time.unscaledTime >= nextFlushTime)
                {
                    DrainPendingWrites(writer);
                    writer.Flush();
                    nextFlushTime = Time.unscaledTime + Mathf.Max(0.1f, flushIntervalSeconds);
                }
            }
            catch (Exception exception) when (IsFileException(exception))
            {
                DisableAfterFileFailure(exception);
            }
        }

        private void OnDisable()
        {
            UnsubscribeFromHost();
            Unregister();
            StopRecording();
        }

        private void OnDestroy()
        {
            UnsubscribeFromHost();
            Unregister();
            StopRecording();
        }

        private void OnApplicationQuit()
        {
            UnsubscribeFromHost();
            Unregister();
            StopRecording();
        }

        public bool StartRecording(string rootOverride = null)
        {
            if (IsRecording)
                return true;
            if (simulationHost == null || simulationHost.SimulationService == null)
                return false;

            FlightLogWriter candidate = null;
            try
            {
                string baseDirectory = string.IsNullOrWhiteSpace(rootOverride)
                    ? Path.Combine(Application.persistentDataPath, "FlightLogs")
                    : rootOverride;
                baseDirectory = Path.GetFullPath(baseDirectory);
                Directory.CreateDirectory(baseDirectory);
                string sessionDirectory = CreateSessionDirectory(baseDirectory);
                FlightLogMetadata metadata = CreateMetadata();
                candidate = new FlightLogWriter(
                    sessionDirectory,
                    metadata,
                    Math.Max(1L, telemetryFileBytes));
                candidate.WriteRecorderEvent(GetSimulationTime(), "RECORDING_STARTED", sessionDirectory);

                writer = candidate;
                candidate = null;
                SessionDirectory = sessionDirectory;
                ClearQueues();
                CaptureCurrentSystems();
                nextFlushTime = Time.unscaledTime + Mathf.Max(0.1f, flushIntervalSeconds);
                RecordingStateChanged?.Invoke(true);
                return true;
            }
            catch (Exception exception) when (IsFileException(exception))
            {
                DisposeAfterFileFailure(candidate);
                DisableAfterFileFailure(exception);
                return false;
            }
        }

        public void StopRecording()
        {
            FlightLogWriter closingWriter = writer;
            writer = null;
            if (closingWriter == null)
            {
                ClearQueues();
                return;
            }

            try
            {
                DrainPendingWrites(closingWriter);
                closingWriter.WriteRecorderEvent(
                    GetSimulationTime(),
                    "RECORDING_STOPPED",
                    "Recording stopped cleanly.");
                closingWriter.Flush();
                closingWriter.Dispose();
            }
            catch (Exception exception) when (IsFileException(exception))
            {
                DisposeAfterFileFailure(closingWriter);
                LogFailureOnce(exception);
            }
            finally
            {
                ClearQueues();
                RecordingStateChanged?.Invoke(false);
            }
        }

        public void AddUserMarker(string message = "USER_MARKER")
        {
            if (!IsRecording)
                return;

            try
            {
                DrainPendingWrites(writer);
                writer.WriteRecorderEvent(
                    GetSimulationTime(),
                    "USER_MARKER",
                    message ?? string.Empty);
                writer.Flush();
                nextFlushTime = Time.unscaledTime + Mathf.Max(0.1f, flushIntervalSeconds);
            }
            catch (Exception exception) when (IsFileException(exception))
            {
                DisableAfterFileFailure(exception);
            }
        }

        public void OnFastState(in AircraftFastState state)
        {
            if (!IsRecording || !IsLocalAircraft(state.Aircraft))
                return;

            TelemetrySample sample = new TelemetrySample
            {
                Fast = state,
                Systems = latestSystems,
                Input = simulationHost.LatestPilotControlInput,
                RawMouseX = simulationHost.LatestRawMouseX,
                RawMouseY = simulationHost.LatestRawMouseY,
                Frame = Time.frameCount,
                CapturedUtcTicks = DateTime.UtcNow.Ticks
            };
            EnqueueTelemetry(in sample);
        }

        public void OnSystemsState(in AircraftSystemsState state)
        {
            if (!IsLocalAircraft(state.Aircraft))
                return;

            if (hasPreviousWarnings && IsRecording)
                EnqueueWarningEdges(in previousWarnings, in state.Warnings, state.SimulationTimeS);
            latestSystems = state;
            previousWarnings = state.Warnings;
            hasPreviousWarnings = true;
        }

        public void OnTacticalPictureState(in TacticalPictureState state)
        {
            // Tactical state is intentionally excluded from the fixed 50 Hz flight-analysis schema.
        }

        public void OnSimulationEvent(in SimulationEvent simulationEvent)
        {
            if (!IsRecording || !IsLocalAircraft(simulationEvent.Aircraft))
                return;

            PendingEvent pending = new PendingEvent
            {
                IsSimulationEvent = true,
                SimulationEvent = simulationEvent
            };
            EnqueueEvent(in pending);
        }

        private void CaptureCurrentSystems()
        {
            if (simulationHost.TryGetLatest(out AircraftSnapshot snapshot))
            {
                latestSystems = snapshot.Systems;
                previousWarnings = snapshot.Systems.Warnings;
                hasPreviousWarnings = true;
            }
        }

        private FlightLogMetadata CreateMetadata()
        {
            return new FlightLogMetadata
            {
                SchemaVersion = 1,
                ContractVersion = simulationHost.SimulationService.ContractVersion,
                UnityVersion = Application.unityVersion,
                SceneName = SceneManager.GetActiveScene().name,
                AircraftId = simulationHost.LocalAircraft.Value,
                UtcStartTime = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture),
                FixedStepHz = 100
            };
        }

        private string CreateSessionDirectory(string baseDirectory)
        {
            string aircraft = SanitizePathSegment(simulationHost.LocalAircraft.Value);
            string prefix = DateTime.UtcNow.ToString(
                "yyyyMMdd'T'HHmmssfff'Z'",
                CultureInfo.InvariantCulture) + "_" + aircraft;
            string candidate = Path.Combine(baseDirectory, prefix);
            int suffix = 1;
            while (Directory.Exists(candidate) || File.Exists(candidate))
            {
                candidate = Path.Combine(
                    baseDirectory,
                    prefix + "_" + suffix.ToString("000", CultureInfo.InvariantCulture));
                suffix++;
            }

            return candidate;
        }

        private static string SanitizePathSegment(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "aircraft";

            char[] invalid = Path.GetInvalidFileNameChars();
            char[] characters = value.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                if (Array.IndexOf(invalid, characters[i]) >= 0)
                    characters[i] = '_';
            }
            return new string(characters);
        }

        private double GetSimulationTime()
        {
            return simulationHost != null && simulationHost.TryGetLatest(out AircraftSnapshot snapshot)
                ? snapshot.Fast.SimulationTimeS
                : 0.0;
        }

        private bool IsLocalAircraft(AircraftId aircraft)
        {
            return simulationHost != null && aircraft == simulationHost.LocalAircraft;
        }

        private void EnqueueTelemetry(in TelemetrySample sample)
        {
            if (telemetryQueueCount >= telemetryQueue.Length)
            {
                if (droppedTelemetrySamples < int.MaxValue)
                    droppedTelemetrySamples++;
                return;
            }

            int index = (telemetryQueueHead + telemetryQueueCount) % telemetryQueue.Length;
            telemetryQueue[index] = sample;
            telemetryQueueCount++;
        }

        private void EnqueueEvent(in PendingEvent pending)
        {
            if (eventQueueCount >= eventQueue.Length)
            {
                if (droppedEvents < int.MaxValue)
                    droppedEvents++;
                return;
            }

            int index = (eventQueueHead + eventQueueCount) % eventQueue.Length;
            eventQueue[index] = pending;
            eventQueueCount++;
        }

        private void EnqueueWarningEdges(
            in WarningState previous,
            in WarningState current,
            double simulationTimeS)
        {
            EnqueueWarningEdge(previous.MasterCaution, current.MasterCaution, simulationTimeS,
                "WARNING_MASTER_CAUTION_ON", "WARNING_MASTER_CAUTION_OFF",
                "Master caution active.", "Master caution cleared.");
            EnqueueWarningEdge(previous.MasterWarning, current.MasterWarning, simulationTimeS,
                "WARNING_MASTER_WARNING_ON", "WARNING_MASTER_WARNING_OFF",
                "Master warning active.", "Master warning cleared.");
            EnqueueWarningEdge(previous.FireWarning, current.FireWarning, simulationTimeS,
                "WARNING_FIRE_ON", "WARNING_FIRE_OFF",
                "Fire warning active.", "Fire warning cleared.");
            EnqueueWarningEdge(previous.HydraulicWarning, current.HydraulicWarning, simulationTimeS,
                "WARNING_HYDRAULIC_ON", "WARNING_HYDRAULIC_OFF",
                "Hydraulic warning active.", "Hydraulic warning cleared.");
            EnqueueWarningEdge(previous.ElectricalWarning, current.ElectricalWarning, simulationTimeS,
                "WARNING_ELECTRICAL_ON", "WARNING_ELECTRICAL_OFF",
                "Electrical warning active.", "Electrical warning cleared.");
            EnqueueWarningEdge(previous.FuelWarning, current.FuelWarning, simulationTimeS,
                "WARNING_FUEL_ON", "WARNING_FUEL_OFF",
                "Fuel warning active.", "Fuel warning cleared.");
            EnqueueWarningEdge(previous.LowAltitudeWarning, current.LowAltitudeWarning, simulationTimeS,
                "WARNING_LOW_ALTITUDE_ON", "WARNING_LOW_ALTITUDE_OFF",
                "Low altitude warning active.", "Low altitude warning cleared.");
            EnqueueWarningEdge(previous.OverspeedWarning, current.OverspeedWarning, simulationTimeS,
                "WARNING_OVERSPEED_ON", "WARNING_OVERSPEED_OFF",
                "Overspeed warning active.", "Overspeed warning cleared.");
            EnqueueWarningEdge(previous.StallWarning, current.StallWarning, simulationTimeS,
                "WARNING_STALL_ON", "WARNING_STALL_OFF",
                "Stall warning active.", "Stall warning cleared.");
            EnqueueWarningEdge(previous.LandingGearWarning, current.LandingGearWarning, simulationTimeS,
                "WARNING_LANDING_GEAR_ON", "WARNING_LANDING_GEAR_OFF",
                "Landing gear warning active.", "Landing gear warning cleared.");
            EnqueueWarningEdge(previous.CanopyWarning, current.CanopyWarning, simulationTimeS,
                "WARNING_CANOPY_ON", "WARNING_CANOPY_OFF",
                "Canopy warning active.", "Canopy warning cleared.");
        }

        private void EnqueueWarningEdge(
            bool previous,
            bool current,
            double simulationTimeS,
            string activeEvent,
            string clearedEvent,
            string activeMessage,
            string clearedMessage)
        {
            if (previous == current)
                return;

            PendingEvent pending = new PendingEvent
            {
                SimulationTimeS = simulationTimeS,
                EventType = current ? activeEvent : clearedEvent,
                Message = current ? activeMessage : clearedMessage
            };
            EnqueueEvent(in pending);
        }

        private void DrainPendingWrites(FlightLogWriter target)
        {
            if (droppedTelemetrySamples > 0 || droppedEvents > 0)
            {
                target.WriteRecorderEvent(
                    GetSimulationTime(),
                    "QUEUE_OVERFLOW",
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Dropped telemetry samples: {0}; dropped events: {1}.",
                        droppedTelemetrySamples,
                        droppedEvents));
                droppedTelemetrySamples = 0;
                droppedEvents = 0;
            }

            while (telemetryQueueCount > 0)
            {
                TelemetrySample sample = telemetryQueue[telemetryQueueHead];
                target.WriteTelemetry(
                    in sample.Fast,
                    in sample.Systems,
                    in sample.Input,
                    sample.RawMouseX,
                    sample.RawMouseY,
                    sample.Frame,
                    sample.CapturedUtcTicks);
                telemetryQueueHead = (telemetryQueueHead + 1) % telemetryQueue.Length;
                telemetryQueueCount--;
            }

            while (eventQueueCount > 0)
            {
                PendingEvent pending = eventQueue[eventQueueHead];
                if (pending.IsSimulationEvent)
                {
                    target.WriteSimulationEvent(in pending.SimulationEvent);
                }
                else
                {
                    target.WriteRecorderEvent(
                        pending.SimulationTimeS,
                        pending.EventType,
                        pending.Message);
                }
                eventQueueHead = (eventQueueHead + 1) % eventQueue.Length;
                eventQueueCount--;
            }
        }

        private void ClearQueues()
        {
            telemetryQueueHead = 0;
            telemetryQueueCount = 0;
            eventQueueHead = 0;
            eventQueueCount = 0;
            droppedTelemetrySamples = 0;
            droppedEvents = 0;
        }

        private void RegisterWithHost()
        {
            if (simulationHost == null)
                simulationHost = GetComponent<FlightSimulationHost>();
            SubscribeToHost();
            if (registered || simulationHost == null || simulationHost.SimulationService == null)
                return;

            CaptureCurrentSystems();
            registeredService = simulationHost.SimulationService;
            registeredService.RegisterSink(this);
            registered = true;
        }

        private void Unregister()
        {
            if (!registered)
                return;

            registeredService?.UnregisterSink(this);
            registeredService = null;
            registered = false;
        }

        private void SubscribeToHost()
        {
            if (simulationHost == null)
                simulationHost = GetComponent<FlightSimulationHost>();
            if (simulationHost == null)
                return;

            simulationHost.SimulationServiceChanged -= OnSimulationServiceChanged;
            simulationHost.SimulationServiceChanged += OnSimulationServiceChanged;
        }

        private void UnsubscribeFromHost()
        {
            if (simulationHost != null)
                simulationHost.SimulationServiceChanged -= OnSimulationServiceChanged;
        }

        private void OnSimulationServiceChanged(FlightSimulationService service)
        {
            Unregister();
            RegisterWithHost();
        }

        private void DisableAfterFileFailure(Exception exception)
        {
            FlightLogWriter failedWriter = writer;
            writer = null;
            ClearQueues();
            DisposeAfterFileFailure(failedWriter);
            LogFailureOnce(exception);
        }

        private void DisposeAfterFileFailure(FlightLogWriter failedWriter)
        {
            if (failedWriter == null)
                return;

            try
            {
                failedWriter.Dispose();
            }
            catch (Exception disposeException) when (IsFileException(disposeException))
            {
                // The original file error is reported once; cleanup errors remain non-fatal.
            }
        }

        private void LogFailureOnce(Exception exception)
        {
            if (failureLogged)
                return;

            failureLogged = true;
            Debug.LogError(
                "Flight recording disabled after file I/O failure: " + exception.Message,
                this);
        }

        private static bool IsFileException(Exception exception)
        {
            return exception is IOException ||
                   exception is UnauthorizedAccessException ||
                   exception is SecurityException ||
                   exception is ArgumentException ||
                   exception is NotSupportedException;
        }

        private struct TelemetrySample
        {
            public AircraftFastState Fast;
            public AircraftSystemsState Systems;
            public PilotControlInput Input;
            public float RawMouseX;
            public float RawMouseY;
            public int Frame;
            public long CapturedUtcTicks;
        }

        private struct PendingEvent
        {
            public bool IsSimulationEvent;
            public SimulationEvent SimulationEvent;
            public double SimulationTimeS;
            public string EventType;
            public string Message;
        }
    }
}
