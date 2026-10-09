using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    [CreateAssetMenu(fileName = "MissionDefinition", menuName = "FlightSim/Mission Definition")]
    public sealed class MissionDefinitionAsset : ScriptableObject
    {
        [SerializeField] private MissionDefinition definition = new MissionDefinition();

        public MissionDefinition Definition => definition;
        public string MissionId => definition?.MissionId ?? string.Empty;

        public void SetDefinition(MissionDefinition value)
        {
            MissionValidationResult validation = MissionDefinitionValidator.Validate(value);
            if (!validation.IsValid)
                throw new System.ArgumentException(validation.FirstError, nameof(value));
            definition = value;
        }

        public string ExportJson(bool prettyPrint = true)
        {
            return MissionJsonCodec.ToJson(definition, prettyPrint);
        }

        public void ImportJson(string json)
        {
            definition = MissionJsonCodec.FromJson(json);
        }
    }
}
