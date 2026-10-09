using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using FlightSim.Platform.Integration;

namespace FlightSim.Platform.Missions
{
    [Serializable]
    public struct MissionRunOptions
    {
        public string BatchId;
        public int Seed;
        public string OutputDirectory;
        public double MaximumSimulationTimeS;
        public bool WriteDetailedLogs;

        public static MissionRunOptions Create(int seed)
        {
            return new MissionRunOptions
            {
                BatchId = "batch-" + seed.ToString("D8", CultureInfo.InvariantCulture),
                Seed = seed,
                OutputDirectory = string.Empty,
                MaximumSimulationTimeS = 0.0,
                WriteDetailedLogs = true
            };
        }
    }

    [Serializable]
    public struct MissionRunReport
    {
        public string BatchId;
        public string RunId;
        public string MissionId;
        public int Seed;
        public bool Succeeded;
        public bool TimedOut;
        public bool HasException;
        public string ExceptionMessage;
        public string CompletionReason;
        public double FinalSimulationTimeS;
        public double WallClockTimeS;
        public double SimulationRate;
        public double FuelUsedKg;
        public double MinimumAglM;
        public double MaximumLoadFactorG;
        public double MaximumAngleOfAttackDeg;
        public int WarningCount;
        public int WeaponLaunchCount;
        public int WeaponHitCount;
        public int FriendlyLossCount;
        public int HostileLossCount;
        public bool TookOff;
        public bool Landed;
        public TerrainSource TerrainSource;
        public bool TerrainDataValid;
        public double FinalPlayerLongitudeRad;
        public double FinalPlayerLatitudeRad;
        public double FinalPlayerHeightM;
        public double FinalPlayerGroundSpeedMps;
        public MissionActorStatus FinalPlayerStatus;
        public MissionAiMode FinalPlayerAiMode;
        public ulong DeterminismHash;
        public string ReportDirectory;
    }

    public sealed class MissionBatchRunner
    {
        private const double TickDurationS = FlightSimulationService.FixedDeltaTimeS;
        private static readonly AnalyticRunway KtexAnalyticRunway = new GroundContactModel().Runway;

        public MissionRunReport RunSingle(
            MissionDefinition mission,
            in MissionRunOptions options,
            ITerrainQuery terrain)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var report = new MissionRunReport
            {
                BatchId = options.BatchId ?? string.Empty,
                MissionId = mission?.MissionId ?? string.Empty,
                Seed = options.Seed,
                MinimumAglM = double.PositiveInfinity,
                TerrainSource = terrain?.Source ?? TerrainSource.None,
                TerrainDataValid = terrain != null,
                DeterminismHash = 14695981039346656037UL
            };
            MissionRunObserver observer = null;
            MissionDetailedLogWriter logs = null;
            try
            {
                MissionValidationResult validation = MissionDefinitionValidator.Validate(mission);
                if (!validation.IsValid)
                    throw new InvalidOperationException(validation.FirstError);

                string reportDirectory = CreateRunDirectory(mission, in options);
                report.ReportDirectory = reportDirectory;
                if (options.WriteDetailedLogs && !string.IsNullOrEmpty(reportDirectory))
                    logs = new MissionDetailedLogWriter(reportDirectory);

                FlightSimulationService service = new FlightSimulationService();
                MissionDirector director = new MissionDirector(service);
                observer = new MissionRunObserver(logs);
                service.RegisterSink(observer);
                director.RegisterSink(observer);
                Require(director.Load(mission, options.Seed));
                Require(director.Start());
                Require(director.SetPlayerAutomation(true, AutomationMode.Batch));
                report.RunId = director.LatestState.RunId;
                director.ConfigureAutomationRun(
                    options.BatchId,
                    reportDirectory,
                    terrain?.Source ?? TerrainSource.None,
                    terrain != null);

                double initialFuelKg = SumFuel(service, mission);
                bool playerWeightOnWheelsInitialized = false;
                bool playerWasWeightOnWheels = false;
                double maximumTimeS = options.MaximumSimulationTimeS > 0.0
                    ? Math.Min(options.MaximumSimulationTimeS, mission.MaximumDurationS)
                    : mission.MaximumDurationS;
                int maximumTicks = Math.Max(1, (int)Math.Ceiling(maximumTimeS / TickDurationS));
                for (int tick = 0; tick < maximumTicks; tick++)
                {
                    ApplyTerrainSamples(service, director, mission, terrain, ref report);
                    director.UpdateTerrainStatus(report.TerrainSource, report.TerrainDataValid);
                    director.Tick();
                    if (tick % 20 == 0)
                    {
                        double wallS = stopwatch.Elapsed.TotalSeconds;
                        director.UpdateAutomationTiming(
                            wallS,
                            wallS > 1e-9 ? director.LatestState.SimulationTimeS / wallS : 0.0);
                    }
                    SampleMetrics(
                        service,
                        director,
                        mission,
                        tick,
                        observer,
                        logs,
                        ref playerWeightOnWheelsInitialized,
                        ref playerWasWeightOnWheels,
                        ref report);
                    MissionPhase phase = director.LatestState.Phase;
                    if (phase == MissionPhase.Succeeded || phase == MissionPhase.Failed || phase == MissionPhase.Aborted)
                        break;
                }

                MissionState finalState = director.LatestState;
                report.Succeeded = finalState.Phase == MissionPhase.Succeeded;
                report.TimedOut = finalState.Phase == MissionPhase.Running;
                report.CompletionReason = report.TimedOut
                    ? "Batch time limit reached."
                    : finalState.CompletionReason;
                report.FinalSimulationTimeS = finalState.SimulationTimeS;
                report.FuelUsedKg = Math.Max(0.0, initialFuelKg - SumFuel(service, mission));
                report.WarningCount = observer.WarningCount;
                report.WeaponLaunchCount = observer.WeaponLaunchCount;
                report.WeaponHitCount = observer.WeaponHitCount;
                report.FriendlyLossCount = CountLosses(director, AircraftSide.Friendly);
                report.HostileLossCount = CountLosses(director, AircraftSide.Hostile);
                report.DeterminismHash = BuildFinalHash(service, director, mission, observer.Hash);
                CaptureFinalPlayer(service, director, mission, ref report);
                if (double.IsPositiveInfinity(report.MinimumAglM)) report.MinimumAglM = 0.0;
            }
            catch (Exception exception)
            {
                report.HasException = true;
                report.ExceptionMessage = exception.ToString();
                report.CompletionReason = exception.Message;
            }
            finally
            {
                logs?.Dispose();
                stopwatch.Stop();
                report.WallClockTimeS = stopwatch.Elapsed.TotalSeconds;
                report.SimulationRate = report.WallClockTimeS > 1e-9
                    ? report.FinalSimulationTimeS / report.WallClockTimeS
                    : 0.0;
                WriteRunReport(in report);
            }
            return report;
        }

