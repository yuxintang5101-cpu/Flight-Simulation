using System;
using UnityEngine;
namespace FlightSim.Platform.Unity
{
    [CreateAssetMenu(menuName = "FlightSim/Mission Library", fileName = "MissionLibrary")]
    public sealed class FlightMissionLibrary : ScriptableObject
    {
        public MissionDefinitionAsset[] Missions = Array.Empty<MissionDefinitionAsset>();
    }

}
