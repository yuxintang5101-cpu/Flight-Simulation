using System;

namespace FlightSim.Platform.Contracts
{
    [Serializable]
    public sealed class MissionDefinition
    {
        public int SchemaVersion = 1;
        public string MissionId = string.Empty;
        public string DisplayName = string.Empty;
        public string Briefing = string.Empty;
        public int DefaultSeed = 1000;
        public double MaximumDurationS = 900.0;
        public MissionActorDefinition[] Actors = Array.Empty<MissionActorDefinition>();
        public MissionObjectiveDefinition[] Objectives = Array.Empty<MissionObjectiveDefinition>();
    }

    [Serializable]
    public struct MissionActorDefinition
    {
        public string AircraftId;
        public string Callsign;
        public bool IsPlayer;
        public AircraftSide Side;
        public AircraftRole Role;
        public MissionAiMode InitialAiMode;
        public string LeaderAircraftId;
        public int FormationSlot;
        public double AiSkillNormalized;
        public double FormationForwardOffsetM;
        public double FormationRightOffsetM;
        public double FormationUpOffsetM;
        public string SimulationModelId;
        public string VisualModelId;
        public AircraftInitialCondition InitialCondition;
        public MissionWaypointDefinition[] Route;
        public MissionFailureDefinition[] InitialFailures;

        public static MissionActorDefinition Create(
            string aircraftId,
            bool isPlayer,
            AircraftSide side,
            AircraftRole role,
            in AircraftInitialCondition initialCondition)
        {
            return new MissionActorDefinition
            {
                AircraftId = aircraftId ?? string.Empty,
                Callsign = aircraftId ?? string.Empty,
                IsPlayer = isPlayer,
                Side = side,
                Role = role,
                InitialAiMode = isPlayer ? MissionAiMode.Manual : MissionAiMode.Formation,
                LeaderAircraftId = string.Empty,
                SimulationModelId = "F16_SEMI_REAL_V1",
                VisualModelId = "F35_VISUAL_V1",
                InitialCondition = initialCondition,
                Route = Array.Empty<MissionWaypointDefinition>(),
                InitialFailures = Array.Empty<MissionFailureDefinition>()
            };
        }
    }

    [Serializable]
    public struct MissionWaypointDefinition
    {
        public string WaypointId;
        public double LongitudeRad;
        public double LatitudeRad;
        public double EllipsoidHeightM;
        public double TargetTrueAirspeedMps;
        public double AcceptanceRadiusM;
    }

    [Serializable]
    public struct MissionFailureDefinition
    {
        public FailureType Type;
        public double SeverityNormalized;
        public double DurationS;
    }

    [Serializable]
    public struct MissionObjectiveDefinition
    {
        public string ObjectiveId;
        public string DisplayName;
        public MissionObjectiveType Type;
        public string SubjectAircraftId;
        public string TargetAircraftId;
        public int RequiredCount;
        public double TimeLimitS;
        public bool IsRequired;
    }

    [Serializable]
    public struct MissionState
    {
        public string MissionId;
        public string RunId;
        public int SchemaVersion;
        public int Seed;
        public MissionPhase Phase;
        public MissionResult Result;
        public ulong Tick;
        public double SimulationTimeS;
        public double ElapsedTimeS;
        public double RemainingTimeS;
        public int ActiveObjectiveCount;
        public int SucceededObjectiveCount;
        public int FailedObjectiveCount;
        public bool IsValid;
        public string CompletionReason;
    }

    [Serializable]
    public struct MissionActorState
    {
        public AircraftId Aircraft;
        public string Callsign;
        public AircraftSide Side;
        public AircraftRole Role;
        public MissionActorStatus Status;
        public MissionAiMode AiMode;
        public ControlAuthority ControlAuthority;
        public AircraftId LeaderAircraft;
        public int FormationSlot;
        public double AiSkillNormalized;
        public int CurrentWaypointIndex;
        public AircraftId SelectedTarget;
        public bool IsDetected;
        public bool IsEngaged;
        public bool IsValid;
    }

    [Serializable]
    public struct MissionObjectiveState
    {
        public string ObjectiveId;
        public string DisplayName;
        public MissionObjectiveType Type;
        public MissionObjectiveStatus Status;
        public double ProgressNormalized;
        public int CurrentCount;
        public int RequiredCount;
        public double DeadlineS;
        public string FailureReason;
    }

    [Serializable]
    public struct AircraftCombatState
    {
        public AircraftId Aircraft;
        public double SimulationTimeS;
        public MasterArmState MasterArm;
        public int SelectedStationIndex;
        public string SelectedStoreType;
        public int SelectedStoreQuantity;
        public int SelectedTrackId;
        public AircraftId SelectedTarget;
        public RadarTrackState RadarTrackState;
        public double TargetRangeM;
        public double ClosureRateMps;
        public double TargetBearingRad;
        public double TargetElevationRad;
        public double TargetAspectAngleRad;
        public double LockQualityNormalized;
        public bool InLaunchZone;
        public bool ShootCue;
        public bool MissileLaunchWarning;
        public bool IsValid;
    }

