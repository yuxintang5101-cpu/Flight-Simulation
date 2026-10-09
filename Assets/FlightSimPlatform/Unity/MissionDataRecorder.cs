using System;
using System.Globalization;
using System.IO;
using System.Text;
using FlightSim.Platform.Contracts;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    [DisallowMultipleComponent]
    public sealed class MissionDataRecorder : MonoBehaviour, IMissionTelemetrySink
    {
        [SerializeField] private FlightSimulationHost simulationHost;
        [SerializeField] private FlightDataRecorder flightRecorder;
        [SerializeField, Min(0.1f)] private float flushIntervalS = 1f;

        private IMissionTelemetrySource registeredSource;
        private StreamWriter missionWriter;
        private StreamWriter actorWriter;
        private StreamWriter objectiveWriter;
        private StreamWriter combatWriter;
        private StreamWriter automationWriter;
        private StreamWriter eventWriter;
        private string activeDirectory = string.Empty;
        private float nextFlushTime;

        private void OnEnable()
        {
            ResolveReferences();
            if (flightRecorder != null)
                flightRecorder.RecordingStateChanged += OnRecordingStateChanged;
            if (simulationHost != null)
                simulationHost.MissionLoaded += OnMissionLoaded;
            Rebind();
        }

        private void Update()
        {
            Rebind();
            EnsureWriters();
            if (missionWriter != null && Time.unscaledTime >= nextFlushTime)
            {
                Flush();
                nextFlushTime = Time.unscaledTime + Mathf.Max(0.1f, flushIntervalS);
            }
        }

        private void OnDisable()
        {
            if (flightRecorder != null)
                flightRecorder.RecordingStateChanged -= OnRecordingStateChanged;
            if (simulationHost != null)
                simulationHost.MissionLoaded -= OnMissionLoaded;
            registeredSource?.UnregisterSink(this);
            registeredSource = null;
            CloseWriters();
        }

        private void OnDestroy()
        {
            OnDisable();
        }

        public void OnMissionState(in MissionState state)
        {
            EnsureWriters();
            missionWriter?.WriteLine(FormattableString.Invariant(
                $"{state.SimulationTimeS:R},{Csv(state.RunId)},{Csv(state.MissionId)},{state.Seed},{state.Phase},{state.Result},{state.ActiveObjectiveCount},{state.SucceededObjectiveCount},{state.FailedObjectiveCount},{Csv(state.CompletionReason)}"));
        }

        public void OnMissionActorState(in MissionActorState state)
        {
            EnsureWriters();
            actorWriter?.WriteLine(FormattableString.Invariant(
                $"{ReadMissionTime():R},{Csv(state.Aircraft.Value)},{Csv(state.Callsign)},{state.Side},{state.Role},{state.Status},{state.AiMode},{state.ControlAuthority},{Csv(state.LeaderAircraft.Value)},{state.FormationSlot},{state.AiSkillNormalized:R},{state.CurrentWaypointIndex},{Csv(state.SelectedTarget.Value)},{state.IsDetected},{state.IsEngaged},{state.IsValid}"));
        }

        public void OnMissionObjectiveState(in MissionObjectiveState state)
        {
            EnsureWriters();
            objectiveWriter?.WriteLine(FormattableString.Invariant(
                $"{ReadMissionTime():R},{Csv(state.ObjectiveId)},{Csv(state.DisplayName)},{state.Type},{state.Status},{state.ProgressNormalized:R},{state.CurrentCount},{state.RequiredCount},{state.DeadlineS:R},{Csv(state.FailureReason)}"));
        }

        public void OnAircraftCombatState(in AircraftCombatState state)
        {
            EnsureWriters();
            combatWriter?.WriteLine(FormattableString.Invariant(
                $"{state.SimulationTimeS:R},{Csv(state.Aircraft.Value)},{state.MasterArm},{state.SelectedStationIndex},{Csv(state.SelectedStoreType)},{state.SelectedStoreQuantity},{Csv(state.SelectedTarget.Value)},{state.RadarTrackState},{state.TargetRangeM:R},{state.ClosureRateMps:R},{state.TargetBearingRad:R},{state.TargetElevationRad:R},{state.LockQualityNormalized:R},{state.InLaunchZone},{state.ShootCue},{state.MissileLaunchWarning}"));
        }

        public void OnAutomationRunState(in AutomationRunState state)
        {
            EnsureWriters();
            automationWriter?.WriteLine(FormattableString.Invariant(
                $"{state.SimulationTimeS:R},{Csv(state.BatchId)},{Csv(state.RunId)},{state.Mode},{state.Status},{state.PlayerControlAuthority},{state.WallClockTimeS:R},{state.SimulationRate:R},{state.TerrainSource},{state.TerrainDataValid},{Csv(state.CompletionReason)},{Csv(state.ReportPath)}"));
        }

        public void OnMissionEvent(in MissionEvent missionEvent)
        {
            EnsureWriters();
            eventWriter?.WriteLine(FormattableString.Invariant(
                $"{missionEvent.SimulationTimeS:R},mission,{missionEvent.Type},{Csv(missionEvent.Source.Value)},{Csv(missionEvent.Target.Value)},{missionEvent.Code},{Csv(missionEvent.Message)}"));
            eventWriter?.Flush();
        }

        public void OnWeaponEngagementEvent(in WeaponEngagementEvent engagementEvent)
        {
            EnsureWriters();
            eventWriter?.WriteLine(FormattableString.Invariant(
                $"{engagementEvent.SimulationTimeS:R},weapon,{engagementEvent.Outcome},{Csv(engagementEvent.Shooter.Value)},{Csv(engagementEvent.Target.Value)},{engagementEvent.EngagementSequence},{Csv(engagementEvent.WeaponType + " " + engagementEvent.Reason)}"));
            eventWriter?.Flush();
        }

        private void OnMissionLoaded(MissionDefinition mission)
        {
            Rebind();
        }

        private void OnRecordingStateChanged(bool isRecording)
        {
            if (isRecording) EnsureWriters();
            else CloseWriters();
        }

        private void ResolveReferences()
        {
            if (simulationHost == null) simulationHost = GetComponent<FlightSimulationHost>();
            if (flightRecorder == null) flightRecorder = GetComponent<FlightDataRecorder>();
        }

        private void Rebind()
        {
            IMissionTelemetrySource source = simulationHost != null ? simulationHost.MissionDirector : null;
            if (ReferenceEquals(source, registeredSource)) return;
            registeredSource?.UnregisterSink(this);
            registeredSource = source;
            registeredSource?.RegisterSink(this);
        }

        private void EnsureWriters()
        {
            if (flightRecorder == null || !flightRecorder.IsRecording || string.IsNullOrEmpty(flightRecorder.SessionDirectory))
            {
                if (missionWriter != null) CloseWriters();
                return;
            }
            string directory = flightRecorder.SessionDirectory;
            if (missionWriter != null && string.Equals(directory, activeDirectory, StringComparison.Ordinal)) return;
            CloseWriters();
            Directory.CreateDirectory(directory);
            StreamWriter openedMission = null;
            StreamWriter openedActor = null;
            StreamWriter openedObjective = null;
            StreamWriter openedCombat = null;
            StreamWriter openedAutomation = null;
            StreamWriter openedEvent = null;
            try
            {
                openedMission = Open(directory, "mission-state.csv", "time_s,run_id,mission_id,seed,phase,result,active_objectives,succeeded_objectives,failed_objectives,completion_reason");
                openedActor = Open(directory, "mission-actors.csv", "time_s,aircraft,callsign,side,role,status,ai_mode,control_authority,leader,formation_slot,ai_skill_normalized,waypoint_index,target,detected,engaged,valid");
                openedObjective = Open(directory, "mission-objectives.csv", "time_s,objective_id,name,type,status,progress,current_count,required_count,deadline_s,failure_reason");
                openedCombat = Open(directory, "combat-state.csv", "time_s,aircraft,master_arm,station,weapon,quantity,target,track_state,range_m,closure_mps,bearing_rad,elevation_rad,lock_quality,in_launch_zone,shoot_cue,missile_warning");
                openedAutomation = Open(directory, "automation-state.csv", "time_s,batch_id,run_id,mode,status,authority,wall_time_s,simulation_rate,terrain_source,terrain_valid,completion_reason,report_path");
                openedEvent = Open(directory, "mission-events.csv", "time_s,domain,type,source,target,code,message");
                missionWriter = openedMission;
                actorWriter = openedActor;
                objectiveWriter = openedObjective;
                combatWriter = openedCombat;
                automationWriter = openedAutomation;
                eventWriter = openedEvent;
                activeDirectory = directory;
            }
            catch
            {
                SafeDispose(openedMission);
                SafeDispose(openedActor);
                SafeDispose(openedObjective);
                SafeDispose(openedCombat);
                SafeDispose(openedAutomation);
                SafeDispose(openedEvent);
                activeDirectory = string.Empty;
                throw;
            }
        }

        private void Flush()
        {
            missionWriter?.Flush(); actorWriter?.Flush(); objectiveWriter?.Flush();
            combatWriter?.Flush(); automationWriter?.Flush(); eventWriter?.Flush();
        }

        private void CloseWriters()
        {
            StreamWriter closingMission = missionWriter;
            StreamWriter closingActor = actorWriter;
            StreamWriter closingObjective = objectiveWriter;
            StreamWriter closingCombat = combatWriter;
            StreamWriter closingAutomation = automationWriter;
            StreamWriter closingEvent = eventWriter;
            missionWriter = null; actorWriter = null; objectiveWriter = null;
            combatWriter = null; automationWriter = null; eventWriter = null;
            activeDirectory = string.Empty;
            SafeDispose(closingMission); SafeDispose(closingActor); SafeDispose(closingObjective);
            SafeDispose(closingCombat); SafeDispose(closingAutomation); SafeDispose(closingEvent);
        }

        private static void SafeDispose(StreamWriter writer)
        {
            if (writer == null) return;
            try { writer.Dispose(); }
            catch (Exception exception) { Debug.LogException(exception); }
        }

        private double ReadMissionTime()
        {
            return simulationHost != null && simulationHost.MissionDirector != null
                ? simulationHost.MissionDirector.LatestState.SimulationTimeS
                : 0.0;
        }

        private static StreamWriter Open(string directory, string fileName, string header)
        {
            var writer = new StreamWriter(Path.Combine(directory, fileName), true, new UTF8Encoding(false));
            if (writer.BaseStream.Length == 0) writer.WriteLine(header);
            return writer;
        }

        private static string Csv(string value)
        {
            return "\"" + (value ?? string.Empty).Replace("\"", "\"\"") + "\"";
        }
    }
}
