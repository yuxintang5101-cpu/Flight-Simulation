using FlightSim.Platform.Unity;
using UnityEngine;

namespace FlightSim.Platform.Presentation
{
    // Retains the original scene component and serialized host reference for existing scenes.
    [DisallowMultipleComponent]
    public sealed class MissionBriefingPanel : MonoBehaviour
    {
        [SerializeField] private FlightSimulationHost simulationHost;
        private void Start()
        {
            if (simulationHost == null) simulationHost = FindObjectOfType<FlightSimulationHost>();
            if (simulationHost == null) { Debug.LogError("FlightShell requires a FlightSimulationHost."); return; }
            var terrain = simulationHost.GetComponent<LocalTerrainEnvironment>() ?? simulationHost.gameObject.AddComponent<LocalTerrainEnvironment>();
            terrain.Initialize();
            var session = simulationHost.GetComponent<FlightSessionController>() ?? simulationHost.gameObject.AddComponent<FlightSessionController>();
            session.Initialize(simulationHost);
            var effects = simulationHost.GetComponent<FlightEffectsController>() ?? simulationHost.gameObject.AddComponent<FlightEffectsController>();
            effects.Initialize(simulationHost);
            var view = gameObject.AddComponent<FlightShellView>();
            view.Initialize(session);
            var display = simulationHost.GetComponent<LowerDisplay.LowerDisplayController>() ?? simulationHost.gameObject.AddComponent<LowerDisplay.LowerDisplayController>();
            display.Initialize(session);
        }
    }
}
