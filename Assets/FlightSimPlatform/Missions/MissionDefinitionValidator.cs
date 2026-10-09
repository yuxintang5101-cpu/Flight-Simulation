using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Integration;

namespace FlightSim.Platform.Missions
{
    public readonly struct MissionValidationResult
    {
        public readonly bool IsValid;
        public readonly int ErrorCount;
        public readonly string FirstError;

        public MissionValidationResult(int errorCount, string firstError)
        {
            ErrorCount = errorCount;
            FirstError = firstError ?? string.Empty;
            IsValid = errorCount == 0;
        }
    }

    public static class MissionDefinitionValidator
    {
        public static MissionValidationResult Validate(MissionDefinition definition)
        {
            int errors = 0;
            string first = string.Empty;
            void Add(string message)
            {
                errors++;
                if (string.IsNullOrEmpty(first))
                {
                    first = message;
                }
            }

            if (definition == null)
            {
                return new MissionValidationResult(1, "Mission definition is null.");
            }

            if (definition.SchemaVersion != 1) Add("Mission schema version must be 1.");
            if (string.IsNullOrWhiteSpace(definition.MissionId)) Add("Mission ID is required.");
            if (!(definition.MaximumDurationS > 0.0) || !IsFinite(definition.MaximumDurationS))
                Add("Maximum duration must be finite and positive.");

            MissionActorDefinition[] actors = definition.Actors ?? Array.Empty<MissionActorDefinition>();
            if (actors.Length == 0 || actors.Length > FlightSimulationService.MaximumAircraft)
                Add($"Mission must define between 1 and {FlightSimulationService.MaximumAircraft} actors.");

            int playerCount = 0;
            for (int index = 0; index < actors.Length; index++)
            {
                MissionActorDefinition actor = actors[index];
                if (actor.IsPlayer) playerCount++;
                if (string.IsNullOrWhiteSpace(actor.AircraftId)) Add($"Actor {index} has no aircraft ID.");
                if (!IsFinite(actor.InitialCondition.InternalFuelFraction) || actor.InitialCondition.InternalFuelFraction < 0.0 || actor.InitialCondition.InternalFuelFraction > 1.0)
                    Add($"Actor '{actor.AircraftId}' has an invalid internal fuel fraction.");
                if (actor.InitialCondition.ExternalFuelKg < 0.0 || !IsFinite(actor.InitialCondition.ExternalFuelKg))
                    Add($"Actor '{actor.AircraftId}' has invalid external fuel.");

                for (int otherIndex = 0; otherIndex < index; otherIndex++)
                {
                    if (string.Equals(actor.AircraftId, actors[otherIndex].AircraftId, StringComparison.Ordinal))
                        Add($"Duplicate aircraft ID '{actor.AircraftId}'.");
                }
                var initial = actor.InitialCondition;
                if (!IsFinite(initial.LongitudeRad) || Math.Abs(initial.LongitudeRad) > Math.PI ||
                    !IsFinite(initial.LatitudeRad) || Math.Abs(initial.LatitudeRad) > Math.PI / 2 ||
                    !IsFinite(initial.EllipsoidHeightM) || !IsFinite(initial.TrueAirspeedMps) || initial.TrueAirspeedMps < 0 ||
                    !IsFinite(initial.HeadingRad) || !IsFinite(initial.PitchRad) || !IsFinite(initial.RollRad))
                    Add($"Actor '{actor.AircraftId}' has invalid initial position, attitude or airspeed.");
                var route = actor.Route ?? Array.Empty<MissionWaypointDefinition>();
                if (route.Length > 128) Add($"Actor '{actor.AircraftId}' exceeds 128 waypoints.");
                for (int point = 0; point < route.Length; point++)
                {
                    var waypoint = route[point];
                    if (string.IsNullOrWhiteSpace(waypoint.WaypointId) || !IsFinite(waypoint.LongitudeRad) || Math.Abs(waypoint.LongitudeRad) > Math.PI ||
                        !IsFinite(waypoint.LatitudeRad) || Math.Abs(waypoint.LatitudeRad) > Math.PI / 2 || !IsFinite(waypoint.EllipsoidHeightM) ||
                        !IsFinite(waypoint.TargetTrueAirspeedMps) || waypoint.TargetTrueAirspeedMps <= 0 ||
                        !IsFinite(waypoint.AcceptanceRadiusM) || waypoint.AcceptanceRadiusM <= 0)
                        Add($"Actor '{actor.AircraftId}' waypoint {point} is invalid.");
                }
                if (!IsFinite(actor.AiSkillNormalized) || actor.AiSkillNormalized < 0.0 || actor.AiSkillNormalized > 1.0)
                    Add($"Actor '{actor.AircraftId}' AI skill must be from 0 to 1.");

                if (!string.IsNullOrEmpty(actor.LeaderAircraftId))
                {
                    if (string.Equals(actor.AircraftId, actor.LeaderAircraftId, StringComparison.Ordinal))
                        Add($"Actor '{actor.AircraftId}' cannot lead itself.");
                    else if (!ContainsActor(actors, actor.LeaderAircraftId))
                        Add($"Actor '{actor.AircraftId}' references unknown leader '{actor.LeaderAircraftId}'.");
                }
            }

            if (playerCount != 1) Add("Mission must define exactly one player actor.");

            MissionObjectiveDefinition[] objectives = definition.Objectives ?? Array.Empty<MissionObjectiveDefinition>();
            if (objectives.Length > 128) Add("Mission exceeds 128 objectives.");
            for (int index = 0; index < objectives.Length; index++)
            {
                MissionObjectiveDefinition objective = objectives[index];
                if (!Enum.IsDefined(typeof(MissionObjectiveType), objective.Type) || !IsFinite(objective.TimeLimitS) || objective.TimeLimitS < 0 || objective.RequiredCount < 0)
                    Add($"Objective '{objective.ObjectiveId}' has invalid type, time limit or count.");
                if (objective.Type == MissionObjectiveType.ReachWaypoint)
                {
                    bool hasRoute = false;
                    foreach (var actor in actors) if (actor.AircraftId == objective.SubjectAircraftId && actor.Route != null && actor.Route.Length > 0) hasRoute = true;
                    if (!hasRoute) Add($"Waypoint objective '{objective.ObjectiveId}' requires a subject with a route.");
                }
                if (string.IsNullOrWhiteSpace(objective.ObjectiveId)) Add($"Objective {index} has no ID.");
                for (int otherIndex = 0; otherIndex < index; otherIndex++)
                {
                    if (string.Equals(objective.ObjectiveId, objectives[otherIndex].ObjectiveId, StringComparison.Ordinal))
                        Add($"Duplicate objective ID '{objective.ObjectiveId}'.");
                }
                if (!string.IsNullOrEmpty(objective.SubjectAircraftId) && !ContainsActor(actors, objective.SubjectAircraftId))
                    Add($"Objective '{objective.ObjectiveId}' references unknown subject '{objective.SubjectAircraftId}'.");
                if (!string.IsNullOrEmpty(objective.TargetAircraftId) && !ContainsActor(actors, objective.TargetAircraftId))
                    Add($"Objective '{objective.ObjectiveId}' references unknown target '{objective.TargetAircraftId}'.");
            }

            return new MissionValidationResult(errors, first);
        }

        private static bool ContainsActor(MissionActorDefinition[] actors, string aircraftId)
        {
            for (int index = 0; index < actors.Length; index++)
            {
                if (string.Equals(actors[index].AircraftId, aircraftId, StringComparison.Ordinal)) return true;
            }
            return false;
        }

        private static bool IsFinite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);
    }
}
