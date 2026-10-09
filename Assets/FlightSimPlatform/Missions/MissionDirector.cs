using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Integration;

namespace FlightSim.Platform.Missions
{
    public sealed class MissionDirector : IMissionTelemetrySource
    {
        public const ushort MissionSchemaVersion = 1;
        private const int MaximumSinks = 8;
        private const double KtexLongitudeRad = -107.9087 * Math.PI / 180.0;
        private const double KtexLatitudeRad = 37.9538 * Math.PI / 180.0;
        private const double KtexRunwayHeightM = 2765.0;
        private const double KtexRunwayHeadingRad = 285.0 * Math.PI / 180.0;
        private const double KtexRunwayHalfLengthM = 2167.0 * 0.5;
        private const double EarthRadiusM = 6371008.8;

        private readonly FlightSimulationService flightService;
        private readonly IMissionTelemetrySink[] sinks = new IMissionTelemetrySink[MaximumSinks];
        private MissionDefinition definition;
        private MissionActorState[] actorStates = Array.Empty<MissionActorState>();
        private AircraftCombatState[] combatStates = Array.Empty<AircraftCombatState>();
        private MissionObjectiveState[] objectiveStates = Array.Empty<MissionObjectiveState>();
        private double[] lastEngagementTimeS = Array.Empty<double>();
        private int[,] pendingStoreReleases = new int[0, 0];
        private MissionState state;
        private AutomationRunState automation;
        private int sinkCount;
        private ulong tick;
        private bool automationWasEnabled;
        private int engagementSequence;
        private readonly PendingEngagement[] pendingEngagements = new PendingEngagement[64];

        public MissionDirector(FlightSimulationService flightService)
        {
            this.flightService = flightService ?? throw new ArgumentNullException(nameof(flightService));
        }

        public ushort SchemaVersion => MissionSchemaVersion;
        public MissionState LatestState => state;
        public int ActorCount => actorStates.Length;
        public MissionDefinition Definition => definition;

        public CommandResult Load(MissionDefinition mission, int seed)
        {
            if (definition != null)
                return CommandResult.Rejected(2001, "A mission is already loaded in this director.");
            MissionValidationResult validation = MissionDefinitionValidator.Validate(mission);
            if (!validation.IsValid)
                return CommandResult.Rejected(2002, validation.FirstError);

            definition = mission;
            actorStates = new MissionActorState[mission.Actors.Length];
            combatStates = new AircraftCombatState[mission.Actors.Length];
            lastEngagementTimeS = new double[mission.Actors.Length];
            pendingStoreReleases = new int[mission.Actors.Length, StoresState.StationCapacity];
            objectiveStates = new MissionObjectiveState[mission.Objectives?.Length ?? 0];
            string runId = mission.MissionId + "-" + seed.ToString("D8");
            state = new MissionState
            {
                MissionId = mission.MissionId,
                RunId = runId,
                SchemaVersion = mission.SchemaVersion,
                Seed = seed,
                Phase = MissionPhase.Briefing,
                Result = MissionResult.None,
                RemainingTimeS = mission.MaximumDurationS,
                IsValid = true,
                CompletionReason = string.Empty
            };

            for (int index = 0; index < mission.Actors.Length; index++)
            {
                MissionActorDefinition actor = mission.Actors[index];
                AircraftId aircraft = new AircraftId(actor.AircraftId);
                AircraftIdentityState identity = new AircraftIdentityState
                {
                    Aircraft = aircraft,
                    Callsign = string.IsNullOrEmpty(actor.Callsign) ? actor.AircraftId : actor.Callsign,
                    Side = actor.Side,
                    Role = actor.Role,
                    SimulationModelId = actor.SimulationModelId,
                    VisualModelId = actor.VisualModelId
                };
                AircraftRegistration registration = new AircraftRegistration
                {
                    Aircraft = aircraft,
                    IsLocalPilot = actor.IsPlayer,
                    Identity = identity,
                    InitialCondition = actor.InitialCondition
                };
                if (!flightService.AddAircraft(in registration))
                    return CommandResult.Rejected(2003, $"Unable to register actor '{actor.AircraftId}'.");
                actorStates[index] = new MissionActorState
                {
                    Aircraft = aircraft,
                    Callsign = identity.Callsign,
                    Side = actor.Side,
                    Role = actor.Role,
                    Status = MissionActorStatus.Ready,
                    AiMode = actor.InitialAiMode,
                    ControlAuthority = actor.IsPlayer
                        ? ControlAuthority.ManualPilot
                        : !string.IsNullOrEmpty(actor.LeaderAircraftId)
                            ? ControlAuthority.FormationAI
                            : ControlAuthority.MissionAI,
                    LeaderAircraft = new AircraftId(actor.LeaderAircraftId),
                    FormationSlot = actor.FormationSlot,
                    AiSkillNormalized = actor.AiSkillNormalized,
                    CurrentWaypointIndex = 0,
                    IsValid = true
                };
                flightService.SetControlAuthority(aircraft, actorStates[index].ControlAuthority);
                combatStates[index] = new AircraftCombatState
                {
                    Aircraft = aircraft,
                    MasterArm = actor.IsPlayer ? MasterArmState.Safe : MasterArmState.Arm,
                    SelectedStationIndex = -1,
                    SelectedTrackId = -1,
                    IsValid = true
                };
                lastEngagementTimeS[index] = -1000.0;
            }

            for (int index = 0; index < objectiveStates.Length; index++)
            {
                MissionObjectiveDefinition objective = mission.Objectives[index];
                objectiveStates[index] = new MissionObjectiveState
                {
                    ObjectiveId = objective.ObjectiveId,
                    DisplayName = objective.DisplayName,
                    Type = objective.Type,
                    Status = MissionObjectiveStatus.Pending,
                    RequiredCount = Math.Max(1, objective.RequiredCount),
                    DeadlineS = objective.TimeLimitS
                };
            }

            EmitMissionEvent(MissionEventType.Loaded, default(AircraftId), default(AircraftId), 0, "Mission loaded.");
            return CommandResult.Success("Mission loaded.");
        }

        public CommandResult Start()
        {
            if (definition == null || state.Phase != MissionPhase.Briefing)
                return CommandResult.Rejected(2010, "Mission is not ready to start.");

            for (int index = 0; index < definition.Actors.Length; index++)
            {
                MissionActorDefinition actor = definition.Actors[index];
                AircraftId aircraft = new AircraftId(actor.AircraftId);
                if (!string.IsNullOrEmpty(actor.LeaderAircraftId))
                {
                    FormationCommand formation = new FormationCommand
                    {
                        Aircraft = aircraft,
                        Type = FormationCommandType.Join,
                        LeaderAircraft = new AircraftId(actor.LeaderAircraftId),
                        ForwardOffsetM = actor.FormationForwardOffsetM,
                        RightOffsetM = actor.FormationRightOffsetM,
                        UpOffsetM = actor.FormationUpOffsetM
                    };
                    flightService.Submit(in formation);
                }
                MissionFailureDefinition[] failures = actor.InitialFailures ?? Array.Empty<MissionFailureDefinition>();
                for (int failureIndex = 0; failureIndex < failures.Length; failureIndex++)
                {
                    MissionFailureDefinition failure = failures[failureIndex];
                    InjectFailureCommand command = new InjectFailureCommand
                    {
                        Aircraft = aircraft,
                        Failure = failure.Type,
                        SeverityNormalized = failure.SeverityNormalized,
                        DurationS = failure.DurationS
                    };
                    flightService.Submit(in command);
                }
                MissionActorState actorState = actorStates[index];
                actorState.Status = MissionActorStatus.Active;
                actorStates[index] = actorState;
            }
            for (int index = 0; index < objectiveStates.Length; index++)
            {
                MissionObjectiveState objective = objectiveStates[index];
                objective.Status = MissionObjectiveStatus.Active;
                objectiveStates[index] = objective;
            }

            state.Phase = MissionPhase.Running;
            state.ActiveObjectiveCount = objectiveStates.Length;
            automation = new AutomationRunState
            {
                RunId = state.RunId,
                MissionId = state.MissionId,
                Seed = state.Seed,
                Mode = AutomationMode.Manual,
                Status = AutomationRunStatus.Running,
                PlayerControlAuthority = ControlAuthority.ManualPilot
            };
            flightService.Tick();
            tick++;
            EmitMissionEvent(MissionEventType.Started, default(AircraftId), default(AircraftId), 0, "Mission started.");
            Publish();
            return CommandResult.Success("Mission started.");
        }

