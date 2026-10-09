using CesiumForUnity;
using FlightSim.Platform.Contracts;
using Unity.Mathematics;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CesiumGlobeAnchor))]
    [DefaultExecutionOrder(FlightSimExecutionOrder.AircraftVisual)]
    public sealed class CesiumAircraftView : MonoBehaviour
    {
        [SerializeField] private FlightSimulationHost simulationHost;
        [SerializeField] private string aircraftIdOverride = string.Empty;

        private CesiumGlobeAnchor globeAnchor;

        public FlightSimulationHost SimulationHost => simulationHost;
        public CesiumGlobeAnchor GlobeAnchor => globeAnchor;
        public AircraftId BoundAircraft => string.IsNullOrEmpty(aircraftIdOverride)
            ? simulationHost != null ? simulationHost.LocalAircraft : default(AircraftId)
            : new AircraftId(aircraftIdOverride);

        private void Awake()
        {
            globeAnchor = GetComponent<CesiumGlobeAnchor>();
            ConfigureAnchor();

            Rigidbody body = GetComponent<Rigidbody>();
            if (body != null)
            {
                body.isKinematic = true;
                body.useGravity = false;
            }
        }

        private void Reset()
        {
            simulationHost = FindObjectOfType<FlightSimulationHost>();
            globeAnchor = GetComponent<CesiumGlobeAnchor>();
            ConfigureAnchor();
        }

        private void OnValidate()
        {
            globeAnchor = GetComponent<CesiumGlobeAnchor>();
            ConfigureAnchor();
        }

        private void LateUpdate()
        {
            if (simulationHost == null ||
                globeAnchor == null ||
                !simulationHost.TryGetLatest(BoundAircraft, out AircraftSnapshot snapshot))
            {
                return;
            }

            ApplyFastState(in snapshot.Fast);
        }

        public void Bind(FlightSimulationHost host, AircraftId aircraft)
        {
            simulationHost = host;
            aircraftIdOverride = aircraft.Value;
        }

        public void ApplyFastState(in AircraftFastState state)
        {
            if (globeAnchor == null)
            {
                globeAnchor = GetComponent<CesiumGlobeAnchor>();
                ConfigureAnchor();
            }

            globeAnchor.localToGlobeFixedMatrix = CreateLocalToGlobeFixedMatrix(in state);
        }

        public static double4x4 CreateLocalToGlobeFixedMatrix(in AircraftFastState state)
        {
            double x = state.BodyToEcefQuaternionX;
            double y = state.BodyToEcefQuaternionY;
            double z = state.BodyToEcefQuaternionZ;
            double w = state.BodyToEcefQuaternionW;
            double lengthSquared = x * x + y * y + z * z + w * w;
            if (!(lengthSquared > 1e-20) ||
                double.IsNaN(lengthSquared) ||
                double.IsInfinity(lengthSquared))
            {
                x = 0.0;
                y = 0.0;
                z = 0.0;
                w = 1.0;
            }
            else
            {
                double inverseLength = 1.0 / System.Math.Sqrt(lengthSquared);
                x *= inverseLength;
                y *= inverseLength;
                z *= inverseLength;
                w *= inverseLength;
            }

            double xx = x * x;
            double yy = y * y;
            double zz = z * z;
            double xy = x * y;
            double xz = x * z;
            double yz = y * z;
            double wx = w * x;
            double wy = w * y;
            double wz = w * z;

            double3 bodyForwardEcef = new double3(
                1.0 - 2.0 * (yy + zz),
                2.0 * (xy + wz),
                2.0 * (xz - wy));
            double3 bodyRightEcef = new double3(
                2.0 * (xy - wz),
                1.0 - 2.0 * (xx + zz),
                2.0 * (yz + wx));
            double3 bodyDownEcef = new double3(
                2.0 * (xz + wy),
                2.0 * (yz - wx),
                1.0 - 2.0 * (xx + yy));

            // Unity model +X/+Y/+Z (right/up/forward) maps to FRD +Y/-Z/+X.
            return new double4x4(
                new double4(bodyRightEcef, 0.0),
                new double4(-bodyDownEcef, 0.0),
                new double4(bodyForwardEcef, 0.0),
                new double4(
                    state.EcefPositionXM,
                    state.EcefPositionYM,
                    state.EcefPositionZM,
                    1.0));
        }

        private void ConfigureAnchor()
        {
            if (globeAnchor == null)
            {
                return;
            }

            globeAnchor.detectTransformChanges = false;
            globeAnchor.adjustOrientationForGlobeWhenMoving = false;
        }
    }
}
