using System;

namespace FlightSim.Platform.Core
{
    public readonly struct EnuBasis
    {
        public readonly DVector3 East;
        public readonly DVector3 North;
        public readonly DVector3 Up;

        public EnuBasis(DVector3 east, DVector3 north, DVector3 up)
        {
            East = east;
            North = north;
            Up = up;
        }
    }

    public readonly struct FrdBasis
    {
        public readonly DVector3 Forward;
        public readonly DVector3 Right;
        public readonly DVector3 Down;

        public FrdBasis(DVector3 forward, DVector3 right, DVector3 down)
        {
            Forward = forward;
            Right = right;
            Down = down;
        }
    }

    public static class GeoMath
    {
        public const double Wgs84SemiMajorAxisM = 6378137.0;
        public const double Wgs84Flattening = 1.0 / 298.257223563;

        private static readonly double Wgs84EccentricitySquared = Wgs84Flattening * (2.0 - Wgs84Flattening);
        private static readonly double Wgs84SemiMinorAxisM = Wgs84SemiMajorAxisM * (1.0 - Wgs84Flattening);
        private static readonly double Wgs84SecondEccentricitySquared =
            (Wgs84SemiMajorAxisM * Wgs84SemiMajorAxisM - Wgs84SemiMinorAxisM * Wgs84SemiMinorAxisM) /
            (Wgs84SemiMinorAxisM * Wgs84SemiMinorAxisM);

        public static DVector3 LlaToEcef(double longitudeRad, double latitudeRad, double ellipsoidHeightM)
        {
            double sinLatitude = Math.Sin(latitudeRad);
            double cosLatitude = Math.Cos(latitudeRad);
            double primeVerticalRadiusM = Wgs84SemiMajorAxisM / Math.Sqrt(1.0 - Wgs84EccentricitySquared * sinLatitude * sinLatitude);
            double radialDistanceM = (primeVerticalRadiusM + ellipsoidHeightM) * cosLatitude;

            return new DVector3(
                radialDistanceM * Math.Cos(longitudeRad),
                radialDistanceM * Math.Sin(longitudeRad),
                (primeVerticalRadiusM * (1.0 - Wgs84EccentricitySquared) + ellipsoidHeightM) * sinLatitude);
        }

        public static void EcefToLla(
            DVector3 ecefPositionM,
            out double longitudeRad,
            out double latitudeRad,
            out double ellipsoidHeightM)
        {
            double horizontalDistanceM = Math.Sqrt(ecefPositionM.X * ecefPositionM.X + ecefPositionM.Y * ecefPositionM.Y);
            longitudeRad = Math.Atan2(ecefPositionM.Y, ecefPositionM.X);

            if (horizontalDistanceM < 1e-9)
            {
                latitudeRad = ecefPositionM.Z >= 0.0 ? Math.PI * 0.5 : -Math.PI * 0.5;
                ellipsoidHeightM = Math.Abs(ecefPositionM.Z) - Wgs84SemiMinorAxisM;
                longitudeRad = 0.0;
                return;
            }

            double theta = Math.Atan2(ecefPositionM.Z * Wgs84SemiMajorAxisM, horizontalDistanceM * Wgs84SemiMinorAxisM);
            double sinTheta = Math.Sin(theta);
            double cosTheta = Math.Cos(theta);
            latitudeRad = Math.Atan2(
                ecefPositionM.Z + Wgs84SecondEccentricitySquared * Wgs84SemiMinorAxisM * sinTheta * sinTheta * sinTheta,
                horizontalDistanceM - Wgs84EccentricitySquared * Wgs84SemiMajorAxisM * cosTheta * cosTheta * cosTheta);
            double sinLatitude = Math.Sin(latitudeRad);
            double primeVerticalRadiusM = Wgs84SemiMajorAxisM / Math.Sqrt(1.0 - Wgs84EccentricitySquared * sinLatitude * sinLatitude);
            ellipsoidHeightM = horizontalDistanceM / Math.Cos(latitudeRad) - primeVerticalRadiusM;
        }

        public static EnuBasis CreateEnuBasis(double longitudeRad, double latitudeRad)
        {
            double sinLongitude = Math.Sin(longitudeRad);
            double cosLongitude = Math.Cos(longitudeRad);
            double sinLatitude = Math.Sin(latitudeRad);
            double cosLatitude = Math.Cos(latitudeRad);
            DVector3 east = new DVector3(-sinLongitude, cosLongitude, 0.0);
            DVector3 north = new DVector3(-sinLatitude * cosLongitude, -sinLatitude * sinLongitude, cosLatitude);
            DVector3 up = new DVector3(cosLatitude * cosLongitude, cosLatitude * sinLongitude, sinLatitude);
            return new EnuBasis(east, north, up);
        }

        public static FrdBasis CreateFrdBasis(DQuaternion bodyToReferenceOrientation)
        {
            return new FrdBasis(
                bodyToReferenceOrientation.Rotate(DVector3.UnitX),
                bodyToReferenceOrientation.Rotate(DVector3.UnitY),
                bodyToReferenceOrientation.Rotate(DVector3.UnitZ));
        }
    }
}