        public CommandResult SetPlayerAutomation(bool enabled, AutomationMode requestedMode)
        {
            if (definition == null || state.Phase != MissionPhase.Running)
                return CommandResult.Rejected(2020, "Mission is not running.");

            int playerIndex = FindPlayerActorIndex();
            if (playerIndex < 0)
                return CommandResult.Rejected(2021, "Mission has no player actor.");

            MissionActorState player = actorStates[playerIndex];
            if (enabled)
            {
                automationWasEnabled = true;
                player.ControlAuthority = ControlAuthority.AutomationPilot;
                player.AiMode = definition.Actors[playerIndex].InitialAiMode == MissionAiMode.Manual
                    ? MissionAiMode.Navigate
                    : definition.Actors[playerIndex].InitialAiMode;
                automation.Mode = requestedMode == AutomationMode.Batch
                    ? AutomationMode.Batch
                    : AutomationMode.Visible;
                automation.Status = AutomationRunStatus.Running;
                automation.PlayerControlAuthority = ControlAuthority.AutomationPilot;
                AircraftCombatState combat = combatStates[playerIndex];
                combat.MasterArm = MasterArmState.Arm;
                combatStates[playerIndex] = combat;
            }
            else
            {
                player.ControlAuthority = ControlAuthority.ManualPilot;
                player.AiMode = MissionAiMode.Manual;
                automation.Mode = automationWasEnabled ? AutomationMode.Mixed : AutomationMode.Manual;
                automation.PlayerControlAuthority = ControlAuthority.ManualPilot;
                AircraftCombatState combat = combatStates[playerIndex];
                combat.MasterArm = MasterArmState.Safe;
                combatStates[playerIndex] = combat;
            }
            actorStates[playerIndex] = player;
            flightService.SetControlAuthority(player.Aircraft, player.ControlAuthority);
            EmitMissionEvent(
                MissionEventType.ControlAuthorityChanged,
                player.Aircraft,
                default(AircraftId),
                enabled ? 1 : 0,
                enabled ? "Automation pilot engaged." : "Manual pilot took control.");
            Publish();
            return CommandResult.Success(enabled ? "Automation enabled." : "Manual control enabled.");
        }

        public void ConfigureAutomationRun(
            string batchId,
            string reportPath,
            TerrainSource terrainSource,
            bool terrainDataValid)
        {
            automation.BatchId = batchId ?? string.Empty;
            automation.ReportPath = reportPath ?? string.Empty;
            automation.TerrainSource = terrainSource;
            automation.TerrainDataValid = terrainDataValid;
        }

        public void UpdateTerrainStatus(TerrainSource source, bool isValid)
        {
            automation.TerrainSource = source;
            automation.TerrainDataValid = isValid;
        }

        public void UpdateAutomationTiming(double wallClockTimeS, double simulationRate)
        {
            automation.WallClockTimeS = Math.Max(0.0, wallClockTimeS);
            automation.SimulationRate = Math.Max(0.0, simulationRate);
        }

        public void ReportTerrainCollision(AircraftId aircraft, double terrainHeightM)
        {
            int actorIndex = IndexOfActor(aircraft);
            if (actorIndex < 0 || actorStates[actorIndex].Status != MissionActorStatus.Active) return;
            MissionActorState actor = actorStates[actorIndex];
            actor.Status = MissionActorStatus.Failed;
            actor.IsValid = false;
            actorStates[actorIndex] = actor;
            EmitMissionEvent(
                MissionEventType.TerrainCollision,
                aircraft,
                default(AircraftId),
                1,
                $"Aircraft descended below terrain height {terrainHeightM:F1} m.");
            if (definition.Actors[actorIndex].IsPlayer)
                Complete(false, $"Player aircraft collided with terrain at {terrainHeightM:F1} m.");
        }

        public CommandResult CyclePlayerTarget()
        {
            int playerIndex = FindPlayerActorIndex();
            if (playerIndex < 0) return CommandResult.Rejected(2030, "Mission has no player actor.");
            int startIndex = IndexOfActor(combatStates[playerIndex].SelectedTarget);
            for (int offset = 1; offset <= actorStates.Length; offset++)
            {
                int candidateIndex = (Math.Max(-1, startIndex) + offset) % actorStates.Length;
                if (candidateIndex == playerIndex ||
                    actorStates[candidateIndex].Side == actorStates[playerIndex].Side ||
                    actorStates[candidateIndex].Status != MissionActorStatus.Active)
                    continue;
                AircraftCombatState combat = combatStates[playerIndex];
                combat.SelectedTarget = actorStates[candidateIndex].Aircraft;
                combat.SelectedTrackId = candidateIndex + 1;
                combat.RadarTrackState = RadarTrackState.Tracked;
                combatStates[playerIndex] = combat;
                RefreshCombatStates();
                return CommandResult.Success("Target selected.");
            }
            return CommandResult.Rejected(2031, "No hostile target is available.");
        }

        public CommandResult CyclePlayerWeapon()
        {
            int playerIndex = FindPlayerActorIndex();
            if (playerIndex < 0 || !flightService.TryGetLatest(actorStates[playerIndex].Aircraft, out AircraftSnapshot snapshot))
                return CommandResult.Rejected(2040, "Player aircraft state is unavailable.");
            int current = combatStates[playerIndex].SelectedStationIndex;
            for (int offset = 1; offset <= StoresState.StationCapacity; offset++)
            {
                int stationIndex = (current + offset + StoresState.StationCapacity) % StoresState.StationCapacity;
                StoreStationState station = snapshot.Systems.Stores.Stations[stationIndex];
                if (station.Quantity <= 0 || !IsStoreSuitableForRange(station.StoreType, combatStates[playerIndex].TargetRangeM)) continue;
                AircraftCombatState combat = combatStates[playerIndex];
                combat.SelectedStationIndex = stationIndex;
                combat.SelectedStoreType = station.StoreType;
                combat.SelectedStoreQuantity = station.Quantity;
                combatStates[playerIndex] = combat;
                return CommandResult.Success("Weapon selected.");
            }
            return CommandResult.Rejected(2041, "No weapon is available.");
        }

