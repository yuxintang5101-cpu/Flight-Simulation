using FlightSim.Platform.Contracts;
using FlightSim.Platform.Unity;
using UnityEngine;

namespace FlightSim.Platform.Presentation
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(F16HudController))]
    public sealed class FlightHudBridge : MonoBehaviour
    {
        [SerializeField] private FlightSimulationHost simulationHost;
        [SerializeField] private FlightCameraRig cameraRig;
        [SerializeField] private Camera cockpitCamera;

        private F16HudController hudController;
        private bool subscribed;

        public FlightSimulationHost SimulationHost => simulationHost;
        public FlightCameraRig CameraRig => cameraRig;

        private void Awake()
        {
            ResolveReferences();
        }

        private void OnEnable()
        {
            ResolveReferences();
            Subscribe();
            PushCurrentState();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Reset()
        {
            ResolveReferences();
        }

        private void OnSnapshotUpdated(AircraftSnapshot snapshot)
        {
            if (hudController == null)
                return;

            HudState hud = snapshot.Systems.Avionics.Hud;
            if (simulationHost != null && simulationHost.MissionDirector != null &&
                simulationHost.MissionDirector.TryGetCombatState(snapshot.Aircraft, out AircraftCombatState combat))
            {
                if (!hud.WeightOnWheels && simulationHost.IsMissionRunning)
                    hud.Mode = HudMode.AirToAir;
                hudController.SetCombatState(combat);
            }
            else
            {
                hudController.SetCombatState(default(AircraftCombatState));
            }
            hudController.SetHudState(hud);
        }

        private void OnCameraModeChanged(FlightCameraMode mode)
        {
            if (hudController != null)
                hudController.SetCockpitContext(mode == FlightCameraMode.Cockpit, cockpitCamera);
        }

        private void PushCurrentState()
        {
            if (hudController == null)
                return;

            bool cockpitActive = cameraRig != null && cameraRig.IsCockpitActive;
            hudController.SetCockpitContext(cockpitActive, cockpitCamera);
            if (simulationHost != null && simulationHost.TryGetLatest(out AircraftSnapshot snapshot))
                OnSnapshotUpdated(snapshot);
        }

        private void ResolveReferences()
        {
            if (hudController == null)
                hudController = GetComponent<F16HudController>();
            if (simulationHost == null)
                simulationHost = FindObjectOfType<FlightSimulationHost>();
            if (cameraRig == null)
                cameraRig = FindObjectOfType<FlightCameraRig>();
            if (cockpitCamera == null && cameraRig != null)
                cockpitCamera = cameraRig.GetComponent<Camera>();
        }

        private void Subscribe()
        {
            if (subscribed)
                return;

            if (simulationHost != null)
                simulationHost.SnapshotUpdated += OnSnapshotUpdated;
            if (cameraRig != null)
                cameraRig.ModeChanged += OnCameraModeChanged;
            subscribed = true;
        }

        private void Unsubscribe()
        {
            if (!subscribed)
                return;

            if (simulationHost != null)
                simulationHost.SnapshotUpdated -= OnSnapshotUpdated;
            if (cameraRig != null)
                cameraRig.ModeChanged -= OnCameraModeChanged;
            subscribed = false;
        }
    }
}
