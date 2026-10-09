using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;

namespace FlightSim.Platform.Integration
{
    public sealed class FlightSimulationService : IFlightSimulationService
    {
        public const int MaximumAircraft = 8;
        public const double FixedDeltaTimeS = 0.01;

        private const int MaximumSinks = 16;
        private const int CommandQueueCapacity = 64;
        private const string CommandQueuedMessage = "Queued for next tick.";
        private const string AircraftNotFoundMessage = "Aircraft not found.";
        private const string InvalidCommandMessage = "Command values are invalid.";
        private const string QueueFullMessage = "Command queue is full.";
        private const string UnsupportedCommandMessage = "Unsupported command type.";
        private const string InformationMessage = "Command applied.";
        private const string ScenarioResetMessage = "Scenario reset.";
        private const string FailureInjectedMessage = "Failure injected.";
        private const string StoreReleasedMessage = "Store released.";
        private const string StoreReleaseRejectedMessage = "Store release rejected.";
        private const string FormationChangedMessage = "Formation mode changed.";
        private const string EngineStartedMessage = "Engine started.";
        private const string EngineStoppedMessage = "Engine stopped.";
        private const string TakeoffMessage = "Takeoff.";
        private const string TouchdownMessage = "Touchdown.";
        private const string CrashMessage = "Crash detected.";
        private const string WarningMessage = "Aircraft warning active.";
        private const string ReturnWaypointName = "KTEX RWY";

        private readonly AircraftSlot[] aircraftSlots;
        private readonly F16AircraftState[] preTickStates;
        private readonly PilotControlInput[] tickInputs;
        private readonly IFlightTelemetrySink[] sinks;
        private readonly IFlightTelemetrySink[] dispatchSinks;
        private readonly QueuedCommand[] commandQueue;
        private readonly FormationController formationController;

        private int aircraftCount;
        private int sinkCount;
        private int dispatchSinkCount;
        private int commandQueueHead;
        private int commandQueueCount;
        private ulong serviceTick;

        public FlightSimulationService()
        {
            aircraftSlots = new AircraftSlot[MaximumAircraft];
            preTickStates = new F16AircraftState[MaximumAircraft];
            tickInputs = new PilotControlInput[MaximumAircraft];
            sinks = new IFlightTelemetrySink[MaximumSinks];
            dispatchSinks = new IFlightTelemetrySink[MaximumSinks];
            commandQueue = new QueuedCommand[CommandQueueCapacity];
            formationController = new FormationController(MaximumAircraft);
        }

        public ushort ContractVersion => FlightSimulationContract.ContractVersion;

        public bool AddAircraft(AircraftId aircraftId, StartupPreset preset, bool isLocalPilot)
        {
            AircraftIdentityState identity = AircraftIdentityState.CreateDefault(aircraftId, isLocalPilot);
            return AddAircraftCore(
                aircraftId,
                preset,
                isLocalPilot,
                in identity,
                F16AircraftSimulation.CreateKtex(preset));
        }

        public bool AddAircraft(in AircraftRegistration registration)
        {
            AircraftInitialCondition initial = registration.InitialCondition;
            AircraftIdentityState identity = registration.Identity;
            if (string.IsNullOrEmpty(identity.Aircraft.Value))
            {
                identity = AircraftIdentityState.CreateDefault(
                    registration.Aircraft,
                    registration.IsLocalPilot);
            }

            return AddAircraftCore(
                registration.Aircraft,
                initial.Preset,
                registration.IsLocalPilot,
                in identity,
                F16AircraftSimulation.CreateKtex(in initial));
        }

        private bool AddAircraftCore(
            AircraftId aircraftId,
            StartupPreset preset,
            bool isLocalPilot,
            in AircraftIdentityState identity,
            F16AircraftSimulation simulation)
        {
            if (aircraftCount >= MaximumAircraft ||
                string.IsNullOrEmpty(aircraftId.Value) ||
                !IsDefinedStartupPreset(preset) ||
                FindAircraftIndex(aircraftId) >= 0 ||
                simulation == null)
            {
                return false;
            }

            int aircraftIndex = aircraftCount;
            AircraftSlot slot = new AircraftSlot
            {
                Aircraft = aircraftId,
                Identity = identity,
                IsLocalPilot = isLocalPilot,
                ActivePreset = preset,
                Simulation = simulation,
                Snapshot = AircraftSnapshot.CreateDefault(aircraftId)
            };
            InitializePresetState(slot, preset);
            slot.LocalInput = CreateDefaultInput(preset, isLocalPilot);
            slot.AppliedInput = slot.LocalInput;
            slot.RequestedControlAuthority = isLocalPilot
                ? ControlAuthority.ManualPilot
                : identity.Role == AircraftRole.Wingman
                    ? ControlAuthority.FormationAI
                    : ControlAuthority.MissionAI;
            slot.AppliedControlAuthority = slot.RequestedControlAuthority;
            aircraftSlots[aircraftIndex] = slot;
            formationController.InitializeAircraft(aircraftIndex);
            aircraftCount++;

            int primaryLocalIndex = FindPrimaryLocalAircraftIndex(aircraftIndex);
            if (!isLocalPilot &&
                identity.Side == AircraftSide.Friendly &&
                identity.Role == AircraftRole.Wingman &&
                primaryLocalIndex >= 0)
            {
                int formationSlot = CountWingmenForLeader(primaryLocalIndex);
                formationController.ConfigureDefaultWingman(
                    aircraftIndex,
                    primaryLocalIndex,
                    aircraftSlots[primaryLocalIndex].Aircraft,
                    formationSlot);
                if (preset == StartupPreset.Airborne)
                {
                    FormationState formation = formationController.GetState(aircraftIndex);
                    F16AircraftState leaderState = aircraftSlots[primaryLocalIndex].Simulation.State;
                    DVector3 offsetBodyM = new DVector3(
                        formation.DesiredForwardOffsetM,
                        formation.DesiredRightOffsetM,
                        -formation.DesiredUpOffsetM);
                    DVector3 offsetEcefM = leaderState.BodyToEcefOrientation.Rotate(offsetBodyM);
                    slot.Simulation.OffsetEcefPosition(in offsetEcefM);
                }
            }
            else if (isLocalPilot)
            {
                AssignUnledWingmen(aircraftIndex);
            }

            InitializeTransitionState(slot);
            RefreshAllSnapshots();
            return true;
        }

        public bool SetLocalPilotInput(AircraftId aircraftId, in PilotControlInput input)
        {
            int aircraftIndex = FindAircraftIndex(aircraftId);
            if (aircraftIndex < 0 || !aircraftSlots[aircraftIndex].IsLocalPilot ||
                aircraftSlots[aircraftIndex].RequestedControlAuthority != ControlAuthority.ManualPilot)
            {
                return false;
            }

            aircraftSlots[aircraftIndex].LocalInput = SanitizeInput(in input);
            return true;
        }

        public bool SetMissionControlInput(AircraftId aircraftId, in PilotControlInput input)
        {
            return SetMissionControlInput(aircraftId, in input, ControlAuthority.MissionAI);
        }

