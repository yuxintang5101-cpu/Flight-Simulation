using System;

namespace FlightSim.Platform.Core
{
    [Serializable]
    public struct FlightControlSensors
    {
        public double AngleOfAttackRad;
        public double SideslipRad;
        public double NormalLoadFactorG;
        public double RollRateRadps;
        public double PitchRateRadps;
        public double YawRateRadps;
        public double CalibratedAirspeedMps;
        public bool WeightOnWheels;
    }

    [Serializable]
    public struct FlightControlOutput
    {
        public double CommandedNormalLoadFactorG;
        public double CommandedRollRateRadps;
        public double CommandedSideslipRad;
        public double ElevatorDeflectionRad;
        public double AileronDeflectionRad;
        public double RudderDeflectionRad;
        public bool AngleOfAttackLimiterActive;
        public bool GForceLimiterActive;
        public bool RollRateLimiterActive;
        public bool LowSpeedModeActive;
        public bool GroundModeActive;
    }

    public sealed class F16FlightControlLaw
    {
        private const double DegreesToRadians = Math.PI / 180.0;
        private const double LoadFactorLimiterMarginG = 0.5;
        private const double RollRateLimiterMarginRadps = 10.0 * DegreesToRadians;
        private const double LowSpeedMaximumLoadFactorG = 3.0;
        private const double FullLoadFactorAirspeedMps = 140.0;
        private const double LoadFactorScheduleStartMps = 75.0;
        private const double AngleOfAttackProtectionOnsetRad = 18.0 * DegreesToRadians;
        private const double ProtectedRollRateRadps = 60.0 * DegreesToRadians;

        private readonly F16AircraftDefinition definition;
        private double elevatorDeflectionRad;
        private double aileronDeflectionRad;
        private double rudderDeflectionRad;

        public F16FlightControlLaw(F16AircraftDefinition definition)
        {
            if (definition == null)
            {
                throw new ArgumentNullException(nameof(definition));
            }

            this.definition = definition;
        }