        public CommandResult SelectPlayerStation(int stationIndex)
        {
            int playerIndex = FindPlayerActorIndex();
            if(stationIndex < 0 || stationIndex >= StoresState.StationCapacity)
                return CommandResult.Rejected(2042, "Invalid station index.");
            if(playerIndex < 0 || !flightService.TryGetLatest(actorStates[playerIndex].Aircraft, out AircraftSnapshot snapshot))
                return CommandResult.Rejected(2040, "Player aircraft state is unavailable.");
            StoreStationState station = snapshot.Systems.Stores.Stations[stationIndex];
            AircraftCombatState combat = combatStates[playerIndex];
            combat.SelectedStationIndex = stationIndex;
            combat.SelectedStoreType = station.StoreType;
            combat.SelectedStoreQuantity = Math.Max(0, station.Quantity - pendingStoreReleases[playerIndex, stationIndex]);
            combatStates[playerIndex] = combat;
            Publish();
            return CommandResult.Success("Station selected.");
        }

        public CommandResult CyclePlayerMasterArm()
        {
            int playerIndex = FindPlayerActorIndex();
            if (playerIndex < 0) return CommandResult.Rejected(2050, "Mission has no player actor.");
            AircraftCombatState combat = combatStates[playerIndex];
            combat.MasterArm = combat.MasterArm == MasterArmState.Safe
                ? MasterArmState.Simulate
                : combat.MasterArm == MasterArmState.Simulate
                    ? MasterArmState.Arm
                    : MasterArmState.Safe;
            combatStates[playerIndex] = combat;
            Publish();
            return CommandResult.Success("Master arm changed.");
        }

        public CommandResult ReleasePlayerWeapon()
        {
            int playerIndex = FindPlayerActorIndex();
            return playerIndex < 0
                ? CommandResult.Rejected(2060, "Mission has no player actor.")
                : ReleaseWeapon(playerIndex);
        }

        public bool TryGetCombatState(AircraftId aircraft, out AircraftCombatState combatState)
        {
            int index = IndexOfActor(aircraft);
            if (index >= 0)
            {
                RefreshCombatState(index);
                combatState = combatStates[index];
                return true;
            }
            combatState = default(AircraftCombatState);
            return false;
        }

        public void Tick()
        {
            if (state.Phase != MissionPhase.Running) return;
            UpdateAutomatedControls();
            flightService.Tick();
            ClearPendingStoreReleases();
            tick++;
            state.Tick = tick;
            state.SimulationTimeS += FlightSimulationService.FixedDeltaTimeS;
            state.ElapsedTimeS += FlightSimulationService.FixedDeltaTimeS;
            state.RemainingTimeS = Math.Max(0.0, definition.MaximumDurationS - state.ElapsedTimeS);
            automation.SimulationTimeS = state.SimulationTimeS;
            ResolvePendingEngagements();
            RefreshActorValidity();
            RefreshCombatStates();
            EvaluateObjectives();
            if (state.Phase == MissionPhase.Running && state.ElapsedTimeS >= definition.MaximumDurationS)
                Complete(false, "Mission time limit reached.");
            else if (tick % 20UL == 0UL)
                Publish();
        }

        public bool TryGetLatest(out MissionSnapshot snapshot)
        {
            snapshot = new MissionSnapshot
            {
                Mission = state,
                Actors = actorStates,
                Objectives = objectiveStates,
                Automation = automation
            };
            return definition != null;
        }

        public bool TryGetActorState(AircraftId aircraft, out MissionActorState actorState)
        {
            for (int index = 0; index < actorStates.Length; index++)
            {
                if (actorStates[index].Aircraft == aircraft)
                {
                    actorState = actorStates[index];
                    return true;
                }
            }
            actorState = default(MissionActorState);
            return false;
        }

        public void RegisterSink(IMissionTelemetrySink sink)
        {
            if (sink == null) throw new ArgumentNullException(nameof(sink));
            for (int index = 0; index < sinkCount; index++) if (ReferenceEquals(sinks[index], sink)) return;
            if (sinkCount >= sinks.Length) throw new InvalidOperationException("Mission telemetry sink capacity exceeded.");
            sinks[sinkCount++] = sink;
        }

        public void UnregisterSink(IMissionTelemetrySink sink)
        {
            for (int index = 0; index < sinkCount; index++)
            {
                if (!ReferenceEquals(sinks[index], sink)) continue;
                for (int move = index; move < sinkCount - 1; move++) sinks[move] = sinks[move + 1];
                sinks[--sinkCount] = null;
                return;
            }
        }

        private void RefreshActorValidity()
        {
            for (int index = 0; index < actorStates.Length; index++)
            {
                MissionActorState actor = actorStates[index];
                if (!flightService.TryGetLatest(actor.Aircraft, out AircraftSnapshot snapshot) ||
                    !IsFinite(snapshot.Fast.EcefPositionXM) ||
                    !IsFinite(snapshot.Fast.TrueAirspeedMps))
                {
                    actor.Status = MissionActorStatus.Failed;
                    actor.IsValid = false;
                    actorStates[index] = actor;
                    if (definition.Actors[index].IsPlayer)
                    {
                        Complete(false, $"Player actor '{actor.Aircraft.Value}' produced invalid state.");
                        return;
                    }
                }
            }
        }

        private void UpdateAutomatedControls()
        {
            for (int index = 0; index < actorStates.Length; index++)
            {
                MissionActorState actorState = actorStates[index];
                if (actorState.Status != MissionActorStatus.Active ||
                    actorState.ControlAuthority == ControlAuthority.ManualPilot)
                    continue;
                if (!flightService.TryGetLatest(actorState.Aircraft, out AircraftSnapshot own))
                    continue;

                if (actorState.AiMode == MissionAiMode.Evade && !HasIncomingEngagement(index))
                {
                    actorState.AiMode = FindNearestOpponentIndex(index, in own.Fast) >= 0
                        ? MissionAiMode.Engage
                        : definition.Actors[index].InitialAiMode;
                }

                if (actorState.ControlAuthority == ControlAuthority.FormationAI)
                {
                    int formationOpponent = FindNearestOpponentIndex(index, in own.Fast);
                    if (formationOpponent < 0 ||
                        !flightService.TryGetLatest(actorStates[formationOpponent].Aircraft, out AircraftSnapshot formationTarget) ||
                        DistanceSquared(in own.Fast, in formationTarget.Fast) > 80000.0 * 80000.0)
                        continue;

                    FormationCommand leaveFormation = new FormationCommand
                    {
                        Aircraft = actorState.Aircraft,
                        Type = FormationCommandType.Leave
                    };
                    flightService.Submit(in leaveFormation);
                    actorState.ControlAuthority = ControlAuthority.MissionAI;
                    actorState.AiMode = MissionAiMode.Intercept;
                    flightService.SetControlAuthority(actorState.Aircraft, ControlAuthority.MissionAI);
                    actorStates[index] = actorState;
                    EmitMissionEvent(
                        MissionEventType.ControlAuthorityChanged,
                        actorState.Aircraft,
                        actorStates[formationOpponent].Aircraft,
                        0,
                        "Wingman broke formation to intercept a threat.");
                }

                if (definition.Actors[index].Role != AircraftRole.Escort &&
                    (actorState.AiMode == MissionAiMode.Patrol ||
                     actorState.AiMode == MissionAiMode.Navigate ||
                     actorState.AiMode == MissionAiMode.Escort))
                {
                    int opponentIndex = FindNearestOpponentIndex(index, in own.Fast);
                    if (opponentIndex >= 0 &&
                        flightService.TryGetLatest(actorStates[opponentIndex].Aircraft, out AircraftSnapshot opponent) &&
                        DistanceSquared(in own.Fast, in opponent.Fast) <= 120000.0 * 120000.0)
                    {
                        actorState.AiMode = MissionAiMode.Intercept;
                    }
                }

                MissionGuidanceTarget target = BuildGuidanceTarget(index, in own, ref actorState);
                MissionControlCommand command = MissionAutopilot.Compute(
                    in own.Fast,
                    in own.Systems,
                    actorState.AiMode,
                    in target);
                flightService.SetMissionControlInput(
                    actorState.Aircraft,
                    in command.Input,
                    actorState.ControlAuthority);
                if (command.HasLandingGearCommand)
                {
                    LandingGearCommand gear = new LandingGearCommand
                    {
                        Aircraft = actorState.Aircraft,
                        IsDown = command.LandingGearDown
                    };
                    flightService.Submit(in gear);
                }
                actorState.AiMode = command.NextMode;
                actorStates[index] = actorState;
                if (command.RequestTargetSelection && string.IsNullOrEmpty(combatStates[index].SelectedTarget.Value))
                    SelectNearestTarget(index);
                if (command.RequestWeaponRelease && state.ElapsedTimeS - lastEngagementTimeS[index] >= 15.0)
                {
                    SelectNearestTarget(index);
                    SelectFirstAvailableWeapon(index);
                    lastEngagementTimeS[index] = state.ElapsedTimeS;
                    ReleaseWeapon(index);
                }
            }
        }

