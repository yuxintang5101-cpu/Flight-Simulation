using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    public static class MissionJsonCodec
    {
        public static string ToJson(MissionDefinition definition, bool prettyPrint = false)
        {
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            MissionValidationResult validation = MissionDefinitionValidator.Validate(definition);
            if (!validation.IsValid) throw new ArgumentException(validation.FirstError, nameof(definition));
            return JsonUtility.ToJson(definition, prettyPrint);
        }

        public static MissionDefinition FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) throw new ArgumentException("Mission JSON is required.", nameof(json));
            MissionDefinition definition = JsonUtility.FromJson<MissionDefinition>(json);
            MissionValidationResult validation = MissionDefinitionValidator.Validate(definition);
            if (!validation.IsValid) throw new FormatException(validation.FirstError);
            return definition;
        }
    }
}