    [Serializable]
    public struct AutomationRunState
    {
        public string BatchId;
        public string RunId;
        public string MissionId;
        public int Seed;
        public AutomationMode Mode;
        public AutomationRunStatus Status;
        public ControlAuthority PlayerControlAuthority;
        public double SimulationTimeS;
        public double WallClockTimeS;
        public double SimulationRate;
        public TerrainSource TerrainSource;
        public bool TerrainDataValid;
        public string CompletionReason;
        public string ReportPath;
    }

    [Serializable]
    public struct MissionEvent
    {
        public string RunId;
        public string MissionId;
        public double SimulationTimeS;
        public MissionEventType Type;
        public AircraftId Source;
        public AircraftId Target;
        public int Code;
        public string Message;
    }

    [Serializable]
    public struct WeaponEngagementEvent
    {
        public string RunId;
        public int EngagementSequence;
        public double SimulationTimeS;
        public AircraftId Shooter;
        public AircraftId Target;
        public string WeaponType;
        public int StationIndex;
        public double LaunchRangeM;
        public double ClosureRateMps;
        public double LockQualityNormalized;
        public double TimeToImpactS;
        public WeaponEngagementOutcome Outcome;
        public string Reason;
    }

    [Serializable]
    public struct WeaponEngagementRequest
    {
        public AircraftId Shooter;
        public AircraftId Target;
        public string WeaponType;
        public int StationIndex;
        public double RangeM;
        public double ClosureRateMps;
        public double LockQualityNormalized;
        public double AspectAngleRad;
        public double ShooterSkillNormalized;
        public double TargetEvasionNormalized;
    }

    [Serializable]
    public struct WeaponEngagementDecision
    {
        public bool Accepted;
        public WeaponEngagementOutcome Outcome;
        public double TimeToImpactS;
        public double DeterministicScoreNormalized;
        public double ProbabilityOfHitNormalized;
        public string Reason;
    }

    [Serializable]
    public struct MissionSnapshot
    {
        public MissionState Mission;
        public MissionActorState[] Actors;
        public MissionObjectiveState[] Objectives;
        public AutomationRunState Automation;
    }

    public interface IMissionTelemetrySource
    {
        ushort SchemaVersion { get; }
        bool TryGetLatest(out MissionSnapshot snapshot);
        void RegisterSink(IMissionTelemetrySink sink);
        void UnregisterSink(IMissionTelemetrySink sink);
    }

    public interface IMissionTelemetrySink
    {
        void OnMissionState(in MissionState state);
        void OnMissionActorState(in MissionActorState state);
        void OnMissionObjectiveState(in MissionObjectiveState state);
        void OnAircraftCombatState(in AircraftCombatState state);
        void OnAutomationRunState(in AutomationRunState state);
        void OnMissionEvent(in MissionEvent missionEvent);
        void OnWeaponEngagementEvent(in WeaponEngagementEvent engagementEvent);
    }

    public interface IAircraftControlSource
    {
        ControlAuthority Authority { get; }
    }

    public interface ITerrainQuery
    {
        TerrainSource Source { get; }
        bool TryGetHeightM(double longitudeRad, double latitudeRad, out double ellipsoidHeightM);
    }

    public enum MissionPhase { None, Briefing, Running, Succeeded, Failed, Aborted }
    public enum MissionResult { None, Success, Failure, Aborted }
    public enum MissionObjectiveType { Takeoff, NeutralizeHostiles, HoldArea, ProtectAircraft, ReachWaypoint, LandAtKtex, Survive }
    public enum MissionObjectiveStatus { Pending, Active, Succeeded, Failed }
    public enum MissionActorStatus { Ready, Active, Neutralized, Landed, Failed, Removed }
    public enum MissionAiMode { Manual, GroundStart, Takeoff, Formation, Navigate, Patrol, Intercept, Engage, Evade, Escort, ReturnToBase, Approach, Land, Stop }
    public enum ControlAuthority { ManualPilot, AutomationPilot, FormationAI, MissionAI }
    public enum AutomationMode { Manual, Visible, Batch, Mixed }
    public enum AutomationRunStatus { None, Preparing, Running, Completed, Failed, Aborted }
    public enum WeaponEngagementOutcome { Rejected, Miss, Hit }
    public enum RadarTrackState { None, Detected, Tracked, Locked }
    public enum TerrainSource { None, Cesium, MissionCache, AnalyticRunway }
    public enum MissionEventType { Loaded, Started, PhaseChanged, ObjectiveChanged, ControlAuthorityChanged, Engagement, WeaponImpact, TerrainCollision, Completed, Failed, Aborted, ValidationFailed }
}
