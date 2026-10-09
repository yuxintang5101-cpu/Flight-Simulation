using System;
using System.IO;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Integration;
using FlightSim.Platform.Missions;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class MissionSystemTests
    {
        [Test]
        public void DefaultCatalogContainsFourValidKtexMissions()
        {
            MissionDefinition[] missions = DefaultMissionCatalog.CreateAll();

            Assert.That(missions.Length, Is.EqualTo(4));
            Assert.That(Array.ConvertAll(missions, mission => mission.MissionId), Is.EquivalentTo(new[]
            {
                "KTEX_SCRAMBLE_01",
                "KTEX_CAP_01",
                "KTEX_ESCORT_01",
                "KTEX_EMERGENCY_RTB_01"
            }));
            foreach (MissionDefinition mission in missions)
            {
                MissionValidationResult validation = MissionDefinitionValidator.Validate(mission);
                Assert.That(validation.IsValid, Is.True, validation.FirstError);
                Assert.That(mission.SchemaVersion, Is.EqualTo(1));
                Assert.That(mission.Actors.Length, Is.InRange(2, 8));
            }
        }

        [Test]
        public void EscortRouteWaypointZonesAccommodateAircraftTurnRadius()
        {
            MissionDefinition escort = DefaultMissionCatalog.Create("KTEX_ESCORT_01");
            MissionActorDefinition protectedAircraft = Array.Find(
                escort.Actors,
                actor => actor.AircraftId == "MAGIC-01");

            foreach (MissionWaypointDefinition waypoint in protectedAircraft.Route)
            {
                double turnRadiusM = waypoint.TargetTrueAirspeedMps * waypoint.TargetTrueAirspeedMps /
                                     (9.80665 * Math.Tan(35.0 * Math.PI / 180.0));
                Assert.That(waypoint.AcceptanceRadiusM, Is.GreaterThanOrEqualTo(turnRadiusM));
            }
        }

        [Test]
        public void DirectorPersistsWaypointAdvanceAfterGuidanceUpdate()
        {
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_ESCORT_01");
            int protectedIndex = Array.FindIndex(mission.Actors, actor => actor.AircraftId == "MAGIC-01");
            MissionActorDefinition protectedAircraft = mission.Actors[protectedIndex];
            protectedAircraft.Route[0].LongitudeRad = protectedAircraft.InitialCondition.LongitudeRad;
            protectedAircraft.Route[0].LatitudeRad = protectedAircraft.InitialCondition.LatitudeRad;
            protectedAircraft.Route[0].EllipsoidHeightM = protectedAircraft.InitialCondition.EllipsoidHeightM;
            mission.Actors[protectedIndex] = protectedAircraft;
            FlightSimulationService service = new FlightSimulationService();
            MissionDirector director = new MissionDirector(service);
            Assert.That(director.Load(mission, 73).Accepted, Is.True);
            Assert.That(director.Start().Accepted, Is.True);

            director.Tick();

            Assert.That(director.TryGetActorState(new AircraftId("MAGIC-01"), out MissionActorState actor), Is.True);
            Assert.That(actor.CurrentWaypointIndex, Is.EqualTo(1));
        }

        [Test]
        public void ValidatorRejectsDuplicateAircraftAndUnknownLeader()
        {
            AircraftInitialCondition airborne = AircraftInitialCondition.CreateAirborneDefault();
            MissionDefinition mission = new MissionDefinition
            {
                SchemaVersion = 1,
                MissionId = "INVALID",
                DisplayName = "Invalid",
                MaximumDurationS = 60.0,
                Actors = new[]
                {
                    MissionActorDefinition.Create("DUPLICATE", true, AircraftSide.Friendly, AircraftRole.Player, in airborne),
                    MissionActorDefinition.Create("DUPLICATE", false, AircraftSide.Friendly, AircraftRole.Wingman, in airborne)
                }
            };
            mission.Actors[1].LeaderAircraftId = "MISSING";

            MissionValidationResult validation = MissionDefinitionValidator.Validate(mission);

            Assert.That(validation.IsValid, Is.False);
            Assert.That(validation.ErrorCount, Is.GreaterThanOrEqualTo(2));
        }

        [Test]
        public void LoadingCapMissionCreatesRosterAndAppliesFuelState()
        {
            FlightSimulationService service = new FlightSimulationService();
            MissionDirector director = new MissionDirector(service);
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_CAP_01");

            CommandResult loaded = director.Load(mission, 1234);
            CommandResult started = director.Start();

            Assert.That(loaded.Accepted, Is.True, loaded.Message);
            Assert.That(started.Accepted, Is.True, started.Message);
            Assert.That(director.LatestState.Phase, Is.EqualTo(MissionPhase.Running));
            Assert.That(director.LatestState.Seed, Is.EqualTo(1234));
            Assert.That(director.ActorCount, Is.EqualTo(mission.Actors.Length));
            Assert.That(service.TryGetLatest(new AircraftId("VIPER-01"), out AircraftSnapshot player), Is.True);
            Assert.That(player.Systems.Fuel.InternalFuelKg, Is.EqualTo(3175.0 * 0.80).Within(0.1));
            Assert.That(player.Fast.EllipsoidHeightM, Is.EqualTo(7000.0).Within(0.1));
        }

        [Test]
        public void EngagementResolutionIsDeterministicForSeedAndRequest()
        {
            WeaponEngagementRequest request = new WeaponEngagementRequest
            {
                Shooter = new AircraftId("VIPER-01"),
                Target = new AircraftId("BANDIT-01"),
                WeaponType = "AIM-120C",
                RangeM = 22000.0,
                ClosureRateMps = 310.0,
                LockQualityNormalized = 0.88,
                AspectAngleRad = 0.25
            };

            WeaponEngagementDecision first = DeterministicEngagementResolver.Resolve(in request, 9981, 4);
            WeaponEngagementDecision second = DeterministicEngagementResolver.Resolve(in request, 9981, 4);

            Assert.That(first.Accepted, Is.True);
            Assert.That(second.Outcome, Is.EqualTo(first.Outcome));
            Assert.That(second.TimeToImpactS, Is.EqualTo(first.TimeToImpactS));
            Assert.That(second.DeterministicScoreNormalized, Is.EqualTo(first.DeterministicScoreNormalized));
        }

        [Test]
        public void SkilledDefenderCanDefeatKnownCloseRangeEngagementSample()
        {
            WeaponEngagementRequest request = new WeaponEngagementRequest
            {
                Shooter = new AircraftId("BANDIT-04"),
                Target = new AircraftId("VIPER-01"),
                WeaponType = "AIM-9X",
                RangeM = 3000.0,
                ClosureRateMps = 600.0,
                LockQualityNormalized = 1.0,
                AspectAngleRad = 0.0,
                ShooterSkillNormalized = 0.45,
                TargetEvasionNormalized = 0.85
            };

            WeaponEngagementDecision decision = DeterministicEngagementResolver.Resolve(in request, 1001, 15);

            Assert.That(decision.Accepted, Is.True);
            Assert.That(decision.Outcome, Is.EqualTo(WeaponEngagementOutcome.Miss));
            Assert.That(decision.ProbabilityOfHitNormalized, Is.LessThan(decision.DeterministicScoreNormalized));
        }

        [Test]
        public void AutopilotCommandsFullThrottleAndRotationDuringTakeoff()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("VIPER-01"));
            fast.HeadingRad = 285.0 * Math.PI / 180.0;
            fast.CalibratedAirspeedMps = 82.0;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            systems.Propulsion.EngineRunning = true;
            systems.LandingGear.WeightOnWheels = true;
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                fast.HeadingRad,
                4300.0,
                180.0);

            MissionControlCommand command = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.Takeoff,
                in target);

            Assert.That(command.Input.ThrottleNormalized, Is.EqualTo(1.0));
            Assert.That(command.Input.WheelBrakeNormalized, Is.EqualTo(0.0));
            Assert.That(command.Input.PitchNormalized, Is.GreaterThan(0.2));
            Assert.That(command.HasLandingGearCommand, Is.True);
            Assert.That(command.LandingGearDown, Is.True);
        }

        [Test]
        public void TacticalAutopilotPullsUpAndLevelsWingsInsideTerrainRecoveryEnvelope()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("VIPER-02"));
            fast.HeadingRad = 285.0 * Math.PI / 180.0;
            fast.RollRad = 35.0 * Math.PI / 180.0;
            fast.TrueAirspeedMps = 205.0;
            fast.GroundSpeedMps = 200.0;
            fast.EllipsoidHeightM = 3465.0;
            fast.AboveGroundLevelAltitudeM = 700.0;
            fast.ClimbRateMps = -80.0;
            fast.TerrainSampleValid = true;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                fast.HeadingRad,
                6500.0,
                220.0);

            MissionControlCommand command = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.Intercept,
                in target);

            Assert.That(command.Input.PitchNormalized, Is.GreaterThanOrEqualTo(0.60));
            Assert.That(command.Input.RollNormalized, Is.LessThan(0.0));
            Assert.That(command.RequestWeaponRelease, Is.False);
        }

        [Test]
        public void ReturnToBaseAutopilotClimbsWhenTerrainClearanceIsLowWithoutAHighSinkRate()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("VIPER-01"));
            fast.HeadingRad = 285.0 * Math.PI / 180.0;
            fast.TrueAirspeedMps = 175.0;
            fast.GroundSpeedMps = 170.0;
            fast.EllipsoidHeightM = 3950.0;
            fast.AboveGroundLevelAltitudeM = 310.0;
            fast.ClimbRateMps = 0.0;
            fast.TerrainSampleValid = true;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                fast.HeadingRad,
                3600.0,
                155.0);
            target.DistanceM = 7000.0;

            MissionControlCommand command = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.ReturnToBase,
                in target);

            Assert.That(command.Input.PitchNormalized, Is.GreaterThanOrEqualTo(0.35));
            Assert.That(command.Input.ThrottleNormalized, Is.EqualTo(1.0));
            Assert.That(command.NextMode, Is.EqualTo(MissionAiMode.ReturnToBase));
        }

        [Test]
        public void ReturnToBaseDoesNotEnterApproachUntilNearTheApproachFix()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("VIPER-01"));
            fast.TrueAirspeedMps = 175.0;
            fast.EllipsoidHeightM = 4500.0;
            fast.AboveGroundLevelAltitudeM = 1000.0;
            fast.TerrainSampleValid = true;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                fast.HeadingRad,
                4300.0,
                155.0);
            target.DistanceM = 5000.0;

            MissionControlCommand command = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.ReturnToBase,
                in target);

            Assert.That(command.NextMode, Is.EqualTo(MissionAiMode.ReturnToBase));
        }

        [Test]
        public void ApproachCommandsControlledSteepDescentWhenFarAboveTheGlidePath()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("VIPER-01"));
            fast.TrueAirspeedMps = 150.0;
            fast.GroundSpeedMps = 145.0;
            fast.EllipsoidHeightM = 5000.0;
            fast.ClimbRateMps = 0.0;
            fast.AboveGroundLevelAltitudeM = 1800.0;
            fast.TerrainSampleValid = true;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                fast.HeadingRad,
                3300.0,
                95.0);
            target.DistanceM = 10000.0;

            MissionControlCommand command = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.Approach,
                in target);

            Assert.That(command.Input.PitchNormalized, Is.LessThanOrEqualTo(-0.15));
            Assert.That(command.LandingGearDown, Is.True);
        }

        [Test]
        public void LandingAutopilotUsesIdleForOverspeedAndFullBrakingAfterTouchdown()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("VIPER-01"));
            fast.TrueAirspeedMps = 120.0;
            fast.GroundSpeedMps = 115.0;
            fast.EllipsoidHeightM = 2900.0;
            fast.AboveGroundLevelAltitudeM = 120.0;
            fast.TerrainSampleValid = true;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                fast.HeadingRad,
                2850.0,
                80.0);
            target.DistanceM = 1800.0;

            MissionControlCommand airborne = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.Land,
                in target);
            systems.LandingGear.WeightOnWheels = true;
            MissionControlCommand rollout = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.Land,
                in target);

            Assert.That(airborne.Input.ThrottleNormalized, Is.LessThanOrEqualTo(0.10));
            Assert.That(airborne.Input.SpeedBrakeNormalized, Is.GreaterThanOrEqualTo(0.60));
            Assert.That(rollout.Input.ThrottleNormalized, Is.EqualTo(0.0));
            Assert.That(rollout.Input.SpeedBrakeNormalized, Is.EqualTo(1.0));
            Assert.That(rollout.Input.WheelBrakeNormalized, Is.EqualTo(1.0));
        }

        [Test]
        public void EvadeAutopilotCommandsDefensiveBreakWithoutWeaponRelease()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("VIPER-01"));
            fast.HeadingRad = 0.0;
            fast.TrueAirspeedMps = 230.0;
            fast.EllipsoidHeightM = 7000.0;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                Math.PI * 0.5,
                7400.0,
                250.0);

            MissionControlCommand command = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.Evade,
                in target);

            Assert.That(Math.Abs(command.Input.RollNormalized), Is.GreaterThanOrEqualTo(0.5));
            Assert.That(command.Input.PitchNormalized, Is.GreaterThanOrEqualTo(0.30));
            Assert.That(command.Input.ThrottleNormalized, Is.EqualTo(1.0));
            Assert.That(command.RequestWeaponRelease, Is.False);
            Assert.That(command.NextMode, Is.EqualTo(MissionAiMode.Evade));
        }

        [Test]
        public void EvadeAutopilotProtectsTheFlightEnvelopeWhenAlreadyOverspeed()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("BANDIT-01"));
            fast.HeadingRad = 0.0;
            fast.TrueAirspeedMps = 650.0;
            fast.EllipsoidHeightM = 7000.0;
            fast.NormalLoadFactorG = 7.5;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                Math.PI * 0.5,
                7400.0,
                250.0);

            MissionControlCommand command = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.Evade,
                in target);

            Assert.That(command.Input.ThrottleNormalized, Is.LessThanOrEqualTo(0.10));
            Assert.That(command.Input.SpeedBrakeNormalized, Is.GreaterThanOrEqualTo(0.70));
            Assert.That(command.Input.PitchNormalized, Is.LessThanOrEqualTo(0.25));
            Assert.That(command.RequestWeaponRelease, Is.False);
        }

        [Test]
        public void EvadeAutopilotUnloadsAndReducesRollAtHighAngleOfAttack()
        {
            AircraftFastState fast = AircraftFastState.CreateDefault(new AircraftId("BANDIT-01"));
            fast.HeadingRad = 0.0;
            fast.TrueAirspeedMps = 200.0;
            fast.EllipsoidHeightM = 7000.0;
            fast.AngleOfAttackRad = 30.0 * Math.PI / 180.0;
            fast.NormalLoadFactorG = 1.0;
            AircraftSystemsState systems = AircraftSystemsState.CreateDefault(fast.Aircraft);
            MissionGuidanceTarget target = MissionGuidanceTarget.CreateHeadingAltitudeSpeed(
                Math.PI * 0.5,
                7400.0,
                250.0);

            MissionControlCommand command = MissionAutopilot.Compute(
                in fast,
                in systems,
                MissionAiMode.Evade,
                in target);

            Assert.That(command.Input.PitchNormalized, Is.LessThanOrEqualTo(0.0));
            Assert.That(Math.Abs(command.Input.RollNormalized), Is.LessThanOrEqualTo(0.01));
            Assert.That(command.Input.ThrottleNormalized, Is.EqualTo(1.0));
            Assert.That(command.Input.SpeedBrakeNormalized, Is.EqualTo(0.0));
        }

        [Test]
        public void IncomingHitAdjudicationPublishesWarningAndTransfersAiToEvade()
        {
            FlightSimulationService service = new FlightSimulationService();
            MissionDirector director = new MissionDirector(service);
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_CAP_01");
            Assert.That(director.Load(mission, 1000).Accepted, Is.True);
            Assert.That(director.Start().Accepted, Is.True);
            Assert.That(director.SetPlayerAutomation(true, AutomationMode.Batch).Accepted, Is.True);
            bool observed = false;

            for (int tick = 0; tick < 7000 && !observed; tick++)
            {
                director.Tick();
                for (int actorIndex = 0; actorIndex < mission.Actors.Length; actorIndex++)
                {
                    AircraftId aircraft = new AircraftId(mission.Actors[actorIndex].AircraftId);
                    if (!director.TryGetCombatState(aircraft, out AircraftCombatState combat) ||
                        !combat.MissileLaunchWarning) continue;
                    Assert.That(director.TryGetActorState(aircraft, out MissionActorState actor), Is.True);
                    Assert.That(actor.AiMode, Is.EqualTo(MissionAiMode.Evade));
                    observed = true;
                    break;
                }
            }

            Assert.That(observed, Is.True, "No incoming weapon warning was published during the CAP engagement.");
        }

        [Test]
        public void SurvivingAiLeavesEvadeAfterIncomingEngagementResolves()
        {
            FlightSimulationService service = new FlightSimulationService();
            MissionDirector director = new MissionDirector(service);
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_CAP_01");
            Assert.That(director.Load(mission, 1001).Accepted, Is.True);
            Assert.That(director.Start().Accepted, Is.True);
            Assert.That(director.SetPlayerAutomation(true, AutomationMode.Batch).Accepted, Is.True);
            AircraftId evadingAircraft = default(AircraftId);

            for (int tick = 0; tick < 10000 && string.IsNullOrEmpty(evadingAircraft.Value); tick++)
            {
                director.Tick();
                for (int actorIndex = 0; actorIndex < mission.Actors.Length; actorIndex++)
                {
                    AircraftId aircraft = new AircraftId(mission.Actors[actorIndex].AircraftId);
                    if (director.TryGetCombatState(aircraft, out AircraftCombatState combat) &&
                        combat.MissileLaunchWarning &&
                        director.TryGetActorState(aircraft, out MissionActorState actor) &&
                        actor.Status == MissionActorStatus.Active)
                    {
                        evadingAircraft = aircraft;
                        break;
                    }
                }
            }

            Assert.That(evadingAircraft.Value, Is.Not.Null.And.Not.Empty, "No active AI received an incoming engagement.");
            bool resumed = false;
            for (int tick = 0; tick < 12000 && director.LatestState.Phase == MissionPhase.Running; tick++)
            {
                director.Tick();
                if (!director.TryGetActorState(evadingAircraft, out MissionActorState actor) ||
                    actor.Status != MissionActorStatus.Active) break;
                director.TryGetCombatState(evadingAircraft, out AircraftCombatState combat);
                if (!combat.MissileLaunchWarning && actor.AiMode != MissionAiMode.Evade)
                {
                    resumed = true;
                    break;
                }
            }

            Assert.That(resumed, Is.True, "A surviving AI remained permanently in evade after the threat resolved.");
        }

        [Test]
        public void AutomatedActorCannotFireMoreWeaponsThanInitialLoadout()
        {
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_EMERGENCY_RTB_01");
            MissionActorDefinition hostile = Array.Find(mission.Actors, actor => actor.AircraftId == "BANDIT-01");
            int initialWeaponCount = 0;
            for (int station = 0; station < hostile.InitialCondition.Loadout.Length; station++)
                initialWeaponCount += hostile.InitialCondition.Loadout[station].Quantity;
            FlightSimulationService service = new FlightSimulationService();
            MissionDirector director = new MissionDirector(service);
            var sink = new WeaponCountingSink(new AircraftId("BANDIT-01"));
            director.RegisterSink(sink);
            Assert.That(director.Load(mission, 1004).Accepted, Is.True);
            Assert.That(director.Start().Accepted, Is.True);
            Assert.That(director.SetPlayerAutomation(true, AutomationMode.Batch).Accepted, Is.True);

            for (int tick = 0; tick < 14000 && director.LatestState.Phase == MissionPhase.Running; tick++)
                director.Tick();

            Assert.That(sink.LaunchCount, Is.LessThanOrEqualTo(initialWeaponCount));
        }

        [Test]
        public void ObjectiveSuccessOnMissionDeadlineRemainsSuccessful()
        {
            AircraftInitialCondition initial = AircraftInitialCondition.CreateAirborneDefault();
            MissionActorDefinition player = MissionActorDefinition.Create(
                "VIPER-01",
                true,
                AircraftSide.Friendly,
                AircraftRole.Player,
                in initial);
            player.Route = new[]
            {
                new MissionWaypointDefinition
                {
                    WaypointId = "FINISH",
                    LongitudeRad = initial.LongitudeRad,
                    LatitudeRad = initial.LatitudeRad,
                    EllipsoidHeightM = initial.EllipsoidHeightM,
                    AcceptanceRadiusM = 10000.0,
                    TargetTrueAirspeedMps = initial.TrueAirspeedMps
                }
            };
            MissionDefinition mission = new MissionDefinition
            {
                SchemaVersion = 1,
                MissionId = "DEADLINE_SUCCESS",
                DisplayName = "Deadline success",
                MaximumDurationS = FlightSimulationService.FixedDeltaTimeS,
                Actors = new[] { player },
                Objectives = new[]
                {
                    new MissionObjectiveDefinition
                    {
                        ObjectiveId = "REACH_FINISH",
                        DisplayName = "Reach finish",
                        Type = MissionObjectiveType.ReachWaypoint,
                        SubjectAircraftId = "VIPER-01",
                        IsRequired = true
                    }
                }
            };
            MissionDirector director = new MissionDirector(new FlightSimulationService());
            Assert.That(director.Load(mission, 9).Accepted, Is.True);
            Assert.That(director.Start().Accepted, Is.True);

            director.Tick();

            Assert.That(director.LatestState.Phase, Is.EqualTo(MissionPhase.Succeeded));
            Assert.That(director.LatestState.Result, Is.EqualTo(MissionResult.Success));
        }

        [Test]
        public void MissedWeaponStillWarnsTargetAndBlocksDuplicateSameTickLaunch()
        {
            for (int seed = 1; seed <= 100; seed++)
            {
                MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_ESCORT_01");
                FlightSimulationService service = new FlightSimulationService();
                MissionDirector director = new MissionDirector(service);
                var sink = new WeaponCountingSink(new AircraftId("VIPER-01"));
                director.RegisterSink(sink);
                Assert.That(director.Load(mission, seed).Accepted, Is.True);
                Assert.That(director.Start().Accepted, Is.True);
                Assert.That(director.CyclePlayerTarget().Accepted, Is.True);
                Assert.That(director.CyclePlayerWeapon().Accepted, Is.True);
                Assert.That(director.CyclePlayerMasterArm().Accepted, Is.True);
                Assert.That(director.CyclePlayerMasterArm().Accepted, Is.True);
                Assert.That(director.TryGetCombatState(new AircraftId("VIPER-01"), out AircraftCombatState combat), Is.True);
                int targetIndex = Array.FindIndex(mission.Actors, actor => actor.AircraftId == combat.SelectedTarget.Value);
                WeaponEngagementRequest request = new WeaponEngagementRequest
                {
                    Shooter = new AircraftId("VIPER-01"),
                    Target = combat.SelectedTarget,
                    WeaponType = combat.SelectedStoreType,
                    StationIndex = combat.SelectedStationIndex,
                    RangeM = combat.TargetRangeM,
                    ClosureRateMps = combat.ClosureRateMps,
                    LockQualityNormalized = combat.LockQualityNormalized,
                    AspectAngleRad = combat.TargetAspectAngleRad,
                    ShooterSkillNormalized = mission.Actors[0].AiSkillNormalized,
                    TargetEvasionNormalized = mission.Actors[targetIndex].AiSkillNormalized
                };
                WeaponEngagementDecision decision = DeterministicEngagementResolver.Resolve(in request, seed, 0);
                if (!decision.Accepted || decision.Outcome != WeaponEngagementOutcome.Miss) continue;

                CommandResult first = director.ReleasePlayerWeapon();
                CommandResult duplicate = director.ReleasePlayerWeapon();

                Assert.That(first.Accepted, Is.True, first.Message);
                Assert.That(duplicate.Accepted, Is.False);
                Assert.That(sink.LaunchCount, Is.EqualTo(1));
                Assert.That(director.TryGetCombatState(combat.SelectedTarget, out AircraftCombatState target), Is.True);
                Assert.That(target.MissileLaunchWarning, Is.True);
                Assert.That(director.TryGetActorState(combat.SelectedTarget, out MissionActorState actor), Is.True);
                Assert.That(actor.AiMode, Is.EqualTo(MissionAiMode.Evade));
                return;
            }

            Assert.Fail("No deterministic miss sample was found in the test seed range.");
        }

        [Test]
        public void DirectorMarksRunMixedAfterAutomationAndManualTakeover()
        {
            FlightSimulationService service = new FlightSimulationService();
            MissionDirector director = new MissionDirector(service);
            Assert.That(director.Load(DefaultMissionCatalog.Create("KTEX_CAP_01"), 55).Accepted, Is.True);
            Assert.That(director.Start().Accepted, Is.True);

            Assert.That(director.SetPlayerAutomation(true, AutomationMode.Visible).Accepted, Is.True);
            Assert.That(director.TryGetActorState(new AircraftId("VIPER-01"), out MissionActorState automated), Is.True);
            Assert.That(automated.ControlAuthority, Is.EqualTo(ControlAuthority.AutomationPilot));
            Assert.That(director.TryGetCombatState(new AircraftId("VIPER-01"), out AircraftCombatState automatedCombat), Is.True);
            Assert.That(automatedCombat.MasterArm, Is.EqualTo(MasterArmState.Arm));
            Assert.That(director.SetPlayerAutomation(false, AutomationMode.Manual).Accepted, Is.True);
            Assert.That(director.TryGetLatest(out MissionSnapshot snapshot), Is.True);
            Assert.That(director.TryGetCombatState(new AircraftId("VIPER-01"), out AircraftCombatState manualCombat), Is.True);

            Assert.That(snapshot.Automation.Mode, Is.EqualTo(AutomationMode.Mixed));
            Assert.That(snapshot.Automation.PlayerControlAuthority, Is.EqualTo(ControlAuthority.ManualPilot));
            Assert.That(manualCombat.MasterArm, Is.EqualTo(MasterArmState.Safe));
        }

        [Test]
        public void WingmanThreatBreakTransfersAuthorityFromFormationToMissionAi()
        {
            FlightSimulationService service = new FlightSimulationService();
            MissionDirector director = new MissionDirector(service);
            Assert.That(director.Load(DefaultMissionCatalog.Create("KTEX_CAP_01"), 88).Accepted, Is.True);
            Assert.That(director.Start().Accepted, Is.True);
            Assert.That(director.SetPlayerAutomation(true, AutomationMode.Batch).Accepted, Is.True);

            director.Tick();

            Assert.That(service.TryGetLatest(new AircraftId("VIPER-02"), out AircraftSnapshot wingman), Is.True);
            Assert.That(wingman.Systems.FlightControls.ActiveControlAuthority, Is.EqualTo(ControlAuthority.MissionAI));
            Assert.That(director.TryGetActorState(new AircraftId("VIPER-02"), out MissionActorState actor), Is.True);
            Assert.That(actor.AiMode, Is.EqualTo(MissionAiMode.Intercept));
        }

        [Test]
        public void PlayerCombatCommandsSelectTargetArmAndConsumeStore()
        {
            FlightSimulationService service = new FlightSimulationService();
            MissionDirector director = new MissionDirector(service);
            Assert.That(director.Load(DefaultMissionCatalog.Create("KTEX_ESCORT_01"), 77).Accepted, Is.True);
            Assert.That(director.Start().Accepted, Is.True);

            Assert.That(director.CyclePlayerTarget().Accepted, Is.True);
            Assert.That(director.CyclePlayerWeapon().Accepted, Is.True);
            Assert.That(director.CyclePlayerMasterArm().Accepted, Is.True);
            Assert.That(director.CyclePlayerMasterArm().Accepted, Is.True);
            Assert.That(director.TryGetCombatState(new AircraftId("VIPER-01"), out AircraftCombatState before), Is.True);
            Assert.That(before.MasterArm, Is.EqualTo(MasterArmState.Arm));
            Assert.That(before.SelectedTarget.Value, Does.StartWith("BANDIT-"));
            Assert.That(before.SelectedStoreQuantity, Is.GreaterThan(0));

            CommandResult release = director.ReleasePlayerWeapon();
            director.Tick();
            Assert.That(release.Accepted, Is.True, release.Message);
            Assert.That(director.TryGetCombatState(new AircraftId("VIPER-01"), out AircraftCombatState after), Is.True);
            Assert.That(after.SelectedStoreQuantity, Is.LessThan(before.SelectedStoreQuantity));
        }

        [Test]
        public void TerrainCacheUsesBilinearInterpolationInsideMissionBounds()
        {
            MissionTerrainCache cache = new MissionTerrainCache
            {
                MissionId = "TEST",
                LongitudeMinRad = 0.0,
                LongitudeMaxRad = 1.0,
                LatitudeMinRad = 0.0,
                LatitudeMaxRad = 1.0,
                ColumnCount = 2,
                RowCount = 2,
                HeightM = new[] { 100.0, 200.0, 300.0, 400.0 }
            };

            Assert.That(cache.TryGetHeightM(0.5, 0.5, out double heightM), Is.True);
            Assert.That(heightM, Is.EqualTo(250.0).Within(1e-9));
            Assert.That(cache.TryGetHeightM(1.5, 0.5, out _), Is.False);
        }

        [Test]
        public void AnalyticFlatFallbackDoesNotClaimToBeMissionCache()
        {
            MissionTerrainCache fallback = MissionTerrainCache.CreateKtexFlat();

            Assert.That(fallback.Source, Is.EqualTo(TerrainSource.AnalyticRunway));
            Assert.That(fallback.TryGetHeightM(-107.9087 * Math.PI / 180.0, 37.9538 * Math.PI / 180.0, out _), Is.True);
            Assert.That(fallback.TryGetHeightM(-107.6330 * Math.PI / 180.0, 37.8956 * Math.PI / 180.0, out _), Is.False);
        }

        [Test]
        public void BatchRunnerProducesSameHashForSameMissionAndSeed()
        {
            MissionBatchRunner runner = new MissionBatchRunner();
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_CAP_01");
            MissionRunOptions options = MissionRunOptions.Create(9042);
            options.MaximumSimulationTimeS = 2.0;
            options.WriteDetailedLogs = false;

            MissionRunReport first = runner.RunSingle(mission, in options, MissionTerrainCache.CreateKtexFlat());
            MissionRunReport second = runner.RunSingle(mission, in options, MissionTerrainCache.CreateKtexFlat());

            Assert.That(first.HasException, Is.False, first.ExceptionMessage);
            Assert.That(second.HasException, Is.False, second.ExceptionMessage);
            Assert.That(second.DeterminismHash, Is.EqualTo(first.DeterminismHash));
            Assert.That(second.FinalSimulationTimeS, Is.EqualTo(first.FinalSimulationTimeS));
        }

        [Test]
        public void BatchRunnerContinuesWhenTerrainDataIsUnavailable()
        {
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_EMERGENCY_RTB_01");
            MissionRunOptions options = MissionRunOptions.Create(mission.DefaultSeed);
            options.MaximumSimulationTimeS = 2.0;
            options.WriteDetailedLogs = false;

            MissionRunReport report = new MissionBatchRunner().RunSingle(
                mission,
                in options,
                null);

            Assert.That(report.HasException, Is.False, report.ExceptionMessage);
            Assert.That(report.TimedOut, Is.True);
            Assert.That(report.FinalSimulationTimeS, Is.EqualTo(2.0).Within(0.011));
            Assert.That(report.TerrainSource, Is.EqualTo(TerrainSource.None));
            Assert.That(report.TerrainDataValid, Is.False);
        }

        [Test]
        public void BatchPolicyAcceptsExplicitTacticalFailureOutsideRegression()
        {
            MissionRunReport tacticalFailure = new MissionRunReport
            {
                Succeeded = false,
                TimedOut = false,
                HasException = false,
                CompletionReason = "Player aircraft neutralized."
            };

            Assert.That(MissionBatchPolicy.IsFailure("smoke", in tacticalFailure), Is.False);
            Assert.That(MissionBatchPolicy.IsFailure("dataset", in tacticalFailure), Is.False);
            Assert.That(MissionBatchPolicy.IsFailure("regression", in tacticalFailure), Is.True);
        }

        [Test]
        public void BatchPolicyRejectsTimeoutsAndExceptionsForEveryProfile()
        {
            MissionRunReport timeout = new MissionRunReport { TimedOut = true };
            MissionRunReport exception = new MissionRunReport { HasException = true };

            foreach (string profile in new[] { "smoke", "regression", "dataset" })
            {
                Assert.That(MissionBatchPolicy.IsFailure(profile, in timeout), Is.True, profile);
                Assert.That(MissionBatchPolicy.IsFailure(profile, in exception), Is.True, profile);
            }
        }

        [Test]
        public void DetailedBatchRunWritesMissionAiObjectiveAndAutomationLogs()
        {
            string output = Path.Combine(Path.GetTempPath(), "FlightSimMissionLogs-" + Guid.NewGuid().ToString("N"));
            try
            {
                MissionRunOptions options = MissionRunOptions.Create(8123);
                options.OutputDirectory = output;
                options.MaximumSimulationTimeS = 0.25;
                options.WriteDetailedLogs = true;

                MissionRunReport report = new MissionBatchRunner().RunSingle(
                    DefaultMissionCatalog.Create("KTEX_CAP_01"),
                    in options,
                    MissionTerrainCache.CreateKtexFlat());

                Assert.That(report.HasException, Is.False, report.ExceptionMessage);
                Assert.That(File.Exists(Path.Combine(report.ReportDirectory, "mission-actors-5hz.csv")), Is.True);
                Assert.That(File.Exists(Path.Combine(report.ReportDirectory, "mission-objectives-5hz.csv")), Is.True);
                Assert.That(File.Exists(Path.Combine(report.ReportDirectory, "automation-5hz.csv")), Is.True);
                Assert.That(File.ReadAllLines(Path.Combine(report.ReportDirectory, "mission-actors-5hz.csv")).Length, Is.GreaterThan(1));
            }
            finally
            {
                if (Directory.Exists(output)) Directory.Delete(output, true);
            }
        }

        private sealed class WeaponCountingSink : IMissionTelemetrySink
        {
            private readonly AircraftId shooter;

            public WeaponCountingSink(AircraftId shooter)
            {
                this.shooter = shooter;
            }

            public int LaunchCount { get; private set; }
            public void OnMissionState(in MissionState state) { }
            public void OnMissionActorState(in MissionActorState state) { }
            public void OnMissionObjectiveState(in MissionObjectiveState state) { }
            public void OnAircraftCombatState(in AircraftCombatState state) { }
            public void OnAutomationRunState(in AutomationRunState state) { }
            public void OnMissionEvent(in MissionEvent missionEvent) { }

            public void OnWeaponEngagementEvent(in WeaponEngagementEvent engagementEvent)
            {
                if (engagementEvent.Shooter == shooter) LaunchCount++;
            }
        }
    }
}