        public FlightControlOutput Update(
            in PilotControlInput input,
            in FlightControlSensors sensors,
            double actuatorAuthorityNormalized,
            double deltaTimeS)
        {
            double pitchInput = ClampNormalized(input.PitchNormalized);
            double rollInput = ClampNormalized(input.RollNormalized);
            double yawInput = ClampNormalized(input.YawNormalized);
            double authority = Clamp01(actuatorAuthorityNormalized);
            double airspeedMps = NonNegativeFinite(sensors.CalibratedAirspeedMps);
            double angleOfAttackRad = FiniteOrZero(sensors.AngleOfAttackRad);
            double normalLoadFactorG = FiniteOrZero(sensors.NormalLoadFactorG);
            double rollRateRadps = FiniteOrZero(sensors.RollRateRadps);
            double pitchRateRadps = FiniteOrZero(sensors.PitchRateRadps);
            double yawRateRadps = FiniteOrZero(sensors.YawRateRadps);
            double sideslipRad = FiniteOrZero(sensors.SideslipRad);

            bool groundModeActive = sensors.WeightOnWheels;
            bool lowSpeedModeActive = groundModeActive || airspeedMps < definition.LowSpeedControlBlendEndMps;
            double airborneBlend = groundModeActive
                ? 0.0
                : InverseLerp(
                    definition.LowSpeedControlThresholdMps,
                    definition.LowSpeedControlBlendEndMps,
                    airspeedMps);

            double positiveLoadLimitG = Lerp(
                LowSpeedMaximumLoadFactorG,
                definition.MaximumLoadFactorG,
                InverseLerp(LoadFactorScheduleStartMps, FullLoadFactorAirspeedMps, airspeedMps));
            double commandedNormalLoadFactorG = MapPitchToLoadFactor(pitchInput, positiveLoadLimitG);
            bool angleOfAttackLimiterActive = angleOfAttackRad >= AngleOfAttackProtectionOnsetRad;
            bool gForceLimiterActive =
                normalLoadFactorG >= definition.MaximumLoadFactorG - LoadFactorLimiterMarginG ||
                (pitchInput > 0.0 && commandedNormalLoadFactorG >= definition.MaximumLoadFactorG);

            double angleOfAttackLimitFraction = InverseLerp(
                AngleOfAttackProtectionOnsetRad,
                definition.MaximumAngleOfAttackRad,
                angleOfAttackRad);
            if (angleOfAttackLimiterActive && commandedNormalLoadFactorG > 1.0)
            {
                commandedNormalLoadFactorG = Lerp(
                    commandedNormalLoadFactorG,
                    1.0,
                    angleOfAttackLimitFraction);
            }

            double gLimitFraction = InverseLerp(
                definition.MaximumLoadFactorG - LoadFactorLimiterMarginG,
                definition.MaximumLoadFactorG,
                normalLoadFactorG);
            if (gForceLimiterActive)
            {
                commandedNormalLoadFactorG = Math.Min(
                    commandedNormalLoadFactorG,
                    definition.MaximumLoadFactorG - gLimitFraction * LoadFactorLimiterMarginG);
            }

            commandedNormalLoadFactorG = Clamp(
                commandedNormalLoadFactorG,
                definition.MinimumLoadFactorG,
                definition.MaximumLoadFactorG);

            double scheduledMaximumRollRateRadps = Lerp(
                definition.MaximumRollRateRadps,
                ProtectedRollRateRadps,
                angleOfAttackLimitFraction);
            double commandedRollRateRadps = rollInput * scheduledMaximumRollRateRadps;
            bool rollRateLimiterActive =
                Math.Abs(rollRateRadps) >= scheduledMaximumRollRateRadps - RollRateLimiterMarginRadps ||
                Math.Abs(rollInput) * definition.MaximumRollRateRadps > scheduledMaximumRollRateRadps;
            double commandedSideslipRad = yawInput * definition.MaximumCommandedSideslipRad;

            // The aerodynamic model defines negative elevator as positive FRD pitch authority.
            double protectedPitchInput = pitchInput > 0.0
                ? pitchInput * (1.0 - angleOfAttackLimitFraction)
                : pitchInput;
            double directElevatorCommandRad = -protectedPitchInput * definition.MaximumElevatorDeflectionRad;
            double loadFactorErrorG = commandedNormalLoadFactorG - normalLoadFactorG;
            double loadFactorElevatorCommandRad =
                -loadFactorErrorG * 4.0 * DegreesToRadians + pitchRateRadps * 0.12;
            double elevatorTargetRad = Lerp(
                directElevatorCommandRad,
                loadFactorElevatorCommandRad,
                airborneBlend);
            elevatorTargetRad += angleOfAttackLimitFraction * definition.MaximumElevatorDeflectionRad;
            elevatorTargetRad += gLimitFraction * 0.35 * definition.MaximumElevatorDeflectionRad;

            double directAileronCommandRad = rollInput * definition.MaximumAileronDeflectionRad;
            double rollRateAileronCommandRad =
                (commandedRollRateRadps - rollRateRadps) * 0.12;
            double aileronTargetRad = Lerp(
                directAileronCommandRad,
                rollRateAileronCommandRad,
                airborneBlend);

            double directRudderCommandRad = yawInput * definition.MaximumRudderDeflectionRad;
            double sideslipRudderCommandRad =
                (sideslipRad - commandedSideslipRad) * 2.0 - yawRateRadps * 0.12;
            double rudderTargetRad = Lerp(
                directRudderCommandRad,
                sideslipRudderCommandRad,
                airborneBlend);

            elevatorTargetRad = Clamp(
                elevatorTargetRad,
                -definition.MaximumElevatorDeflectionRad,
                definition.MaximumElevatorDeflectionRad);
            aileronTargetRad = Clamp(
                aileronTargetRad,
                -definition.MaximumAileronDeflectionRad,
                definition.MaximumAileronDeflectionRad);
            rudderTargetRad = Clamp(
                rudderTargetRad,
                -definition.MaximumRudderDeflectionRad,
                definition.MaximumRudderDeflectionRad);

            UpdateActuators(
                elevatorTargetRad,
                aileronTargetRad,
                rudderTargetRad,
                authority,
                NonNegativeFinite(deltaTimeS));

            return new FlightControlOutput
            {
                CommandedNormalLoadFactorG = commandedNormalLoadFactorG,
                CommandedRollRateRadps = commandedRollRateRadps,
                CommandedSideslipRad = commandedSideslipRad,
                ElevatorDeflectionRad = elevatorDeflectionRad,
                AileronDeflectionRad = aileronDeflectionRad,
                RudderDeflectionRad = rudderDeflectionRad,
                AngleOfAttackLimiterActive = angleOfAttackLimiterActive,
                GForceLimiterActive = gForceLimiterActive,
                RollRateLimiterActive = rollRateLimiterActive,
                LowSpeedModeActive = lowSpeedModeActive,
                GroundModeActive = groundModeActive
            };
        }