        private MissionGuidanceTarget BuildGuidanceTarget(
            int actorIndex,
            in AircraftSnapshot own,
            ref MissionActorState actorState)
        {
            MissionActorDefinition actor = definition.Actors[actorIndex];
            if (actorState.AiMode == MissionAiMode.Intercept || actorState.AiMode == MissionAiMode.Engage)
            {
                int targetIndex = FindNearestOpponentIndex(actorIndex, in own.Fast);
                if (targetIndex >= 0 && flightService.TryGetLatest(actorStates[targetIndex].Aircraft, out AircraftSnapshot target))
                {
                    actorState.SelectedTarget = target.Aircraft;
                    return CreatePositionTarget(in own.Fast, in target.Fast, Math.Max(170.0, target.Fast.TrueAirspeedMps));
                }
            }

            MissionWaypointDefinition[] route = actor.Route ?? Array.Empty<MissionWaypointDefinition>();
            if (route.Length > 0 && actorState.AiMode != MissionAiMode.Evade)
            {
                int waypointIndex = Math.Min(actorState.CurrentWaypointIndex, route.Length - 1);
                MissionWaypointDefinition waypoint = route[waypointIndex];
                double distanceM = GreatCircleDistanceM(
                    own.Fast.LatitudeRad,
                    own.Fast.LongitudeRad,
                    waypoint.LatitudeRad,
                    waypoint.LongitudeRad);
                if (distanceM <= Math.Max(100.0, waypoint.AcceptanceRadiusM) && waypointIndex < route.Length - 1)
                {
                    actorState.CurrentWaypointIndex++;
                    waypoint = route[actorState.CurrentWaypointIndex];
                    distanceM = GreatCircleDistanceM(
                        own.Fast.LatitudeRad,
                        own.Fast.LongitudeRad,
                        waypoint.LatitudeRad,
                        waypoint.LongitudeRad);
                }
                return new MissionGuidanceTarget
                {
                    PositionValid = true,
                    LongitudeRad = waypoint.LongitudeRad,
                    LatitudeRad = waypoint.LatitudeRad,
                    DesiredAltitudeM = waypoint.EllipsoidHeightM,
                    DesiredTrueAirspeedMps = waypoint.TargetTrueAirspeedMps,
                    DistanceM = distanceM
                };
            }

            if (actorState.AiMode == MissionAiMode.Evade)
            {
                double turnDirection = ((actorIndex + state.Seed) & 1) == 0 ? 1.0 : -1.0;
                return MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                    own.Fast.HeadingRad + turnDirection * Math.PI * 0.5,
                    own.Fast.EllipsoidHeightM + 400.0,
                    250.0);
            }

            if (actorState.AiMode == MissionAiMode.ReturnToBase ||
                actorState.AiMode == MissionAiMode.Approach ||
                actorState.AiMode == MissionAiMode.Land)
            {
                double forwardEast = Math.Sin(KtexRunwayHeadingRad);
                double forwardNorth = Math.Cos(KtexRunwayHeadingRad);
                double thresholdOffsetM = -KtexRunwayHalfLengthM;
                double touchdownOffsetM = thresholdOffsetM + 300.0;
                double approachFixOffsetM = thresholdOffsetM - 12000.0;
                double eastM = NormalizeSignedAngle(own.Fast.LongitudeRad - KtexLongitudeRad) *
                               EarthRadiusM * Math.Cos(KtexLatitudeRad);
                double northM = (own.Fast.LatitudeRad - KtexLatitudeRad) * EarthRadiusM;
                double alongRunwayM = eastM * forwardEast + northM * forwardNorth;
                bool returnToBase = actorState.AiMode == MissionAiMode.ReturnToBase;
                double targetOffsetM = returnToBase
                    ? approachFixOffsetM
                    : actorState.AiMode == MissionAiMode.Approach
                        ? touchdownOffsetM
                        : alongRunwayM + 3000.0;

                OffsetFromKtex(
                    forwardEast * targetOffsetM,
                    forwardNorth * targetOffsetM,
                    out double targetLongitudeRad,
                    out double targetLatitudeRad);
                double targetDistanceM;
                if (returnToBase)
                {
                    targetDistanceM = GreatCircleDistanceM(
                        own.Fast.LatitudeRad,
                        own.Fast.LongitudeRad,
                        targetLatitudeRad,
                        targetLongitudeRad);
                }
                else
                {
                    OffsetFromKtex(
                        forwardEast * touchdownOffsetM,
                        forwardNorth * touchdownOffsetM,
                        out double touchdownLongitudeRad,
                        out double touchdownLatitudeRad);
                    targetDistanceM = GreatCircleDistanceM(
                        own.Fast.LatitudeRad,
                        own.Fast.LongitudeRad,
                        touchdownLatitudeRad,
                        touchdownLongitudeRad);
                }

                double altitudeM = 3600.0;
                if (!returnToBase)
                {
                    double distanceBeforeTouchdownM = Math.Max(0.0, touchdownOffsetM - alongRunwayM);
                    double glidePathLeadM = Math.Min(20.0, distanceBeforeTouchdownM * 0.005);
                    altitudeM = KtexRunwayHeightM + 1.7 +
                                distanceBeforeTouchdownM * 0.047 - glidePathLeadM;
                    if (actorState.AiMode == MissionAiMode.Land &&
                        !own.Systems.LandingGear.WeightOnWheels &&
                        distanceBeforeTouchdownM < 200.0)
                    {
                        altitudeM = KtexRunwayHeightM - 10.0;
                    }
                }
                return new MissionGuidanceTarget
                {
                    PositionValid = true,
                    LongitudeRad = targetLongitudeRad,
                    LatitudeRad = targetLatitudeRad,
                    DesiredAltitudeM = altitudeM,
                    DesiredTrueAirspeedMps = actorState.AiMode == MissionAiMode.Land
                        ? 80.0
                        : actorState.AiMode == MissionAiMode.Approach ? 95.0 : 155.0,
                    DistanceM = targetDistanceM
                };
            }

