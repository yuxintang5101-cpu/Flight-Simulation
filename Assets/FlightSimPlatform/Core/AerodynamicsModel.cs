using System;

namespace FlightSim.Platform.Core
{
    public readonly struct AirDataState
    {
        public readonly double TrueAirspeedMps;
        public readonly double CalibratedAirspeedMps;
        public readonly double Mach;
        public readonly double AngleOfAttackRad;
        public readonly double SideslipRad;
        public readonly double DynamicPressurePa;

        public AirDataState(
            double trueAirspeedMps,
            double calibratedAirspeedMps,
            double mach,
            double angleOfAttackRad,
            double sideslipRad,
            double dynamicPressurePa)
        {
            TrueAirspeedMps = trueAirspeedMps;
            CalibratedAirspeedMps = calibratedAirspeedMps;
            Mach = mach;
            AngleOfAttackRad = angleOfAttackRad;
            SideslipRad = sideslipRad;
            DynamicPressurePa = dynamicPressurePa;
        }
    }

    public readonly struct AerodynamicLoads
    {
        public readonly DVector3 ForceBodyN;
        public readonly DVector3 MomentBodyNm;
        public readonly AirDataState AirData;
        public readonly double LiftN;
        public readonly double DragN;
        public readonly double SideForceN;
        public readonly double LiftCoefficient;
        public readonly double DragCoefficient;

        public AerodynamicLoads(
            DVector3 forceBodyN,
            DVector3 momentBodyNm,
            in AirDataState airData,
            double liftN,
            double dragN,
            double sideForceN,
            double liftCoefficient,
            double dragCoefficient)
        {
            ForceBodyN = forceBodyN;
            MomentBodyNm = momentBodyNm;
            AirData = airData;
            LiftN = liftN;
            DragN = dragN;
            SideForceN = sideForceN;
            LiftCoefficient = liftCoefficient;
            DragCoefficient = dragCoefficient;
        }
    }

    /// <summary>
    /// Deterministic, coefficient-based F-16 aerodynamic model in body FRD axes.
    /// </summary>
    public sealed class AerodynamicsModel
    {
        private const double SeaLevelPressurePa = 101325.0;
        private const double SeaLevelSpeedOfSoundMps = 340.293988;
        private const double RatioOfSpecificHeats = 1.4;
        private const double DefaultReferenceSpanM = 9.96;
        private const double DefaultReferenceChordM = 3.45;
        private const double MinimumDampingSpeedMps = 25.0;
        private const double MaximumCoefficientAlphaRad = 35.0 * Math.PI / 180.0;

        private readonly double wingAreaM2;
        private readonly double referenceSpanM;
        private readonly double referenceChordM;

        public AerodynamicsModel(double wingAreaM2)
            : this(wingAreaM2, DefaultReferenceSpanM, DefaultReferenceChordM)
        {
        }

        public AerodynamicsModel(F16AircraftDefinition definition)
            : this(
                definition != null ? definition.WingAreaM2 : 0.0,
                definition != null ? definition.WingSpanM : 0.0,
                definition != null ? definition.MeanAerodynamicChordM : 0.0)
        {
            if (definition == null)
            {
                throw new ArgumentNullException(nameof(definition));
            }
        }

        private AerodynamicsModel(double wingAreaM2, double referenceSpanM, double referenceChordM)
        {
            if (!(wingAreaM2 > 0.0) || !(referenceSpanM > 0.0) || !(referenceChordM > 0.0))
            {
                throw new ArgumentOutOfRangeException(nameof(wingAreaM2));
            }

            this.wingAreaM2 = wingAreaM2;
            this.referenceSpanM = referenceSpanM;
            this.referenceChordM = referenceChordM;
        }

        public static AirDataState ComputeAirData(
            in DVector3 bodyAirVelocityMps,
            in AtmosphereSample atmosphere)
        {
            double trueAirspeedMps = bodyAirVelocityMps.Length;
            double speedOfSoundMps = Math.Max(1.0, atmosphere.SpeedOfSoundMps);
            double mach = trueAirspeedMps / speedOfSoundMps;
            double dynamicPressurePa = 0.5 * Math.Max(0.0, atmosphere.DensityKgpm3) *
                                       trueAirspeedMps * trueAirspeedMps;
            double angleOfAttackRad = trueAirspeedMps > 1e-9
                ? Math.Atan2(bodyAirVelocityMps.Z, bodyAirVelocityMps.X)
                : 0.0;
            double sideslipRad = trueAirspeedMps > 1e-9
                ? Math.Asin(Clamp(bodyAirVelocityMps.Y / trueAirspeedMps, -1.0, 1.0))
                : 0.0;

            // Calibrated airspeed is obtained from local pitot impact pressure and
            // the ISA sea-level calibration, retaining compressibility at flight Mach.
            double localPressurePa = Math.Max(1.0, atmosphere.PressurePa);
            double impactPressurePa = localPressurePa * ImpactPressureRatio(mach);
            double calibratedMach = CalibratedMachFromImpactPressure(impactPressurePa);
            double calibratedAirspeedMps = SeaLevelSpeedOfSoundMps * calibratedMach;

            return new AirDataState(
                trueAirspeedMps,
                calibratedAirspeedMps,
                mach,
                angleOfAttackRad,
                sideslipRad,
                dynamicPressurePa);
        }