        public bool SetMissionControlInput(
            AircraftId aircraftId,
            in PilotControlInput input,
            ControlAuthority authority)
        {
            int aircraftIndex = FindAircraftIndex(aircraftId);
            if (aircraftIndex < 0 ||
                (authority != ControlAuthority.AutomationPilot && authority != ControlAuthority.MissionAI) ||
                aircraftSlots[aircraftIndex].RequestedControlAuthority != authority)
            {
                return false;
            }

            aircraftSlots[aircraftIndex].LocalInput = SanitizeInput(in input);
            return true;
        }

        public bool SetControlAuthority(AircraftId aircraftId, ControlAuthority authority)
        {
            int aircraftIndex = FindAircraftIndex(aircraftId);
            if (aircraftIndex < 0 ||
                (authority == ControlAuthority.ManualPilot && !aircraftSlots[aircraftIndex].IsLocalPilot))
                return false;
            aircraftSlots[aircraftIndex].RequestedControlAuthority = authority;
            return true;
        }

        public bool SetTerrainSample(
            AircraftId aircraftId,
            bool isValid,
            double aboveGroundLevelM,
            double sampleAgeS)
        {
            int aircraftIndex = FindAircraftIndex(aircraftId);
            if (aircraftIndex < 0 ||
                (isValid && (!IsFinite(aboveGroundLevelM) || aboveGroundLevelM < 0.0)) ||
                !IsFinite(sampleAgeS) ||
                sampleAgeS < 0.0)
            {
                return false;
            }

            AircraftSlot slot = aircraftSlots[aircraftIndex];
            slot.TerrainSampleValid = isValid;
            slot.TerrainAboveGroundLevelM = isValid ? aboveGroundLevelM : 0.0;
            slot.TerrainSampleAgeS = sampleAgeS;
            MapFastState(slot);
            return true;
        }

        public void Tick()
        {
            CaptureDispatchSinks();
            ApplyCommandsQueuedBeforeTick();
            serviceTick++;

            for (int aircraftIndex = 0; aircraftIndex < aircraftCount; aircraftIndex++)
            {
                AircraftSlot slot = aircraftSlots[aircraftIndex];
                preTickStates[aircraftIndex] = slot.Simulation.State;
                tickInputs[aircraftIndex] = slot.LocalInput;
                slot.AppliedControlAuthority = slot.RequestedControlAuthority;
            }

            for (int aircraftIndex = 0; aircraftIndex < aircraftCount; aircraftIndex++)
            {
                AircraftSlot slot = aircraftSlots[aircraftIndex];
                if (formationController.ShouldControl(aircraftIndex))
                {
                    slot.AppliedControlAuthority = ControlAuthority.FormationAI;
                    F16AircraftState leaderState = default(F16AircraftState);
                    PilotControlInput leaderInput = PilotControlInput.Neutral;
                    bool hasLeader = formationController.TryGetLeaderIndex(
                        aircraftIndex,
                        out int leaderIndex);
                    if (hasLeader && leaderIndex < aircraftCount)
                    {
                        leaderState = preTickStates[leaderIndex];
                        leaderInput = tickInputs[leaderIndex];
                    }

                    if (hasLeader ||
                        formationController.GetMode(aircraftIndex) == FormationMode.ReturnToBase)
                    {
                        DVector3 separationVelocityEcefMps = DVector3.Zero;
                        for (int otherIndex = 0; otherIndex < aircraftCount; otherIndex++)
                        {
                            if (otherIndex == aircraftIndex)
                            {
                                continue;
                            }

                            separationVelocityEcefMps += formationController.ComputeSeparationVelocity(
                                aircraftIndex,
                                otherIndex,
                                in preTickStates[aircraftIndex],
                                in preTickStates[otherIndex]);
                        }

                        AnalyticRunway runway = slot.Simulation.Runway;
                        tickInputs[aircraftIndex] = formationController.ComputeInput(
                            aircraftIndex,
                            in preTickStates[aircraftIndex],
                            in leaderState,
                            in leaderInput,
                            in runway,
                            in separationVelocityEcefMps);
                    }
                }

                slot.AppliedInput = tickInputs[aircraftIndex];
                if (formationController.GetMode(aircraftIndex) == FormationMode.ReturnToBase &&
                    preTickStates[aircraftIndex].AboveGroundLevelAltitudeM < 500.0)
                {
                    slot.Simulation.SetLandingGearDown(true);
                }
            }

            for (int aircraftIndex = 0; aircraftIndex < aircraftCount; aircraftIndex++)
            {
                AircraftSlot slot = aircraftSlots[aircraftIndex];
                PilotControlInput input = tickInputs[aircraftIndex];
                slot.Simulation.Step(in input, FixedDeltaTimeS);
            }

            RefreshAllSnapshots();
            EmitDiscreteStateEvents();
            PublishScheduledTelemetry();
        }

        public bool TryGetLatest(AircraftId aircraftId, out AircraftSnapshot snapshot)
        {
            int aircraftIndex = FindAircraftIndex(aircraftId);
            if (aircraftIndex < 0)
            {
                snapshot = default(AircraftSnapshot);
                return false;
            }

            snapshot = aircraftSlots[aircraftIndex].Snapshot;
            return true;
        }

        public CommandResult Submit<T>(in T command) where T : struct, ISimulationCommand
        {
            if (typeof(T) == typeof(ResetScenarioCommand))
            {
                ResetScenarioCommand typed = (ResetScenarioCommand)(object)command;
                return SubmitResetScenario(in typed);
            }

            if (typeof(T) == typeof(StartupPresetCommand))
            {
                StartupPresetCommand typed = (StartupPresetCommand)(object)command;
                return SubmitStartupPreset(in typed);
            }

            if (typeof(T) == typeof(SystemSwitchCommand))
            {
                SystemSwitchCommand typed = (SystemSwitchCommand)(object)command;
                return SubmitSystemSwitch(in typed);
            }

            if (typeof(T) == typeof(LandingGearCommand))
            {
                LandingGearCommand typed = (LandingGearCommand)(object)command;
                return SubmitLandingGear(in typed);
            }

            if (typeof(T) == typeof(LoadoutConfigurationCommand))
            {
                LoadoutConfigurationCommand typed = (LoadoutConfigurationCommand)(object)command;
                return SubmitLoadout(in typed);
            }

            if (typeof(T) == typeof(ReleaseStoreCommand))
            {
                ReleaseStoreCommand typed = (ReleaseStoreCommand)(object)command;
                return SubmitReleaseStore(in typed);
            }

            if (typeof(T) == typeof(InjectFailureCommand))
            {
                InjectFailureCommand typed = (InjectFailureCommand)(object)command;
                return SubmitInjectFailure(in typed);
            }

            if (typeof(T) == typeof(FormationCommand))
            {
                FormationCommand typed = (FormationCommand)(object)command;
                return SubmitFormation(in typed);
            }

            return CommandResult.Rejected(1, UnsupportedCommandMessage);
        }