            return MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                actor.InitialCondition.HeadingRad,
                Math.Max(actor.InitialCondition.EllipsoidHeightM, 4300.0),
                Math.Max(actor.InitialCondition.TrueAirspeedMps, 170.0));
        }

        private int FindNearestOpponentIndex(int actorIndex, in AircraftFastState own)
        {
            int nearestIndex = -1;
            double nearestDistanceSquared = double.MaxValue;
            AircraftSide ownSide = actorStates[actorIndex].Side;
            for (int index = 0; index < actorStates.Length; index++)
            {
                if (index == actorIndex || actorStates[index].Side == ownSide || actorStates[index].Status != MissionActorStatus.Active)
                    continue;
                if (!flightService.TryGetLatest(actorStates[index].Aircraft, out AircraftSnapshot candidate)) continue;
                double dx = candidate.Fast.EcefPositionXM - own.EcefPositionXM;
                double dy = candidate.Fast.EcefPositionYM - own.EcefPositionYM;
                double dz = candidate.Fast.EcefPositionZM - own.EcefPositionZM;
                double distanceSquared = dx * dx + dy * dy + dz * dz;
                if (ownSide == AircraftSide.Hostile && actorStates[index].Role == AircraftRole.Escort)
                    distanceSquared += 120000.0 * 120000.0;
                if (ownSide == AircraftSide.Hostile && actorStates[index].Role == AircraftRole.Player)
                    distanceSquared += 25000.0 * 25000.0;
                if (distanceSquared >= nearestDistanceSquared) continue;
                nearestDistanceSquared = distanceSquared;
                nearestIndex = index;
            }
            return nearestIndex;
        }

        private static MissionGuidanceTarget CreatePositionTarget(
            in AircraftFastState own,
            in AircraftFastState target,
            double speedMps)
        {
            double dx = target.EcefPositionXM - own.EcefPositionXM;
            double dy = target.EcefPositionYM - own.EcefPositionYM;
            double dz = target.EcefPositionZM - own.EcefPositionZM;
            return new MissionGuidanceTarget
            {
                PositionValid = true,
                LongitudeRad = target.LongitudeRad,
                LatitudeRad = target.LatitudeRad,
                DesiredAltitudeM = target.EllipsoidHeightM,
                DesiredTrueAirspeedMps = speedMps,
                DistanceM = Math.Sqrt(dx * dx + dy * dy + dz * dz)
            };
        }

        private static double DistanceSquared(
            in AircraftFastState first,
            in AircraftFastState second)
        {
            double dx = second.EcefPositionXM - first.EcefPositionXM;
            double dy = second.EcefPositionYM - first.EcefPositionYM;
            double dz = second.EcefPositionZM - first.EcefPositionZM;
            return dx * dx + dy * dy + dz * dz;
        }

        private int FindPlayerActorIndex()
        {
            for (int index = 0; index < definition.Actors.Length; index++)
                if (definition.Actors[index].IsPlayer) return index;
            return -1;
        }

        private static double GreatCircleDistanceM(double lat1, double lon1, double lat2, double lon2)
        {
            double sinLat = Math.Sin((lat2 - lat1) * 0.5);
            double sinLon = Math.Sin((lon2 - lon1) * 0.5);
            double a = sinLat * sinLat + Math.Cos(lat1) * Math.Cos(lat2) * sinLon * sinLon;
            return 6371008.8 * 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(Math.Max(0.0, 1.0 - a)));
        }

        private static void OffsetFromKtex(
            double eastM,
            double northM,
            out double longitudeRad,
            out double latitudeRad)
        {
            latitudeRad = KtexLatitudeRad + northM / EarthRadiusM;
            longitudeRad = KtexLongitudeRad + eastM / (EarthRadiusM * Math.Cos(KtexLatitudeRad));
        }

        private void Complete(bool succeeded, string reason)
        {
            if (state.Phase != MissionPhase.Running) return;
            state.Phase = succeeded ? MissionPhase.Succeeded : MissionPhase.Failed;
            state.Result = succeeded ? MissionResult.Success : MissionResult.Failure;
            state.CompletionReason = reason;
            automation.Status = succeeded ? AutomationRunStatus.Completed : AutomationRunStatus.Failed;
            automation.CompletionReason = reason;
            EmitMissionEvent(
                succeeded ? MissionEventType.Completed : MissionEventType.Failed,
                default(AircraftId),
                default(AircraftId),
                succeeded ? 0 : 1,
                reason);
            Publish();
        }

        private void Publish()
        {
            for (int sinkIndex = 0; sinkIndex < sinkCount; sinkIndex++)
            {
                IMissionTelemetrySink sink = sinks[sinkIndex];
                MissionState missionState = state;
                sink.OnMissionState(in missionState);
                for (int actorIndex = 0; actorIndex < actorStates.Length; actorIndex++)
                {
                    MissionActorState actor = actorStates[actorIndex];
                    sink.OnMissionActorState(in actor);
                    AircraftCombatState combat = combatStates[actorIndex];
                    sink.OnAircraftCombatState(in combat);
                }
                for (int objectiveIndex = 0; objectiveIndex < objectiveStates.Length; objectiveIndex++)
                {
                    MissionObjectiveState objective = objectiveStates[objectiveIndex];
                    sink.OnMissionObjectiveState(in objective);
                }
                AutomationRunState run = automation;
                sink.OnAutomationRunState(in run);
            }
        }

        private void EmitMissionEvent(
            MissionEventType type,
            AircraftId source,
            AircraftId target,
            int code,
            string message)
        {
            MissionEvent missionEvent = new MissionEvent
            {
                RunId = state.RunId,
                MissionId = state.MissionId,
                SimulationTimeS = state.SimulationTimeS,
                Type = type,
                Source = source,
                Target = target,
                Code = code,
                Message = message
            };
            for (int index = 0; index < sinkCount; index++) sinks[index].OnMissionEvent(in missionEvent);
        }

        private CommandResult ReleaseWeapon(int shooterIndex)
        {
            AircraftCombatState combat = combatStates[shooterIndex];
            if (combat.MasterArm == MasterArmState.Safe)
                return CommandResult.Rejected(2061, "Master arm is SAFE.");
            int targetIndex = IndexOfActor(combat.SelectedTarget);
            if (targetIndex < 0 || actorStates[targetIndex].Status != MissionActorStatus.Active)
                return CommandResult.Rejected(2062, "Selected target is unavailable.");
            if (HasPendingEngagement(shooterIndex, targetIndex))
                return CommandResult.Rejected(2065, "A weapon is already in flight to the selected target.");
            if (combat.SelectedStationIndex < 0)
            {
                if (!SelectFirstAvailableWeapon(shooterIndex))
                    return CommandResult.Rejected(2063, "No ready weapon is selected.");
                combat = combatStates[shooterIndex];
            }

            RefreshCombatState(shooterIndex);
            combat = combatStates[shooterIndex];
            if (combat.SelectedStationIndex < 0 ||
                combat.SelectedStoreQuantity <= 0 ||
                string.IsNullOrWhiteSpace(combat.SelectedStoreType))
            {
                if (!SelectFirstAvailableWeapon(shooterIndex))
                    return CommandResult.Rejected(2063, "No ready weapon is selected.");
                RefreshCombatState(shooterIndex);
                combat = combatStates[shooterIndex];
                if (combat.SelectedStoreQuantity <= 0)
                    return CommandResult.Rejected(2063, "No ready weapon is selected.");
            }
            WeaponEngagementRequest request = new WeaponEngagementRequest
            {
                Shooter = actorStates[shooterIndex].Aircraft,
                Target = actorStates[targetIndex].Aircraft,
                WeaponType = combat.SelectedStoreType,
                StationIndex = combat.SelectedStationIndex,
                RangeM = combat.TargetRangeM,
                ClosureRateMps = combat.ClosureRateMps,
                LockQualityNormalized = combat.LockQualityNormalized,
                AspectAngleRad = combat.TargetAspectAngleRad,
                ShooterSkillNormalized = definition.Actors[shooterIndex].AiSkillNormalized,
                TargetEvasionNormalized = definition.Actors[targetIndex].AiSkillNormalized
            };
            WeaponEngagementDecision decision = DeterministicEngagementResolver.Resolve(
                in request,
                state.Seed,
                engagementSequence);
            if (!decision.Accepted)
                return CommandResult.Rejected(2064, decision.Reason);

            if (combat.MasterArm == MasterArmState.Arm)
            {
                ReleaseStoreCommand release = new ReleaseStoreCommand
                {
                    Aircraft = request.Shooter,
                    StationIndex = request.StationIndex,
                    Quantity = 1,
                    EmergencyJettison = false
                };
                CommandResult submitted = flightService.Submit(in release);
                if (!submitted.Accepted) return submitted;
                pendingStoreReleases[shooterIndex, request.StationIndex]++;
            }

            int sequence = engagementSequence++;
            lastEngagementTimeS[shooterIndex] = state.ElapsedTimeS;
            WeaponEngagementEvent engagement = new WeaponEngagementEvent
            {
                RunId = state.RunId,
                EngagementSequence = sequence,
                SimulationTimeS = state.SimulationTimeS,
                Shooter = request.Shooter,
                Target = request.Target,
                WeaponType = request.WeaponType,
                StationIndex = request.StationIndex,
                LaunchRangeM = request.RangeM,
                ClosureRateMps = request.ClosureRateMps,
                LockQualityNormalized = request.LockQualityNormalized,
                TimeToImpactS = decision.TimeToImpactS,
                Outcome = decision.Outcome,
                Reason = decision.Reason
            };
            for (int index = 0; index < sinkCount; index++) sinks[index].OnWeaponEngagementEvent(in engagement);
            EmitMissionEvent(MissionEventType.Engagement, request.Shooter, request.Target, sequence, decision.Reason);

            if (combat.MasterArm == MasterArmState.Arm)
            {
                ScheduleEngagementImpact(
                    sequence,
                    shooterIndex,
                    targetIndex,
                    decision.TimeToImpactS,
                    decision.Outcome);
            }
            MissionActorState shooter = actorStates[shooterIndex];
            shooter.IsEngaged = true;
            actorStates[shooterIndex] = shooter;
            return CommandResult.Success(decision.Reason);
        }

        private void ScheduleEngagementImpact(
            int sequence,
            int shooterIndex,
            int targetIndex,
            double timeToImpactS,
            WeaponEngagementOutcome outcome)
        {
            for (int index = 0; index < pendingEngagements.Length; index++)
            {
                if (pendingEngagements[index].IsActive) continue;
                pendingEngagements[index] = new PendingEngagement
                {
                    IsActive = true,
                    Sequence = sequence,
                    ShooterIndex = shooterIndex,
                    TargetIndex = targetIndex,
                    ImpactTimeS = state.SimulationTimeS + Math.Max(0.1, timeToImpactS),
                    Outcome = outcome
                };
                if (actorStates[targetIndex].ControlAuthority != ControlAuthority.ManualPilot)
                {
                    MissionActorState target = actorStates[targetIndex];
                    target.AiMode = MissionAiMode.Evade;
                    actorStates[targetIndex] = target;
                }
                return;
            }
            Complete(false, "Pending engagement capacity was exceeded.");
        }

        private void ResolvePendingEngagements()
        {
            for (int index = 0; index < pendingEngagements.Length; index++)
            {
                PendingEngagement pending = pendingEngagements[index];
                if (!pending.IsActive || pending.ImpactTimeS > state.SimulationTimeS) continue;
                pendingEngagements[index] = default(PendingEngagement);
                if (pending.TargetIndex < 0 || pending.TargetIndex >= actorStates.Length ||
                    actorStates[pending.TargetIndex].Status != MissionActorStatus.Active) continue;
                if (pending.Outcome != WeaponEngagementOutcome.Hit)
                {
                    EmitMissionEvent(
                        MissionEventType.Engagement,
                        actorStates[pending.ShooterIndex].Aircraft,
                        actorStates[pending.TargetIndex].Aircraft,
                        pending.Sequence,
                        "Weapon reached closest approach and missed the target.");
                    continue;
                }
                MissionActorState target = actorStates[pending.TargetIndex];
                target.Status = MissionActorStatus.Neutralized;
                target.IsEngaged = true;
                actorStates[pending.TargetIndex] = target;
                EmitMissionEvent(
                    MissionEventType.WeaponImpact,
                    actorStates[pending.ShooterIndex].Aircraft,
                    target.Aircraft,
                    pending.Sequence,
                    "Weapon impact adjudicated; target neutralized.");
                if (definition.Actors[pending.TargetIndex].IsPlayer)
                    Complete(false, "Player aircraft was neutralized by a weapon engagement.");
                else if (IsProtectedAircraft(target.Aircraft))
                    Complete(false, $"Protected aircraft '{target.Aircraft.Value}' was neutralized.");
            }
        }

        private bool HasPendingEngagement(int shooterIndex, int targetIndex)
        {
            for (int index = 0; index < pendingEngagements.Length; index++)
                if (pendingEngagements[index].IsActive &&
                    pendingEngagements[index].ShooterIndex == shooterIndex &&
                    pendingEngagements[index].TargetIndex == targetIndex) return true;
            return false;
        }

        private bool SelectNearestTarget(int shooterIndex)
        {
            if (!flightService.TryGetLatest(actorStates[shooterIndex].Aircraft, out AircraftSnapshot own)) return false;
            int targetIndex = FindNearestOpponentIndex(shooterIndex, in own.Fast);
            if (targetIndex < 0) return false;
            AircraftCombatState combat = combatStates[shooterIndex];
            combat.SelectedTarget = actorStates[targetIndex].Aircraft;
            combat.SelectedTrackId = targetIndex + 1;
            combat.RadarTrackState = RadarTrackState.Tracked;
            combatStates[shooterIndex] = combat;
            MissionActorState actor = actorStates[shooterIndex];
            actor.SelectedTarget = combat.SelectedTarget;
            actorStates[shooterIndex] = actor;
            RefreshCombatState(shooterIndex);
            return true;
        }

        private bool SelectFirstAvailableWeapon(int actorIndex)
        {
            if (!flightService.TryGetLatest(actorStates[actorIndex].Aircraft, out AircraftSnapshot snapshot)) return false;
            int start = combatStates[actorIndex].SelectedStationIndex;
            for (int offset = 1; offset <= StoresState.StationCapacity; offset++)
            {
                int stationIndex = (start + offset + StoresState.StationCapacity) % StoresState.StationCapacity;
                StoreStationState station = snapshot.Systems.Stores.Stations[stationIndex];
                int availableQuantity = station.Quantity - pendingStoreReleases[actorIndex, stationIndex];
                if (availableQuantity <= 0 || !station.IsReady ||
                    !IsStoreSuitableForRange(station.StoreType, combatStates[actorIndex].TargetRangeM)) continue;
                AircraftCombatState combat = combatStates[actorIndex];
                combat.SelectedStationIndex = stationIndex;
                combat.SelectedStoreType = station.StoreType;
                combat.SelectedStoreQuantity = availableQuantity;
                combatStates[actorIndex] = combat;
                return true;
            }
            return false;
        }

        private static bool IsStoreSuitableForRange(string storeType, double rangeM)
        {
            return storeType == null ||
                   storeType.IndexOf("AIM-9", StringComparison.OrdinalIgnoreCase) < 0 ||
                   rangeM <= 22000.0;
        }

        private void RefreshCombatStates()
        {
            for (int index = 0; index < combatStates.Length; index++) RefreshCombatState(index);
        }

        private void RefreshCombatState(int actorIndex)
        {
            AircraftCombatState combat = combatStates[actorIndex];
            if (!flightService.TryGetLatest(actorStates[actorIndex].Aircraft, out AircraftSnapshot own))
            {
                combat.IsValid = false;
                combatStates[actorIndex] = combat;
                return;
            }
            combat.SimulationTimeS = own.Fast.SimulationTimeS;
            combat.MissileLaunchWarning = HasIncomingEngagement(actorIndex);
            if (combat.SelectedStationIndex >= 0)
            {
                StoreStationState station = own.Systems.Stores.Stations[combat.SelectedStationIndex];
                combat.SelectedStoreType = station.StoreType;
                combat.SelectedStoreQuantity = Math.Max(
                    0,
                    station.Quantity - pendingStoreReleases[actorIndex, combat.SelectedStationIndex]);
            }
            int targetIndex = IndexOfActor(combat.SelectedTarget);
            if (targetIndex >= 0 && flightService.TryGetLatest(actorStates[targetIndex].Aircraft, out AircraftSnapshot target))
            {
                double dx = target.Fast.EcefPositionXM - own.Fast.EcefPositionXM;
                double dy = target.Fast.EcefPositionYM - own.Fast.EcefPositionYM;
                double dz = target.Fast.EcefPositionZM - own.Fast.EcefPositionZM;
                double rangeM = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                double rvx = target.Fast.EcefVelocityXMps - own.Fast.EcefVelocityXMps;
                double rvy = target.Fast.EcefVelocityYMps - own.Fast.EcefVelocityYMps;
                double rvz = target.Fast.EcefVelocityZMps - own.Fast.EcefVelocityZMps;
                double closureMps = rangeM > 1.0 ? -(rvx * dx + rvy * dy + rvz * dz) / rangeM : 0.0;
                double bearing = InitialBearingRad(
                    own.Fast.LatitudeRad,
                    own.Fast.LongitudeRad,
                    target.Fast.LatitudeRad,
                    target.Fast.LongitudeRad);
                combat.TargetRangeM = rangeM;
                combat.ClosureRateMps = closureMps;
                combat.TargetBearingRad = NormalizeSignedAngle(bearing - own.Fast.HeadingRad);
                combat.TargetElevationRad = Math.Atan2(
                    target.Fast.EllipsoidHeightM - own.Fast.EllipsoidHeightM,
                    Math.Max(1.0, rangeM));
                combat.TargetAspectAngleRad = NormalizeSignedAngle(target.Fast.HeadingRad - bearing - Math.PI);
                combat.LockQualityNormalized = Clamp01(1.1 - rangeM / 120000.0);
                combat.RadarTrackState = combat.LockQualityNormalized >= 0.5
                    ? RadarTrackState.Locked
                    : RadarTrackState.Tracked;
                combat.InLaunchZone = rangeM >= 1000.0 && rangeM <= 50000.0;
                combat.ShootCue = combat.InLaunchZone && combat.LockQualityNormalized >= 0.5 && combat.MasterArm != MasterArmState.Safe;
            }
            else
            {
                combat.SelectedTarget = default(AircraftId);
                combat.SelectedTrackId = -1;
                combat.RadarTrackState = RadarTrackState.None;
                combat.TargetRangeM = 0.0;
                combat.LockQualityNormalized = 0.0;
                combat.InLaunchZone = false;
                combat.ShootCue = false;
            }
            combat.IsValid = true;
            combatStates[actorIndex] = combat;
        }

        private bool HasIncomingEngagement(int actorIndex)
        {
            for (int index = 0; index < pendingEngagements.Length; index++)
                if (pendingEngagements[index].IsActive &&
                    pendingEngagements[index].TargetIndex == actorIndex) return true;
            return false;
        }

        private void EvaluateObjectives()
        {
            if (state.Phase != MissionPhase.Running) return;
            int active = 0;
            int succeeded = 0;
            int failed = 0;
            for (int index = 0; index < objectiveStates.Length; index++)
            {
                MissionObjectiveState objectiveState = objectiveStates[index];
                MissionObjectiveDefinition objective = definition.Objectives[index];
                if (objectiveState.Status == MissionObjectiveStatus.Active)
                {
                    EvaluateObjective(ref objectiveState, in objective);
                    if (objectiveState.Status == MissionObjectiveStatus.Active &&
                        objective.TimeLimitS > 0.0 && state.ElapsedTimeS >= objective.TimeLimitS)
                    {
                        objectiveState.Status = MissionObjectiveStatus.Failed;
                        objectiveState.FailureReason = "Objective time limit reached.";
                    }
                    objectiveStates[index] = objectiveState;
                }
                if (objectiveState.Status == MissionObjectiveStatus.Active) active++;
                else if (objectiveState.Status == MissionObjectiveStatus.Succeeded) succeeded++;
                else if (objectiveState.Status == MissionObjectiveStatus.Failed) failed++;
            }
            state.ActiveObjectiveCount = active;
            state.SucceededObjectiveCount = succeeded;
            state.FailedObjectiveCount = failed;

            bool requiredFailed = false;
            bool allRequiredSucceeded = true;
            for (int index = 0; index < objectiveStates.Length; index++)
            {
                if (!definition.Objectives[index].IsRequired) continue;
                requiredFailed |= objectiveStates[index].Status == MissionObjectiveStatus.Failed;
                allRequiredSucceeded &= objectiveStates[index].Status == MissionObjectiveStatus.Succeeded;
            }
            if (requiredFailed)
            {
                for (int index = 0; index < objectiveStates.Length; index++)
                {
                    if (!definition.Objectives[index].IsRequired ||
                        objectiveStates[index].Status != MissionObjectiveStatus.Failed) continue;
                    Complete(
                        false,
                        $"Objective '{objectiveStates[index].ObjectiveId}' failed: {objectiveStates[index].FailureReason}");
                    break;
                }
            }
            else if (objectiveStates.Length > 0 && allRequiredSucceeded) Complete(true, "All required objectives succeeded.");
        }

        private void EvaluateObjective(
            ref MissionObjectiveState objectiveState,
            in MissionObjectiveDefinition objective)
        {
            int subjectIndex = IndexOfActor(new AircraftId(objective.SubjectAircraftId));
            switch (objective.Type)
            {
                case MissionObjectiveType.Takeoff:
                    if (subjectIndex >= 0 && flightService.TryGetLatest(actorStates[subjectIndex].Aircraft, out AircraftSnapshot takeoff) &&
                        !takeoff.Systems.LandingGear.WeightOnWheels && takeoff.Fast.EllipsoidHeightM > 2785.0)
                        Succeed(ref objectiveState, 1);
                    break;
                case MissionObjectiveType.NeutralizeHostiles:
                    int neutralized = 0;
                    for (int index = 0; index < actorStates.Length; index++)
                        if (actorStates[index].Side == AircraftSide.Hostile &&
                            (actorStates[index].Status == MissionActorStatus.Neutralized || actorStates[index].Status == MissionActorStatus.Failed))
                            neutralized++;
                    objectiveState.CurrentCount = neutralized;
                    objectiveState.ProgressNormalized = Clamp01(neutralized / (double)Math.Max(1, objectiveState.RequiredCount));
                    if (neutralized >= objectiveState.RequiredCount) objectiveState.Status = MissionObjectiveStatus.Succeeded;
                    break;
                case MissionObjectiveType.HoldArea:
                    objectiveState.ProgressNormalized = Clamp01(state.ElapsedTimeS / Math.Min(120.0, Math.Max(1.0, objective.TimeLimitS)));
                    if (objectiveState.ProgressNormalized >= 1.0) objectiveState.Status = MissionObjectiveStatus.Succeeded;
                    break;
                case MissionObjectiveType.ProtectAircraft:
                    int protectedIndex = IndexOfActor(new AircraftId(objective.TargetAircraftId));
                    if (protectedIndex >= 0 &&
                        (actorStates[protectedIndex].Status == MissionActorStatus.Neutralized ||
                         actorStates[protectedIndex].Status == MissionActorStatus.Failed))
                    {
                        objectiveState.Status = MissionObjectiveStatus.Failed;
                        objectiveState.FailureReason = "Protected aircraft was neutralized.";
                    }
                    else if (CountActiveHostiles() == 0 && state.ElapsedTimeS >= 30.0) Succeed(ref objectiveState, 1);
                    break;
                case MissionObjectiveType.ReachWaypoint:
                    if (subjectIndex >= 0)
                    {
                        int count = definition.Actors[subjectIndex].Route?.Length ?? 0;
                        bool reached = HasReachedFinalWaypoint(subjectIndex);
                        objectiveState.RequiredCount = Math.Max(1, count);
                        objectiveState.CurrentCount = reached ? count : Math.Min(actorStates[subjectIndex].CurrentWaypointIndex, count);
                        objectiveState.ProgressNormalized = objectiveState.CurrentCount / (double)objectiveState.RequiredCount;
                        if (reached) Succeed(ref objectiveState, count);
                    }
                    break;
                case MissionObjectiveType.LandAtKtex:
                    if (subjectIndex >= 0 && flightService.TryGetLatest(actorStates[subjectIndex].Aircraft, out AircraftSnapshot landed) &&
                        state.ElapsedTimeS > 20.0 && landed.Systems.LandingGear.WeightOnWheels && landed.Fast.GroundSpeedMps < 25.0)
                    {
                        MissionActorState actor = actorStates[subjectIndex];
                        actor.Status = MissionActorStatus.Landed;
                        actorStates[subjectIndex] = actor;
                        Succeed(ref objectiveState, 1);
                    }
                    break;
                case MissionObjectiveType.Survive:
                    if (subjectIndex >= 0 && actorStates[subjectIndex].Status == MissionActorStatus.Landed) Succeed(ref objectiveState, 1);
                    break;
            }
        }

        private bool HasReachedFinalWaypoint(int actorIndex)
        {
            MissionWaypointDefinition[] route = definition.Actors[actorIndex].Route ?? Array.Empty<MissionWaypointDefinition>();
            if (route.Length == 0 || !flightService.TryGetLatest(actorStates[actorIndex].Aircraft, out AircraftSnapshot snapshot)) return false;
            var actor = actorStates[actorIndex];
            int index = Math.Min(actor.CurrentWaypointIndex, route.Length - 1);
            MissionWaypointDefinition waypoint = route[index];
            double radius = Math.Max(100.0, waypoint.AcceptanceRadiusM);
            bool reached = GreatCircleDistanceM(snapshot.Fast.LatitudeRad, snapshot.Fast.LongitudeRad,
                waypoint.LatitudeRad, waypoint.LongitudeRad) <= radius &&
                Math.Abs(snapshot.Fast.EllipsoidHeightM - waypoint.EllipsoidHeightM) <= radius;
            if (!reached) return false;
            if (index == route.Length - 1) return true;
            // Manual pilots advance through the same ordered route as the autopilot.
            actor.CurrentWaypointIndex = index + 1;
            actorStates[actorIndex] = actor;
            return false;
        }

        private int CountActiveHostiles()
        {
            int count = 0;
            for (int index = 0; index < actorStates.Length; index++)
                if (actorStates[index].Side == AircraftSide.Hostile && actorStates[index].Status == MissionActorStatus.Active) count++;
            return count;
        }

        private bool IsProtectedAircraft(AircraftId aircraft)
        {
            MissionObjectiveDefinition[] objectives = definition.Objectives ?? Array.Empty<MissionObjectiveDefinition>();
            for (int index = 0; index < objectives.Length; index++)
                if (objectives[index].Type == MissionObjectiveType.ProtectAircraft &&
                    new AircraftId(objectives[index].TargetAircraftId) == aircraft) return true;
            return false;
        }

        private int IndexOfActor(AircraftId aircraft)
        {
            for (int index = 0; index < actorStates.Length; index++)
                if (actorStates[index].Aircraft == aircraft) return index;
            return -1;
        }

        private static void Succeed(ref MissionObjectiveState stateToUpdate, int count)
        {
            stateToUpdate.CurrentCount = count;
            stateToUpdate.ProgressNormalized = 1.0;
            stateToUpdate.Status = MissionObjectiveStatus.Succeeded;
        }

        private static double InitialBearingRad(double lat1, double lon1, double lat2, double lon2)
        {
            double deltaLon = lon2 - lon1;
            double angle = Math.Atan2(
                Math.Sin(deltaLon) * Math.Cos(lat2),
                Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(deltaLon));
            double twoPi = Math.PI * 2.0;
            angle %= twoPi;
            return angle < 0.0 ? angle + twoPi : angle;
        }

        private static double NormalizeSignedAngle(double angleRad)
        {
            double twoPi = Math.PI * 2.0;
            double value = (angleRad + Math.PI) % twoPi;
            if (value < 0.0) value += twoPi;
            return value - Math.PI;
        }

        private static double Clamp01(double value) => value < 0.0 ? 0.0 : value > 1.0 ? 1.0 : value;

        private static bool IsFinite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);

        private void ClearPendingStoreReleases()
        {
            for (int actorIndex = 0; actorIndex < pendingStoreReleases.GetLength(0); actorIndex++)
                for (int stationIndex = 0; stationIndex < pendingStoreReleases.GetLength(1); stationIndex++)
                    pendingStoreReleases[actorIndex, stationIndex] = 0;
        }

        private struct PendingEngagement
        {
            public bool IsActive;
            public int Sequence;
            public int ShooterIndex;
            public int TargetIndex;
            public double ImpactTimeS;
            public WeaponEngagementOutcome Outcome;
        }
    }
}
