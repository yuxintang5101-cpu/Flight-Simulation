using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Core
{
    public sealed class F16FuelSystem
    {
        private const double ExternalTransferRateKgps = 18.0;
        private const double LongitudinalTransferRateKgps = 10.0;
        private const double ReferenceCgPercentMac = 30.0;

        private readonly double internalCapacityKg;
        private FuelState state;
        private double leftInternalFuelKg;
        private double rightInternalFuelKg;
        private double externalFuelKg;
        private double longitudinalBiasPercentMac;

        public F16FuelSystem(F16AircraftDefinition definition)
        {
            internalCapacityKg = Max(0.0, definition.InternalFuelCapacityKg);
            Reset(internalCapacityKg, 0.0, 450.0, 900.0);
        }

        public FuelState State => state;

        public bool BingoReached => state.TotalFuelKg <= state.BingoFuelKg;

        public bool JokerReached => state.TotalFuelKg <= state.JokerFuelKg;

        public double AvailableFuelKg => state.FuelPumpEnabled ? state.TotalFuelKg : 0.0;

        public void Reset(double internalFuelKg, double externalFuelKg, double bingoFuelKg, double jokerFuelKg)
        {
            double clampedInternal = Clamp(internalFuelKg, 0.0, internalCapacityKg);
            leftInternalFuelKg = clampedInternal * 0.5;
            rightInternalFuelKg = clampedInternal - leftInternalFuelKg;
            this.externalFuelKg = Max(0.0, externalFuelKg);
            longitudinalBiasPercentMac = 0.0;

            state = default(FuelState);
            state.TransferMode = this.externalFuelKg > 0.0
                ? FuelTransferMode.Automatic
                : FuelTransferMode.Off;
            state.BingoFuelKg = Max(0.0, bingoFuelKg);
            state.JokerFuelKg = Max(state.BingoFuelKg, jokerFuelKg);
            state.FuelPumpEnabled = true;
            RefreshState(0.0);
        }

        public void Update(double demandedFuelFlowKgps, double deltaTimeS)
        {
            double dt = Max(0.0, deltaTimeS);
            UpdateLongitudinalTransfer(dt);

            if (state.FuelPumpEnabled &&
                (state.TransferMode == FuelTransferMode.Automatic ||
                 state.TransferMode == FuelTransferMode.ExternalToInternal))
            {
                TransferExternalToInternal(ExternalTransferRateKgps * dt);
            }

            double requestedBurnKg = state.FuelPumpEnabled
                ? Max(0.0, demandedFuelFlowKgps) * dt
                : 0.0;
            double burnedKg = BurnFuel(requestedBurnKg);
            double actualFuelFlowKgps = dt > 0.0 ? burnedKg / dt : 0.0;
            RefreshState(actualFuelFlowKgps);
        }

        public void SetTransferMode(FuelTransferMode mode)
        {
            state.TransferMode = mode;
        }

        public void SetFuelPumpEnabled(bool enabled)
        {
            state.FuelPumpEnabled = enabled;
            if (!enabled)
            {
                state.FuelFlowKgps = 0.0;
            }
        }

        public void SetInternalFuelDistribution(double leftFuelKg, double rightFuelKg)
        {
            double left = Max(0.0, leftFuelKg);
            double right = Max(0.0, rightFuelKg);
            double total = left + right;
            if (total > internalCapacityKg && total > 0.0)
            {
                double scale = internalCapacityKg / total;
                left *= scale;
                right *= scale;
            }

            leftInternalFuelKg = left;
            rightInternalFuelKg = right;
            RefreshState(0.0);
        }

        public void SetExternalFuel(double fuelKg)
        {
            externalFuelKg = Max(0.0, fuelKg);
            RefreshState(0.0);
        }

        public void Refuel(double internalFuelKg, double externalFuelKg)
        {
            double clampedInternal = Clamp(internalFuelKg, 0.0, internalCapacityKg);
            leftInternalFuelKg = clampedInternal * 0.5;
            rightInternalFuelKg = clampedInternal - leftInternalFuelKg;
            this.externalFuelKg = Max(0.0, externalFuelKg);
            RefreshState(0.0);
        }

        private double BurnFuel(double requestedKg)
        {
            if (requestedKg <= 0.0)
            {
                return 0.0;
            }

            double initialTotalKg = leftInternalFuelKg + rightInternalFuelKg + externalFuelKg;
            double internalRequestKg = Min(requestedKg, leftInternalFuelKg + rightInternalFuelKg);
            BurnInternalBalanced(internalRequestKg);

            double remainingRequestKg = requestedKg - internalRequestKg;
            if (remainingRequestKg > 0.0)
            {
                externalFuelKg = Max(0.0, externalFuelKg - remainingRequestKg);
            }

            double finalTotalKg = leftInternalFuelKg + rightInternalFuelKg + externalFuelKg;
            return initialTotalKg - finalTotalKg;
        }

        private void BurnInternalBalanced(double requestedKg)
        {
            double halfRequestKg = requestedKg * 0.5;
            double leftBurnKg = Min(leftInternalFuelKg, halfRequestKg);
            double rightBurnKg = Min(rightInternalFuelKg, halfRequestKg);
            leftInternalFuelKg -= leftBurnKg;
            rightInternalFuelKg -= rightBurnKg;

            double remainderKg = requestedKg - leftBurnKg - rightBurnKg;
            if (remainderKg > 0.0)
            {
                double additionalLeftKg = Min(leftInternalFuelKg, remainderKg);
                leftInternalFuelKg -= additionalLeftKg;
                remainderKg -= additionalLeftKg;
                rightInternalFuelKg = Max(0.0, rightInternalFuelKg - remainderKg);
            }
        }

        private void TransferExternalToInternal(double requestedKg)
        {
            double availableSpaceKg = Max(0.0, internalCapacityKg - leftInternalFuelKg - rightInternalFuelKg);
            double transferredKg = Min(requestedKg, Min(externalFuelKg, availableSpaceKg));
            if (transferredKg <= 0.0)
            {
                return;
            }

            double halfCapacityKg = internalCapacityKg * 0.5;
            double leftSpaceKg = Max(0.0, halfCapacityKg - leftInternalFuelKg);
            double rightSpaceKg = Max(0.0, halfCapacityKg - rightInternalFuelKg);
            double leftTransferKg = Min(leftSpaceKg, transferredKg * 0.5);
            double rightTransferKg = Min(rightSpaceKg, transferredKg * 0.5);
            double remainderKg = transferredKg - leftTransferKg - rightTransferKg;

            double additionalLeftKg = Min(leftSpaceKg - leftTransferKg, remainderKg);
            leftTransferKg += additionalLeftKg;
            remainderKg -= additionalLeftKg;
            rightTransferKg += Min(rightSpaceKg - rightTransferKg, remainderKg);

            leftInternalFuelKg += leftTransferKg;
            rightInternalFuelKg += rightTransferKg;
            externalFuelKg -= transferredKg;
        }

        private void UpdateLongitudinalTransfer(double dt)
        {
            double totalInternalKg = leftInternalFuelKg + rightInternalFuelKg;
            double maximumBias = totalInternalKg > 0.0 ? 2.0 : 0.0;
            double biasRate = totalInternalKg > 0.0
                ? (LongitudinalTransferRateKgps / totalInternalKg) * 4.0
                : 4.0;

            switch (state.TransferMode)
            {
                case FuelTransferMode.Forward:
                    longitudinalBiasPercentMac = MoveTowards(
                        longitudinalBiasPercentMac,
                        -maximumBias,
                        biasRate * dt);
                    break;
                case FuelTransferMode.Aft:
                    longitudinalBiasPercentMac = MoveTowards(
                        longitudinalBiasPercentMac,
                        maximumBias,
                        biasRate * dt);
                    break;
                case FuelTransferMode.Automatic:
                    longitudinalBiasPercentMac = MoveTowards(longitudinalBiasPercentMac, 0.0, biasRate * dt);
                    break;
            }
        }

        private void RefreshState(double actualFuelFlowKgps)
        {
            state.InternalFuelKg = leftInternalFuelKg + rightInternalFuelKg;
            state.ExternalFuelKg = externalFuelKg;
            state.LeftFuelKg = leftInternalFuelKg;
            state.RightFuelKg = rightInternalFuelKg;
            state.TotalFuelKg = state.InternalFuelKg + state.ExternalFuelKg;
            state.FuelImbalanceKg = Math.Abs(leftInternalFuelKg - rightInternalFuelKg);
            state.FuelFlowKgps = Max(0.0, actualFuelFlowKgps);
            state.LowFuelWarning = state.TotalFuelKg <= state.BingoFuelKg;

            double internalFraction = internalCapacityKg > 0.0
                ? Clamp01(state.InternalFuelKg / internalCapacityKg)
                : 0.0;
            double externalFraction = state.TotalFuelKg > 0.0
                ? state.ExternalFuelKg / state.TotalFuelKg
                : 0.0;
            state.CenterOfGravityPercentMac = Clamp(
                ReferenceCgPercentMac +
                longitudinalBiasPercentMac +
                (0.8 * (internalFraction - 0.5)) -
                (0.6 * externalFraction),
                24.0,
                36.0);
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