        public void RegisterSink(IFlightTelemetrySink sink)
        {
            if (sink == null)
            {
                return;
            }

            for (int index = 0; index < sinkCount; index++)
            {
                if (ReferenceEquals(sinks[index], sink))
                {
                    return;
                }
            }

            if (sinkCount < MaximumSinks)
            {
                sinks[sinkCount++] = sink;
            }
        }

        public void UnregisterSink(IFlightTelemetrySink sink)
        {
            if (sink == null)
            {
                return;
            }

            for (int index = 0; index < sinkCount; index++)
            {
                if (!ReferenceEquals(sinks[index], sink))
                {
                    continue;
                }

                for (int moveIndex = index + 1; moveIndex < sinkCount; moveIndex++)
                {
                    sinks[moveIndex - 1] = sinks[moveIndex];
                }

                sinks[--sinkCount] = null;
                return;
            }
        }

        private CommandResult SubmitResetScenario(in ResetScenarioCommand command)
        {
            int aircraftIndex = FindAircraftIndex(command.Aircraft);
            if (aircraftIndex < 0)
            {
                return CommandResult.Rejected(2, AircraftNotFoundMessage);
            }

            QueuedCommand queued = default(QueuedCommand);
            queued.Type = QueuedCommandType.ResetScenario;
            queued.AircraftIndex = aircraftIndex;
            queued.ResetScenario = command;
            return Enqueue(in queued);
        }

        private CommandResult SubmitStartupPreset(in StartupPresetCommand command)
        {
            int aircraftIndex = FindAircraftIndex(command.Aircraft);
            if (aircraftIndex < 0)
            {
                return CommandResult.Rejected(2, AircraftNotFoundMessage);
            }

            if (!IsDefinedStartupPreset(command.Preset))
            {
                return CommandResult.Rejected(3, InvalidCommandMessage);
            }

            QueuedCommand queued = default(QueuedCommand);
            queued.Type = QueuedCommandType.StartupPreset;
            queued.AircraftIndex = aircraftIndex;
            queued.StartupPreset = command;
            return Enqueue(in queued);
        }

        private CommandResult SubmitSystemSwitch(in SystemSwitchCommand command)
        {
            int aircraftIndex = FindAircraftIndex(command.Aircraft);
            if (aircraftIndex < 0)
            {
                return CommandResult.Rejected(2, AircraftNotFoundMessage);
            }

            if (command.Switch < AircraftSystemSwitch.Battery ||
                command.Switch > AircraftSystemSwitch.DataLink)
            {
                return CommandResult.Rejected(3, InvalidCommandMessage);
            }

            QueuedCommand queued = default(QueuedCommand);
            queued.Type = QueuedCommandType.SystemSwitch;
            queued.AircraftIndex = aircraftIndex;
            queued.SystemSwitch = command;
            return Enqueue(in queued);
        }

        private CommandResult SubmitLoadout(in LoadoutConfigurationCommand command)
        {
            int aircraftIndex = FindAircraftIndex(command.Aircraft);
            if (aircraftIndex < 0)
            {
                return CommandResult.Rejected(2, AircraftNotFoundMessage);
            }

            QueuedCommand queued = default(QueuedCommand);
            queued.Type = QueuedCommandType.Loadout;
            queued.AircraftIndex = aircraftIndex;
            queued.Loadout = command;
            return Enqueue(in queued);
        }

        private CommandResult SubmitLandingGear(in LandingGearCommand command)
        {
            int aircraftIndex = FindAircraftIndex(command.Aircraft);
            if (aircraftIndex < 0)
            {
                return CommandResult.Rejected(2, AircraftNotFoundMessage);
            }

            QueuedCommand queued = default(QueuedCommand);
            queued.Type = QueuedCommandType.LandingGear;
            queued.AircraftIndex = aircraftIndex;
            queued.LandingGear = command;
            return Enqueue(in queued);
        }

        private CommandResult SubmitReleaseStore(in ReleaseStoreCommand command)
        {
            int aircraftIndex = FindAircraftIndex(command.Aircraft);
            if (aircraftIndex < 0)
            {
                return CommandResult.Rejected(2, AircraftNotFoundMessage);
            }

            if (command.StationIndex < 0 ||
                command.StationIndex >= StoresState.StationCapacity ||
                (!command.EmergencyJettison && command.Quantity <= 0))
            {
                return CommandResult.Rejected(3, InvalidCommandMessage);
            }

            QueuedCommand queued = default(QueuedCommand);
            queued.Type = QueuedCommandType.ReleaseStore;
            queued.AircraftIndex = aircraftIndex;
            queued.ReleaseStore = command;
            return Enqueue(in queued);
        }

        private CommandResult SubmitInjectFailure(in InjectFailureCommand command)
        {
            int aircraftIndex = FindAircraftIndex(command.Aircraft);
            if (aircraftIndex < 0)
            {
                return CommandResult.Rejected(2, AircraftNotFoundMessage);
            }

            if (command.Failure < FailureType.Engine ||
                command.Failure > FailureType.Avionics ||
                !IsFinite(command.SeverityNormalized) ||
                !IsFinite(command.DurationS) ||
                command.SeverityNormalized < 0.0 ||
                command.SeverityNormalized > 1.0 ||
                command.DurationS < 0.0)
            {
                return CommandResult.Rejected(3, InvalidCommandMessage);
            }

            QueuedCommand queued = default(QueuedCommand);
            queued.Type = QueuedCommandType.InjectFailure;
            queued.AircraftIndex = aircraftIndex;
            queued.InjectFailure = command;
            return Enqueue(in queued);
        }

        private CommandResult SubmitFormation(in FormationCommand command)
        {
            int aircraftIndex = FindAircraftIndex(command.Aircraft);
            if (aircraftIndex < 0)
            {
                return CommandResult.Rejected(2, AircraftNotFoundMessage);
            }

            if (command.Type < FormationCommandType.Join ||
                command.Type > FormationCommandType.ReturnToBase ||
                !IsFinite(command.ForwardOffsetM) ||
                !IsFinite(command.RightOffsetM) ||
                !IsFinite(command.UpOffsetM))
            {
                return CommandResult.Rejected(3, InvalidCommandMessage);
            }

            if (!TryResolveFormationLeader(
                    aircraftIndex,
                    in command,
                    out int leaderIndex,
                    out AircraftId leaderAircraft))
            {
                return CommandResult.Rejected(3, InvalidCommandMessage);
            }

            QueuedCommand queued = default(QueuedCommand);
            queued.Type = QueuedCommandType.Formation;
            queued.AircraftIndex = aircraftIndex;
            queued.LeaderIndex = leaderIndex;
            queued.LeaderAircraft = leaderAircraft;
            queued.Formation = command;
            return Enqueue(in queued);
        }

        private CommandResult Enqueue(in QueuedCommand command)
        {
            if (commandQueueCount >= CommandQueueCapacity)
            {
                return CommandResult.Rejected(4, QueueFullMessage);
            }

            int tail = (commandQueueHead + commandQueueCount) % CommandQueueCapacity;
            commandQueue[tail] = command;
            commandQueueCount++;
            return CommandResult.Success(CommandQueuedMessage);
        }

