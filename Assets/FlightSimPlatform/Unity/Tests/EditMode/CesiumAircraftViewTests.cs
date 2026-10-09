using CesiumForUnity;
using FlightSim.Platform.Contracts;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

namespace FlightSim.Platform.Unity.Tests.EditMode
{
    public sealed class CesiumAircraftViewTests
    {
        private const double Tolerance = 1e-12;

        [Test]
        public void IdentityFrdOrientationMapsUnityModelAxesToEcefFrdAxes()
        {
            AircraftFastState state = AircraftFastState.CreateDefault(new AircraftId("TEST"));

            double4x4 matrix = CesiumAircraftView.CreateLocalToGlobeFixedMatrix(in state);

            AssertDirection(matrix.c0.xyz, new double3(0.0, 1.0, 0.0));
            AssertDirection(matrix.c1.xyz, new double3(0.0, 0.0, -1.0));
            AssertDirection(matrix.c2.xyz, new double3(1.0, 0.0, 0.0));
        }

        [Test]
        public void MatrixTranslationUsesAircraftEcefPosition()
        {
            AircraftFastState state = AircraftFastState.CreateDefault(new AircraftId("TEST"));
            state.EcefPositionXM = -1292935.125;
            state.EcefPositionYM = -4740026.5;
            state.EcefPositionZM = 4056960.75;

            double4x4 matrix = CesiumAircraftView.CreateLocalToGlobeFixedMatrix(in state);

            Assert.That(matrix.c3.x, Is.EqualTo(state.EcefPositionXM).Within(Tolerance));
            Assert.That(matrix.c3.y, Is.EqualTo(state.EcefPositionYM).Within(Tolerance));
            Assert.That(matrix.c3.z, Is.EqualTo(state.EcefPositionZM).Within(Tolerance));
            Assert.That(matrix.c3.w, Is.EqualTo(1.0).Within(Tolerance));
        }

        [Test]
        public void ApplyingStateDisablesAutomaticAnchorTransformAndOrientationChanges()
        {
            GameObject georeferenceObject = new GameObject("Test Georeference");
            GameObject aircraftObject = new GameObject("Test Aircraft");
            try
            {
                georeferenceObject.AddComponent<CesiumGeoreference>();
                aircraftObject.transform.SetParent(georeferenceObject.transform, false);
                CesiumGlobeAnchor anchor = aircraftObject.AddComponent<CesiumGlobeAnchor>();
                CesiumAircraftView view = aircraftObject.AddComponent<CesiumAircraftView>();
                AircraftFastState state = AircraftFastState.CreateDefault(new AircraftId("TEST"));
                state.EcefPositionXM = 6378137.0;

                view.ApplyFastState(in state);

                Assert.That(anchor.detectTransformChanges, Is.False);
                Assert.That(anchor.adjustOrientationForGlobeWhenMoving, Is.False);
            }
            finally
            {
                Object.DestroyImmediate(georeferenceObject);
            }
        }

        private static void AssertDirection(double3 actual, double3 expected)
        {
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(Tolerance));
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(Tolerance));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(Tolerance));
        }
    }
}