        public MissionRunReport[] RunBatch(
            MissionDefinition[] missions,
            string profile,
            int runsPerMission,
            int seedStart,
            string outputDirectory,
            ITerrainQuery terrain)
        {
            MissionDefinition[] definitions = missions ?? Array.Empty<MissionDefinition>();
            int runCount = Math.Max(1, runsPerMission);
            MissionRunReport[] reports = new MissionRunReport[definitions.Length * runCount];
            string batchId = (profile ?? "batch") + "-" + seedStart.ToString("D8", CultureInfo.InvariantCulture);
            int outputIndex = 0;
            for (int missionIndex = 0; missionIndex < definitions.Length; missionIndex++)
            {
                for (int runIndex = 0; runIndex < runCount; runIndex++)
                {
                    MissionRunOptions options = MissionRunOptions.Create(seedStart + runIndex);
                    options.BatchId = batchId;
                    options.OutputDirectory = outputDirectory ?? string.Empty;
                    options.WriteDetailedLogs = true;
                    reports[outputIndex++] = RunSingle(definitions[missionIndex], in options, terrain);
                }
            }
            MissionBatchReportWriter.Write(outputDirectory, batchId, profile, reports);
            return reports;
        }

        private static void ApplyTerrainSamples(
            FlightSimulationService service,
            MissionDirector director,
            MissionDefinition mission,
            ITerrainQuery terrain,
            ref MissionRunReport report)
        {
            bool anyValid = false;
            for (int index = 0; index < mission.Actors.Length; index++)
            {
                AircraftId aircraft = new AircraftId(mission.Actors[index].AircraftId);
                if (!service.TryGetLatest(aircraft, out AircraftSnapshot snapshot)) continue;
                double terrainHeightM = 0.0;
                bool valid = terrain != null && terrain.TryGetHeightM(
                    snapshot.Fast.LongitudeRad,
                    snapshot.Fast.LatitudeRad,
                    out terrainHeightM);
                double aglM = valid ? Math.Max(0.0, snapshot.Fast.EllipsoidHeightM - terrainHeightM) : 0.0;
                service.SetTerrainSample(aircraft, valid, aglM, 0.0);
                bool collisionSurface = terrain == null ||
                                        terrain.Source != TerrainSource.AnalyticRunway ||
                                        IsInsideAnalyticRunway(in snapshot.Fast);
                if (valid && collisionSurface && snapshot.Fast.EllipsoidHeightM < terrainHeightM - 2.0)
                    director.ReportTerrainCollision(aircraft, terrainHeightM);
                anyValid |= valid;
            }
            report.TerrainDataValid = anyValid;
        }

        private static bool IsInsideAnalyticRunway(in AircraftFastState fast)
        {
            DVector3 positionEcefM = new DVector3(
                fast.EcefPositionXM,
                fast.EcefPositionYM,
                fast.EcefPositionZM);
            return KtexAnalyticRunway.ContainsPlanarPoint(in positionEcefM);
        }