        public void Reset()
        {
            elevatorDeflectionRad = 0.0;
            aileronDeflectionRad = 0.0;
            rudderDeflectionRad = 0.0;
        }

        private double MapPitchToLoadFactor(double pitchInput, double positiveLoadLimitG)
        {
            if (pitchInput >= 0.0)
            {
                return 1.0 + pitchInput * (positiveLoadLimitG - 1.0);
            }

            return 1.0 + pitchInput * (1.0 - definition.MinimumLoadFactorG);
        }

        private void UpdateActuators(
            double elevatorTargetRad,
            double aileronTargetRad,
            double rudderTargetRad,
            double authority,
            double deltaTimeS)
        {
            if (authority <= 0.0)
            {
                Reset();
                return;
            }

            double elevatorLimitRad = definition.MaximumElevatorDeflectionRad * authority;
            double aileronLimitRad = definition.MaximumAileronDeflectionRad * authority;
            double rudderLimitRad = definition.MaximumRudderDeflectionRad * authority;
            double elevatorCommandRad = Clamp(elevatorTargetRad * authority, -elevatorLimitRad, elevatorLimitRad);
            double aileronCommandRad = Clamp(aileronTargetRad * authority, -aileronLimitRad, aileronLimitRad);
            double rudderCommandRad = Clamp(rudderTargetRad * authority, -rudderLimitRad, rudderLimitRad);

            elevatorDeflectionRad = MoveTowards(
                elevatorDeflectionRad,
                elevatorCommandRad,
                definition.ElevatorRateLimitRadps * authority * deltaTimeS);
            aileronDeflectionRad = MoveTowards(
                aileronDeflectionRad,
                aileronCommandRad,
                definition.AileronRateLimitRadps * authority * deltaTimeS);
            rudderDeflectionRad = MoveTowards(
                rudderDeflectionRad,
                rudderCommandRad,
                definition.RudderRateLimitRadps * authority * deltaTimeS);

            elevatorDeflectionRad = Clamp(elevatorDeflectionRad, -elevatorLimitRad, elevatorLimitRad);
            aileronDeflectionRad = Clamp(aileronDeflectionRad, -aileronLimitRad, aileronLimitRad);
            rudderDeflectionRad = Clamp(rudderDeflectionRad, -rudderLimitRad, rudderLimitRad);
        }

        private static double MoveTowards(double current, double target, double maximumDelta)
        {
            double difference = target - current;
            if (Math.Abs(difference) <= maximumDelta)
            {
                return target;
            }

            return current + Math.Sign(difference) * maximumDelta;
        }

        private static double ClampNormalized(double value)
        {
            return Clamp(FiniteOrZero(value), -1.0, 1.0);
        }

        private static double Clamp01(double value)
        {
            return Clamp(FiniteOrZero(value), 0.0, 1.0);
        }

        private static double NonNegativeFinite(double value)
        {
            value = FiniteOrZero(value);
            return value > 0.0 ? value : 0.0;
        }

        private static double FiniteOrZero(double value)
        {
            return double.IsNaN(value) || double.IsInfinity(value) ? 0.0 : value;
        }

        private static double InverseLerp(double from, double to, double value)
        {
            if (value <= from)
            {
                return 0.0;
            }

            if (value >= to)
            {
                return 1.0;
            }

            return (value - from) / (to - from);
        }

        private static double Lerp(double from, double to, double amount)
        {
            return from + (to - from) * amount;
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            if (value < minimum)
            {
                return minimum;
            }

            if (value > maximum)
            {
                return maximum;
            }

            return value;
        }
    }
}
