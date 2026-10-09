using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace FlightSim.Platform.Unity.Tests.EditMode
{
    public sealed class FlightLogWriterTests
    {
        private static readonly string[] ExpectedTelemetryColumns =
        {
            "frame", "aircraft_id", "tick", "simulation_time_s", "utc_time", "systems_time_s",
            "ecef_position_x_m", "ecef_position_y_m", "ecef_position_z_m",
            "ecef_velocity_x_mps", "ecef_velocity_y_mps", "ecef_velocity_z_mps",
            "body_velocity_x_mps", "body_velocity_y_mps", "body_velocity_z_mps",
            "body_angular_velocity_x_radps", "body_angular_velocity_y_radps", "body_angular_velocity_z_radps",
            "body_to_ecef_quaternion_x", "body_to_ecef_quaternion_y", "body_to_ecef_quaternion_z",
            "body_to_ecef_quaternion_w", "longitude_deg", "latitude_deg", "ellipsoid_height_m",
            "heading_deg", "pitch_deg", "roll_deg", "true_airspeed_mps", "calibrated_airspeed_mps",
            "ground_speed_mps", "mach", "angle_of_attack_deg", "sideslip_deg", "normal_load_factor_g",
            "mean_sea_level_altitude_m", "above_ground_level_altitude_m", "climb_rate_mps",
            "terrain_sample_valid", "terrain_sample_age_s", "input_pitch", "input_roll", "input_yaw",
            "input_throttle", "input_wheel_brake", "input_speed_brake", "input_engine_start",
            "raw_mouse_x", "raw_mouse_y", "applied_pitch_command", "applied_roll_command",
            "applied_yaw_command", "aileron_deflection_deg", "elevator_deflection_deg",
            "rudder_deflection_deg", "leading_edge_flap_deflection_deg", "trailing_edge_flap_deflection_deg",
            "aoa_limiter_active", "g_limiter_active", "roll_rate_limiter_active", "fcc_enabled",
            "autopilot_engaged", "engine_mode", "engine_running", "afterburner_active", "n1_percent",
            "n2_percent", "egt_c", "engine_fuel_flow_kgps", "nozzle_position", "thrust_n",
            "internal_fuel_kg", "external_fuel_kg", "left_fuel_kg", "right_fuel_kg", "total_fuel_kg",
            "fuel_transfer_mode", "fuel_imbalance_kg", "bingo_fuel_kg", "joker_fuel_kg",
            "cg_percent_mac", "fuel_flow_kgps", "low_fuel_warning", "fuel_pump_enabled",
            "main_bus_voltage_v", "essential_bus_voltage_v", "avionics_bus_voltage_v", "battery_voltage_v",
            "generator_current_a", "main_bus_powered", "essential_bus_powered", "avionics_bus_powered",
            "generator_online", "battery_online", "hydraulic_a_pressure_pa", "hydraulic_b_pressure_pa",
            "brake_pressure_pa", "hydraulic_a_online", "hydraulic_b_online", "nose_gear_position",
            "left_main_gear_position", "right_main_gear_position", "left_brake_pressure_pa",
            "right_brake_pressure_pa", "speed_brake_position", "canopy_position",
            "nose_wheel_steering_enabled", "weight_on_wheels", "ins_state", "hud_enabled", "hud_mode",
            "startup_state", "master_mode_air_to_air", "radar_enabled", "radar_locked", "radar_range_m",
            "navigation_computer_enabled", "data_link_enabled", "master_caution", "master_warning",
            "fire_warning", "hydraulic_warning", "electrical_warning", "fuel_warning",
            "low_altitude_warning", "overspeed_warning", "stall_warning", "landing_gear_warning",
            "canopy_warning"
        };

        [Test]
        public void WriterCreatesStableMetadataTelemetryAndEventFiles()
        {
            string root = CreateTemporaryDirectory();
            try
            {
                using (var writer = new FlightLogWriter(root, CreateMetadata(), 1024 * 1024))
                {
                    writer.WriteTelemetry(
                        CreateFastState(),
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        42);
                    writer.WriteRecorderEvent(1.25, "USER_MARKER", "reproduced");
                    writer.WriteSimulationEvent(new SimulationEvent
                    {
                        Aircraft = new AircraftId("TEST-01"),
                        SimulationTimeS = 1.5,
                        Type = SimulationEventType.Warning,
                        Code = 7,
                        Message = "quoted, \"message\"\r\nnext line"
                    });
                    writer.Flush();
                }

                string metadata = File.ReadAllText(Path.Combine(root, "metadata.json"));
                string[] telemetry = File.ReadAllLines(Path.Combine(root, "telemetry_000.csv"));
                string events = File.ReadAllText(Path.Combine(root, "events.csv"));
                Assert.That(metadata, Does.Contain("\"SchemaVersion\": 1"));
                Assert.That(metadata, Does.Contain("\"AircraftId\": \"TEST-01\""));
                Assert.That(telemetry.Length, Is.EqualTo(2));
                string[] header = telemetry[0].Split(',');
                string[] row = telemetry[1].Split(',');
                Assert.That(header, Is.EqualTo(ExpectedTelemetryColumns));
                Assert.That(row.Length, Is.EqualTo(header.Length));
                int utcColumn = Array.IndexOf(header, "utc_time");
                Assert.That(
                    DateTimeOffset.TryParseExact(
                        row[utcColumn],
                        "O",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                        out DateTimeOffset utcTime),
                    Is.True);
                Assert.That(utcTime.Offset, Is.EqualTo(TimeSpan.Zero));
                Assert.That(events, Does.Contain("USER_MARKER"));
                Assert.That(events, Does.Contain("\"quoted, \"\"message\"\""));
                Assert.That(events, Does.Contain("next line\""));
            }
            finally
            {
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void WriterPreservesPerSampleUtcTicksAndFiftyHertzSpacing()
        {
            string root = CreateTemporaryDirectory();
            DateTime firstCaptureUtc = new DateTime(638887716000000000L, DateTimeKind.Utc);
            DateTime secondCaptureUtc = firstCaptureUtc.AddMilliseconds(20.0);
            try
            {
                using (var writer = new FlightLogWriter(root, CreateMetadata(), 1024 * 1024))
                {
                    writer.WriteTelemetry(
                        CreateFastState(),
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        42,
                        firstCaptureUtc.Ticks);
                    writer.WriteTelemetry(
                        CreateFastState(),
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        43,
                        secondCaptureUtc.Ticks);
                }

                string[] telemetry = File.ReadAllLines(Path.Combine(root, "telemetry_000.csv"));
                string[] header = telemetry[0].Split(',');
                int utcColumn = Array.IndexOf(header, "utc_time");
                DateTime firstWrittenUtc = DateTime.ParseExact(
                    telemetry[1].Split(',')[utcColumn],
                    "O",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);
                DateTime secondWrittenUtc = DateTime.ParseExact(
                    telemetry[2].Split(',')[utcColumn],
                    "O",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);

                Assert.That(firstWrittenUtc, Is.EqualTo(firstCaptureUtc));
                Assert.That(secondWrittenUtc, Is.EqualTo(secondCaptureUtc));
                Assert.That(secondWrittenUtc - firstWrittenUtc, Is.EqualTo(TimeSpan.FromMilliseconds(20.0)));
            }
            finally
            {
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void WriterUsesInvariantCultureForNumericCsvFields()
        {
            string root = CreateTemporaryDirectory();
            CultureInfo originalCulture = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
                using (var writer = new FlightLogWriter(root, CreateMetadata(), 1024 * 1024))
                {
                    writer.WriteTelemetry(
                        CreateFastState(),
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        42);
                }

                string row = File.ReadAllLines(Path.Combine(root, "telemetry_000.csv"))[1];
                Assert.That(row, Does.Contain("123.5"));
                Assert.That(row, Does.Contain("0.25"));
                Assert.That(row, Does.Not.Contain("123,5"));
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void WriterRollsTelemetryBeforeConfiguredByteLimit()
        {
            string root = CreateTemporaryDirectory();
            try
            {
                using (var writer = new FlightLogWriter(root, CreateMetadata(), 4096))
                {
                    for (int i = 0; i < 20; i++)
                    {
                        AircraftFastState fast = CreateFastState();
                        fast.Tick = (ulong)i;
                        writer.WriteTelemetry(
                            fast,
                            CreateSystemsState(),
                            PilotControlInput.Neutral,
                            0.25f,
                            -0.5f,
                            i);
                    }
                }

                string[] files = Directory.GetFiles(root, "telemetry_*.csv");
                Assert.That(files.Length, Is.GreaterThan(1));
                Assert.That(files.OrderBy(path => path).Select(File.ReadLines).All(lines =>
                    lines.First().Contains("simulation_time_s")), Is.True);
                Assert.That(files.All(path => new FileInfo(path).Length <= 4096), Is.True);
            }
            finally
            {
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void WriterRetainsRolloverAccountingAcrossFlushAndReopen()
        {
            string sizingRoot = CreateTemporaryDirectory();
            string root = CreateTemporaryDirectory();
            try
            {
                using (var sizingWriter = new FlightLogWriter(sizingRoot, CreateMetadata(), 1024 * 1024))
                {
                    sizingWriter.WriteTelemetry(
                        CreateFastState(),
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        42);
                }
                long oneRowSegmentBytes = new FileInfo(
                    Path.Combine(sizingRoot, "telemetry_000.csv")).Length;

                using (var writer = new FlightLogWriter(root, CreateMetadata(), oneRowSegmentBytes))
                {
                    writer.WriteTelemetry(
                        CreateFastState(),
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        42);
                    writer.Flush();
                    writer.WriteTelemetry(
                        CreateFastState(),
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        42);
                }

                string[] files = Directory.GetFiles(root, "telemetry_*.csv");
                Assert.That(files.Length, Is.EqualTo(2));
                Assert.That(files.All(path => new FileInfo(path).Length <= oneRowSegmentBytes), Is.True);
                Assert.That(files.All(path => File.ReadAllLines(path).Length == 2), Is.True);
            }
            finally
            {
                Directory.Delete(sizingRoot, true);
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void WriterPlacesOversizedTelemetryRowInDedicatedSegment()
        {
            const long byteLimit = 4096;
            string root = CreateTemporaryDirectory();
            try
            {
                using (var writer = new FlightLogWriter(root, CreateMetadata(), byteLimit))
                {
                    writer.WriteTelemetry(
                        CreateFastState(),
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        42);
                    AircraftFastState oversized = CreateFastState();
                    oversized.Aircraft = new AircraftId(new string('X', 8192));
                    oversized.Tick = 43;
                    writer.WriteTelemetry(
                        oversized,
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        43);
                    AircraftFastState final = CreateFastState();
                    final.Tick = 44;
                    writer.WriteTelemetry(
                        final,
                        CreateSystemsState(),
                        PilotControlInput.Neutral,
                        0.25f,
                        -0.5f,
                        44);
                }

                string[] files = Directory.GetFiles(root, "telemetry_*.csv")
                    .OrderBy(path => path)
                    .ToArray();
                Assert.That(files.Length, Is.EqualTo(3));
                Assert.That(new FileInfo(files[0]).Length, Is.LessThanOrEqualTo(byteLimit));
                Assert.That(new FileInfo(files[1]).Length, Is.GreaterThan(byteLimit));
                Assert.That(new FileInfo(files[2]).Length, Is.LessThanOrEqualTo(byteLimit));
                Assert.That(files.All(path => File.ReadAllLines(path).Length == 2), Is.True);
                Assert.That(File.ReadAllText(files[1]), Does.Contain(",43,"));
            }
            finally
            {
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void ConstructorReleasesTelemetryHandleWhenEventFileInitializationFails()
        {
            string root = CreateTemporaryDirectory();
            Directory.CreateDirectory(Path.Combine(root, "events.csv"));
            try
            {
                Exception exception = Assert.Catch<Exception>(() =>
                    new FlightLogWriter(root, CreateMetadata(), 1024 * 1024));
                Assert.That(
                    exception,
                    Is.InstanceOf<UnauthorizedAccessException>().Or.InstanceOf<IOException>());
                Assert.DoesNotThrow(() =>
                {
                    using (File.Open(
                        Path.Combine(root, "telemetry_000.csv"),
                        FileMode.Open,
                        FileAccess.ReadWrite,
                        FileShare.None))
                    {
                    }
                });
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void RecorderDefaultsUseFlightLogsUnderscoreSessionAnd256MbSegments()
        {
            GameObject gameObject = new GameObject("Recorder Defaults Test");
            gameObject.SetActive(false);
            FlightDataRecorder recorder = null;
            string session = null;
            try
            {
                FlightSimulationHost host = gameObject.AddComponent<FlightSimulationHost>();
                var hostSerialized = new SerializedObject(host);
                hostSerialized.FindProperty("enableUdpIntegration").boolValue = false;
                hostSerialized.ApplyModifiedPropertiesWithoutUndo();
                recorder = gameObject.AddComponent<FlightDataRecorder>();
                var recorderSerialized = new SerializedObject(recorder);
                recorderSerialized.FindProperty("simulationHost").objectReferenceValue = host;
                recorderSerialized.FindProperty("autoStart").boolValue = false;
                recorderSerialized.ApplyModifiedPropertiesWithoutUndo();
                gameObject.SetActive(true);
                EnsureHostInitialized(host);

                Assert.That(
                    recorderSerialized.FindProperty("telemetryFileBytes").longValue,
                    Is.EqualTo(268435456L));
                Assert.That(recorder.StartRecording(), Is.True);
                session = recorder.SessionDirectory;
                Assert.That(
                    Directory.GetParent(session).FullName,
                    Is.EqualTo(Path.GetFullPath(Path.Combine(Application.persistentDataPath, "FlightLogs")))
                        .IgnoreCase);
                Assert.That(
                    Path.GetFileName(session),
                    Does.Match(@"^\d{8}T\d{9}Z_VIPER-01(?:_\d{3})?$"));
            }
            finally
            {
                recorder?.StopRecording();
                UnityEngine.Object.DestroyImmediate(gameObject);
                if (!string.IsNullOrEmpty(session) && Directory.Exists(session))
                    Directory.Delete(session, true);
            }
        }

        [Test]
        public void RecorderReportsDroppedTelemetryAndEventCountsByCategory()
        {
            string root = CreateTemporaryDirectory();
            GameObject gameObject = new GameObject("Recorder Overflow Test");
            gameObject.SetActive(false);
            FlightDataRecorder recorder = null;
            try
            {
                FlightSimulationHost host = gameObject.AddComponent<FlightSimulationHost>();
                var hostSerialized = new SerializedObject(host);
                hostSerialized.FindProperty("enableUdpIntegration").boolValue = false;
                hostSerialized.ApplyModifiedPropertiesWithoutUndo();
                recorder = gameObject.AddComponent<FlightDataRecorder>();
                var recorderSerialized = new SerializedObject(recorder);
                recorderSerialized.FindProperty("simulationHost").objectReferenceValue = host;
                recorderSerialized.FindProperty("autoStart").boolValue = false;
                recorderSerialized.ApplyModifiedPropertiesWithoutUndo();
                gameObject.SetActive(true);
                EnsureHostInitialized(host);

                Assert.That(recorder.StartRecording(root), Is.True);
                AircraftFastState fast = AircraftFastState.CreateDefault(host.LocalAircraft);
                for (int i = 0; i < 2050; i++)
                    recorder.OnFastState(in fast);
                var simulationEvent = new SimulationEvent
                {
                    Aircraft = host.LocalAircraft,
                    Type = SimulationEventType.Warning,
                    Message = "overflow-test"
                };
                for (int i = 0; i < 260; i++)
                    recorder.OnSimulationEvent(in simulationEvent);

                string session = recorder.SessionDirectory;
                recorder.StopRecording();
                string events = File.ReadAllText(Path.Combine(session, "events.csv"));
                Assert.That(events, Does.Contain("QUEUE_OVERFLOW"));
                Assert.That(events, Does.Contain("Dropped telemetry samples: 2; dropped events: 4."));
            }
            finally
            {
                recorder?.StopRecording();
                UnityEngine.Object.DestroyImmediate(gameObject);
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void InvalidRecordingRootDoesNotDisableSimulationHost()
        {
            GameObject gameObject = new GameObject("Recorder Failure Test");
            gameObject.SetActive(false);
            try
            {
                FlightSimulationHost host = gameObject.AddComponent<FlightSimulationHost>();
                var hostSerialized = new SerializedObject(host);
                hostSerialized.FindProperty("enableUdpIntegration").boolValue = false;
                hostSerialized.ApplyModifiedPropertiesWithoutUndo();

                FlightDataRecorder recorder = gameObject.AddComponent<FlightDataRecorder>();
                var recorderSerialized = new SerializedObject(recorder);
                recorderSerialized.FindProperty("simulationHost").objectReferenceValue = host;
                recorderSerialized.FindProperty("autoStart").boolValue = false;
                recorderSerialized.ApplyModifiedPropertiesWithoutUndo();
                gameObject.SetActive(true);
                EnsureHostInitialized(host);

                LogAssert.Expect(LogType.Error, new Regex("Flight recording disabled after file I/O failure"));
                Assert.That(recorder.StartRecording("invalid\0root"), Is.False);
                Assert.That(recorder.IsRecording, Is.False);
                Assert.That(host.enabled, Is.True);
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(gameObject);
            }
        }

        [Test]
        public void RecorderWritesOneEventForEachWarningEdge()
        {
            string root = CreateTemporaryDirectory();
            GameObject gameObject = new GameObject("Recorder Warning Test");
            gameObject.SetActive(false);
            try
            {
                FlightSimulationHost host = gameObject.AddComponent<FlightSimulationHost>();
                var hostSerialized = new SerializedObject(host);
                hostSerialized.FindProperty("enableUdpIntegration").boolValue = false;
                hostSerialized.ApplyModifiedPropertiesWithoutUndo();
                FlightDataRecorder recorder = gameObject.AddComponent<FlightDataRecorder>();
                var recorderSerialized = new SerializedObject(recorder);
                recorderSerialized.FindProperty("simulationHost").objectReferenceValue = host;
                recorderSerialized.FindProperty("autoStart").boolValue = false;
                recorderSerialized.ApplyModifiedPropertiesWithoutUndo();
                gameObject.SetActive(true);
                EnsureHostInitialized(host);

                Assert.That(recorder.StartRecording(root), Is.True);
                AircraftSystemsState systems = AircraftSystemsState.CreateDefault(host.LocalAircraft);
                systems.SimulationTimeS = 1.0;
                recorder.OnSystemsState(in systems);
                systems.SimulationTimeS = 2.0;
                systems.Warnings.StallWarning = true;
                recorder.OnSystemsState(in systems);
                recorder.OnSystemsState(in systems);
                systems.SimulationTimeS = 3.0;
                systems.Warnings.StallWarning = false;
                recorder.OnSystemsState(in systems);
                string session = recorder.SessionDirectory;
                recorder.StopRecording();

                string events = File.ReadAllText(Path.Combine(session, "events.csv"));
                Assert.That(Regex.Matches(events, "WARNING_STALL_ON").Count, Is.EqualTo(1));
                Assert.That(Regex.Matches(events, "WARNING_STALL_OFF").Count, Is.EqualTo(1));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(gameObject);
                Directory.Delete(root, true);
            }
        }

        [Test]
        public void UserMarkerFlushesTelemetryAlreadyQueuedForTheFrame()
        {
            string root = CreateTemporaryDirectory();
            GameObject gameObject = new GameObject("Recorder Marker Test");
            gameObject.SetActive(false);
            FlightDataRecorder recorder = null;
            try
            {
                FlightSimulationHost host = gameObject.AddComponent<FlightSimulationHost>();
                var hostSerialized = new SerializedObject(host);
                hostSerialized.FindProperty("enableUdpIntegration").boolValue = false;
                hostSerialized.ApplyModifiedPropertiesWithoutUndo();
                recorder = gameObject.AddComponent<FlightDataRecorder>();
                var recorderSerialized = new SerializedObject(recorder);
                recorderSerialized.FindProperty("simulationHost").objectReferenceValue = host;
                recorderSerialized.FindProperty("autoStart").boolValue = false;
                recorderSerialized.ApplyModifiedPropertiesWithoutUndo();
                gameObject.SetActive(true);
                EnsureHostInitialized(host);

                Assert.That(recorder.StartRecording(root), Is.True);
                AircraftFastState fast = AircraftFastState.CreateDefault(host.LocalAircraft);
                fast.Tick = 2;
                fast.SimulationTimeS = 0.02;
                recorder.OnFastState(in fast);
                recorder.AddUserMarker("FLUSH_MARKER");

                string session = recorder.SessionDirectory;
                Assert.That(File.ReadAllLines(Path.Combine(session, "telemetry_000.csv")).Length, Is.EqualTo(2));
                Assert.That(File.ReadAllText(Path.Combine(session, "events.csv")), Does.Contain("FLUSH_MARKER"));
                recorder.StopRecording();
            }
            finally
            {
                recorder?.StopRecording();
                UnityEngine.Object.DestroyImmediate(gameObject);
                Directory.Delete(root, true);
            }
        }

        private static FlightLogMetadata CreateMetadata()
        {
            return new FlightLogMetadata
            {
                SchemaVersion = 1,
                ContractVersion = 1,
                UnityVersion = "test",
                SceneName = "test-scene",
                AircraftId = "TEST-01",
                UtcStartTime = "2026-07-21T00:00:00.0000000Z",
                FixedStepHz = 100
            };
        }

        private static AircraftFastState CreateFastState()
        {
            return new AircraftFastState
            {
                Aircraft = new AircraftId("TEST-01"),
                Tick = 42,
                SimulationTimeS = 1.25,
                EcefPositionXM = 123.5,
                EcefPositionYM = -234.25,
                EcefPositionZM = 345.75,
                BodyToEcefQuaternionW = 1.0,
                TrueAirspeedMps = 140.25,
                CalibratedAirspeedMps = 135.5,
                Mach = 0.45,
                AngleOfAttackRad = 10.0 * Math.PI / 180.0,
                SideslipRad = -2.0 * Math.PI / 180.0,
                NormalLoadFactorG = 1.2,
                MeanSeaLevelAltitudeM = 2500.0,
                AboveGroundLevelAltitudeM = 500.0,
                TerrainSampleValid = true
            };
        }

        private static AircraftSystemsState CreateSystemsState()
        {
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(new AircraftId("TEST-01"));
            systems.SimulationTimeS = 1.2;
            systems.FlightControls.PitchCommandNormalized = 0.1;
            systems.Propulsion.EngineRunning = true;
            systems.Propulsion.N1Percent = 87.5;
            systems.Fuel.TotalFuelKg = 2500.5;
            systems.Electrical.MainBusVoltageV = 28.0;
            systems.Hydraulics.SystemAPressurePa = 20_000_000.0;
            systems.LandingGear.NoseGearPositionNormalized = 1.0;
            systems.Warnings.MasterCaution = true;
            systems.Warnings.FuelWarning = true;
            return systems;
        }

        private static string CreateTemporaryDirectory()
        {
            string path = Path.Combine(Path.GetTempPath(), "FlightSim-Recorder-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(path);
            return path;
        }

        private static void EnsureHostInitialized(FlightSimulationHost host)
        {
            if (host.SimulationService == null)
            {
                typeof(FlightSimulationHost)
                    .GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(host, null);
            }
        }
    }
}