        private static void SampleMetrics(
            FlightSimulationService service,
            MissionDirector director,
            MissionDefinition mission,
            int tick,
            MissionRunObserver observer,
            MissionDetailedLogWriter logs,
            ref bool playerWeightOnWheelsInitialized,
            ref bool playerWasWeightOnWheels,
            ref MissionRunReport report)
        {
            for (int index = 0; index < mission.Actors.Length; index++)
            {
                AircraftId aircraft = new AircraftId(mission.Actors[index].AircraftId);
                if (!service.TryGetLatest(aircraft, out AircraftSnapshot snapshot)) continue;
                AircraftFastState fast = snapshot.Fast;
                if (!IsFinite(fast.EcefPositionXM) || !IsFinite(fast.TrueAirspeedMps) || !IsFinite(fast.NormalLoadFactorG))
                    throw new InvalidOperationException($"Aircraft '{aircraft.Value}' produced a non-finite state.");
                if (director.TryGetActorState(aircraft, out MissionActorState actor) &&
                    actor.Status != MissionActorStatus.Active &&
                    actor.Status != MissionActorStatus.Landed)
                {
                    continue;
                }
                if (fast.TerrainSampleValid)
                    report.MinimumAglM = Math.Min(report.MinimumAglM, fast.AboveGroundLevelAltitudeM);
                report.MaximumLoadFactorG = Math.Max(report.MaximumLoadFactorG, Math.Abs(fast.NormalLoadFactorG));
                report.MaximumAngleOfAttackDeg = Math.Max(report.MaximumAngleOfAttackDeg, Math.Abs(fast.AngleOfAttackRad * 180.0 / Math.PI));
                if (mission.Actors[index].IsPlayer)
                {
                    bool weightOnWheels = snapshot.Systems.LandingGear.WeightOnWheels;
                    if (playerWeightOnWheelsInitialized)
                    {
                        if (playerWasWeightOnWheels && !weightOnWheels) report.TookOff = true;
                        if (!playerWasWeightOnWheels && weightOnWheels) report.Landed = true;
                    }
                    playerWasWeightOnWheels = weightOnWheels;
                    playerWeightOnWheelsInitialized = true;
                }
                if (tick % 2 == 0) logs?.WriteFlight(in snapshot);
                if (tick % 10 == 0)
                {
                    logs?.WriteSystems(in snapshot);
                    if (director.TryGetCombatState(aircraft, out AircraftCombatState combat)) logs?.WriteCombat(in combat);
                }
                if (tick % 20 == 0) logs?.WriteTactical(in snapshot);
            }
            if (tick % 20 == 0 && director.TryGetLatest(out MissionSnapshot missionSnapshot))
                logs?.WriteMission(in missionSnapshot);
        }

        private static double SumFuel(FlightSimulationService service, MissionDefinition mission)
        {
            double total = 0.0;
            for (int index = 0; index < mission.Actors.Length; index++)
                if (service.TryGetLatest(new AircraftId(mission.Actors[index].AircraftId), out AircraftSnapshot snapshot))
                    total += snapshot.Systems.Fuel.TotalFuelKg;
            return total;
        }

        private static int CountLosses(MissionDirector director, AircraftSide side)
        {
            int count = 0;
            MissionDefinition mission = director.Definition;
            for (int index = 0; index < mission.Actors.Length; index++)
            {
                if (mission.Actors[index].Side != side) continue;
                if (director.TryGetActorState(new AircraftId(mission.Actors[index].AircraftId), out MissionActorState actor) &&
                    (actor.Status == MissionActorStatus.Neutralized || actor.Status == MissionActorStatus.Failed)) count++;
            }
            return count;
        }

        private static void CaptureFinalPlayer(
            FlightSimulationService service,
            MissionDirector director,
            MissionDefinition mission,
            ref MissionRunReport report)
        {
            for (int index = 0; index < mission.Actors.Length; index++)
            {
                if (!mission.Actors[index].IsPlayer) continue;
                AircraftId player = new AircraftId(mission.Actors[index].AircraftId);
                if (service.TryGetLatest(player, out AircraftSnapshot snapshot))
                {
                    report.FinalPlayerLongitudeRad = snapshot.Fast.LongitudeRad;
                    report.FinalPlayerLatitudeRad = snapshot.Fast.LatitudeRad;
                    report.FinalPlayerHeightM = snapshot.Fast.EllipsoidHeightM;
                    report.FinalPlayerGroundSpeedMps = snapshot.Fast.GroundSpeedMps;
                }
                if (director.TryGetActorState(player, out MissionActorState actor))
                {
                    report.FinalPlayerStatus = actor.Status;
                    report.FinalPlayerAiMode = actor.AiMode;
                }
                return;
            }
        }

