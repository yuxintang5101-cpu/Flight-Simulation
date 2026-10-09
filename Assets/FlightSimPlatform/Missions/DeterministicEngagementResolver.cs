using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Missions
{
    public static class DeterministicEngagementResolver
    {
        public static WeaponEngagementDecision Resolve(
            in WeaponEngagementRequest request,
            int seed,
            int engagementSequence)
        {
            if (string.IsNullOrWhiteSpace(request.WeaponType))
                return Reject("No weapon is selected.");
            if (string.IsNullOrEmpty(request.Target.Value))
                return Reject("No target is selected.");
            bool shortRangeWeapon = request.WeaponType.IndexOf("AIM-9", StringComparison.OrdinalIgnoreCase) >= 0;
            double maximumRangeM = shortRangeWeapon ? 22000.0 : 80000.0;
            if (!(request.RangeM > 0.0) || request.RangeM > maximumRangeM)
                return Reject("Target is outside the supported engagement range.");
            if (request.LockQualityNormalized < 0.5)
                return Reject("Radar lock quality is insufficient.");

            double rangeFactor = Clamp01(1.0 - request.RangeM / maximumRangeM);
            double aspectFactor = 0.65 + 0.35 * Math.Cos(Math.Min(Math.PI, Math.Abs(request.AspectAngleRad)));
            double probability = Clamp01(
                0.18 +
                0.48 * request.LockQualityNormalized +
                0.25 * rangeFactor +
                0.09 * aspectFactor);
            double shooterFactor = 0.5 + 0.5 * Clamp01(request.ShooterSkillNormalized);
            double evasionFactor = 1.0 - 0.75 * Clamp01(request.TargetEvasionNormalized);
            probability = Clamp01(probability * shooterFactor * evasionFactor);
            double score = HashToUnitInterval(seed, engagementSequence, request.Shooter.Value, request.Target.Value, request.WeaponType);
            double closingSpeedMps = Math.Max(250.0, 850.0 + request.ClosureRateMps * 0.25);
            return new WeaponEngagementDecision
            {
                Accepted = true,
                Outcome = score <= probability ? WeaponEngagementOutcome.Hit : WeaponEngagementOutcome.Miss,
                TimeToImpactS = request.RangeM / closingSpeedMps,
                DeterministicScoreNormalized = score,
                ProbabilityOfHitNormalized = probability,
                Reason = score <= probability ? "Deterministic tactical hit." : "Deterministic tactical miss."
            };
        }

        private static WeaponEngagementDecision Reject(string reason)
        {
            return new WeaponEngagementDecision
            {
                Accepted = false,
                Outcome = WeaponEngagementOutcome.Rejected,
                Reason = reason
            };
        }

        private static double HashToUnitInterval(int seed, int sequence, params string[] values)
        {
            unchecked
            {
                uint hash = 2166136261u;
                hash = (hash ^ (uint)seed) * 16777619u;
                hash = (hash ^ (uint)sequence) * 16777619u;
                for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
                {
                    string value = values[valueIndex] ?? string.Empty;
                    for (int index = 0; index < value.Length; index++)
                        hash = (hash ^ value[index]) * 16777619u;
                }
                return hash / (double)uint.MaxValue;
            }
        }

        private static double Clamp01(double value) => value < 0.0 ? 0.0 : value > 1.0 ? 1.0 : value;
    }
}
