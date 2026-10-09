using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using FlightSim.Platform.Data;
using FlightSim.Platform.Integration;
using FlightSim.Platform.Missions;
using UnityEngine;
using FlightSim.Platform.Unity.Controls;

namespace FlightSim.Platform.Unity
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(FlightSimExecutionOrder.Simulation)]
    public sealed class FlightSimulationHost : MonoBehaviour
    {
        [Header("Simulation")]
        [SerializeField] private string localAircraftId = "VIPER-01";
        [SerializeField] private StartupPreset initialPreset = StartupPreset.RunwayReady;
        [SerializeField, Range(1, 20)] private int maximumCatchUpTicks = 8;

        [Header("Pilot Input (Legacy Input Manager)")]
        [SerializeField] private bool readLocalPilotInput = true;
        [SerializeField] private KeyCode pitchDownKey = KeyCode.UpArrow;
        [SerializeField] private KeyCode pitchUpKey = KeyCode.DownArrow;
        [SerializeField] private KeyCode rollLeftKey = KeyCode.A;
        [SerializeField] private KeyCode rollRightKey = KeyCode.D;
        [SerializeField] private KeyCode yawLeftKey = KeyCode.Q;
        [SerializeField] private KeyCode yawRightKey = KeyCode.E;
        [SerializeField] private KeyCode throttleUpKey = KeyCode.W;
        [SerializeField] private KeyCode throttleDownKey = KeyCode.S;
        [SerializeField] private KeyCode wheelBrakeKey = KeyCode.Space;
        [SerializeField] private KeyCode speedBrakeKey = KeyCode.B;
        [SerializeField] private KeyCode engineStartKey = KeyCode.I;
        [SerializeField] private string mouseXAxis = "Mouse X";
        [SerializeField] private string mouseYAxis = "Mouse Y";
        [SerializeField, Range(0.05f, 1f)] private float throttleChangePerSecond = 0.35f;
        [SerializeField, Range(0f, 1f)] private float initialThrottleNormalized;

        [Header("Mission Runtime")]
        [SerializeField] private bool enableMissionRuntime = true;
        [SerializeField] private string initialMissionId = "KTEX_SCRAMBLE_01";
        [SerializeField] private MissionDefinitionAsset[] missionAssets = Array.Empty<MissionDefinitionAsset>();
        [SerializeField] private KeyCode toggleAutomationKey = KeyCode.F8;
        [SerializeField] private KeyCode cycleTargetKey = KeyCode.Tab;
        [SerializeField] private KeyCode cycleWeaponKey = KeyCode.X;
        [SerializeField] private KeyCode cycleMasterArmKey = KeyCode.M;
        [SerializeField] private KeyCode weaponReleaseKey = KeyCode.LeftControl;

        [Header("High-Level Commands")]
        [SerializeField] private KeyCode resetScenarioKey = KeyCode.R;
        [SerializeField] private KeyCode togglePrimarySystemsKey = KeyCode.P;
        [SerializeField] private KeyCode toggleLandingGearKey = KeyCode.G;
        [SerializeField] private KeyCode cycleCameraKey = KeyCode.C;
        [SerializeField] private FlightCameraRig cameraRig;

        [Header("Read-Only Telemetry")]
        [SerializeField] private bool enableUdpIntegration = true;
        [SerializeField] private bool enableTcpNdjson = true;
        [SerializeField, Range(0, 65535)] private int udpBindPort = UdpIntegrationEndpoint.DefaultBindPort;
        [SerializeField, Range(0, 65535)] private int udpTargetPort = UdpIntegrationEndpoint.DefaultTargetPort;
        [SerializeField, Range(0, 65535)] private int tcpNdjsonPort = NdjsonTelemetryServer.DefaultPort;

        private FlightSimulationService simulationService;
        private FlightDataHub dataHub;
        private ReadOnlyTelemetryPublisher telemetryPublisher;
        private MissionDirector missionDirector;
        private MissionDefinition loadedMission;
        private AircraftId aircraftId;
        private AircraftSnapshot latestSnapshot;
        private bool hasLatestSnapshot;
        private bool requestedLandingGearDown = true;
        private bool primarySystemsRequested = true;
        private double accumulatorS;
        private double droppedSimulationTimeS;
        private float throttleNormalized;
        private readonly PilotInputConditioner pilotInputConditioner =
            new PilotInputConditioner(PilotInputConditionerSettings.Default);
        private PilotInputAxes latestPilotInput;
        private PilotControlInput latestPilotControlInput;
        private float latestRawMouseX;
        private float latestRawMouseY;
        private bool playerAutomationEnabled;
        private bool sessionPaused;
        private PilotInputRouter inputRouter;
        public bool IsSessionPaused => sessionPaused;
        public bool IsDisplayInputCaptured => inputRouter != null && inputRouter.DisplayInputCaptured;
        public CommandResult SelectDisplayStation(int index)
        {
            var result = !sessionPaused && IsMissionRunning ? missionDirector.SelectPlayerStation(index) : CommandResult.Rejected(3060, "当前未运行任务，挂点仅供查看。");
            SubmitResult(result); return result;
        }
        public CommandResult CycleDisplayMasterArm()
        {
            var result = !sessionPaused && IsMissionRunning ? missionDirector.CyclePlayerMasterArm() : CommandResult.Rejected(3061, "当前未运行任务，武器保险不可操作。");
            SubmitResult(result); return result;
        }
        public MissionDefinitionAsset[] ConfiguredMissions => missionAssets;
        public void SetInputRouter(PilotInputRouter router) => inputRouter = router;
        public void SetSessionPaused(bool paused)
        {
            sessionPaused = paused;
            accumulatorS = 0;
            ResetPilotInputConditioner();
        }
        public CommandResult StartFreeFlight(StartupPreset preset)
        {
            enableMissionRuntime = false;
            loadedMission = null;
            playerAutomationEnabled = false;
            aircraftId = new AircraftId("VIPER-01");
            localAircraftId = aircraftId.Value;
            initialPreset = preset;
            CreateSimulationService();
            if (!simulationService.AddAircraft(aircraftId, preset, true))
                return CommandResult.Rejected(3050, "Unable to create free-flight aircraft.");
            throttleNormalized = preset == StartupPreset.Airborne ? .68f : 0f;
            accumulatorS = 0;
            ResetPilotInputConditioner();
            RefreshLatestSnapshot(true);
            requestedLandingGearDown = IsLandingGearDown(in latestSnapshot.Systems.LandingGear);
            primarySystemsRequested = IsPrimarySystemPowerOn(in latestSnapshot.Systems);
            MissionLoaded?.Invoke(null);
            return CommandResult.Success("Free flight ready.");
        }

        public event Action ResetRequested;
        public event Action<AircraftSnapshot> SnapshotUpdated;
        public event Action<CommandResult> CommandSubmitted;
        public event Action<FlightSimulationService> SimulationServiceChanged;
        public event Action<MissionDefinition> MissionLoaded;
        public event Action<MissionState> MissionStateUpdated;

        public FlightSimulationService SimulationService => simulationService;
        public IFlightDataHub DataHub => dataHub;
        public UdpIntegrationEndpoint UdpEndpoint => telemetryPublisher?.UdpEndpoint;
        public NdjsonTelemetryServer NdjsonServer => telemetryPublisher?.TcpServer;
        public MissionDirector MissionDirector => missionDirector;
        public MissionDefinition LoadedMission => loadedMission;
        public bool IsMissionRuntimeEnabled => enableMissionRuntime;
        public bool IsMissionRunning => missionDirector != null && missionDirector.LatestState.Phase == MissionPhase.Running;
        public bool IsPlayerAutomationEnabled => playerAutomationEnabled;
        public AircraftId LocalAircraft => aircraftId;
        public bool HasLatestSnapshot => hasLatestSnapshot;
        public AircraftSnapshot LatestSnapshot => latestSnapshot;
        public double AccumulatorSeconds => accumulatorS;
        public double DroppedSimulationTimeSeconds => droppedSimulationTimeS;
        public bool RequestedLandingGearDown => requestedLandingGearDown;
        public PilotInputAxes LatestPilotInput => latestPilotInput;
        public PilotControlInput LatestPilotControlInput => latestPilotControlInput;
        public float LatestRawMouseX => latestRawMouseX;
        public float LatestRawMouseY => latestRawMouseY;

        private void Awake()
        {
            aircraftId = new AircraftId(localAircraftId);
            throttleNormalized = Mathf.Clamp01(initialThrottleNormalized);
            ResetPilotInputConditioner();

            if (enableMissionRuntime)
            {
                CommandResult loaded = LoadMission(initialMissionId);
                if (!loaded.Accepted)
                {
                    Debug.LogError($"Unable to load mission '{initialMissionId}': {loaded.Message}", this);
                    enabled = false;
                    return;
                }
            }
            else
            {
                CreateSimulationService();
                if (!simulationService.AddAircraft(aircraftId, initialPreset, true))
                {
                    Debug.LogError(
                        $"Unable to add local aircraft '{aircraftId}' to the flight simulation service.",
                        this);
                    enabled = false;
                    return;
                }
            }

            RefreshLatestSnapshot(false);
            requestedLandingGearDown = IsLandingGearDown(in latestSnapshot.Systems.LandingGear);
            primarySystemsRequested = IsPrimarySystemPowerOn(in latestSnapshot.Systems);
        }

        private void OnValidate()
        {
            maximumCatchUpTicks = Mathf.Clamp(maximumCatchUpTicks, 1, 20);
            throttleChangePerSecond = Mathf.Clamp(throttleChangePerSecond, 0.05f, 1f);
            initialThrottleNormalized = Mathf.Clamp01(initialThrottleNormalized);
            udpBindPort = Mathf.Clamp(udpBindPort, 0, 65535);
            udpTargetPort = Mathf.Clamp(udpTargetPort, 0, 65535);
            tcpNdjsonPort = Mathf.Clamp(tcpNdjsonPort, 0, 65535);
        }

        private void Update()
        {
            if (simulationService == null || sessionPaused)
            {
                return;
            }

            ReadHighLevelCommands();
            if (readLocalPilotInput && IsPlayerUnderManualControl())
            {
                PilotControlInput input = ReadPilotInput();
                simulationService.SetLocalPilotInput(aircraftId, in input);
            }

            double frameDeltaS = Math.Max(0.0, Time.deltaTime);
            double catchUpWindowS = maximumCatchUpTicks * FlightSimulationService.FixedDeltaTimeS;
            double acceptedFrameDeltaS = Math.Min(frameDeltaS, catchUpWindowS);
            droppedSimulationTimeS += frameDeltaS - acceptedFrameDeltaS;
            accumulatorS += acceptedFrameDeltaS;

            int tickCount = 0;
            while (accumulatorS >= FlightSimulationService.FixedDeltaTimeS &&
                   tickCount < maximumCatchUpTicks)
            {
                if (missionDirector != null)
                {
                    if (missionDirector.LatestState.Phase != MissionPhase.Running)
                    {
                        accumulatorS = 0.0;
                        break;
                    }
                    missionDirector.Tick();
                }
                else
                {
                    simulationService.Tick();
                }
                accumulatorS -= FlightSimulationService.FixedDeltaTimeS;
                tickCount++;
            }

            if (tickCount > 0)
            {
                RefreshLatestSnapshot(true);
                if (missionDirector != null)
                    MissionStateUpdated?.Invoke(missionDirector.LatestState);
                telemetryPublisher?.Pump(latestSnapshot.Fast.SimulationTimeS);
            }
        }

        private void OnDestroy()
        {
            telemetryPublisher?.Dispose();
            telemetryPublisher = null;
            DetachDataHub();
        }

        public bool TryGetLatest(out AircraftSnapshot snapshot)
        {
            snapshot = latestSnapshot;
            return hasLatestSnapshot;
        }

        public bool TryGetLatest(AircraftId aircraft, out AircraftSnapshot snapshot)
        {
            if (simulationService == null)
            {
                snapshot = default;
                return false;
            }

            return simulationService.TryGetLatest(aircraft, out snapshot);
        }

        public CommandResult LoadMission(string missionId)
        {
            MissionDefinition definition = ResolveMission(missionId);
            if (definition == null)
                return CommandResult.Rejected(3001, $"Unknown mission '{missionId}'.");

            return LoadMissionDefinition(definition);
        }

        public CommandResult LoadMissionDefinition(MissionDefinition source)
        {
            var validation = MissionDefinitionValidator.Validate(source);
            if (!validation.IsValid) return CommandResult.Rejected(3002, validation.FirstError);
            MissionDefinition definition = MissionJsonCodec.FromJson(MissionJsonCodec.ToJson(source));
            enableMissionRuntime = true;
            CreateSimulationService();
            MissionDirector director = new MissionDirector(simulationService);
            director.RegisterSink(dataHub);
            CommandResult result = director.Load(definition, definition.DefaultSeed);
            if (!result.Accepted)
            {
                director.UnregisterSink(dataHub);
                return result;
            }

            missionDirector = director;
            PublishMissionSnapshotToDataHub(director);
            loadedMission = definition;
            playerAutomationEnabled = false;
            for (int index = 0; index < definition.Actors.Length; index++)
            {
                if (!definition.Actors[index].IsPlayer) continue;
                aircraftId = new AircraftId(definition.Actors[index].AircraftId);
                localAircraftId = aircraftId.Value;
                throttleNormalized = definition.Actors[index].InitialCondition.Preset == StartupPreset.Airborne
                    ? 0.68f
                    : 0f;
                break;
            }
            ResetPilotInputConditioner();
            RefreshLatestSnapshot(false);
            requestedLandingGearDown = IsLandingGearDown(in latestSnapshot.Systems.LandingGear);
            primarySystemsRequested = IsPrimarySystemPowerOn(in latestSnapshot.Systems);
            MissionLoaded?.Invoke(definition);
            MissionStateUpdated?.Invoke(director.LatestState);
            return result;
        }

        public CommandResult StartMission(bool automated)
        {
            if (missionDirector == null)
                return CommandResult.Rejected(3010, "No mission is loaded.");
            CommandResult result = missionDirector.Start();
            if (!result.Accepted) return result;
            if (automated)
            {
                CommandResult automation = missionDirector.SetPlayerAutomation(true, AutomationMode.Visible);
                if (!automation.Accepted) return automation;
                playerAutomationEnabled = true;
            }
            accumulatorS = 0.0;
            RefreshLatestSnapshot(true);
            MissionStateUpdated?.Invoke(missionDirector.LatestState);
            return result;
        }

        public CommandResult SetPlayerAutomation(bool enabled)
        {
            if (missionDirector == null)
                return CommandResult.Rejected(3020, "Mission runtime is unavailable.");
            CommandResult result = missionDirector.SetPlayerAutomation(
                enabled,
                enabled ? AutomationMode.Visible : AutomationMode.Manual);
            if (result.Accepted)
            {
                playerAutomationEnabled = enabled;
                if (!enabled) ResetPilotInputConditioner();
            }
            return result;
        }

        public bool TryGetMissionSnapshot(out MissionSnapshot snapshot)
        {
            if (missionDirector != null) return missionDirector.TryGetLatest(out snapshot);
            snapshot = default(MissionSnapshot);
            return false;
        }

        public bool SubmitPilotInput(in PilotControlInput input)
        {
            bool accepted = simulationService != null &&
                            simulationService.SetLocalPilotInput(aircraftId, in input);
            if (accepted)
                latestPilotControlInput = input;
            return accepted;
        }

        public CommandResult ResetScenario(string scenarioId = "")
        {
            if (missionDirector != null && loadedMission != null)
            {
                bool restartAutomated = playerAutomationEnabled;
                string missionId = string.IsNullOrEmpty(scenarioId) ? loadedMission.MissionId : scenarioId;
                CommandResult loaded = string.IsNullOrEmpty(scenarioId) ? LoadMissionDefinition(loadedMission) : LoadMission(missionId);
                return loaded.Accepted ? StartMission(restartAutomated) : loaded;
            }
            return StartFreeFlight(initialPreset);
        }

        public CommandResult SetSystemSwitch(AircraftSystemSwitch systemSwitch, bool isEnabled)
        {
            SystemSwitchCommand command = new SystemSwitchCommand
            {
                Aircraft = aircraftId,
                Switch = systemSwitch,
                IsEnabled = isEnabled
            };
            return Submit(in command);
        }

        public bool SetPrimarySystemsEnabled(bool isEnabled)
        {
            primarySystemsRequested = isEnabled;
            bool allAccepted = true;
            allAccepted &= SetSystemSwitch(AircraftSystemSwitch.Battery, isEnabled).Accepted;
            allAccepted &= SetSystemSwitch(AircraftSystemSwitch.Generator, isEnabled).Accepted;
            allAccepted &= SetSystemSwitch(AircraftSystemSwitch.FuelPump, isEnabled).Accepted;
            allAccepted &= SetSystemSwitch(AircraftSystemSwitch.FlightControlComputer, isEnabled).Accepted;
            allAccepted &= SetSystemSwitch(AircraftSystemSwitch.Radar, isEnabled).Accepted;
            allAccepted &= SetSystemSwitch(AircraftSystemSwitch.DataLink, isEnabled).Accepted;
            return allAccepted;
        }

        public CommandResult RequestLandingGear(bool isDown)
        {
            LandingGearCommand command = new LandingGearCommand
            {
                Aircraft = aircraftId,
                IsDown = isDown
            };
            CommandResult result = Submit(in command);

            if (result.Accepted)
            {
                requestedLandingGearDown = isDown;
            }

            return result;
        }

        public bool SetTerrainSample(
            bool isValid,
            double aboveGroundLevelM,
            double sampleAgeS)
        {
            bool accepted = simulationService != null &&
                            simulationService.SetTerrainSample(
                       aircraftId,
                       isValid,
                       aboveGroundLevelM,
                       sampleAgeS);
            if (accepted && missionDirector != null)
                missionDirector.UpdateTerrainStatus(TerrainSource.Cesium, isValid);
            return accepted;
        }

        public bool SetTerrainSample(
            AircraftId aircraft,
            bool isValid,
            double aboveGroundLevelM,
            double sampleAgeS,
            TerrainSource source = TerrainSource.Cesium)
        {
            bool accepted = simulationService != null &&
                            simulationService.SetTerrainSample(aircraft, isValid, aboveGroundLevelM, sampleAgeS);
            if (accepted && missionDirector != null && aircraft == aircraftId)
                missionDirector.UpdateTerrainStatus(source, isValid);
            return accepted;
        }

        public void SetCameraMode(FlightCameraMode mode)
        {
            if (cameraRig != null)
            {
                cameraRig.SetMode(mode);
            }
        }

        public void CycleCameraMode()
        {
            if (cameraRig != null)
            {
                cameraRig.CycleMode();
            }
        }

        private CommandResult Submit<T>(in T command) where T : struct, ISimulationCommand
        {
            CommandResult result = simulationService != null
                ? simulationService.Submit(in command)
                : CommandResult.Rejected(100, "Simulation service is unavailable.");
            CommandSubmitted?.Invoke(result);
            return result;
        }

        private void RefreshLatestSnapshot(bool notify)
        {
            hasLatestSnapshot = simulationService.TryGetLatest(aircraftId, out latestSnapshot);
            if (notify && hasLatestSnapshot)
            {
                SnapshotUpdated?.Invoke(latestSnapshot);
            }
        }

        private void ReadHighLevelCommands()
        {
            if (Input.GetKeyDown(resetScenarioKey))
            {
                if (ResetRequested != null) ResetRequested.Invoke(); else ResetScenario();
            }

            if (Input.GetKeyDown(togglePrimarySystemsKey))
            {
                SetPrimarySystemsEnabled(!primarySystemsRequested);
            }

            if ((Input.GetKeyDown(toggleLandingGearKey) || (inputRouter != null && inputRouter.Pressed(PilotAction.LandingGear))))
            {
                RequestLandingGear(!requestedLandingGearDown);
            }

            if ((Input.GetKeyDown(cycleCameraKey) || (inputRouter != null && inputRouter.Pressed(PilotAction.Camera))))
            {
                CycleCameraMode();
            }

            if (missionDirector == null || !IsMissionRunning)
                return;
            if ((Input.GetKeyDown(toggleAutomationKey) || (inputRouter != null && inputRouter.Pressed(PilotAction.Automation))))
                SubmitResult(SetPlayerAutomation(!playerAutomationEnabled));
            if ((Input.GetKeyDown(cycleTargetKey) || (inputRouter != null && inputRouter.Pressed(PilotAction.Target))))
                SubmitResult(missionDirector.CyclePlayerTarget());
            if ((Input.GetKeyDown(cycleWeaponKey) || (inputRouter != null && inputRouter.Pressed(PilotAction.Weapon))))
                SubmitResult(missionDirector.CyclePlayerWeapon());
            if ((Input.GetKeyDown(cycleMasterArmKey) || (inputRouter != null && inputRouter.Pressed(PilotAction.MasterArm))))
                SubmitResult(missionDirector.CyclePlayerMasterArm());
            if ((Input.GetKeyDown(weaponReleaseKey) || (inputRouter != null && inputRouter.Pressed(PilotAction.WeaponRelease))))
                SubmitResult(missionDirector.ReleasePlayerWeapon());
        }

        private PilotControlInput ReadPilotInput()
        {
            float keyboardPitch = inputRouter != null && inputRouter.DisplayFocused ? 0 : ReadKeyAxis(pitchDownKey, pitchUpKey);
            float keyboardRoll = ReadKeyAxis(rollLeftKey, rollRightKey);
            float keyboardYaw = ReadKeyAxis(yawLeftKey, yawRightKey);
            bool cameraConsumesMouse = IsDisplayInputCaptured || (cameraRig != null && cameraRig.Mode == FlightCameraMode.Free);
            latestRawMouseX = Input.GetAxisRaw(mouseXAxis);
            latestRawMouseY = Input.GetAxisRaw(mouseYAxis);
            latestPilotInput = pilotInputConditioner.Update(
                keyboardPitch,
                keyboardRoll,
                keyboardYaw,
                latestRawMouseX,
                latestRawMouseY,
                !cameraConsumesMouse && (inputRouter == null || inputRouter.UseMouseFlight),
                Time.deltaTime);

            float throttleDirection = ReadKeyAxis(throttleDownKey, throttleUpKey);
            float scroll = cameraConsumesMouse ? 0f : Input.mouseScrollDelta.y;
            throttleNormalized = Mathf.Clamp01(
                throttleNormalized +
                throttleDirection * throttleChangePerSecond * Time.deltaTime +
                scroll * 0.04f);

            float pitch = latestPilotInput.Pitch, roll = latestPilotInput.Roll, yaw = latestPilotInput.Yaw;
            float hardwareBrake = 0;
            if (inputRouter != null)
            {
                bool blendKeyboard = inputRouter.Profile.Mode != PilotInputMode.Hotas;
                if (inputRouter.TryReadAxis(FlightAxis.Pitch, out float hp)) pitch = Mathf.Clamp(hp + (blendKeyboard ? keyboardPitch : 0), -1, 1);
                if (inputRouter.TryReadAxis(FlightAxis.Roll, out float hr)) roll = Mathf.Clamp(hr + (blendKeyboard ? keyboardRoll : 0), -1, 1);
                if (inputRouter.TryReadAxis(FlightAxis.Yaw, out float hy)) yaw = Mathf.Clamp(hy + (blendKeyboard ? keyboardYaw : 0), -1, 1);
                yaw = Mathf.Clamp(yaw + (inputRouter.Held(PilotAction.YawRight) ? 1 : 0) - (inputRouter.Held(PilotAction.YawLeft) ? 1 : 0), -1, 1);
                if (inputRouter.TryReadThrottle(out float ht)) throttleNormalized = ht;
                inputRouter.TryReadAxis(FlightAxis.WheelBrake, out hardwareBrake);
            }
            latestPilotInput = new PilotInputAxes(pitch, roll, yaw);
            latestPilotControlInput = new PilotControlInput
            {
                PitchNormalized = latestPilotInput.Pitch,
                RollNormalized = latestPilotInput.Roll,
                YawNormalized = latestPilotInput.Yaw,
                ThrottleNormalized = throttleNormalized,
                WheelBrakeNormalized = (Input.GetKey(wheelBrakeKey) || (inputRouter != null && inputRouter.Held(PilotAction.WheelBrake))) ? 1.0 : hardwareBrake,
                SpeedBrakeNormalized = (Input.GetKey(speedBrakeKey) || (inputRouter != null && inputRouter.Held(PilotAction.SpeedBrake))) ? 1.0 : 0.0,
                EngineStartCommand = Input.GetKey(engineStartKey) || (inputRouter != null && inputRouter.Held(PilotAction.EngineStart))
            };
            return latestPilotControlInput;
        }

        private void ResetPilotInputConditioner()
        {
            pilotInputConditioner.Reset();
            latestPilotInput = default;
            latestPilotControlInput = PilotControlInput.Neutral;
            latestRawMouseX = 0f;
            latestRawMouseY = 0f;
        }

        private void CreateSimulationService()
        {
            telemetryPublisher?.Dispose();
            telemetryPublisher = null;
            DetachDataHub();
            simulationService = new FlightSimulationService();
            if (dataHub == null)
                dataHub = new FlightDataHub();
            else
                dataHub.Reset();
            simulationService.RegisterSink(dataHub);
            missionDirector = null;
            if (enableUdpIntegration || enableTcpNdjson)
            {
                telemetryPublisher = new ReadOnlyTelemetryPublisher(
                    dataHub,
                    udpBindPort,
                    enableUdpIntegration ? udpTargetPort : -1,
                    enableTcpNdjson ? tcpNdjsonPort : -1);
                telemetryPublisher.Start();
            }
            SimulationServiceChanged?.Invoke(simulationService);
        }

        private void DetachDataHub()
        {
            if (dataHub == null) return;
            if (missionDirector != null) missionDirector.UnregisterSink(dataHub);
            if (simulationService != null) simulationService.UnregisterSink(dataHub);
        }

        private void PublishMissionSnapshotToDataHub(MissionDirector director)
        {
            if (director == null || dataHub == null || !director.TryGetLatest(out MissionSnapshot snapshot))
                return;
            dataHub.OnMissionState(in snapshot.Mission);
            MissionActorState[] actors = snapshot.Actors ?? Array.Empty<MissionActorState>();
            for (int index = 0; index < actors.Length; index++)
                dataHub.OnMissionActorState(in actors[index]);
            MissionObjectiveState[] objectives = snapshot.Objectives ?? Array.Empty<MissionObjectiveState>();
            for (int index = 0; index < objectives.Length; index++)
                dataHub.OnMissionObjectiveState(in objectives[index]);
            dataHub.OnAutomationRunState(in snapshot.Automation);
        }

        private MissionDefinition ResolveMission(string missionId)
        {
            MissionDefinitionAsset[] assets = missionAssets ?? Array.Empty<MissionDefinitionAsset>();
            for (int index = 0; index < assets.Length; index++)
            {
                MissionDefinitionAsset asset = assets[index];
                if (asset != null && string.Equals(asset.MissionId, missionId, StringComparison.Ordinal))
                    return asset.Definition;
            }
            return DefaultMissionCatalog.Create(missionId);
        }

        private bool IsPlayerUnderManualControl()
        {
            return missionDirector == null || !playerAutomationEnabled;
        }

        private void SubmitResult(CommandResult result)
        {
            CommandSubmitted?.Invoke(result);
        }

        private static float ReadKeyAxis(KeyCode negative, KeyCode positive)
        {
            float value = 0f;
            if (Input.GetKey(negative))
            {
                value -= 1f;
            }

            if (Input.GetKey(positive))
            {
                value += 1f;
            }

            return value;
        }

        private static bool IsLandingGearDown(in LandingGearState gear)
        {
            return gear.NoseGearPositionNormalized >= 0.95 &&
                   gear.LeftMainGearPositionNormalized >= 0.95 &&
                   gear.RightMainGearPositionNormalized >= 0.95;
        }

        private static bool IsPrimarySystemPowerOn(in AircraftSystemsState systems)
        {
            return systems.Electrical.BatteryOnline ||
                   systems.Electrical.GeneratorOnline ||
                   systems.Electrical.MainBusPowered;
        }
    }
}