        private static ulong BuildFinalHash(
            FlightSimulationService service,
            MissionDirector director,
            MissionDefinition mission,
            ulong eventHash)
        {
            ulong hash = eventHash == 0UL ? 14695981039346656037UL : eventHash;
            MissionState missionState = director.LatestState;
            hash = Hash(hash, (long)missionState.Phase);
            hash = Hash(hash, (long)missionState.Result);
            hash = Hash(hash, (long)missionState.Tick);
            hash = Hash(hash, missionState.CompletionReason);
            if (director.TryGetLatest(out MissionSnapshot missionSnapshot))
            {
                for (int index = 0; index < missionSnapshot.Actors.Length; index++)
                {
                    MissionActorState actor = missionSnapshot.Actors[index];
                    hash = Hash(hash, actor.Aircraft.Value);
                    hash = Hash(hash, (long)actor.Status);
                    hash = Hash(hash, (long)actor.AiMode);
                    hash = Hash(hash, (long)actor.ControlAuthority);
                    hash = Hash(hash, actor.CurrentWaypointIndex);
                    hash = Hash(hash, actor.SelectedTarget.Value);
                    hash = Hash(hash, actor.IsDetected ? 1L : 0L);
                    hash = Hash(hash, actor.IsEngaged ? 1L : 0L);
                    if (director.TryGetCombatState(actor.Aircraft, out AircraftCombatState combat))
                    {
                        hash = Hash(hash, (long)combat.MasterArm);
                        hash = Hash(hash, combat.SelectedStationIndex);
                        hash = Hash(hash, combat.SelectedStoreType);
                        hash = Hash(hash, combat.SelectedStoreQuantity);
                        hash = Hash(hash, combat.SelectedTarget.Value);
                        hash = Hash(hash, (long)combat.RadarTrackState);
                        hash = Hash(hash, combat.MissileLaunchWarning ? 1L : 0L);
                    }
                }
                for (int index = 0; index < missionSnapshot.Objectives.Length; index++)
                {
                    MissionObjectiveState objective = missionSnapshot.Objectives[index];
                    hash = Hash(hash, objective.ObjectiveId);
                    hash = Hash(hash, (long)objective.Status);
                    hash = Hash(hash, Quantize(objective.ProgressNormalized, 1e-6));
                    hash = Hash(hash, objective.CurrentCount);
                    hash = Hash(hash, objective.FailureReason);
                }
            }
            for (int index = 0; index < mission.Actors.Length; index++)
            {
                AircraftId aircraft = new AircraftId(mission.Actors[index].AircraftId);
                hash = Hash(hash, aircraft.Value);
                if (!service.TryGetLatest(aircraft, out AircraftSnapshot snapshot)) continue;
                hash = Hash(hash, Quantize(snapshot.Fast.EcefPositionXM, 0.01));
                hash = Hash(hash, Quantize(snapshot.Fast.EcefPositionYM, 0.01));
                hash = Hash(hash, Quantize(snapshot.Fast.EcefPositionZM, 0.01));
                hash = Hash(hash, Quantize(snapshot.Fast.EcefVelocityXMps, 0.001));
                hash = Hash(hash, Quantize(snapshot.Fast.EcefVelocityYMps, 0.001));
                hash = Hash(hash, Quantize(snapshot.Fast.EcefVelocityZMps, 0.001));
                hash = Hash(hash, Quantize(snapshot.Fast.BodyAngularVelocityXRadps, 1e-6));
                hash = Hash(hash, Quantize(snapshot.Fast.BodyAngularVelocityYRadps, 1e-6));
                hash = Hash(hash, Quantize(snapshot.Fast.BodyAngularVelocityZRadps, 1e-6));
                hash = Hash(hash, Quantize(snapshot.Fast.BodyToEcefQuaternionX, 1e-9));
                hash = Hash(hash, Quantize(snapshot.Fast.BodyToEcefQuaternionY, 1e-9));
                hash = Hash(hash, Quantize(snapshot.Fast.BodyToEcefQuaternionZ, 1e-9));
                hash = Hash(hash, Quantize(snapshot.Fast.BodyToEcefQuaternionW, 1e-9));
                hash = Hash(hash, Quantize(snapshot.Fast.TrueAirspeedMps, 0.001));
                hash = Hash(hash, Quantize(snapshot.Fast.AngleOfAttackRad, 1e-6));
                hash = Hash(hash, Quantize(snapshot.Fast.SideslipRad, 1e-6));
                hash = Hash(hash, Quantize(snapshot.Fast.NormalLoadFactorG, 1e-4));
                hash = Hash(hash, Quantize(snapshot.Fast.AboveGroundLevelAltitudeM, 0.01));
                hash = Hash(hash, snapshot.Fast.TerrainSampleValid ? 1L : 0L);
                FlightControlState controls = snapshot.Systems.FlightControls;
                hash = Hash(hash, (long)controls.ActiveControlAuthority);
                hash = Hash(hash, Quantize(controls.PitchCommandNormalized, 1e-6));
                hash = Hash(hash, Quantize(controls.RollCommandNormalized, 1e-6));
                hash = Hash(hash, Quantize(controls.YawCommandNormalized, 1e-6));
                hash = Hash(hash, controls.FlightControlComputerEnabled ? 1L : 0L);
                hash = Hash(hash, (long)snapshot.Systems.Propulsion.Mode);
                hash = Hash(hash, Quantize(snapshot.Systems.Propulsion.N1Percent, 1e-4));
                hash = Hash(hash, Quantize(snapshot.Systems.Propulsion.ThrustN, 0.1));
                hash = Hash(hash, Quantize(snapshot.Systems.Fuel.TotalFuelKg, 0.001));
                hash = Hash(hash, Quantize(snapshot.Systems.Fuel.FuelImbalanceKg, 0.001));
                hash = Hash(hash, Quantize(snapshot.Systems.Fuel.CenterOfGravityPercentMac, 1e-6));
                hash = Hash(hash, Quantize(snapshot.Systems.Electrical.MainBusVoltageV, 1e-4));
                hash = Hash(hash, snapshot.Systems.Electrical.GeneratorOnline ? 1L : 0L);
                hash = Hash(hash, Quantize(snapshot.Systems.Hydraulics.SystemAPressurePa, 1.0));
                hash = Hash(hash, Quantize(snapshot.Systems.Hydraulics.SystemBPressurePa, 1.0));
                hash = Hash(hash, snapshot.Systems.Hydraulics.SystemAOnline ? 1L : 0L);
                hash = Hash(hash, snapshot.Systems.Hydraulics.SystemBOnline ? 1L : 0L);
                hash = Hash(hash, Quantize(snapshot.Systems.LandingGear.NoseGearPositionNormalized, 1e-6));
                hash = Hash(hash, Quantize(snapshot.Systems.LandingGear.SpeedBrakePositionNormalized, 1e-6));
                hash = Hash(hash, snapshot.Systems.LandingGear.WeightOnWheels ? 1L : 0L);
                for (int stationIndex = 0; stationIndex < StoresState.StationCapacity; stationIndex++)
                {
                    StoreStationState station = snapshot.Systems.Stores.Stations[stationIndex];
                    hash = Hash(hash, station.StoreType);
                    hash = Hash(hash, station.Quantity);
                    hash = Hash(hash, Quantize(station.StoreMassKg, 0.001));
                    hash = Hash(hash, Quantize(station.DragCoefficient, 1e-6));
                    hash = Hash(hash, station.IsReady ? 1L : 0L);
                    hash = Hash(hash, station.IsReleased ? 1L : 0L);
                }
                hash = Hash(hash, (long)snapshot.Systems.Avionics.InsState);
                hash = Hash(hash, (long)snapshot.Systems.Avionics.HudMode);
                hash = Hash(hash, (long)snapshot.Systems.Avionics.StartupState);
                hash = Hash(hash, snapshot.Systems.Avionics.RadarEnabled ? 1L : 0L);
                hash = Hash(hash, snapshot.Systems.Avionics.DataLinkEnabled ? 1L : 0L);
                WarningState warnings = snapshot.Systems.Warnings;
                hash = Hash(hash, WarningMask(in warnings));
            }
            return hash;
        }