        private void ApplyCommandsQueuedBeforeTick()
        {
            int commandsToApply = commandQueueCount;
            for (int commandIndex = 0; commandIndex < commandsToApply; commandIndex++)
            {
                QueuedCommand command = commandQueue[commandQueueHead];
                commandQueue[commandQueueHead] = default(QueuedCommand);
                commandQueueHead = (commandQueueHead + 1) % CommandQueueCapacity;
                commandQueueCount--;
                ApplyCommand(in command);
            }
        }

        private void ApplyCommand(in QueuedCommand command)
        {
            AircraftSlot slot = aircraftSlots[command.AircraftIndex];
            switch (command.Type)
            {
                case QueuedCommandType.ResetScenario:
                    slot.LocalInput = PilotControlInput.Neutral;
                    ReplaceSimulation(slot, ResolveResetPreset(slot, command.ResetScenario.ScenarioId));
                    EmitEvent(slot, SimulationEventType.ScenarioReset, 0, ScenarioResetMessage);
                    break;

                case QueuedCommandType.StartupPreset:
                    ReplaceSimulation(slot, command.StartupPreset.Preset);
                    EmitEvent(slot, SimulationEventType.Information, 0, InformationMessage);
                    break;

                case QueuedCommandType.SystemSwitch:
                    ApplySystemSwitch(slot, in command.SystemSwitch);
                    EmitEvent(slot, SimulationEventType.Information, (int)command.SystemSwitch.Switch, InformationMessage);
                    break;

                case QueuedCommandType.LandingGear:
                    slot.Simulation.SetLandingGearDown(command.LandingGear.IsDown);
                    EmitEvent(slot, SimulationEventType.Information, command.LandingGear.IsDown ? 1 : 0, InformationMessage);
                    break;

                case QueuedCommandType.Loadout:
                    StoreLoadoutCollection loadout = command.Loadout.Stations;
                    slot.Simulation.ConfigureLoadout(in loadout);
                    EmitEvent(slot, SimulationEventType.Information, 0, InformationMessage);
                    break;

                case QueuedCommandType.ReleaseStore:
                    bool released = slot.Simulation.TryReleaseStore(
                        command.ReleaseStore.StationIndex,
                        command.ReleaseStore.Quantity,
                        command.ReleaseStore.EmergencyJettison,
                        out StoreReleaseResult releaseResult);
                    EmitEvent(
                        slot,
                        released ? SimulationEventType.StoreReleased : SimulationEventType.Warning,
                        released ? releaseResult.ReleasedQuantity : command.ReleaseStore.StationIndex,
                        released ? StoreReleasedMessage : StoreReleaseRejectedMessage);
                    break;

                case QueuedCommandType.InjectFailure:
                    slot.Simulation.InjectFailure(
                        command.InjectFailure.Failure,
                        command.InjectFailure.SeverityNormalized,
                        command.InjectFailure.DurationS);
                    EmitEvent(
                        slot,
                        SimulationEventType.Failure,
                        (int)command.InjectFailure.Failure,
                        FailureInjectedMessage);
                    break;

                case QueuedCommandType.Formation:
                    formationController.ApplyCommand(
                        command.AircraftIndex,
                        command.LeaderIndex,
                        command.LeaderAircraft,
                        in command.Formation);
                    EmitEvent(
                        slot,
                        SimulationEventType.FormationModeChanged,
                        (int)formationController.GetMode(command.AircraftIndex),
                        FormationChangedMessage);
                    break;
            }
        }

        private void ApplySystemSwitch(AircraftSlot slot, in SystemSwitchCommand command)
        {
            slot.Simulation.SetSystemSwitch(command.Switch, command.IsEnabled);
            switch (command.Switch)
            {
                case AircraftSystemSwitch.FlightControlComputer:
                    slot.FlightControlComputerEnabled = command.IsEnabled;
                    break;
                case AircraftSystemSwitch.Radar:
                    slot.RadarEnabled = command.IsEnabled;
                    break;
                case AircraftSystemSwitch.DataLink:
                    slot.DataLinkEnabled = command.IsEnabled;
                    break;
            }
        }

        private void ReplaceSimulation(AircraftSlot slot, StartupPreset preset)
        {
            slot.ActivePreset = preset;
            slot.Simulation = F16AircraftSimulation.CreateKtex(preset);
            InitializePresetState(slot, preset);
            slot.AppliedInput = slot.LocalInput;
            InitializeTransitionState(slot);
        }

