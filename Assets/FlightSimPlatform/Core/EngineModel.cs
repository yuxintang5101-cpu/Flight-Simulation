using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Core
{
    public sealed class F16EngineModel
    {
        private const double IdleN1Percent = 30.0;
        private const double IdleN2Percent = 65.0;
        private const double MilitaryThrottleThreshold = 0.08;
        private const double AfterburnerThrottleThreshold = 0.90;

        private readonly F16AircraftDefinition definition;
        private PropulsionState state;
        private bool failureLatched;

        public F16EngineModel(F16AircraftDefinition definition)
        {
            this.definition = definition;
            Reset(EngineMode.Off);
        }

        public PropulsionState State => state;

        public bool FailureLatched => failureLatched;

        public bool AccessoryDriveAvailable =>
            state.EngineRunning &&
            state.Mode != EngineMode.Off &&
            state.Mode != EngineMode.Failed &&
            state.N2Percent >= 55.0;

        public void Reset(EngineMode mode)
        {
            failureLatched = mode == EngineMode.Failed;
            state = default(PropulsionState);
            state.Mode = mode;

            switch (mode)
            {
                case EngineMode.Starting:
                    state.EngineRunning = true;
                    state.ExhaustGasTemperatureC = 120.0;
                    state.NozzlePositionNormalized = 0.35;
                    break;
                case EngineMode.Idle:
                    SetRunningState(IdleN1Percent, IdleN2Percent, 420.0, 0.20);
                    break;
                case EngineMode.Military:
                    SetRunningState(92.0, 96.0, 760.0, 0.10);
                    break;
                case EngineMode.Afterburner:
                    SetRunningState(100.0, 100.0, 920.0, 1.0);
                    state.AfterburnerActive = true;
                    break;
                case EngineMode.Off:
                case EngineMode.Failed:
                default:
                    break;
            }
        }

        public void Update(
            double throttleNormalized,
            bool startCommand,
            bool fuelAvailable,
            bool electricalPowerAvailable,
            double altitudeM,
            double mach,
            double deltaTimeS)
        {
            double dt = Max(0.0, deltaTimeS);
            double throttle = Clamp01(throttleNormalized);

            if (failureLatched)
            {
                state.Mode = EngineMode.Failed;
                state.EngineRunning = false;
                state.AfterburnerActive = false;
                state.N1Percent = MoveTowards(state.N1Percent, 0.0, 18.0 * dt);
                state.N2Percent = MoveTowards(state.N2Percent, 0.0, 12.0 * dt);
                state.ExhaustGasTemperatureC = MoveTowards(state.ExhaustGasTemperatureC, 20.0, 45.0 * dt);
                state.NozzlePositionNormalized = MoveTowards(state.NozzlePositionNormalized, 1.0, 1.5 * dt);
                state.FuelFlowKgps = 0.0;
                state.ThrustN = 0.0;
                return;
            }

            if (state.Mode == EngineMode.Off)
            {
                if (startCommand && fuelAvailable && electricalPowerAvailable)
                {
                    state.Mode = EngineMode.Starting;
                    state.EngineRunning = true;
                }
                else
                {
                    UpdateStopped(dt);
                    return;
                }
            }

            if (state.Mode == EngineMode.Starting)
            {
                UpdateStarting(fuelAvailable, electricalPowerAvailable, dt);
                return;
            }

            if (!fuelAvailable)
            {
                state.Mode = EngineMode.Off;
                UpdateStopped(dt);
                return;
            }

            EngineMode demandedMode;
            if (throttle <= MilitaryThrottleThreshold)
            {
                demandedMode = EngineMode.Idle;
            }
            else if (throttle < AfterburnerThrottleThreshold)
            {
                demandedMode = EngineMode.Military;
            }
            else
            {
                demandedMode = EngineMode.Afterburner;
            }

            state.Mode = demandedMode;
            state.EngineRunning = true;
            state.AfterburnerActive = demandedMode == EngineMode.Afterburner;

            double targetN1 = demandedMode == EngineMode.Idle ? IdleN1Percent :
                demandedMode == EngineMode.Military ? 92.0 : 100.0;
            double targetN2 = demandedMode == EngineMode.Idle ? IdleN2Percent :
                demandedMode == EngineMode.Military ? 96.0 : 100.0;
            state.N1Percent = MoveTowards(state.N1Percent, targetN1, 28.0 * dt);
            state.N2Percent = MoveTowards(state.N2Percent, targetN2, 22.0 * dt);

            double dryThrottle = Clamp01(
                (throttle - MilitaryThrottleThreshold) /
                (AfterburnerThrottleThreshold - MilitaryThrottleThreshold));
            double afterburnerCommand = Clamp01(
                (throttle - AfterburnerThrottleThreshold) /
                (1.0 - AfterburnerThrottleThreshold));
            double spoolFactor = Clamp01((state.N2Percent - 55.0) / 45.0);

            double commandedThrustN;
            double commandedFuelFlowKgps;
            double targetTemperatureC;
            double targetNozzle;
            if (demandedMode == EngineMode.Idle)
            {
                commandedThrustN = definition.MaximumMilitaryThrustN * 0.035;
                commandedFuelFlowKgps = 0.24;
                targetTemperatureC = 420.0;
                targetNozzle = 0.20;
            }
            else if (demandedMode == EngineMode.Military)
            {
                commandedThrustN = definition.MaximumMilitaryThrustN * (0.08 + (0.92 * dryThrottle));
                commandedFuelFlowKgps = 0.30 + (1.15 * dryThrottle);
                targetTemperatureC = 470.0 + (290.0 * dryThrottle);
                targetNozzle = 0.18 - (0.08 * dryThrottle);
            }
            else
            {
                commandedThrustN = definition.MaximumMilitaryThrustN +
                    ((definition.MaximumAfterburnerThrustN - definition.MaximumMilitaryThrustN) * afterburnerCommand);
                commandedFuelFlowKgps = 1.45 + (2.25 * afterburnerCommand);
                targetTemperatureC = 780.0 + (140.0 * afterburnerCommand);
                targetNozzle = 0.25 + (0.75 * afterburnerCommand);
            }

            double altitudeFactor = Clamp(1.0 - (Max(0.0, altitudeM) * 0.000022), 0.18, 1.05);
            double nonnegativeMach = Max(0.0, mach);
            double machFactor = Clamp(
                1.0 - (0.12 * nonnegativeMach) + (0.04 * nonnegativeMach * nonnegativeMach),
                0.65,
                1.05);
            state.ThrustN = Max(0.0, commandedThrustN * altitudeFactor * machFactor * spoolFactor);
            state.FuelFlowKgps = commandedFuelFlowKgps * (0.70 + (0.30 * altitudeFactor)) * spoolFactor;
            state.ExhaustGasTemperatureC = MoveTowards(state.ExhaustGasTemperatureC, targetTemperatureC, 180.0 * dt);
            state.NozzlePositionNormalized = MoveTowards(state.NozzlePositionNormalized, targetNozzle, 1.5 * dt);
        }

        public void Fail()
        {
            failureLatched = true;
            state.Mode = EngineMode.Failed;
            state.AfterburnerActive = false;
            state.FuelFlowKgps = 0.0;
            state.ThrustN = 0.0;
        }

        public void ClearFailure(EngineMode resetMode = EngineMode.Off)
        {
            if (resetMode == EngineMode.Failed)
            {
                resetMode = EngineMode.Off;
            }

            Reset(resetMode);
        }

        private void UpdateStarting(bool fuelAvailable, bool electricalPowerAvailable, double dt)
        {
            if (!fuelAvailable || (!electricalPowerAvailable && state.N2Percent < 50.0))
            {
                state.Mode = EngineMode.Off;
                UpdateStopped(dt);
                return;
            }

            state.EngineRunning = true;
            state.AfterburnerActive = false;
            state.N1Percent = MoveTowards(state.N1Percent, IdleN1Percent, 10.0 * dt);
            state.N2Percent = MoveTowards(state.N2Percent, IdleN2Percent, 14.0 * dt);
            state.ExhaustGasTemperatureC = MoveTowards(state.ExhaustGasTemperatureC, 420.0, 100.0 * dt);
            state.NozzlePositionNormalized = MoveTowards(state.NozzlePositionNormalized, 0.20, 0.8 * dt);
            state.FuelFlowKgps = state.N2Percent < 15.0 ? 0.0 : 0.16;
            state.ThrustN = definition.MaximumMilitaryThrustN * 0.035 * Clamp01(state.N2Percent / IdleN2Percent);

            if (state.N2Percent >= IdleN2Percent)
            {
                state.Mode = EngineMode.Idle;
            }
        }

        private void UpdateStopped(double dt)
        {
            state.EngineRunning = false;
            state.AfterburnerActive = false;
            state.N1Percent = MoveTowards(state.N1Percent, 0.0, 12.0 * dt);
            state.N2Percent = MoveTowards(state.N2Percent, 0.0, 9.0 * dt);
            state.ExhaustGasTemperatureC = MoveTowards(state.ExhaustGasTemperatureC, 20.0, 35.0 * dt);
            state.NozzlePositionNormalized = MoveTowards(state.NozzlePositionNormalized, 1.0, 1.0 * dt);
            state.FuelFlowKgps = 0.0;
            state.ThrustN = 0.0;
        }

        private void SetRunningState(double n1Percent, double n2Percent, double temperatureC, double nozzle)
        {
            state.EngineRunning = true;
            state.N1Percent = n1Percent;
            state.N2Percent = n2Percent;
            state.ExhaustGasTemperatureC = temperatureC;
            state.NozzlePositionNormalized = nozzle;
        }

        private static double MoveTowards(double current, double target, double maximumDelta)
        {
            if (current < target)
            {
                return Min(current + maximumDelta, target);
            }

            return Max(current - maximumDelta, target);
        }

        private static double Clamp01(double value)
        {
            return Clamp(value, 0.0, 1.0);
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            return value < minimum ? minimum : value > maximum ? maximum : value;
        }

        private static double Min(double left, double right)
        {
            return left < right ? left : right;
        }

        private static double Max(double left, double right)
        {
            return left > right ? left : right;
        }
    }
}