        private static long WarningMask(in WarningState warning)
        {
            long mask = 0;
            if (warning.MasterCaution) mask |= 1L << 0;
            if (warning.MasterWarning) mask |= 1L << 1;
            if (warning.FireWarning) mask |= 1L << 2;
            if (warning.HydraulicWarning) mask |= 1L << 3;
            if (warning.ElectricalWarning) mask |= 1L << 4;
            if (warning.FuelWarning) mask |= 1L << 5;
            if (warning.LowAltitudeWarning) mask |= 1L << 6;
            if (warning.OverspeedWarning) mask |= 1L << 7;
            if (warning.StallWarning) mask |= 1L << 8;
            if (warning.LandingGearWarning) mask |= 1L << 9;
            if (warning.CanopyWarning) mask |= 1L << 10;
            return mask;
        }

        private static string CreateRunDirectory(MissionDefinition mission, in MissionRunOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.OutputDirectory)) return string.Empty;
            string directory = Path.Combine(
                Path.GetFullPath(options.OutputDirectory),
                Sanitize(options.BatchId),
                Sanitize(mission.MissionId + "-" + options.Seed.ToString("D8", CultureInfo.InvariantCulture)));
            Directory.CreateDirectory(directory);
            return directory;
        }

        private static void WriteRunReport(in MissionRunReport report)
        {
            if (string.IsNullOrEmpty(report.ReportDirectory)) return;
            File.WriteAllText(Path.Combine(report.ReportDirectory, "run-report.json"), MissionBatchReportWriter.ToJson(in report));
        }

        private static void Require(CommandResult result)
        {
            if (!result.Accepted) throw new InvalidOperationException(result.Message);
        }

        private static bool IsFinite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);
        private static long Quantize(double value, double resolution) => (long)Math.Round(value / resolution);
        private static ulong Hash(ulong hash, long value)
        {
            unchecked
            {
                ulong data = (ulong)value;
                for (int index = 0; index < 8; index++)
                {
                    hash ^= (byte)(data & 0xffUL);
                    hash *= 1099511628211UL;
                    data >>= 8;
                }
                return hash;
            }
        }

        internal static ulong Hash(ulong hash, string value)
        {
            unchecked
            {
                string text = value ?? string.Empty;
                for (int index = 0; index < text.Length; index++)
                {
                    hash ^= text[index];
                    hash *= 1099511628211UL;
                }
                return hash;
            }
        }

        private static string Sanitize(string value)
        {
            string result = value ?? string.Empty;
            foreach (char invalid in Path.GetInvalidFileNameChars()) result = result.Replace(invalid, '_');
            return string.IsNullOrEmpty(result) ? "run" : result;
        }
    }

    internal sealed class MissionRunObserver : IFlightTelemetrySink, IMissionTelemetrySink
    {
        private readonly MissionDetailedLogWriter logs;
        private readonly AircraftId[] warningAircraft = new AircraftId[FlightSimulationService.MaximumAircraft];
        private readonly long[] warningMasks = new long[FlightSimulationService.MaximumAircraft];
        private int warningAircraftCount;
        public int WarningCount { get; private set; }
        public int WeaponLaunchCount { get; private set; }
        public int WeaponHitCount { get; private set; }
        public ulong Hash { get; private set; } = 14695981039346656037UL;
        private double missionTimeS;

        public MissionRunObserver(MissionDetailedLogWriter logs) { this.logs = logs; }
        public void OnFastState(in AircraftFastState state) { }
        public void OnSystemsState(in AircraftSystemsState state)
        {
            int index = FindWarningAircraft(state.Aircraft);
            long mask = MissionBatchRunnerWarningMask(in state.Warnings);
            long activated = mask & ~warningMasks[index];
            while (activated != 0)
            {
                WarningCount += (int)(activated & 1L);
                activated >>= 1;
            }
            warningMasks[index] = mask;
        }
        public void OnTacticalPictureState(in TacticalPictureState state) { }
        public void OnSimulationEvent(in SimulationEvent simulationEvent)
        {
            Hash = MissionBatchRunner.Hash(Hash, simulationEvent.Type + ":" + simulationEvent.Aircraft.Value + ":" + simulationEvent.Code);
            logs?.WriteSimulationEvent(in simulationEvent);
        }
        public void OnMissionState(in MissionState state) { missionTimeS = state.SimulationTimeS; }
        public void OnMissionActorState(in MissionActorState state) => logs?.WriteActor(missionTimeS, in state);
        public void OnMissionObjectiveState(in MissionObjectiveState state) => logs?.WriteObjective(missionTimeS, in state);
        public void OnAircraftCombatState(in AircraftCombatState state) { }
        public void OnAutomationRunState(in AutomationRunState state) => logs?.WriteAutomation(in state);
        public void OnMissionEvent(in MissionEvent missionEvent)
        {
            Hash = MissionBatchRunner.Hash(Hash, missionEvent.Type + ":" + missionEvent.Source.Value + ":" + missionEvent.Target.Value);
            logs?.WriteMissionEvent(in missionEvent);
        }
        public void OnWeaponEngagementEvent(in WeaponEngagementEvent engagementEvent)
        {
            WeaponLaunchCount++;
            if (engagementEvent.Outcome == WeaponEngagementOutcome.Hit) WeaponHitCount++;
            Hash = MissionBatchRunner.Hash(Hash, engagementEvent.EngagementSequence + ":" + engagementEvent.Outcome);
            logs?.WriteEngagement(in engagementEvent);
        }

        private int FindWarningAircraft(AircraftId aircraft)
        {
            for (int index = 0; index < warningAircraftCount; index++)
                if (warningAircraft[index] == aircraft) return index;
            int added = warningAircraftCount++;
            warningAircraft[added] = aircraft;
            return added;
        }

        private static long MissionBatchRunnerWarningMask(in WarningState warning)
        {
            long mask = 0;
            if (warning.FireWarning) mask |= 1L << 0;
            if (warning.HydraulicWarning) mask |= 1L << 1;
            if (warning.ElectricalWarning) mask |= 1L << 2;
            if (warning.FuelWarning) mask |= 1L << 3;
            if (warning.LowAltitudeWarning) mask |= 1L << 4;
            if (warning.OverspeedWarning) mask |= 1L << 5;
            if (warning.StallWarning) mask |= 1L << 6;
            if (warning.LandingGearWarning) mask |= 1L << 7;
            if (warning.CanopyWarning) mask |= 1L << 8;
            return mask;
        }
    }

    internal sealed class MissionDetailedLogWriter : IDisposable
    {
        private readonly StreamWriter flight;
        private readonly StreamWriter systems;
        private readonly StreamWriter tactical;
        private readonly StreamWriter combat;
        private readonly StreamWriter mission;
        private readonly StreamWriter actors;
        private readonly StreamWriter objectives;
        private readonly StreamWriter automation;
        private readonly StreamWriter events;

        public MissionDetailedLogWriter(string directory)
        {
            flight = Open(directory, "flight-50hz.csv", "time_s,aircraft,lon_rad,lat_rad,height_m,tas_mps,cas_mps,mach,aoa_rad,g,agl_m,terrain_valid");
            systems = Open(directory, "systems-10hz.csv", "time_s,aircraft,fuel_kg,engine_mode,rpm,thrust_n,hyd_a_psi,hyd_b_psi,gear_wow,master_caution,master_warning");
            tactical = Open(directory, "tactical-5hz.csv", "time_s,aircraft,track_count,waypoint_count,formation_mode");
            combat = Open(directory, "combat-10hz.csv", "time_s,aircraft,target,track_state,range_m,closure_mps,lock_quality,in_launch_zone,shoot_cue,weapon,quantity,master_arm");
            mission = Open(directory, "mission-5hz.csv", "time_s,mission,phase,result,active_objectives,succeeded_objectives,failed_objectives,automation,authority");
            actors = Open(directory, "mission-actors-5hz.csv", "time_s,aircraft,callsign,side,role,status,ai_mode,control_authority,leader,formation_slot,ai_skill_normalized,waypoint_index,target,detected,engaged,valid");
            objectives = Open(directory, "mission-objectives-5hz.csv", "time_s,objective_id,name,type,status,progress,current_count,required_count,deadline_s,failure_reason");
            automation = Open(directory, "automation-5hz.csv", "time_s,batch_id,run_id,mission_id,seed,mode,status,authority,wall_time_s,simulation_rate,terrain_source,terrain_valid,completion_reason,report_path");
            events = Open(directory, "events.csv", "time_s,domain,type,source,target,code,message");
        }

        public void WriteFlight(in AircraftSnapshot snapshot)
        {
            AircraftFastState s = snapshot.Fast;
            flight.WriteLine(FormattableString.Invariant($"{s.SimulationTimeS:R},{Csv(snapshot.Aircraft.Value)},{s.LongitudeRad:R},{s.LatitudeRad:R},{s.EllipsoidHeightM:R},{s.TrueAirspeedMps:R},{s.CalibratedAirspeedMps:R},{s.Mach:R},{s.AngleOfAttackRad:R},{s.NormalLoadFactorG:R},{s.AboveGroundLevelAltitudeM:R},{s.TerrainSampleValid}"));
        }

        public void WriteSystems(in AircraftSnapshot snapshot)
        {
            AircraftSystemsState s = snapshot.Systems;
            systems.WriteLine(FormattableString.Invariant($"{s.SimulationTimeS:R},{Csv(snapshot.Aircraft.Value)},{s.Fuel.TotalFuelKg:R},{s.Propulsion.Mode},{s.Propulsion.N1Percent:R},{s.Propulsion.ThrustN:R},{s.Hydraulics.SystemAPressurePa:R},{s.Hydraulics.SystemBPressurePa:R},{s.LandingGear.WeightOnWheels},{s.Warnings.MasterCaution},{s.Warnings.MasterWarning}"));
        }

        public void WriteTactical(in AircraftSnapshot snapshot)
        {
            TacticalPictureState s = snapshot.Tactical;
            tactical.WriteLine(FormattableString.Invariant($"{s.SimulationTimeS:R},{Csv(snapshot.Aircraft.Value)},{CountTracks(in s.Tracks)},{CountWaypoints(in s.Waypoints)},{s.Formation.Mode}"));
        }

        public void WriteCombat(in AircraftCombatState s)
        {
            combat.WriteLine(FormattableString.Invariant($"{s.SimulationTimeS:R},{Csv(s.Aircraft.Value)},{Csv(s.SelectedTarget.Value)},{s.RadarTrackState},{s.TargetRangeM:R},{s.ClosureRateMps:R},{s.LockQualityNormalized:R},{s.InLaunchZone},{s.ShootCue},{Csv(s.SelectedStoreType)},{s.SelectedStoreQuantity},{s.MasterArm}"));
        }

        public void WriteMission(in MissionSnapshot s)
        {
            MissionState m = s.Mission;
            mission.WriteLine(FormattableString.Invariant($"{m.SimulationTimeS:R},{Csv(m.MissionId)},{m.Phase},{m.Result},{m.ActiveObjectiveCount},{m.SucceededObjectiveCount},{m.FailedObjectiveCount},{s.Automation.Mode},{s.Automation.PlayerControlAuthority}"));
        }

        public void WriteActor(double simulationTimeS, in MissionActorState s)
        {
            actors.WriteLine(FormattableString.Invariant(
                $"{simulationTimeS:R},{Csv(s.Aircraft.Value)},{Csv(s.Callsign)},{s.Side},{s.Role},{s.Status},{s.AiMode},{s.ControlAuthority},{Csv(s.LeaderAircraft.Value)},{s.FormationSlot},{s.AiSkillNormalized:R},{s.CurrentWaypointIndex},{Csv(s.SelectedTarget.Value)},{s.IsDetected},{s.IsEngaged},{s.IsValid}"));
        }

        public void WriteObjective(double simulationTimeS, in MissionObjectiveState s)
        {
            objectives.WriteLine(FormattableString.Invariant(
                $"{simulationTimeS:R},{Csv(s.ObjectiveId)},{Csv(s.DisplayName)},{s.Type},{s.Status},{s.ProgressNormalized:R},{s.CurrentCount},{s.RequiredCount},{s.DeadlineS:R},{Csv(s.FailureReason)}"));
        }

        public void WriteAutomation(in AutomationRunState s)
        {
            automation.WriteLine(FormattableString.Invariant(
                $"{s.SimulationTimeS:R},{Csv(s.BatchId)},{Csv(s.RunId)},{Csv(s.MissionId)},{s.Seed},{s.Mode},{s.Status},{s.PlayerControlAuthority},{s.WallClockTimeS:R},{s.SimulationRate:R},{s.TerrainSource},{s.TerrainDataValid},{Csv(s.CompletionReason)},{Csv(s.ReportPath)}"));
        }

        public void WriteSimulationEvent(in SimulationEvent e) => events.WriteLine(FormattableString.Invariant($"{e.SimulationTimeS:R},flight,{e.Type},{Csv(e.Aircraft.Value)},,{e.Code},{Csv(e.Message)}"));
        public void WriteMissionEvent(in MissionEvent e) => events.WriteLine(FormattableString.Invariant($"{e.SimulationTimeS:R},mission,{e.Type},{Csv(e.Source.Value)},{Csv(e.Target.Value)},{e.Code},{Csv(e.Message)}"));
        public void WriteEngagement(in WeaponEngagementEvent e) => events.WriteLine(FormattableString.Invariant($"{e.SimulationTimeS:R},weapon,{e.Outcome},{Csv(e.Shooter.Value)},{Csv(e.Target.Value)},{e.EngagementSequence},{Csv(e.WeaponType + " " + e.Reason)}"));

        public void Dispose()
        {
            flight.Dispose(); systems.Dispose(); tactical.Dispose(); combat.Dispose(); mission.Dispose();
            actors.Dispose(); objectives.Dispose(); automation.Dispose(); events.Dispose();
        }

        private static StreamWriter Open(string directory, string file, string header)
        {
            var writer = new StreamWriter(Path.Combine(directory, file), false, new UTF8Encoding(false));
            writer.WriteLine(header);
            return writer;
        }

        private static int CountTracks(in TacticalTrackCollection tracks)
        {
            int count = 0;
            for (int index = 0; index < tracks.Length; index++) if (tracks[index].IsValid) count++;
            return count;
        }

        private static int CountWaypoints(in WaypointCollection waypoints)
        {
            int count = 0;
            for (int index = 0; index < waypoints.Length; index++) if (waypoints[index].IsValid) count++;
            return count;
        }

        private static string Csv(string value) => "\"" + (value ?? string.Empty).Replace("\"", "\"\"") + "\"";
    }

    public static class MissionBatchPolicy
    {
        public static bool IsFailure(string profile, in MissionRunReport report)
        {
            if (report.HasException || report.TimedOut)
                return true;

            return string.Equals(profile, "regression", StringComparison.OrdinalIgnoreCase) &&
                   !report.Succeeded;
        }
    }

    public static class MissionBatchReportWriter
    {
        public static void Write(
            string outputDirectory,
            string batchId,
            string profile,
            MissionRunReport[] reports)
        {
            if (string.IsNullOrWhiteSpace(outputDirectory)) return;
            string directory = Path.Combine(Path.GetFullPath(outputDirectory), batchId ?? "batch");
            Directory.CreateDirectory(directory);
            using (var csv = new StreamWriter(Path.Combine(directory, "batch-summary.csv"), false, new UTF8Encoding(false)))
            {
                csv.WriteLine("mission_id,run_id,seed,succeeded,timed_out,exception,sim_time_s,wall_time_s,rate,fuel_used_kg,min_agl_m,max_g,max_aoa_deg,warnings,launches,hits,friendly_losses,hostile_losses,took_off,landed,terrain_source,terrain_valid,player_status,player_ai_mode,player_lon_rad,player_lat_rad,player_height_m,player_ground_speed_mps,hash,reason");
                for (int index = 0; index < reports.Length; index++)
                {
                    MissionRunReport r = reports[index];
                    csv.WriteLine(FormattableString.Invariant($"{Csv(r.MissionId)},{Csv(r.RunId)},{r.Seed},{r.Succeeded},{r.TimedOut},{r.HasException},{r.FinalSimulationTimeS:R},{r.WallClockTimeS:R},{r.SimulationRate:R},{r.FuelUsedKg:R},{r.MinimumAglM:R},{r.MaximumLoadFactorG:R},{r.MaximumAngleOfAttackDeg:R},{r.WarningCount},{r.WeaponLaunchCount},{r.WeaponHitCount},{r.FriendlyLossCount},{r.HostileLossCount},{r.TookOff},{r.Landed},{r.TerrainSource},{r.TerrainDataValid},{r.FinalPlayerStatus},{r.FinalPlayerAiMode},{r.FinalPlayerLongitudeRad:R},{r.FinalPlayerLatitudeRad:R},{r.FinalPlayerHeightM:R},{r.FinalPlayerGroundSpeedMps:R},{r.DeterminismHash},{Csv(r.CompletionReason)}"));
                }
            }
            var json = new StringBuilder();
            json.Append("{\"batchId\":\"").Append(Escape(batchId)).Append("\",\"runs\":[");
            for (int index = 0; index < reports.Length; index++)
            {
                if (index > 0) json.Append(',');
                json.Append(ToJson(in reports[index]));
            }
            json.Append("]}");
            File.WriteAllText(Path.Combine(directory, "batch-summary.json"), json.ToString(), new UTF8Encoding(false));
            WriteJUnit(Path.Combine(directory, "junit.xml"), profile, reports);
        }

        public static string ToJson(in MissionRunReport r)
        {
            return FormattableString.Invariant(
                $"{{\"missionId\":\"{Escape(r.MissionId)}\",\"runId\":\"{Escape(r.RunId)}\",\"seed\":{r.Seed},\"succeeded\":{Bool(r.Succeeded)},\"timedOut\":{Bool(r.TimedOut)},\"hasException\":{Bool(r.HasException)},\"simulationTimeS\":{r.FinalSimulationTimeS:R},\"wallClockTimeS\":{r.WallClockTimeS:R},\"simulationRate\":{r.SimulationRate:R},\"fuelUsedKg\":{r.FuelUsedKg:R},\"minimumAglM\":{r.MinimumAglM:R},\"maximumG\":{r.MaximumLoadFactorG:R},\"maximumAoADeg\":{r.MaximumAngleOfAttackDeg:R},\"warningCount\":{r.WarningCount},\"weaponLaunchCount\":{r.WeaponLaunchCount},\"weaponHitCount\":{r.WeaponHitCount},\"friendlyLossCount\":{r.FriendlyLossCount},\"hostileLossCount\":{r.HostileLossCount},\"tookOff\":{Bool(r.TookOff)},\"landed\":{Bool(r.Landed)},\"terrainSource\":\"{r.TerrainSource}\",\"terrainDataValid\":{Bool(r.TerrainDataValid)},\"finalPlayerStatus\":\"{r.FinalPlayerStatus}\",\"finalPlayerAiMode\":\"{r.FinalPlayerAiMode}\",\"finalPlayerLongitudeRad\":{r.FinalPlayerLongitudeRad:R},\"finalPlayerLatitudeRad\":{r.FinalPlayerLatitudeRad:R},\"finalPlayerHeightM\":{r.FinalPlayerHeightM:R},\"finalPlayerGroundSpeedMps\":{r.FinalPlayerGroundSpeedMps:R},\"determinismHash\":\"{r.DeterminismHash:X16}\",\"completionReason\":\"{Escape(r.CompletionReason)}\",\"exception\":\"{Escape(r.ExceptionMessage)}\"}}");
        }

        private static void WriteJUnit(string path, string profile, MissionRunReport[] reports)
        {
            int failures = 0;
            for (int index = 0; index < reports.Length; index++)
                if (MissionBatchPolicy.IsFailure(profile, in reports[index])) failures++;
            using (var writer = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                writer.WriteLine($"<testsuite name=\"FlightSim.Missions\" tests=\"{reports.Length}\" failures=\"{failures}\">");
                for (int index = 0; index < reports.Length; index++)
                {
                    MissionRunReport report = reports[index];
                    writer.Write($"  <testcase classname=\"{Xml(report.MissionId)}\" name=\"seed-{report.Seed}\" time=\"{report.WallClockTimeS.ToString("R", CultureInfo.InvariantCulture)}\">");
                    if (MissionBatchPolicy.IsFailure(profile, in report))
                        writer.Write($"<failure message=\"{Xml(report.CompletionReason)}\" />");
                    writer.WriteLine("</testcase>");
                }
                writer.WriteLine("</testsuite>");
            }
        }

        private static string Bool(bool value) => value ? "true" : "false";
        private static string Csv(string value) => "\"" + (value ?? string.Empty).Replace("\"", "\"\"") + "\"";
        private static string Escape(string value) => (value ?? string.Empty).Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n");
        private static string Xml(string value) => (value ?? string.Empty).Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;");
    }
}