        private static StartupPreset ResolveResetPreset(AircraftSlot slot, string scenarioId)
        {
            if (string.IsNullOrEmpty(scenarioId))
            {
                return slot.ActivePreset;
            }

            if (scenarioId.IndexOf("cold", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return StartupPreset.ColdAndDark;
            }

            if (scenarioId.IndexOf("runway", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return StartupPreset.RunwayReady;
            }

            if (scenarioId.IndexOf("airborne", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return StartupPreset.Airborne;
            }

            return slot.ActivePreset;
        }

        private bool TryResolveFormationLeader(
            int aircraftIndex,
            in FormationCommand command,
            out int leaderIndex,
            out AircraftId leaderAircraft)
        {
            if (command.Type == FormationCommandType.Leave ||
                command.Type == FormationCommandType.ReturnToBase)
            {
                leaderIndex = -1;
                leaderAircraft = default(AircraftId);
                return true;
            }

            if (!string.IsNullOrEmpty(command.LeaderAircraft.Value))
            {
                leaderIndex = FindAircraftIndex(command.LeaderAircraft);
            }
            else if (!formationController.TryGetLeaderIndex(aircraftIndex, out leaderIndex))
            {
                leaderIndex = FindPrimaryLocalAircraftIndex(aircraftIndex);
            }

            if (command.Type == FormationCommandType.SetOffset && leaderIndex < 0)
            {
                leaderAircraft = default(AircraftId);
                return true;
            }

            if (leaderIndex < 0 || leaderIndex == aircraftIndex)
            {
                leaderAircraft = default(AircraftId);
                return false;
            }

            leaderAircraft = aircraftSlots[leaderIndex].Aircraft;
            return true;
        }

        private void CaptureDispatchSinks()
        {
            dispatchSinkCount = sinkCount;
            for (int index = 0; index < dispatchSinkCount; index++)
            {
                dispatchSinks[index] = sinks[index];
            }
        }

        private void RefreshAllSnapshots()
        {
            for (int aircraftIndex = 0; aircraftIndex < aircraftCount; aircraftIndex++)
            {
                MapFastState(aircraftSlots[aircraftIndex]);
                MapSystemsState(aircraftIndex, aircraftSlots[aircraftIndex]);
            }

            for (int aircraftIndex = 0; aircraftIndex < aircraftCount; aircraftIndex++)
            {
                MapTacticalState(aircraftIndex, aircraftSlots[aircraftIndex]);
            }
        }

        private static void MapFastState(AircraftSlot slot)
        {
            F16AircraftState state = slot.Simulation.State;
            DQuaternion orientation = state.BodyToEcefOrientation;
            slot.Snapshot.Aircraft = slot.Aircraft;
            slot.Snapshot.Fast = new AircraftFastState
            {
                Aircraft = slot.Aircraft,
                Tick = state.Tick,
                SimulationTimeS = state.SimulationTimeS,
                EcefPositionXM = state.EcefPositionM.X,
                EcefPositionYM = state.EcefPositionM.Y,
                EcefPositionZM = state.EcefPositionM.Z,
                EcefVelocityXMps = state.EcefVelocityMps.X,
                EcefVelocityYMps = state.EcefVelocityMps.Y,
                EcefVelocityZMps = state.EcefVelocityMps.Z,
                BodyVelocityXMps = state.BodyVelocityMps.X,
                BodyVelocityYMps = state.BodyVelocityMps.Y,
                BodyVelocityZMps = state.BodyVelocityMps.Z,
                BodyAngularVelocityXRadps = state.BodyAngularVelocityRadps.X,
                BodyAngularVelocityYRadps = state.BodyAngularVelocityRadps.Y,
                BodyAngularVelocityZRadps = state.BodyAngularVelocityRadps.Z,
                BodyToEcefQuaternionX = orientation.X,
                BodyToEcefQuaternionY = orientation.Y,
                BodyToEcefQuaternionZ = orientation.Z,
                BodyToEcefQuaternionW = orientation.W,
                LongitudeRad = state.LongitudeRad,
                LatitudeRad = state.LatitudeRad,
                EllipsoidHeightM = state.EllipsoidHeightM,
                HeadingRad = state.HeadingRad,
                PitchRad = state.PitchRad,
                RollRad = state.RollRad,
                TrueAirspeedMps = state.TrueAirspeedMps,
                CalibratedAirspeedMps = state.CalibratedAirspeedMps,
                GroundSpeedMps = state.GroundSpeedMps,
                Mach = state.Mach,
                AngleOfAttackRad = state.AngleOfAttackRad,
                SideslipRad = state.SideslipRad,
                NormalLoadFactorG = state.NormalLoadFactorG,
                MeanSeaLevelAltitudeM = state.MeanSeaLevelAltitudeM,
                AboveGroundLevelAltitudeM = slot.TerrainSampleValid
                    ? slot.TerrainAboveGroundLevelM
                    : state.AboveGroundLevelAltitudeM,
                ClimbRateMps = state.ClimbRateMps,
                TerrainSampleValid = slot.TerrainSampleValid,
                TerrainSampleAgeS = slot.TerrainSampleAgeS
            };
        }

        private void MapSystemsState(int aircraftIndex, AircraftSlot slot)
        {
            F16AircraftState state = slot.Simulation.State;
            if (state.NormalLoadFactorG > slot.MaximumRecordedLoadFactorG)
            {
                slot.MaximumRecordedLoadFactorG = state.NormalLoadFactorG;
            }

            FlightControlOutput controls = slot.Simulation.FlightControls;
            WarningState warnings = BuildWarningState(slot, in state);
            AircraftSystemsState systems = new AircraftSystemsState
            {
                Aircraft = slot.Aircraft,
                SimulationTimeS = state.SimulationTimeS,
                FlightControls = new FlightControlState
                {
                    ActiveControlAuthority = slot.AppliedControlAuthority,
                    PitchCommandNormalized = slot.AppliedInput.PitchNormalized,
                    RollCommandNormalized = slot.AppliedInput.RollNormalized,
                    YawCommandNormalized = slot.AppliedInput.YawNormalized,
                    AileronDeflectionRad = controls.AileronDeflectionRad,
                    ElevatorDeflectionRad = controls.ElevatorDeflectionRad,
                    RudderDeflectionRad = controls.RudderDeflectionRad,
                    AngleOfAttackLimiterActive = controls.AngleOfAttackLimiterActive,
                    GForceLimiterActive = controls.GForceLimiterActive,
                    RollRateLimiterActive = controls.RollRateLimiterActive,
                    FlightControlComputerEnabled = slot.FlightControlComputerEnabled &&
                                                   slot.Simulation.Electrical.EssentialBusPowered,
                    AutopilotEngaged = slot.AppliedControlAuthority != ControlAuthority.ManualPilot
                },
                Propulsion = slot.Simulation.Propulsion,
                Fuel = slot.Simulation.Fuel,
                Electrical = slot.Simulation.Electrical,
                Hydraulics = slot.Simulation.Hydraulics,
                LandingGear = slot.Simulation.LandingGear,
                Stores = slot.Simulation.Stores,
                Warnings = warnings
            };
            systems.Avionics = BuildAvionicsState(slot, in state, in systems);
            slot.Snapshot.Systems = systems;
        }

        private static WarningState BuildWarningState(AircraftSlot slot, in F16AircraftState state)
        {
            WarningState warnings = slot.Simulation.Warnings;
            FuelState fuel = slot.Simulation.Fuel;
            LandingGearState gear = slot.Simulation.LandingGear;
            bool gearDown = IsLandingGearDown(in gear);
            warnings.FuelWarning = warnings.FuelWarning || fuel.LowFuelWarning;
            double aglM = slot.TerrainSampleValid
                ? slot.TerrainAboveGroundLevelM
                : state.AboveGroundLevelAltitudeM;
            return DynamicWarningEvaluator.Evaluate(
                in warnings,
                slot.TerrainSampleValid,
                state.HasTakenOff,
                state.WeightOnWheels,
                aglM,
                state.ClimbRateMps,
                gearDown,
                state.Mach,
                state.AngleOfAttackRad);
        }

        private static AvionicsState BuildAvionicsState(
            AircraftSlot slot,
            in F16AircraftState state,
            in AircraftSystemsState systems)
        {
            bool avionicsPowered = systems.Electrical.AvionicsBusPowered;
            double aglM = slot.TerrainSampleValid
                ? slot.TerrainAboveGroundLevelM
                : state.AboveGroundLevelAltitudeM;
            bool gearDown = IsLandingGearDown(in systems.LandingGear);
            AircraftStartupState startupState = ResolveStartupState(
                avionicsPowered,
                systems.Electrical.EssentialBusPowered,
                in systems.Propulsion);
            HudMode hudMode = !avionicsPowered
                ? HudMode.Off
                : gearDown || state.WeightOnWheels ? HudMode.Landing : HudMode.Nav;
            ResolveSelectedStore(
                in systems.Stores,
                out string selectedStoreType,
                out int selectedStoreQuantity,
                out bool masterArmEnabled);

            AnalyticRunway runway = slot.Simulation.Runway;
            DVector3 steerpointEcefM = GetReturnWaypointEcef(in runway);
            DVector3 steerpointBodyM = state.BodyToEcefOrientation.RotateInverse(
                steerpointEcefM - state.EcefPositionM);
            double steerpointHorizontalM = Math.Sqrt(
                steerpointBodyM.X * steerpointBodyM.X +
                steerpointBodyM.Y * steerpointBodyM.Y);
            double bodyHorizontalSpeedMps = Math.Sqrt(
                state.BodyVelocityMps.X * state.BodyVelocityMps.X +
                state.BodyVelocityMps.Y * state.BodyVelocityMps.Y);
            double steerpointDistanceM = steerpointBodyM.Length;
            double steerpointTimeToGoS = steerpointDistanceM /
                                         Math.Max(1.0, state.GroundSpeedMps);
            double localizerDeviationNormalized = ClampFinite(
                -state.DistanceRightOfRunwayCenterlineM / Math.Max(1.0, runway.WidthM * 0.5),
                -1.0,
                1.0);
            double distanceBeforeThresholdM = Math.Max(
                0.0,
                -runway.DistanceAlongM(in state.EcefPositionM));
            double desiredGlideHeightM = 15.0 + distanceBeforeThresholdM *
                                         Math.Tan(3.0 * Math.PI / 180.0);
            double glideslopeDeviationNormalized = ClampFinite(
                (aglM - desiredGlideHeightM) / 120.0,
                -1.0,
                1.0);
            double steerpointBearingRad = Math.Atan2(
                steerpointBodyM.Y,
                Math.Max(1.0, steerpointBodyM.X));
            double steerpointElevationRad = Math.Atan2(
                -steerpointBodyM.Z,
                Math.Max(1.0, steerpointHorizontalM));
            bool ilsValid = hudMode == HudMode.Landing &&
                            slot.TerrainSampleValid && aglM < 1200.0;
            HudState hud = new HudState
            {
                Enabled = avionicsPowered,
                IsValid = avionicsPowered,
                Mode = hudMode,
                ScaleMode = HudScaleMode.VvVah,
                VelocitySource = HudVelocitySource.Calibrated,
                AltitudeSource = HudAltitudeSource.Automatic,
                BrightnessNormalized = avionicsPowered ? 1.0 : 0.0,
                LandingDeclutter = hudMode == HudMode.Landing,
                DriftCutout = false,
                ShowFlightPathMarker = avionicsPowered,
                CalibratedAirspeedMps = state.CalibratedAirspeedMps,
                TrueAirspeedMps = state.TrueAirspeedMps,
                GroundSpeedMps = state.GroundSpeedMps,
                BarometricAltitudeM = state.MeanSeaLevelAltitudeM,
                RadarAltitudeM = aglM,
                RadarAltitudeValid = slot.TerrainSampleValid && aglM >= 0.0 && IsFinite(aglM),
                HeadingRad = state.HeadingRad,
                PitchRad = state.PitchRad,
                RollRad = state.RollRad,
                FlightPathAzimuthRad = Math.Atan2(
                    state.BodyVelocityMps.Y,
                    Math.Max(1.0, state.BodyVelocityMps.X)),
                FlightPathElevationRad = Math.Atan2(
                    -state.BodyVelocityMps.Z,
                    Math.Max(1.0, bodyHorizontalSpeedMps)),
                AngleOfAttackRad = state.AngleOfAttackRad,
                NormalLoadFactorG = state.NormalLoadFactorG,
                MaximumRecordedLoadFactorG = slot.MaximumRecordedLoadFactorG,
                Mach = state.Mach,
                VerticalSpeedMps = state.ClimbRateMps,
                AltitudeLowWarningM = 120.0,
                SteerpointValid = true,
                SelectedSteerpointIndex = 0,
                SteerpointBearingRad = steerpointBearingRad,
                SteerpointElevationRad = steerpointElevationRad,
                SteerpointDistanceM = steerpointDistanceM,
                SteerpointSlantRangeM = steerpointDistanceM,
                SteerpointTimeToGoS = steerpointTimeToGoS,
                GreatCircleSteeringErrorRad = steerpointBearingRad,
                IlsEnabled = hudMode == HudMode.Landing,
                LocalizerValid = ilsValid,
                LocalizerDeviationNormalized = localizerDeviationNormalized,
                GlideslopeValid = ilsValid,
                GlideslopeDeviationNormalized = glideslopeDeviationNormalized,
                CommandSteeringLateralNormalized = ClampFinite(
                    steerpointBearingRad / (20.0 * Math.PI / 180.0),
                    -1.0,
                    1.0),
                CommandSteeringVerticalNormalized = ClampFinite(
                    steerpointElevationRad / (10.0 * Math.PI / 180.0),
                    -1.0,
                    1.0),
                MasterCaution = systems.Warnings.MasterCaution,
                MasterWarning = systems.Warnings.MasterWarning,
                WeightOnWheels = state.WeightOnWheels,
                LandingGearDown = gearDown,
                MasterArm = masterArmEnabled ? MasterArmState.Arm : MasterArmState.Safe,
                MasterArmEnabled = masterArmEnabled,
                SelectedStoreType = selectedStoreType,
                SelectedStoreQuantity = selectedStoreQuantity
            };

            return new AvionicsState
            {
                InsState = !avionicsPowered
                    ? InertialNavigationState.Off
                    : startupState == AircraftStartupState.Ready
                        ? InertialNavigationState.Navigation
                        : InertialNavigationState.Aligning,
                HudEnabled = avionicsPowered,
                HudMode = hudMode,
                StartupState = startupState,
                ActiveStartupPreset = slot.ActivePreset,
                MasterModeAirToAir = selectedStoreType.StartsWith("AIM-", StringComparison.Ordinal),
                RadarEnabled = slot.RadarEnabled && avionicsPowered,
                RadarLocked = false,
                RadarRangeM = slot.RadarEnabled && avionicsPowered ? 80000.0 : 0.0,
                NavigationComputerEnabled = avionicsPowered,
                DataLinkEnabled = slot.DataLinkEnabled && avionicsPowered,
                Hud = hud
            };
        }

        private void MapTacticalState(int aircraftIndex, AircraftSlot slot)
        {
            F16AircraftState observerState = slot.Simulation.State;
            TacticalPictureState tactical = TacticalPictureState.CreateDefault(slot.Aircraft);
            tactical.SimulationTimeS = observerState.SimulationTimeS;
            tactical.Formation = formationController.GetState(aircraftIndex);
            TacticalTrackCollection tracks = default(TacticalTrackCollection);
            int trackIndex = 0;
            for (int otherIndex = 0; otherIndex < aircraftCount; otherIndex++)
            {
                if (otherIndex == aircraftIndex)
                {
                    continue;
                }

                F16AircraftState otherState = aircraftSlots[otherIndex].Simulation.State;
                DVector3 relativeBodyM = observerState.BodyToEcefOrientation.RotateInverse(
                    otherState.EcefPositionM - observerState.EcefPositionM);
                double horizontalRangeM = Math.Sqrt(
                    relativeBodyM.X * relativeBodyM.X +
                    relativeBodyM.Y * relativeBodyM.Y);
                tracks[trackIndex++] = new TacticalTrack
                {
                    TrackId = otherIndex + 1,
                    Classification = TacticalTrackClassification.Air,
                    Affiliation = ResolveAffiliation(slot.Identity.Side, aircraftSlots[otherIndex].Identity.Side),
                    EcefPositionXM = otherState.EcefPositionM.X,
                    EcefPositionYM = otherState.EcefPositionM.Y,
                    EcefPositionZM = otherState.EcefPositionM.Z,
                    EcefVelocityXMps = otherState.EcefVelocityMps.X,
                    EcefVelocityYMps = otherState.EcefVelocityMps.Y,
                    EcefVelocityZMps = otherState.EcefVelocityMps.Z,
                    RangeM = relativeBodyM.Length,
                    BearingRad = Math.Atan2(
                        relativeBodyM.Y,
                        Math.Max(1.0, relativeBodyM.X)),
                    ElevationRad = Math.Atan2(
                        -relativeBodyM.Z,
                        Math.Max(1.0, horizontalRangeM)),
                    ConfidenceNormalized = 1.0,
                    IsValid = true
                };
            }

            tactical.Tracks = tracks;
            WaypointCollection waypoints = default(WaypointCollection);
            AnalyticRunway runway = slot.Simulation.Runway;
            DVector3 waypointEcefM = GetReturnWaypointEcef(in runway);
            GeoMath.EcefToLla(
                waypointEcefM,
                out double longitudeRad,
                out double latitudeRad,
                out double ellipsoidHeightM);
            waypoints[0] = new WaypointState
            {
                Index = 0,
                LongitudeRad = longitudeRad,
                LatitudeRad = latitudeRad,
                EllipsoidHeightM = ellipsoidHeightM,
                EcefPositionXM = waypointEcefM.X,
                EcefPositionYM = waypointEcefM.Y,
                EcefPositionZM = waypointEcefM.Z,
                Name = ReturnWaypointName,
                IsActive = formationController.GetMode(aircraftIndex) == FormationMode.ReturnToBase,
                IsValid = true
            };
            tactical.Waypoints = waypoints;
            slot.Snapshot.Tactical = tactical;
        }

        private void EmitDiscreteStateEvents()
        {
            for (int aircraftIndex = 0; aircraftIndex < aircraftCount; aircraftIndex++)
            {
                AircraftSlot slot = aircraftSlots[aircraftIndex];
                F16AircraftState state = slot.Simulation.State;
                bool engineRunning = slot.Simulation.Propulsion.EngineRunning;
                bool masterCaution = slot.Snapshot.Systems.Warnings.MasterCaution;

                if (!slot.WasEngineRunning && engineRunning)
                {
                    EmitEvent(slot, SimulationEventType.EngineStarted, 0, EngineStartedMessage);
                }
                else if (slot.WasEngineRunning && !engineRunning)
                {
                    EmitEvent(slot, SimulationEventType.EngineStopped, 0, EngineStoppedMessage);
                }

                if (!slot.HadTakenOff && state.HasTakenOff)
                {
                    EmitEvent(slot, SimulationEventType.Takeoff, 0, TakeoffMessage);
                }

                if (slot.HadTakenOff && !slot.WasWeightOnWheels && state.WeightOnWheels)
                {
                    EmitEvent(slot, SimulationEventType.Touchdown, 0, TouchdownMessage);
                }

                bool crashed = slot.TerrainSampleValid &&
                               !state.WeightOnWheels &&
                               slot.TerrainAboveGroundLevelM <= 0.5 &&
                               state.ClimbRateMps < -15.0;
                if (!slot.CrashReported && crashed)
                {
                    slot.CrashReported = true;
                    EmitEvent(slot, SimulationEventType.Crash, 0, CrashMessage);
                }

                if (!slot.WasMasterCaution && masterCaution)
                {
                    EmitEvent(slot, SimulationEventType.Warning, 0, WarningMessage);
                }

                slot.WasEngineRunning = engineRunning;
                slot.WasWeightOnWheels = state.WeightOnWheels;
                slot.HadTakenOff = slot.HadTakenOff || state.HasTakenOff;
                slot.WasMasterCaution = masterCaution;
            }
        }

        private void EmitEvent(
            AircraftSlot slot,
            SimulationEventType type,
            int code,
            string message)
        {
            SimulationEvent simulationEvent = new SimulationEvent
            {
                Aircraft = slot.Aircraft,
                SimulationTimeS = slot.Simulation.State.SimulationTimeS,
                Type = type,
                Code = code,
                Message = message
            };
            for (int sinkIndex = 0; sinkIndex < dispatchSinkCount; sinkIndex++)
            {
                IFlightTelemetrySink sink = dispatchSinks[sinkIndex];
                if (sink != null)
                {
                    sink.OnSimulationEvent(in simulationEvent);
                }
            }
        }

        private void PublishScheduledTelemetry()
        {
            bool publishFast = serviceTick % 2UL == 0UL;
            bool publishSystems = serviceTick % 10UL == 0UL;
            bool publishTactical = serviceTick % 20UL == 0UL;
            if (!publishFast && !publishSystems && !publishTactical)
            {
                return;
            }

            for (int aircraftIndex = 0; aircraftIndex < aircraftCount; aircraftIndex++)
            {
                AircraftSnapshot snapshot = aircraftSlots[aircraftIndex].Snapshot;
                for (int sinkIndex = 0; sinkIndex < dispatchSinkCount; sinkIndex++)
                {
                    IFlightTelemetrySink sink = dispatchSinks[sinkIndex];
                    if (sink == null)
                    {
                        continue;
                    }

                    if (publishFast)
                    {
                        AircraftFastState fast = snapshot.Fast;
                        sink.OnFastState(in fast);
                    }

                    if (publishSystems)
                    {
                        AircraftSystemsState systems = snapshot.Systems;
                        sink.OnSystemsState(in systems);
                    }

                    if (publishTactical)
                    {
                        TacticalPictureState tactical = snapshot.Tactical;
                        sink.OnTacticalPictureState(in tactical);
                    }
                }
            }
        }

        private int FindAircraftIndex(AircraftId aircraftId)
        {
            for (int index = 0; index < aircraftCount; index++)
            {
                if (aircraftSlots[index].Aircraft == aircraftId)
                {
                    return index;
                }
            }

            return -1;
        }

        private int FindPrimaryLocalAircraftIndex(int excludedIndex)
        {
            for (int index = 0; index < aircraftCount; index++)
            {
                if (index != excludedIndex && aircraftSlots[index].IsLocalPilot)
                {
                    return index;
                }
            }

            return -1;
        }

        private int CountWingmenForLeader(int leaderIndex)
        {
            int count = 1;
            for (int index = 0; index < aircraftCount; index++)
            {
                if (index != leaderIndex &&
                    formationController.TryGetLeaderIndex(index, out int existingLeaderIndex) &&
                    existingLeaderIndex == leaderIndex)
                {
                    count++;
                }
            }

            return count > 3 ? 3 : count;
        }

        private void AssignUnledWingmen(int leaderIndex)
        {
            int formationSlot = 1;
            for (int index = 0; index < aircraftCount; index++)
            {
                if (index == leaderIndex || aircraftSlots[index].IsLocalPilot)
                {
                    continue;
                }

                if (!formationController.TryGetLeaderIndex(index, out _))
                {
                    formationController.ConfigureDefaultWingman(
                        index,
                        leaderIndex,
                        aircraftSlots[leaderIndex].Aircraft,
                        formationSlot > 3 ? 3 : formationSlot);
                    formationSlot++;
                }
            }
        }

        private static void InitializePresetState(AircraftSlot slot, StartupPreset preset)
        {
            bool ready = preset != StartupPreset.ColdAndDark;
            slot.FlightControlComputerEnabled = ready;
            slot.RadarEnabled = ready;
            slot.DataLinkEnabled = ready;
            slot.TerrainSampleValid = false;
            slot.TerrainAboveGroundLevelM = 0.0;
            slot.TerrainSampleAgeS = 0.0;
        }

        private static void InitializeTransitionState(AircraftSlot slot)
        {
            F16AircraftState state = slot.Simulation.State;
            slot.WasEngineRunning = slot.Simulation.Propulsion.EngineRunning;
            slot.WasWeightOnWheels = state.WeightOnWheels;
            slot.HadTakenOff = state.HasTakenOff;
            slot.WasMasterCaution = slot.Simulation.Warnings.MasterCaution;
            slot.CrashReported = false;
            slot.MaximumRecordedLoadFactorG = state.NormalLoadFactorG;
        }

        private static PilotControlInput CreateDefaultInput(StartupPreset preset, bool isLocalPilot)
        {
            PilotControlInput input = PilotControlInput.Neutral;
            if (!isLocalPilot && preset != StartupPreset.ColdAndDark)
            {
                input.ThrottleNormalized = 0.72;
            }

            return input;
        }

        private static PilotControlInput SanitizeInput(in PilotControlInput input)
        {
            return new PilotControlInput
            {
                PitchNormalized = ClampFinite(input.PitchNormalized, -1.0, 1.0),
                RollNormalized = ClampFinite(input.RollNormalized, -1.0, 1.0),
                YawNormalized = ClampFinite(input.YawNormalized, -1.0, 1.0),
                ThrottleNormalized = ClampFinite(input.ThrottleNormalized, 0.0, 1.0),
                WheelBrakeNormalized = ClampFinite(input.WheelBrakeNormalized, 0.0, 1.0),
                SpeedBrakeNormalized = ClampFinite(input.SpeedBrakeNormalized, 0.0, 1.0),
                EngineStartCommand = input.EngineStartCommand
            };
        }

        private static AircraftStartupState ResolveStartupState(
            bool avionicsPowered,
            bool essentialPower,
            in PropulsionState propulsion)
        {
            if (!essentialPower)
            {
                return AircraftStartupState.Off;
            }

            if (propulsion.Mode == EngineMode.Starting)
            {
                return AircraftStartupState.EngineStarting;
            }

            if (!propulsion.EngineRunning)
            {
                return avionicsPowered
                    ? AircraftStartupState.SystemsInitializing
                    : AircraftStartupState.PowerApplied;
            }

            return avionicsPowered
                ? AircraftStartupState.Ready
                : AircraftStartupState.PowerApplied;
        }

        private static void ResolveSelectedStore(
            in StoresState stores,
            out string selectedStoreType,
            out int selectedStoreQuantity,
            out bool masterArmEnabled)
        {
            selectedStoreType = string.Empty;
            selectedStoreQuantity = 0;
            masterArmEnabled = false;
            StoreStationCollection stations = stores.Stations;
            int firstLoadedIndex = -1;
            for (int stationIndex = 0; stationIndex < StoresState.StationCapacity; stationIndex++)
            {
                StoreStationState station = stations[stationIndex];
                masterArmEnabled = masterArmEnabled || station.IsArmed;
                if (station.Quantity > 0 && firstLoadedIndex < 0)
                {
                    firstLoadedIndex = stationIndex;
                }

                if (station.IsSelected && station.Quantity > 0)
                {
                    selectedStoreType = station.StoreType ?? string.Empty;
                    selectedStoreQuantity = station.Quantity;
                    return;
                }
            }

            if (firstLoadedIndex >= 0)
            {
                StoreStationState selected = stations[firstLoadedIndex];
                selectedStoreType = selected.StoreType ?? string.Empty;
                selectedStoreQuantity = selected.Quantity;
            }
        }

        private static DVector3 GetReturnWaypointEcef(in AnalyticRunway runway)
        {
            return runway.ThresholdEcefM - runway.ForwardEcef * 1200.0 + runway.UpEcef * 350.0;
        }

        private static bool IsLandingGearDown(in LandingGearState gear)
        {
            return gear.NoseGearPositionNormalized >= 0.95 &&
                   gear.LeftMainGearPositionNormalized >= 0.95 &&
                   gear.RightMainGearPositionNormalized >= 0.95;
        }

        private static bool IsDefinedStartupPreset(StartupPreset preset)
        {
            return preset >= StartupPreset.ColdAndDark && preset <= StartupPreset.Airborne;
        }

        private static TacticalTrackAffiliation ResolveAffiliation(
            AircraftSide observer,
            AircraftSide other)
        {
            if (other == AircraftSide.Neutral) return TacticalTrackAffiliation.Neutral;
            if (observer == AircraftSide.Neutral) return TacticalTrackAffiliation.Unknown;
            return observer == other
                ? TacticalTrackAffiliation.Friendly
                : TacticalTrackAffiliation.Hostile;
        }

        private static bool IsFinite(double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value);
        }

        private static double ClampFinite(double value, double minimum, double maximum)
        {
            if (!IsFinite(value))
            {
                return 0.0;
            }

            return value < minimum ? minimum : value > maximum ? maximum : value;
        }

        private sealed class AircraftSlot
        {
            public AircraftId Aircraft;
            public AircraftIdentityState Identity;
            public bool IsLocalPilot;
            public StartupPreset ActivePreset;
            public F16AircraftSimulation Simulation;
            public PilotControlInput LocalInput;
            public PilotControlInput AppliedInput;
            public ControlAuthority RequestedControlAuthority;
            public ControlAuthority AppliedControlAuthority;
            public AircraftSnapshot Snapshot;
            public bool FlightControlComputerEnabled;
            public bool RadarEnabled;
            public bool DataLinkEnabled;
            public bool WasEngineRunning;
            public bool WasWeightOnWheels;
            public bool HadTakenOff;
            public bool WasMasterCaution;
            public bool CrashReported;
            public double MaximumRecordedLoadFactorG;
            public bool TerrainSampleValid;
            public double TerrainAboveGroundLevelM;
            public double TerrainSampleAgeS;
        }

        private struct QueuedCommand
        {
            public QueuedCommandType Type;
            public int AircraftIndex;
            public int LeaderIndex;
            public AircraftId LeaderAircraft;
            public ResetScenarioCommand ResetScenario;
            public StartupPresetCommand StartupPreset;
            public SystemSwitchCommand SystemSwitch;
            public LandingGearCommand LandingGear;
            public LoadoutConfigurationCommand Loadout;
            public ReleaseStoreCommand ReleaseStore;
            public InjectFailureCommand InjectFailure;
            public FormationCommand Formation;
        }

        private enum QueuedCommandType : byte
        {
            ResetScenario,
            StartupPreset,
            SystemSwitch,
            LandingGear,
            Loadout,
            ReleaseStore,
            InjectFailure,
            Formation
        }
    }
}
