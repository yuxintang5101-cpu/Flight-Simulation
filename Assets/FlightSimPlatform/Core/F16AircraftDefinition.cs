using System;

namespace FlightSim.Platform.Core
{
    [Serializable]
    public sealed class F16AircraftDefinition
    {
        public readonly double EmptyMassKg;
        public readonly double InternalFuelCapacityKg;
        public readonly double MaximumTakeoffMassKg;
        public readonly double MaximumMilitaryThrustN;
        public readonly double MaximumAfterburnerThrustN;

        public readonly double WingAreaM2;
        public readonly double WingSpanM;
        public readonly double MeanAerodynamicChordM;

        public readonly double RollMomentOfInertiaKgm2;
        public readonly double PitchMomentOfInertiaKgm2;
        public readonly double YawMomentOfInertiaKgm2;
        public readonly double ProductOfInertiaXzKgm2;

        public readonly double MinimumLoadFactorG;
        public readonly double MaximumLoadFactorG;
        public readonly double MaximumAngleOfAttackRad;
        public readonly double MaximumRollRateRadps;
        public readonly double MaximumCommandedSideslipRad;

        public readonly double MaximumElevatorDeflectionRad;
        public readonly double MaximumAileronDeflectionRad;
        public readonly double MaximumRudderDeflectionRad;
        public readonly double ElevatorRateLimitRadps;
        public readonly double AileronRateLimitRadps;
        public readonly double RudderRateLimitRadps;

        public readonly double LowSpeedControlThresholdMps;
        public readonly double LowSpeedControlBlendEndMps;

        private F16AircraftDefinition(
            double emptyMassKg,
            double internalFuelCapacityKg,
            double maximumTakeoffMassKg,
            double maximumMilitaryThrustN,
            double maximumAfterburnerThrustN,
            double wingAreaM2,
            double wingSpanM,
            double meanAerodynamicChordM,
            double rollMomentOfInertiaKgm2,
            double pitchMomentOfInertiaKgm2,
            double yawMomentOfInertiaKgm2,
            double productOfInertiaXzKgm2,
            double minimumLoadFactorG,
            double maximumLoadFactorG,
            double maximumAngleOfAttackRad,
            double maximumRollRateRadps,
            double maximumCommandedSideslipRad,
            double maximumElevatorDeflectionRad,
            double maximumAileronDeflectionRad,
            double maximumRudderDeflectionRad,
            double elevatorRateLimitRadps,
            double aileronRateLimitRadps,
            double rudderRateLimitRadps,
            double lowSpeedControlThresholdMps,
            double lowSpeedControlBlendEndMps)
        {
            EmptyMassKg = emptyMassKg;
            InternalFuelCapacityKg = internalFuelCapacityKg;
            MaximumTakeoffMassKg = maximumTakeoffMassKg;
            MaximumMilitaryThrustN = maximumMilitaryThrustN;
            MaximumAfterburnerThrustN = maximumAfterburnerThrustN;
            WingAreaM2 = wingAreaM2;
            WingSpanM = wingSpanM;
            MeanAerodynamicChordM = meanAerodynamicChordM;
            RollMomentOfInertiaKgm2 = rollMomentOfInertiaKgm2;
            PitchMomentOfInertiaKgm2 = pitchMomentOfInertiaKgm2;
            YawMomentOfInertiaKgm2 = yawMomentOfInertiaKgm2;
            ProductOfInertiaXzKgm2 = productOfInertiaXzKgm2;
            MinimumLoadFactorG = minimumLoadFactorG;
            MaximumLoadFactorG = maximumLoadFactorG;
            MaximumAngleOfAttackRad = maximumAngleOfAttackRad;
            MaximumRollRateRadps = maximumRollRateRadps;
            MaximumCommandedSideslipRad = maximumCommandedSideslipRad;
            MaximumElevatorDeflectionRad = maximumElevatorDeflectionRad;
            MaximumAileronDeflectionRad = maximumAileronDeflectionRad;
            MaximumRudderDeflectionRad = maximumRudderDeflectionRad;
            ElevatorRateLimitRadps = elevatorRateLimitRadps;
            AileronRateLimitRadps = aileronRateLimitRadps;
            RudderRateLimitRadps = rudderRateLimitRadps;
            LowSpeedControlThresholdMps = lowSpeedControlThresholdMps;
            LowSpeedControlBlendEndMps = lowSpeedControlBlendEndMps;
        }

        public static F16AircraftDefinition CreateDefault()
        {
            const double degreesToRadians = Math.PI / 180.0;
            return new F16AircraftDefinition(
                emptyMassKg: 8936.0,
                internalFuelCapacityKg: 3175.0,
                maximumTakeoffMassKg: 16875.0,
                maximumMilitaryThrustN: 79000.0,
                maximumAfterburnerThrustN: 120000.0,
                wingAreaM2: 27.8709,
                wingSpanM: 9.144,
                meanAerodynamicChordM: 3.450336,
                rollMomentOfInertiaKgm2: 12874.8,
                pitchMomentOfInertiaKgm2: 75673.6,
                yawMomentOfInertiaKgm2: 85552.1,
                productOfInertiaXzKgm2: 1331.3,
                minimumLoadFactorG: -3.0,
                maximumLoadFactorG: 9.0,
                maximumAngleOfAttackRad: 25.0 * degreesToRadians,
                maximumRollRateRadps: 240.0 * degreesToRadians,
                maximumCommandedSideslipRad: 5.0 * degreesToRadians,
                maximumElevatorDeflectionRad: 25.0 * degreesToRadians,
                maximumAileronDeflectionRad: 21.5 * degreesToRadians,
                maximumRudderDeflectionRad: 30.0 * degreesToRadians,
                elevatorRateLimitRadps: 60.0 * degreesToRadians,
                aileronRateLimitRadps: 80.0 * degreesToRadians,
                rudderRateLimitRadps: 120.0 * degreesToRadians,
                lowSpeedControlThresholdMps: 55.0,
                lowSpeedControlBlendEndMps: 90.0);
        }
    }
}
