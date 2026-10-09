using System;
using System.Diagnostics;
using System.Linq;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using FlightSim.Platform.Integration;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class IntegrationContractTests
    {
        [Test]
        public void ServicePublishesAtSpecifiedDomainRates()
        {
            FlightSimulationService service = new FlightSimulationService();
            AircraftId aircraft = new AircraftId("VIPER-01");
            CountingSink sink = new CountingSink();
            Assert.That(service.AddAircraft(aircraft, StartupPreset.RunwayReady, true), Is.True);
            service.RegisterSink(sink);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 0.7;
            service.SetLocalPilotInput(aircraft, in input);

            for (int tick = 0; tick < 100; tick++)
            {
                service.Tick();
            }

            Assert.That(sink.FastCount, Is.EqualTo(50));
            Assert.That(sink.SystemsCount, Is.EqualTo(10));
            Assert.That(sink.TacticalCount, Is.EqualTo(5));
            Assert.That(service.TryGetLatest(aircraft, out AircraftSnapshot snapshot), Is.True);
            Assert.That(snapshot.Fast.Tick, Is.EqualTo(100));
        }

        [Test]
        public void HighLevelCommandAppliesOnNextTickAndReturnsAcknowledgement()
        {
            FlightSimulationService service = new FlightSimulationService();
            AircraftId aircraft = new AircraftId("VIPER-01");
            service.AddAircraft(aircraft, StartupPreset.RunwayReady, true);
            StartupPresetCommand command = new StartupPresetCommand
            {
                Aircraft = aircraft,
                Preset = StartupPreset.Airborne
            };

            CommandResult accepted = service.Submit(in command);
            service.TryGetLatest(aircraft, out AircraftSnapshot beforeTick);
            service.Tick();
            service.TryGetLatest(aircraft, out AircraftSnapshot afterTick);

            Assert.That(accepted.Accepted, Is.True);
            Assert.That(beforeTick.Systems.Avionics.ActiveStartupPreset, Is.EqualTo(StartupPreset.RunwayReady));
            Assert.That(afterTick.Systems.Avionics.ActiveStartupPreset, Is.EqualTo(StartupPreset.Airborne));
            Assert.That(afterTick.Fast.AboveGroundLevelAltitudeM, Is.GreaterThan(1000.0));
        }

        [Test]
        public void ServiceAcceptsEightAircraftAndRejectsNinth()
        {
            FlightSimulationService service = new FlightSimulationService();

            for (int index = 1; index <= 8; index++)
            {
                Assert.That(
                    service.AddAircraft(
                        new AircraftId($"VIPER-{index:00}"),
                        StartupPreset.Airborne,
                        index == 1),
                    Is.True);
            }

            Assert.That(
                service.AddAircraft(new AircraftId("VIPER-09"), StartupPreset.Airborne, false),
                Is.False);
        }

        [Test]
        public void ExplicitControlAuthorityRejectsManualInputDuringAutomation()
        {
            FlightSimulationService service = new FlightSimulationService();
            AircraftId aircraft = new AircraftId("VIPER-01");
            Assert.That(service.AddAircraft(aircraft, StartupPreset.Airborne, true), Is.True);
            Assert.That(service.SetControlAuthority(aircraft, ControlAuthority.AutomationPilot), Is.True);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 0.85;

            Assert.That(service.SetLocalPilotInput(aircraft, in input), Is.False);
            Assert.That(service.SetMissionControlInput(aircraft, in input, ControlAuthority.AutomationPilot), Is.True);
            service.Tick();
            service.TryGetLatest(aircraft, out AircraftSnapshot snapshot);

            Assert.That(snapshot.Systems.FlightControls.ActiveControlAuthority, Is.EqualTo(ControlAuthority.AutomationPilot));
        }

        [Test]
        public void SelectedControlAuthorityRejectsAnotherAutomatedSourceInSameTick()
        {
            FlightSimulationService service = new FlightSimulationService();
            AircraftId aircraft = new AircraftId("VIPER-01");
            Assert.That(service.AddAircraft(aircraft, StartupPreset.Airborne, true), Is.True);
            Assert.That(service.SetControlAuthority(aircraft, ControlAuthority.AutomationPilot), Is.True);
            PilotControlInput automation = PilotControlInput.Neutral;
            automation.ThrottleNormalized = 0.72;
            automation.PitchNormalized = 0.21;
            PilotControlInput missionAi = PilotControlInput.Neutral;
            missionAi.ThrottleNormalized = 1.0;
            missionAi.PitchNormalized = 0.87;

            Assert.That(
                service.SetMissionControlInput(aircraft, in automation, ControlAuthority.AutomationPilot),
                Is.True);
            Assert.That(
                service.SetMissionControlInput(aircraft, in missionAi, ControlAuthority.MissionAI),
                Is.False);
            service.Tick();
            Assert.That(service.TryGetLatest(aircraft, out AircraftSnapshot snapshot), Is.True);

            Assert.That(
                snapshot.Systems.FlightControls.ActiveControlAuthority,
                Is.EqualTo(ControlAuthority.AutomationPilot));
            Assert.That(snapshot.Systems.FlightControls.PitchCommandNormalized, Is.EqualTo(0.21).Within(1e-9));
        }

        [Test]
        public void RegistrationAppliesArbitraryAirborneInitialCondition()
        {
            FlightSimulationService service = new FlightSimulationService();
            AircraftInitialCondition initial = AircraftInitialCondition.CreateAirborneDefault();
            initial.LongitudeRad = -107.95 * Math.PI / 180.0;
            initial.LatitudeRad = 37.91 * Math.PI / 180.0;
            initial.EllipsoidHeightM = 6250.0;
            initial.HeadingRad = 247.0 * Math.PI / 180.0;
            initial.PitchRad = 4.0 * Math.PI / 180.0;
            initial.RollRad = -8.0 * Math.PI / 180.0;
            initial.TrueAirspeedMps = 205.0;
            initial.InternalFuelFraction = 0.35;
            initial.ExternalFuelKg = 420.0;
            initial.LandingGearDown = false;
            AircraftRegistration registration = AircraftRegistration.Create(
                new AircraftId("VIPER-01"),
                true,
                in initial);

            Assert.That(service.AddAircraft(in registration), Is.True);
            Assert.That(service.TryGetLatest(registration.Aircraft, out AircraftSnapshot snapshot), Is.True);
            Assert.That(snapshot.Fast.LongitudeRad, Is.EqualTo(initial.LongitudeRad).Within(1e-8));
            Assert.That(snapshot.Fast.LatitudeRad, Is.EqualTo(initial.LatitudeRad).Within(1e-8));
            Assert.That(snapshot.Fast.EllipsoidHeightM, Is.EqualTo(initial.EllipsoidHeightM).Within(0.05));
            Assert.That(snapshot.Fast.HeadingRad, Is.EqualTo(initial.HeadingRad).Within(1e-6));
            Assert.That(snapshot.Fast.PitchRad, Is.EqualTo(initial.PitchRad).Within(1e-6));
            Assert.That(snapshot.Fast.RollRad, Is.EqualTo(initial.RollRad).Within(1e-6));
            Assert.That(snapshot.Fast.TrueAirspeedMps, Is.EqualTo(initial.TrueAirspeedMps).Within(1e-6));
            Assert.That(snapshot.Systems.Fuel.InternalFuelKg,
                Is.EqualTo(F16AircraftDefinition.CreateDefault().InternalFuelCapacityKg * 0.35).Within(1e-6));
            Assert.That(snapshot.Systems.Fuel.ExternalFuelKg, Is.EqualTo(420.0).Within(1e-6));
            Assert.That(snapshot.Systems.LandingGear.NoseGearPositionNormalized, Is.EqualTo(0.0));
        }

        [Test]
        public void TerrainSampleValidityAndAglFlowIntoPublishedSnapshot()
        {
            FlightSimulationService service = new FlightSimulationService();
            AircraftId aircraft = new AircraftId("VIPER-01");
            service.AddAircraft(aircraft, StartupPreset.Airborne, true);
            service.TryGetLatest(aircraft, out AircraftSnapshot initial);

            Assert.That(initial.Fast.TerrainSampleValid, Is.False);
            Assert.That(service.SetTerrainSample(aircraft, true, 321.5, 0.02), Is.True);
            service.TryGetLatest(aircraft, out AircraftSnapshot sampled);

            Assert.That(sampled.Fast.TerrainSampleValid, Is.True);
            Assert.That(sampled.Fast.AboveGroundLevelAltitudeM, Is.EqualTo(321.5));
            Assert.That(sampled.Fast.TerrainSampleAgeS, Is.EqualTo(0.02));
        }

        [Test]
        public void InitialClimbBelowWarningAltitudeDoesNotRaiseMasterCaution()
        {
            FlightSimulationService service = new FlightSimulationService();
            AircraftId aircraft = new AircraftId("TEST-01");
            Assert.That(service.AddAircraft(aircraft, StartupPreset.Airborne, true), Is.True);
            Assert.That(service.SetTerrainSample(aircraft, true, 80.0, 0.0), Is.True);
            for (int tick = 0; tick < 20; tick++)
            {
                service.Tick();
            }

            Assert.That(service.TryGetLatest(aircraft, out AircraftSnapshot snapshot), Is.True);
            Assert.That(snapshot.Fast.ClimbRateMps, Is.GreaterThan(-1.0));
            Assert.That(snapshot.Systems.Warnings.LowAltitudeWarning, Is.False);
            Assert.That(snapshot.Systems.Warnings.LandingGearWarning, Is.False);
            Assert.That(snapshot.Systems.Warnings.MasterCaution, Is.False);
        }

        [Test]
        public void DescendingLowWithGearDownRaisesOnlyLowAltitudeCaution()
        {
            WarningState warnings = DynamicWarningEvaluator.Evaluate(
                default, true, true, false, 60.0, -2.0, true, 0.4, 8.0 * Math.PI / 180.0);

            Assert.That(warnings.LowAltitudeWarning, Is.True);
            Assert.That(warnings.LandingGearWarning, Is.False);
            Assert.That(warnings.MasterCaution, Is.True);
        }

        [Test]
        public void DescendingAtOneHundredFiftyMetersWithGearUpRaisesOnlyGearCaution()
        {
            WarningState warnings = DynamicWarningEvaluator.Evaluate(
                default, true, true, false, 150.0, -1.0, false, 0.4, 8.0 * Math.PI / 180.0);

            Assert.That(warnings.LowAltitudeWarning, Is.False);
            Assert.That(warnings.LandingGearWarning, Is.True);
            Assert.That(warnings.MasterCaution, Is.True);
        }

        [TestCase(21.9, false)]
        [TestCase(22.0, false)]
        [TestCase(22.1, true)]
        public void StallWarningUsesTwentyTwoDegreeThreshold(double aoaDegrees, bool expected)
        {
            WarningState warnings = DynamicWarningEvaluator.Evaluate(
                default, false, true, false, 500.0, 0.0, false, 0.5, aoaDegrees * Math.PI / 180.0);

            Assert.That(warnings.StallWarning, Is.EqualTo(expected));
        }

        [Test]
        public void LandingGearAndFlightControlComputerCommandsAffectCoreOnNextTick()
        {
            FlightSimulationService service = new FlightSimulationService();
            AircraftId aircraft = new AircraftId("VIPER-01");
            service.AddAircraft(aircraft, StartupPreset.RunwayReady, true);
            LandingGearCommand gearCommand = new LandingGearCommand { Aircraft = aircraft, IsDown = false };
            SystemSwitchCommand flightComputerCommand = new SystemSwitchCommand
            {
                Aircraft = aircraft,
                Switch = AircraftSystemSwitch.FlightControlComputer,
                IsEnabled = false
            };
            PilotControlInput input = PilotControlInput.Neutral;
            input.PitchNormalized = 1.0;
            service.SetLocalPilotInput(aircraft, in input);

            Assert.That(service.Submit(in gearCommand).Accepted, Is.True);
            Assert.That(service.Submit(in flightComputerCommand).Accepted, Is.True);
            service.Tick();
            service.TryGetLatest(aircraft, out AircraftSnapshot snapshot);

            Assert.That(snapshot.Systems.LandingGear.NoseGearPositionNormalized, Is.LessThan(1.0));
            Assert.That(snapshot.Systems.FlightControls.FlightControlComputerEnabled, Is.False);
            Assert.That(snapshot.Systems.FlightControls.ElevatorDeflectionRad, Is.EqualTo(0.0).Within(1e-9));
        }

        [Test]
        public void FastStateUdpPacketRoundTripsAndDetectsCorruption()
        {
            AircraftFastState expected = AircraftFastState.CreateDefault(new AircraftId("VIPER-01"));
            expected.Tick = 123456;
            expected.SimulationTimeS = 42.25;
            expected.EcefPositionXM = -1292935.125;
            expected.EcefPositionYM = -4740026.5;
            expected.EcefPositionZM = 4056960.75;
            expected.CalibratedAirspeedMps = 151.25;
            expected.Mach = 0.72;
            Span<byte> packet = stackalloc byte[UdpPacketCodec.MaximumPacketBytes];

            bool encoded = UdpPacketCodec.TryEncodeFastState(in expected, 77, packet, out int bytesWritten);
            bool decoded = UdpPacketCodec.TryDecodeFastState(
                packet.Slice(0, bytesWritten),
                out UdpPacketHeader header,
                out AircraftFastState actual);

            Assert.That(encoded, Is.True);
            Assert.That(bytesWritten, Is.LessThanOrEqualTo(1200));
            Assert.That(decoded, Is.True);
            Assert.That(header.Magic, Is.EqualTo(UdpPacketHeader.FsimMagic));
            Assert.That(header.ContractVersion, Is.EqualTo((ushort)2));
            Assert.That(header.Sequence, Is.EqualTo(77));
            Assert.That(header.Tick, Is.EqualTo(expected.Tick));
            Assert.That(actual.Aircraft, Is.EqualTo(expected.Aircraft));
            Assert.That(actual.EcefPositionXM, Is.EqualTo(expected.EcefPositionXM));
            Assert.That(actual.CalibratedAirspeedMps, Is.EqualTo(expected.CalibratedAirspeedMps));

            packet[bytesWritten - 1] ^= 0x5A;
            Assert.That(
                UdpPacketCodec.TryDecodeFastState(
                    packet.Slice(0, bytesWritten),
                    out _,
                    out _),
                Is.False);
        }

        [Test]
        public void UdpPublicSurfaceContainsTelemetryOnly()
        {
            string[] publicMethods = typeof(UdpPacketCodec)
                .GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                .Select(method => method.Name)
                .ToArray();

            Assert.That(publicMethods.Any(name => name.Contains("Command")), Is.False);
            Assert.That(publicMethods.Any(name => name.Contains("Acknowledgement")), Is.False);
        }

        [Test]
        public void UdpV2MissionAndCombatPacketsRoundTrip()
        {
            MissionState mission = new MissionState
            {
                MissionId = "KTEX_CAP_01",
                RunId = "RUN-42",
                SchemaVersion = 1,
                Seed = 42,
                Phase = MissionPhase.Running,
                Result = MissionResult.None,
                Tick = 900,
                SimulationTimeS = 9.0,
                ElapsedTimeS = 9.0,
                RemainingTimeS = 891.0,
                ActiveObjectiveCount = 2,
                IsValid = true
            };
            Span<byte> packet = stackalloc byte[UdpPacketCodec.MaximumPacketBytes];
            Assert.That(UdpPacketCodec.TryEncodeMissionState(in mission, 100, packet, out int missionBytes), Is.True);
            Assert.That(UdpPacketCodec.TryDecodeMissionState(packet.Slice(0, missionBytes), out UdpPacketHeader missionHeader, out MissionState decodedMission), Is.True);
            Assert.That(missionHeader.MessageType, Is.EqualTo(UdpMessageType.MissionState));
            Assert.That(decodedMission.MissionId, Is.EqualTo(mission.MissionId));
            Assert.That(decodedMission.Tick, Is.EqualTo(mission.Tick));

            AircraftCombatState combat = new AircraftCombatState
            {
                Aircraft = new AircraftId("VIPER-01"),
                SimulationTimeS = 9.0,
                MasterArm = MasterArmState.Arm,
                SelectedStationIndex = 3,
                SelectedStoreType = "AIM-120C",
                SelectedStoreQuantity = 2,
                SelectedTrackId = 7,
                SelectedTarget = new AircraftId("BANDIT-01"),
                RadarTrackState = RadarTrackState.Locked,
                TargetRangeM = 18400.0,
                ClosureRateMps = 255.0,
                LockQualityNormalized = 0.91,
                InLaunchZone = true,
                ShootCue = true,
                IsValid = true
            };
            Assert.That(UdpPacketCodec.TryEncodeAircraftCombatState(in combat, 900, 101, packet, out int combatBytes), Is.True);
            Assert.That(UdpPacketCodec.TryDecodeAircraftCombatState(packet.Slice(0, combatBytes), out UdpPacketHeader combatHeader, out AircraftCombatState decodedCombat), Is.True);
            Assert.That(combatHeader.ContractVersion, Is.EqualTo(2));
            Assert.That(decodedCombat.SelectedTarget, Is.EqualTo(combat.SelectedTarget));
            Assert.That(decodedCombat.TargetRangeM, Is.EqualTo(combat.TargetRangeM));
            Assert.That(decodedCombat.ShootCue, Is.True);
        }

        [Test]
        public void UdpV2RemainingMissionDomainsRoundTrip()
        {
            Span<byte> packet = stackalloc byte[UdpPacketCodec.MaximumPacketBytes];
            MissionActorState actor = new MissionActorState
            {
                Aircraft = new AircraftId("VIPER-02"), Callsign = "Viper 2", Side = AircraftSide.Friendly,
                Role = AircraftRole.Wingman, Status = MissionActorStatus.Active, AiMode = MissionAiMode.Intercept,
                ControlAuthority = ControlAuthority.MissionAI, LeaderAircraft = new AircraftId("VIPER-01"),
                FormationSlot = 1, AiSkillNormalized = 0.85, CurrentWaypointIndex = 2,
                SelectedTarget = new AircraftId("BANDIT-01"), IsDetected = true, IsEngaged = true, IsValid = true
            };
            Assert.That(UdpPacketCodec.TryEncodeMissionActorState(in actor, 500, 201, packet, out int actorBytes), Is.True);
            Assert.That(UdpPacketCodec.TryDecodeMissionActorState(packet.Slice(0, actorBytes), out _, out MissionActorState decodedActor), Is.True);
            Assert.That(decodedActor.Aircraft, Is.EqualTo(actor.Aircraft));
            Assert.That(decodedActor.AiSkillNormalized, Is.EqualTo(actor.AiSkillNormalized));

            MissionObjectiveState objective = new MissionObjectiveState
            {
                ObjectiveId = "CLEAR_CAP", DisplayName = "Clear CAP", Type = MissionObjectiveType.NeutralizeHostiles,
                Status = MissionObjectiveStatus.Active, ProgressNormalized = 0.5, CurrentCount = 2,
                RequiredCount = 4, DeadlineS = 850.0, FailureReason = string.Empty
            };
            Assert.That(UdpPacketCodec.TryEncodeMissionObjectiveState(in objective, 500, 202, packet, out int objectiveBytes), Is.True);
            Assert.That(UdpPacketCodec.TryDecodeMissionObjectiveState(packet.Slice(0, objectiveBytes), out _, out MissionObjectiveState decodedObjective), Is.True);
            Assert.That(decodedObjective.ObjectiveId, Is.EqualTo(objective.ObjectiveId));
            Assert.That(decodedObjective.ProgressNormalized, Is.EqualTo(0.5));

            AutomationRunState automation = new AutomationRunState
            {
                BatchId = "regression-001", RunId = "run-001", MissionId = "KTEX_CAP_01", Seed = 1000,
                Mode = AutomationMode.Batch, Status = AutomationRunStatus.Running,
                PlayerControlAuthority = ControlAuthority.AutomationPilot, SimulationTimeS = 12.5,
                WallClockTimeS = 2.5, SimulationRate = 5.0, TerrainSource = TerrainSource.MissionCache,
                TerrainDataValid = true, CompletionReason = string.Empty, ReportPath = "Artifacts/Missions"
            };
            Assert.That(UdpPacketCodec.TryEncodeAutomationRunState(in automation, 500, 203, packet, out int automationBytes), Is.True);
            Assert.That(UdpPacketCodec.TryDecodeAutomationRunState(packet.Slice(0, automationBytes), out _, out AutomationRunState decodedAutomation), Is.True);
            Assert.That(decodedAutomation.BatchId, Is.EqualTo(automation.BatchId));
            Assert.That(decodedAutomation.TerrainSource, Is.EqualTo(TerrainSource.MissionCache));

            MissionEvent missionEvent = new MissionEvent
            {
                RunId = "run-001", MissionId = "KTEX_CAP_01", SimulationTimeS = 15.0,
                Type = MissionEventType.Engagement, Source = actor.Aircraft,
                Target = actor.SelectedTarget, Code = 42, Message = "engaged"
            };
            Assert.That(UdpPacketCodec.TryEncodeMissionEvent(in missionEvent, 500, 204, packet, out int eventBytes), Is.True);
            Assert.That(UdpPacketCodec.TryDecodeMissionEvent(packet.Slice(0, eventBytes), out _, out MissionEvent decodedEvent), Is.True);
            Assert.That(decodedEvent.Source, Is.EqualTo(missionEvent.Source));
            Assert.That(decodedEvent.Target, Is.EqualTo(missionEvent.Target));

            WeaponEngagementEvent engagement = new WeaponEngagementEvent
            {
                RunId = "run-001", EngagementSequence = 7, SimulationTimeS = 16.0,
                Shooter = actor.Aircraft, Target = actor.SelectedTarget, WeaponType = "AIM-120C",
                StationIndex = 2, LaunchRangeM = 22000.0, ClosureRateMps = 250.0,
                LockQualityNormalized = 0.9, TimeToImpactS = 18.0,
                Outcome = WeaponEngagementOutcome.Hit, Reason = "deterministic hit"
            };
            Assert.That(UdpPacketCodec.TryEncodeWeaponEngagementEvent(in engagement, 500, 205, packet, out int engagementBytes), Is.True);
            Assert.That(UdpPacketCodec.TryDecodeWeaponEngagementEvent(packet.Slice(0, engagementBytes), out _, out WeaponEngagementEvent decodedEngagement), Is.True);
            Assert.That(decodedEngagement.Shooter, Is.EqualTo(engagement.Shooter));
            Assert.That(decodedEngagement.Outcome, Is.EqualTo(WeaponEngagementOutcome.Hit));
        }

        [Test]
        public void OnePlusThreeFormationRunsTenSimulatedMinutesWithoutStateLoss()
        {
            FlightSimulationService service = CreateFourAircraftService(out AircraftId[] aircraft);
            PilotControlInput leaderInput = PilotControlInput.Neutral;
            leaderInput.ThrottleNormalized = 0.72;
            service.SetLocalPilotInput(aircraft[0], in leaderInput);

            for (int tick = 0; tick < 60000; tick++)
            {
                service.Tick();
            }

            for (int index = 0; index < aircraft.Length; index++)
            {
                Assert.That(service.TryGetLatest(aircraft[index], out AircraftSnapshot snapshot), Is.True);
                Assert.That(double.IsNaN(snapshot.Fast.EcefPositionXM), Is.False);
                Assert.That(double.IsInfinity(snapshot.Fast.EcefPositionXM), Is.False);
                Assert.That(
                    snapshot.Fast.TrueAirspeedMps,
                    Is.GreaterThan(45.0),
                    $"Aircraft {index} final TAS {snapshot.Fast.TrueAirspeedMps:F1} m/s.");
                Assert.That(
                    snapshot.Systems.Warnings.StallWarning,
                    Is.False,
                    $"Aircraft {index} final AoA {snapshot.Fast.AngleOfAttackRad * 180.0 / Math.PI:F1} deg, " +
                    $"TAS {snapshot.Fast.TrueAirspeedMps:F1} m/s, pitch {snapshot.Fast.PitchRad * 180.0 / Math.PI:F1} deg.");
                if (index > 0)
                {
                    Assert.That(snapshot.Tactical.Formation.IsFormationActive, Is.True);
                }
            }

            service.TryGetLatest(aircraft[0], out AircraftSnapshot leader);
            for (int index = 1; index < aircraft.Length; index++)
            {
                service.TryGetLatest(aircraft[index], out AircraftSnapshot wingman);
                double dx = wingman.Fast.EcefPositionXM - leader.Fast.EcefPositionXM;
                double dy = wingman.Fast.EcefPositionYM - leader.Fast.EcefPositionYM;
                double dz = wingman.Fast.EcefPositionZM - leader.Fast.EcefPositionZM;
                Assert.That(Math.Sqrt(dx * dx + dy * dy + dz * dz), Is.GreaterThan(25.0));
            }
        }

        [Test]
        public void FourAircraftSteadyStateMeetsTickBudgetAndAllocatesZeroBytes()
        {
            FlightSimulationService service = CreateFourAircraftService(out _);
            for (int tick = 0; tick < 500; tick++)
            {
                service.Tick();
            }

            Stopwatch stopwatch = new Stopwatch();
            long beforeBytes = GC.GetAllocatedBytesForCurrentThread();
            stopwatch.Start();
            for (int tick = 0; tick < 1000; tick++)
            {
                service.Tick();
            }
            stopwatch.Stop();
            long allocatedBytes = GC.GetAllocatedBytesForCurrentThread() - beforeBytes;

            Assert.That(stopwatch.Elapsed.TotalMilliseconds / 1000.0, Is.LessThan(2.0));
            Assert.That(allocatedBytes, Is.EqualTo(0));
        }

        private static FlightSimulationService CreateFourAircraftService(out AircraftId[] aircraft)
        {
            FlightSimulationService service = new FlightSimulationService();
            aircraft = new[]
            {
                new AircraftId("VIPER-01"),
                new AircraftId("VIPER-02"),
                new AircraftId("VIPER-03"),
                new AircraftId("VIPER-04")
            };
            for (int index = 0; index < aircraft.Length; index++)
            {
                Assert.That(
                    service.AddAircraft(aircraft[index], StartupPreset.Airborne, index == 0),
                    Is.True);
            }

            return service;
        }

        private sealed class CountingSink : IFlightTelemetrySink
        {
            public int FastCount;
            public int SystemsCount;
            public int TacticalCount;
            public int EventCount;

            public void OnFastState(in AircraftFastState state) => FastCount++;
            public void OnSystemsState(in AircraftSystemsState state) => SystemsCount++;
            public void OnTacticalPictureState(in TacticalPictureState state) => TacticalCount++;
            public void OnSimulationEvent(in SimulationEvent simulationEvent) => EventCount++;
        }
    }
}