        public AerodynamicLoads Evaluate(
            in DVector3 bodyAirVelocityMps,
            in DVector3 bodyAngularVelocityRadps,
            double elevatorDeflectionRad,
            double aileronDeflectionRad,
            double rudderDeflectionRad,
            double additionalDragCoefficient,
            in AtmosphereSample atmosphere)
        {
            AirDataState airData = ComputeAirData(in bodyAirVelocityMps, in atmosphere);
            double alphaForCoefficients = Clamp(
                airData.AngleOfAttackRad,
                -MaximumCoefficientAlphaRad,
                MaximumCoefficientAlphaRad);
            double betaForCoefficients = Clamp(
                airData.SideslipRad,
                -MaximumCoefficientAlphaRad,
                MaximumCoefficientAlphaRad);
            double dampingSpeedMps = Math.Max(MinimumDampingSpeedMps, airData.TrueAirspeedMps);
            double rollRateHat = bodyAngularVelocityRadps.X * referenceSpanM / (2.0 * dampingSpeedMps);
            double pitchRateHat = bodyAngularVelocityRadps.Y * referenceChordM / (2.0 * dampingSpeedMps);
            double yawRateHat = bodyAngularVelocityRadps.Z * referenceSpanM / (2.0 * dampingSpeedMps);

            double liftCoefficient = 0.18 + 4.70 * alphaForCoefficients - 0.35 * elevatorDeflectionRad;
            liftCoefficient = ApplyPostStallSoftening(liftCoefficient, alphaForCoefficients);
            double dragCoefficient = 0.022 + Math.Max(0.0, additionalDragCoefficient) +
                                     0.075 * liftCoefficient * liftCoefficient;
            double sideForceCoefficient = -0.90 * betaForCoefficients +
                                          0.12 * rudderDeflectionRad -
                                          0.18 * yawRateHat;

            double rollMomentCoefficient = 0.15 * aileronDeflectionRad -
                                           0.10 * betaForCoefficients -
                                           0.55 * rollRateHat;
            double pitchMomentCoefficient = -0.78 * alphaForCoefficients -
                                            1.20 * elevatorDeflectionRad -
                                            7.50 * pitchRateHat;
            double yawMomentCoefficient = 0.18 * betaForCoefficients +
                                           0.20 * rudderDeflectionRad -
                                           0.45 * yawRateHat;

            double pressureArea = airData.DynamicPressurePa * wingAreaM2;
            double liftN = pressureArea * liftCoefficient;
            double dragN = pressureArea * dragCoefficient;
            double sideForceN = pressureArea * sideForceCoefficient;

            double cosineAlpha = Math.Cos(airData.AngleOfAttackRad);
            double sineAlpha = Math.Sin(airData.AngleOfAttackRad);
            DVector3 liftDirectionBody = new DVector3(sineAlpha, 0.0, -cosineAlpha);
            DVector3 dragDirectionBody = airData.TrueAirspeedMps > 1e-9
                ? -bodyAirVelocityMps / airData.TrueAirspeedMps
                : DVector3.Zero;
            DVector3 forceBodyN = dragDirectionBody * dragN +
                                  liftDirectionBody * liftN +
                                  DVector3.UnitY * sideForceN;
            DVector3 momentBodyNm = new DVector3(
                pressureArea * referenceSpanM * rollMomentCoefficient,
                pressureArea * referenceChordM * pitchMomentCoefficient,
                pressureArea * referenceSpanM * yawMomentCoefficient);

            return new AerodynamicLoads(
                forceBodyN,
                momentBodyNm,
                in airData,
                liftN,
                dragN,
                sideForceN,
                liftCoefficient,
                dragCoefficient);
        }

        private static double ApplyPostStallSoftening(double liftCoefficient, double angleOfAttackRad)
        {
            const double linearLimitRad = 20.0 * Math.PI / 180.0;
            double excessRad = Math.Abs(angleOfAttackRad) - linearLimitRad;
            if (excessRad <= 0.0)
            {
                return Clamp(liftCoefficient, -1.55, 1.55);
            }

            double attenuation = 1.0 / (1.0 + 3.0 * excessRad);
            return Clamp(liftCoefficient * attenuation, -1.55, 1.55);
        }

        private static double CalibratedMachFromImpactPressure(double impactPressurePa)
        {
            double targetRatio = Math.Max(0.0, impactPressurePa / SeaLevelPressurePa);
            if (targetRatio <= ImpactPressureRatio(1.0))
            {
                double calibratedTerm = Math.Pow(targetRatio + 1.0, 1.0 / 3.5) - 1.0;
                return Math.Sqrt(Math.Max(0.0, 5.0 * calibratedTerm));
            }

            double lowerMach = 1.0;
            double upperMach = 5.0;
            for (int iteration = 0; iteration < 32; iteration++)
            {
                double candidateMach = (lowerMach + upperMach) * 0.5;
                if (ImpactPressureRatio(candidateMach) < targetRatio)
                {
                    lowerMach = candidateMach;
                }
                else
                {
                    upperMach = candidateMach;
                }
            }

            return (lowerMach + upperMach) * 0.5;
        }

        private static double ImpactPressureRatio(double mach)
        {
            double nonnegativeMach = Math.Max(0.0, mach);
            if (nonnegativeMach <= 1.0)
            {
                return Math.Pow(1.0 + 0.2 * nonnegativeMach * nonnegativeMach, 3.5) - 1.0;
            }

            double machSquared = nonnegativeMach * nonnegativeMach;
            double firstTerm = Math.Pow(1.2 * machSquared, 3.5);
            double secondTerm = Math.Pow(2.4 / (2.8 * machSquared - 0.4), 2.5);
            return firstTerm * secondTerm - 1.0;
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            return value < minimum ? minimum : value > maximum ? maximum : value;
        }
    }
}
