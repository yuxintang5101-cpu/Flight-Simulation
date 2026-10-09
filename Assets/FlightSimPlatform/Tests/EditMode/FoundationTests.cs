using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class FoundationTests
    {
        private const double Tolerance = 1e-9;

        [Test]
        public void DVector3_ArithmeticDotAndCross_AreCorrect()
        {
            var left = new DVector3(1.0, 2.0, 3.0);
            var right = new DVector3(4.0, -5.0, 6.0);

            Assert.That(left + right, Is.EqualTo(new DVector3(5.0, -3.0, 9.0)));
            Assert.That(left - right, Is.EqualTo(new DVector3(-3.0, 7.0, -3.0)));
            Assert.That(left * 2.0, Is.EqualTo(new DVector3(2.0, 4.0, 6.0)));
            Assert.That(DVector3.Dot(left, right), Is.EqualTo(12.0).Within(Tolerance));
            Assert.That(DVector3.Cross(left, right), Is.EqualTo(new DVector3(27.0, 6.0, -13.0)));
        }

        [Test]
        public void DQuaternion_RotationNormalizationAndIntegration_AreCorrect()
        {
            var quarterTurn = DQuaternion.FromAxisAngle(DVector3.UnitZ, System.Math.PI / 2.0);
            var rotated = quarterTurn.Rotate(DVector3.UnitX);
            var normalized = new DQuaternion(0.0, 0.0, 0.0, 2.0).Normalized;
            var integrated = DQuaternion.Identity.IntegrateBodyAngularVelocity(
                new DVector3(0.0, 0.0, System.Math.PI), 0.5);

            Assert.That(rotated.X, Is.EqualTo(0.0).Within(Tolerance));
            Assert.That(rotated.Y, Is.EqualTo(1.0).Within(Tolerance));
            Assert.That(rotated.Z, Is.EqualTo(0.0).Within(Tolerance));
            Assert.That(normalized, Is.EqualTo(DQuaternion.Identity));
            Assert.That(integrated.Rotate(DVector3.UnitX).X, Is.EqualTo(0.0).Within(Tolerance));
            Assert.That(integrated.Rotate(DVector3.UnitX).Y, Is.EqualTo(1.0).Within(Tolerance));
        }

        [Test]
        public void DQuaternion_InverseRotation_RestoresOriginalVector()
        {
            DQuaternion orientation =
                DQuaternion.FromAxisAngle(DVector3.UnitZ, 0.73) *
                DQuaternion.FromAxisAngle(DVector3.UnitY, -0.21);
            DVector3 original = new DVector3(142.0, -8.0, 19.0);

            DVector3 restored = orientation.RotateInverse(orientation.Rotate(original));
            DQuaternion identity = (orientation * orientation.Inverse).Normalized;

            Assert.That(restored.X, Is.EqualTo(original.X).Within(Tolerance));
            Assert.That(restored.Y, Is.EqualTo(original.Y).Within(Tolerance));
            Assert.That(restored.Z, Is.EqualTo(original.Z).Within(Tolerance));
            Assert.That(identity.X, Is.EqualTo(0.0).Within(Tolerance));
            Assert.That(identity.Y, Is.EqualTo(0.0).Within(Tolerance));
            Assert.That(identity.Z, Is.EqualTo(0.0).Within(Tolerance));
            Assert.That(identity.W, Is.EqualTo(1.0).Within(Tolerance));
        }

        [Test]
        public void DQuaternion_BodyRateIntegrationUsesRightMultiplication()
        {
            DQuaternion initial = DQuaternion.FromAxisAngle(DVector3.UnitZ, 0.7);
            DVector3 bodyRateRadps = new DVector3(0.0, 0.4, 0.0);
            const double deltaTimeS = 0.3;
            DQuaternion expected = (
                initial * DQuaternion.FromAxisAngle(DVector3.UnitY, bodyRateRadps.Y * deltaTimeS))
                .Normalized;

            DQuaternion actual = initial.IntegrateBodyAngularVelocity(bodyRateRadps, deltaTimeS);
            DVector3 expectedForward = expected.Rotate(DVector3.UnitX);
            DVector3 actualForward = actual.Rotate(DVector3.UnitX);

            Assert.That(actualForward.X, Is.EqualTo(expectedForward.X).Within(Tolerance));
            Assert.That(actualForward.Y, Is.EqualTo(expectedForward.Y).Within(Tolerance));
            Assert.That(actualForward.Z, Is.EqualTo(expectedForward.Z).Within(Tolerance));
        }

        [Test]
        public void GeoMath_KtexLlaEcefRoundTrip_PreservesCoordinates()
        {
            const double degreesToRadians = System.Math.PI / 180.0;
            const double latitudeRad = 37.9538 * degreesToRadians;
            const double longitudeRad = -107.9087 * degreesToRadians;
            const double ellipsoidHeightM = 2765.0;

            DVector3 ecef = GeoMath.LlaToEcef(longitudeRad, latitudeRad, ellipsoidHeightM);
            GeoMath.EcefToLla(ecef, out double resultLongitudeRad, out double resultLatitudeRad, out double resultEllipsoidHeightM);

            Assert.That(resultLongitudeRad, Is.EqualTo(longitudeRad).Within(Tolerance));
            Assert.That(resultLatitudeRad, Is.EqualTo(latitudeRad).Within(Tolerance));
            Assert.That(resultEllipsoidHeightM, Is.EqualTo(ellipsoidHeightM).Within(1e-4));
        }

        [Test]
        public void AircraftId_Equality_IsValueBased()
        {
            Assert.That(new AircraftId("VIPER-01"), Is.EqualTo(new AircraftId("VIPER-01")));
            Assert.That(new AircraftId("VIPER-01"), Is.Not.EqualTo(new AircraftId("VIPER-02")));
        }

        [Test]
        public void FlightSimulationContract_Version_IsExactlyTwo()
        {
            Assert.That(FlightSimulationContract.ContractVersion, Is.EqualTo((ushort)2));
        }

        [Test]
        public void AircraftSystemsState_StoreStations_HasNineSlots()
        {
            Assert.That(AircraftSystemsState.CreateDefault().Stores.Stations.Length, Is.EqualTo(9));
        }

        [Test]
        public void TacticalPictureState_Tracks_HasThirtyTwoSlots()
        {
            Assert.That(TacticalPictureState.CreateDefault().Tracks.Length, Is.EqualTo(32));
        }

        [Test]
        public void GeoMath_EnuBasis_UsesEastNorthUpAxes()
        {
            EnuBasis basis = GeoMath.CreateEnuBasis(0.0, 0.0);

            Assert.That(basis.East, Is.EqualTo(DVector3.UnitY));
            Assert.That(basis.North, Is.EqualTo(DVector3.UnitZ));
            Assert.That(basis.Up, Is.EqualTo(DVector3.UnitX));
            Assert.That(DVector3.Cross(basis.East, basis.North), Is.EqualTo(basis.Up));
        }

        [Test]
        public void GeoMath_FrdBasis_UsesForwardRightDownAxes()
        {
            FrdBasis basis = GeoMath.CreateFrdBasis(DQuaternion.Identity);

            Assert.That(basis.Forward, Is.EqualTo(DVector3.UnitX));
            Assert.That(basis.Right, Is.EqualTo(DVector3.UnitY));
            Assert.That(basis.Down, Is.EqualTo(DVector3.UnitZ));
        }
    }
}
