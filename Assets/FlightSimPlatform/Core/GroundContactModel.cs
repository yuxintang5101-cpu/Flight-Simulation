using System;

namespace FlightSim.Platform.Core
{
    public readonly struct AnalyticRunway
    {
        public readonly DVector3 CenterEcefM;
        public readonly DVector3 ThresholdEcefM;
        public readonly DVector3 ForwardEcef;
        public readonly DVector3 RightEcef;
        public readonly DVector3 UpEcef;
        public readonly double LengthM;
        public readonly double WidthM;
        public readonly double HeadingTrueRad;

        public AnalyticRunway(
            DVector3 centerEcefM,
            DVector3 forwardEcef,
            DVector3 rightEcef,
            DVector3 upEcef,
            double lengthM,
            double widthM,
            double headingTrueRad)
        {
            CenterEcefM = centerEcefM;
            ForwardEcef = forwardEcef.Normalized;
            RightEcef = rightEcef.Normalized;
            UpEcef = upEcef.Normalized;
            LengthM = lengthM;
            WidthM = widthM;
            HeadingTrueRad = headingTrueRad;
            ThresholdEcefM = centerEcefM - ForwardEcef * (lengthM * 0.5);
        }

        public double DistanceAlongM(in DVector3 ecefPositionM)
        {
            return DVector3.Dot(ecefPositionM - ThresholdEcefM, ForwardEcef);
        }

        public double DistanceRightOfCenterM(in DVector3 ecefPositionM)
        {
            return DVector3.Dot(ecefPositionM - CenterEcefM, RightEcef);
        }

        public double HeightAbovePlaneM(in DVector3 ecefPositionM)
        {
            return DVector3.Dot(ecefPositionM - CenterEcefM, UpEcef);
        }

        public bool ContainsPlanarPoint(in DVector3 ecefPositionM)
        {
            double alongM = DistanceAlongM(in ecefPositionM);
            double rightM = DistanceRightOfCenterM(in ecefPositionM);
            return alongM >= 0.0 && alongM <= LengthM && Math.Abs(rightM) <= WidthM * 0.5;
        }
    }

    public readonly struct GroundContactLoads
    {
        public readonly DVector3 ForceBodyN;
        public readonly DVector3 MomentBodyNm;
        public readonly double DistanceAlongRunwayM;
        public readonly double DistanceRightOfCenterlineM;
        public readonly bool NoseGearContact;
        public readonly bool LeftMainGearContact;
        public readonly bool RightMainGearContact;

        public GroundContactLoads(
            DVector3 forceBodyN,
            DVector3 momentBodyNm,
            double distanceAlongRunwayM,
            double distanceRightOfCenterlineM,
            bool noseGearContact,
            bool leftMainGearContact,
            bool rightMainGearContact)
        {
            ForceBodyN = forceBodyN;
            MomentBodyNm = momentBodyNm;
            DistanceAlongRunwayM = distanceAlongRunwayM;
            DistanceRightOfCenterlineM = distanceRightOfCenterlineM;
            NoseGearContact = noseGearContact;
            LeftMainGearContact = leftMainGearContact;
            RightMainGearContact = rightMainGearContact;
        }

        public bool WeightOnWheels => NoseGearContact || LeftMainGearContact || RightMainGearContact;
    }

    /// <summary>
    /// Three-point F-16 landing gear against a finite analytic KTEX runway plane.
    /// </summary>
    public sealed class GroundContactModel
    {
        public const double KtexLongitudeRad = -107.9087 * Math.PI / 180.0;
        public const double KtexLatitudeRad = 37.9538 * Math.PI / 180.0;
        public const double KtexEllipsoidHeightM = 2765.0;
        public const double KtexRunwayLengthM = 2167.0;
        public const double KtexRunwayWidthM = 30.5;
        public const double KtexRunwayHeadingTrueRad = 285.0 * Math.PI / 180.0;

        private const double SpringRateNpm = 550000.0;
        private const double CompressionDampingNspm = 42000.0;
        private const double MaximumNormalForcePerGearN = 450000.0;
        private const double RollingResistanceCoefficient = 0.018;
        private const double BrakingFrictionCoefficient = 0.62;
        private const double LateralFrictionCoefficient = 0.75;
        private const double LateralCorneringNspm = 65000.0;
        private const double FrictionBlendSpeedMps = 0.15;

        private static readonly DVector3 NoseGearBodyM = new DVector3(3.35, 0.0, 1.57);
        private static readonly DVector3 LeftMainGearBodyM = new DVector3(-0.55, -1.15, 1.635);
        private static readonly DVector3 RightMainGearBodyM = new DVector3(-0.55, 1.15, 1.635);

        private readonly AnalyticRunway runway;

        public GroundContactModel()
        {
            DVector3 centerEcefM = GeoMath.LlaToEcef(
                KtexLongitudeRad,
                KtexLatitudeRad,
                KtexEllipsoidHeightM);
            EnuBasis enu = GeoMath.CreateEnuBasis(KtexLongitudeRad, KtexLatitudeRad);
            double sineHeading = Math.Sin(KtexRunwayHeadingTrueRad);
            double cosineHeading = Math.Cos(KtexRunwayHeadingTrueRad);
            DVector3 forwardEcef = enu.North * cosineHeading + enu.East * sineHeading;
            DVector3 rightEcef = enu.East * cosineHeading - enu.North * sineHeading;
            runway = new AnalyticRunway(
                centerEcefM,
                forwardEcef,
                rightEcef,
                enu.Up,
                KtexRunwayLengthM,
                KtexRunwayWidthM,
                KtexRunwayHeadingTrueRad);
        }

        public AnalyticRunway Runway => runway;

        public GroundContactLoads Evaluate(
            in DVector3 centerOfMassEcefM,
            in DVector3 centerOfMassVelocityEcefMps,
            in DQuaternion bodyToEcefOrientation,
            in DVector3 bodyAngularVelocityRadps,
            double noseGearExtensionNormalized,
            double leftMainGearExtensionNormalized,
            double rightMainGearExtensionNormalized,
            double leftWheelBrakeNormalized,
            double rightWheelBrakeNormalized)
        {
            DVector3 forceBodyN = DVector3.Zero;
            DVector3 momentBodyNm = DVector3.Zero;
            bool noseContact = AddGearContact(
                in NoseGearBodyM,
                in centerOfMassEcefM,
                in centerOfMassVelocityEcefMps,
                in bodyToEcefOrientation,
                in bodyAngularVelocityRadps,
                noseGearExtensionNormalized,
                0.0,
                ref forceBodyN,
                ref momentBodyNm);
            bool leftMainContact = AddGearContact(
                in LeftMainGearBodyM,
                in centerOfMassEcefM,
                in centerOfMassVelocityEcefMps,
                in bodyToEcefOrientation,
                in bodyAngularVelocityRadps,
                leftMainGearExtensionNormalized,
                Clamp(leftWheelBrakeNormalized, 0.0, 1.0),
                ref forceBodyN,
                ref momentBodyNm);
            bool rightMainContact = AddGearContact(
                in RightMainGearBodyM,
                in centerOfMassEcefM,
                in centerOfMassVelocityEcefMps,
                in bodyToEcefOrientation,
                in bodyAngularVelocityRadps,
                rightMainGearExtensionNormalized,
                Clamp(rightWheelBrakeNormalized, 0.0, 1.0),
                ref forceBodyN,
                ref momentBodyNm);

            return new GroundContactLoads(
                forceBodyN,
                momentBodyNm,
                runway.DistanceAlongM(in centerOfMassEcefM),
                runway.DistanceRightOfCenterM(in centerOfMassEcefM),
                noseContact,
                leftMainContact,
                rightMainContact);
        }

        private bool AddGearContact(
            in DVector3 gearBodyM,
            in DVector3 centerOfMassEcefM,
            in DVector3 centerOfMassVelocityEcefMps,
            in DQuaternion bodyToEcefOrientation,
            in DVector3 bodyAngularVelocityRadps,
            double gearExtensionNormalized,
            double brakeNormalized,
            ref DVector3 forceBodyN,
            ref DVector3 momentBodyNm)
        {
            if (gearExtensionNormalized < 0.95)
            {
                return false;
            }

            DVector3 gearArmEcefM = bodyToEcefOrientation.Rotate(gearBodyM);
            DVector3 gearPositionEcefM = centerOfMassEcefM + gearArmEcefM;
            if (!runway.ContainsPlanarPoint(in gearPositionEcefM))
            {
                return false;
            }

            double penetrationM = -runway.HeightAbovePlaneM(in gearPositionEcefM);
            if (penetrationM <= 0.0)
            {
                return false;
            }

            DVector3 angularVelocityEcefRadps = bodyToEcefOrientation.Rotate(bodyAngularVelocityRadps);
            DVector3 gearVelocityEcefMps = centerOfMassVelocityEcefMps +
                                           DVector3.Cross(angularVelocityEcefRadps, gearArmEcefM);
            double downwardVelocityMps = -DVector3.Dot(gearVelocityEcefMps, runway.UpEcef);
            double normalForceN = SpringRateNpm * penetrationM +
                                  CompressionDampingNspm * downwardVelocityMps;
            normalForceN = Clamp(normalForceN, 0.0, MaximumNormalForcePerGearN);
            if (normalForceN <= 0.0)
            {
                return false;
            }

            double longitudinalVelocityMps = DVector3.Dot(gearVelocityEcefMps, runway.ForwardEcef);
            double lateralVelocityMps = DVector3.Dot(gearVelocityEcefMps, runway.RightEcef);
            double longitudinalLimitN = normalForceN *
                                        (RollingResistanceCoefficient +
                                         BrakingFrictionCoefficient * brakeNormalized);
            double longitudinalForceN = OpposingForce(longitudinalVelocityMps, longitudinalLimitN);
            double lateralLimitN = normalForceN * LateralFrictionCoefficient;
            double lateralForceN = Clamp(
                -lateralVelocityMps * LateralCorneringNspm,
                -lateralLimitN,
                lateralLimitN);
            DVector3 contactForceEcefN = runway.UpEcef * normalForceN +
                                         runway.ForwardEcef * longitudinalForceN +
                                         runway.RightEcef * lateralForceN;
            DVector3 contactForceBodyN = InverseRotate(in bodyToEcefOrientation, in contactForceEcefN);
            forceBodyN += contactForceBodyN;
            momentBodyNm += DVector3.Cross(gearBodyM, contactForceBodyN);
            return true;
        }

        private static DVector3 InverseRotate(in DQuaternion orientation, in DVector3 vector)
        {
            DQuaternion normalized = orientation.Normalized;
            DQuaternion inverse = new DQuaternion(-normalized.X, -normalized.Y, -normalized.Z, normalized.W);
            return inverse.Rotate(vector);
        }

        private static double OpposingForce(double velocityMps, double maximumMagnitudeN)
        {
            if (maximumMagnitudeN <= 0.0)
            {
                return 0.0;
            }

            double blend = Clamp(velocityMps / FrictionBlendSpeedMps, -1.0, 1.0);
            return -maximumMagnitudeN * blend;
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            return value < minimum ? minimum : value > maximum ? maximum : value;
        }
    }
}
