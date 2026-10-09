using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;

namespace FlightSim.Platform.Missions
{
    public struct MissionGuidanceTarget
    {
        public bool PositionValid;
        public double LongitudeRad;
        public double LatitudeRad;
        public double DesiredHeadingRad;
        public double DesiredAltitudeM;
        public double DesiredTrueAirspeedMps;
        public double DistanceM;

        public static MissionGuidanceTarget CreateHeadingAltitudeSpeed(
            double headingRad,
            double altitudeM,
            double trueAirspeedMps)
        {
            return new MissionGuidanceTarget
            {
                DesiredHeadingRad = headingRad,
                DesiredAltitudeM = altitudeM,
                DesiredTrueAirspeedMps = trueAirspeedMps
            };
        }
    }

    public struct MissionControlCommand
    {
        public PilotControlInput Input;
        public MissionAiMode NextMode;
        public bool HasLandingGearCommand;
        public bool LandingGearDown;
        public bool RequestTargetSelection;
        public bool RequestWeaponRelease;
    }

    public static class MissionAutopilot
    {
        private const double DegreesToRadians = Math.PI / 180.0;
        private const double RotateSpeedMps = 78.0;
        private const double MinimumTacticalTerrainClearanceM = 900.0;
        private const double TerrainRecoveryLookaheadS = 8.0;
        private const double ApproachFixCaptureRadiusM = 1200.0;
        private const double EvadeOverspeedOnsetMps = 30.0;
        private const double EvadeFullSpeedBrakeOverspeedMps = 170.0;
        private const double EvadeAoAProtectionOnsetRad = 12.0 * DegreesToRadians;
        private const double EvadeAoAProtectionFullRad = 20.0 * DegreesToRadians;

        public static MissionControlCommand Compute(
            in AircraftFastState fast,
            in AircraftSystemsState systems,
            MissionAiMode mode,
            in MissionGuidanceTarget target)
        {
            MissionControlCommand command = new MissionControlCommand { NextMode = mode };
            PilotControlInput input = PilotControlInput.Neutral;

            if (mode == MissionAiMode.Manual || mode == MissionAiMode.Stop)
            {
                input.WheelBrakeNormalized = mode == MissionAiMode.Stop ? 1.0 : 0.0;
                command.Input = input;
                return command;
            }

            if (mode == MissionAiMode.GroundStart)
            {
                input.WheelBrakeNormalized = 1.0;
                input.EngineStartCommand = !systems.Propulsion.EngineRunning;
                input.ThrottleNormalized = systems.Propulsion.EngineRunning ? 0.2 : 0.0;
                command.NextMode = systems.Propulsion.EngineRunning ? MissionAiMode.Takeoff : MissionAiMode.GroundStart;
                command.HasLandingGearCommand = true;
                command.LandingGearDown = true;
                command.Input = input;
                return command;
            }

            if (mode == MissionAiMode.Takeoff)
            {
                input.ThrottleNormalized = 1.0;
                input.WheelBrakeNormalized = 0.0;
                input.RollNormalized = Clamp(
                    NormalizeSignedAngle(target.DesiredHeadingRad - fast.HeadingRad) / (20.0 * DegreesToRadians),
                    -0.35,
                    0.35);
                input.YawNormalized = input.RollNormalized * 0.65;
                input.PitchNormalized = fast.CalibratedAirspeedMps >= RotateSpeedMps ? 0.38 : 0.0;
                command.NextMode = systems.LandingGear.WeightOnWheels
                    ? MissionAiMode.Takeoff
                    : MissionAiMode.Navigate;
                command.HasLandingGearCommand = true;
                command.LandingGearDown = systems.LandingGear.WeightOnWheels;
                command.Input = input;
                return command;
            }

            double desiredHeadingRad = target.PositionValid
                ? InitialBearingRad(
                    fast.LatitudeRad,
                    fast.LongitudeRad,
                    target.LatitudeRad,
                    target.LongitudeRad)
                : target.DesiredHeadingRad;
            double headingErrorRad = NormalizeSignedAngle(desiredHeadingRad - fast.HeadingRad);
            double altitudeErrorM = target.DesiredAltitudeM - fast.EllipsoidHeightM;
            double speedErrorMps = target.DesiredTrueAirspeedMps - fast.TrueAirspeedMps;
            bool landingMode = mode == MissionAiMode.Approach || mode == MissionAiMode.Land;
            bool evasionMode = mode == MissionAiMode.Evade;
            double evasionAoAProtection = 0.0;
            double maximumBankRad = (landingMode ? 24.0 : evasionMode ? 65.0 : 35.0) * DegreesToRadians;
            double desiredBankRad = Clamp(
                headingErrorRad * (evasionMode ? 0.85 : 0.45),
                -maximumBankRad,
                maximumBankRad);
            double bankErrorRad = NormalizeSignedAngle(desiredBankRad - fast.RollRad);
            input.RollNormalized = evasionMode
                ? Clamp(bankErrorRad / (65.0 * DegreesToRadians), -0.85, 0.85)
                : landingMode
                    ? Clamp(bankErrorRad / (90.0 * DegreesToRadians), -0.30, 0.30)
                    : Clamp(bankErrorRad / (180.0 * DegreesToRadians), -0.18, 0.18);
            input.YawNormalized = 0.0;
            double bankForCompensationRad = Clamp(fast.RollRad, -70.0 * DegreesToRadians, 70.0 * DegreesToRadians);
            double bankLoadCompensation = Math.Max(0.0, 1.0 / Math.Cos(bankForCompensationRad) - 1.0) * 0.11;
            input.PitchNormalized = Clamp(
                altitudeErrorM / 5000.0 - fast.ClimbRateMps / 45.0 + bankLoadCompensation,
                -0.2,
                0.35);
            input.ThrottleNormalized = Clamp(0.68 + speedErrorMps * 0.0045, 0.28, 1.0);

            if (landingMode)
            {
                command.HasLandingGearCommand = true;
                command.LandingGearDown = true;
                input.ThrottleNormalized = Clamp(0.32 + speedErrorMps * 0.010, 0.0, 0.65);
                input.SpeedBrakeNormalized = mode == MissionAiMode.Land
                    ? Clamp((fast.TrueAirspeedMps - target.DesiredTrueAirspeedMps - 5.0) / 50.0, 0.0, 0.60)
                    : Clamp((fast.TrueAirspeedMps - target.DesiredTrueAirspeedMps - 5.0) / 70.0, 0.0, 0.35);
                bool needsGlidePathCapture = target.DistanceM > 500.0 && altitudeErrorM < -40.0;
                double altitudeCaptureTimeS = needsGlidePathCapture ? 20.0 : 55.0;
                double desiredClimbRateMps = -Math.Max(0.0, fast.GroundSpeedMps) * 0.047 +
                                             altitudeErrorM / altitudeCaptureTimeS;
                if (mode == MissionAiMode.Land &&
                    target.DistanceM < 350.0 &&
                    fast.AboveGroundLevelAltitudeM < 25.0)
                {
                    desiredClimbRateMps = -1.5;
                }
                desiredClimbRateMps = Clamp(desiredClimbRateMps, needsGlidePathCapture ? -28.0 : -12.0, 3.0);
                input.PitchNormalized = Clamp(
                    (desiredClimbRateMps - fast.ClimbRateMps) / 3.0 + bankLoadCompensation,
                    needsGlidePathCapture ? -0.24 : -0.10,
                    0.60);
                if (systems.LandingGear.WeightOnWheels)
                {
                    input.ThrottleNormalized = 0.0;
                    input.SpeedBrakeNormalized = 1.0;
                    input.WheelBrakeNormalized = 1.0;
                    command.NextMode = fast.GroundSpeedMps < 2.0 ? MissionAiMode.Stop : MissionAiMode.Land;
                }
            }
            else
            {
                command.HasLandingGearCommand = true;
                command.LandingGearDown = false;
            }

            if (evasionMode)
            {
                double overspeedMps = Math.Max(0.0, fast.TrueAirspeedMps - target.DesiredTrueAirspeedMps);
                double overspeedFraction = Clamp(
                    (overspeedMps - EvadeOverspeedOnsetMps) /
                    (EvadeFullSpeedBrakeOverspeedMps - EvadeOverspeedOnsetMps),
                    0.0,
                    1.0);
                double loadProtection = Clamp((fast.NormalLoadFactorG - 6.0) / 2.0, 0.0, 1.0);
                evasionAoAProtection = Clamp(
                    (fast.AngleOfAttackRad - EvadeAoAProtectionOnsetRad) /
                    (EvadeAoAProtectionFullRad - EvadeAoAProtectionOnsetRad),
                    0.0,
                    1.0);
                double defensivePitch = Lerp(0.32, 0.16, overspeedFraction);
                input.PitchNormalized = Math.Max(input.PitchNormalized, defensivePitch);
                input.PitchNormalized = Math.Min(input.PitchNormalized, Lerp(0.32, 0.05, loadProtection));
                input.ThrottleNormalized = overspeedMps <= EvadeOverspeedOnsetMps
                    ? 1.0
                    : Clamp(0.55 - (overspeedMps - EvadeOverspeedOnsetMps) / 200.0, 0.0, 0.55);
                input.SpeedBrakeNormalized = 0.85 * overspeedFraction;
                command.RequestTargetSelection = false;
                command.RequestWeaponRelease = false;
            }

            if (mode == MissionAiMode.Intercept && target.DistanceM < 45000.0)
                command.NextMode = MissionAiMode.Engage;
            else if (mode == MissionAiMode.ReturnToBase && target.DistanceM < ApproachFixCaptureRadiusM)
                command.NextMode = MissionAiMode.Approach;
            else if (mode == MissionAiMode.Approach && target.DistanceM < 2500.0)
                command.NextMode = MissionAiMode.Land;

            command.RequestTargetSelection = mode == MissionAiMode.Intercept || mode == MissionAiMode.Engage;
            command.RequestWeaponRelease = mode == MissionAiMode.Engage && target.DistanceM > 1000.0 && target.DistanceM < 50000.0;
            double predictedTerrainClearanceM = fast.AboveGroundLevelAltitudeM +
                                                Math.Min(0.0, fast.ClimbRateMps) * TerrainRecoveryLookaheadS;
            bool terrainRecovery = !landingMode &&
                                   fast.TerrainSampleValid &&
                                   predictedTerrainClearanceM < MinimumTacticalTerrainClearanceM;
            if (terrainRecovery)
            {
                double clearanceErrorM = MinimumTacticalTerrainClearanceM - predictedTerrainClearanceM;
                double recoverySeverity = Clamp(
                    clearanceErrorM / MinimumTacticalTerrainClearanceM,
                    0.0,
                    1.0);
                double recoveryPitch = Clamp(
                    (MinimumTacticalTerrainClearanceM - fast.AboveGroundLevelAltitudeM) / 1500.0 -
                    fast.ClimbRateMps / 45.0 +
                    bankLoadCompensation,
                    0.0,
                    0.65);
                input.PitchNormalized = Math.Max(input.PitchNormalized, recoveryPitch);
                input.RollNormalized = Clamp(
                    -fast.RollRad / (60.0 * DegreesToRadians) * recoverySeverity,
                    -0.65,
                    0.65);
                input.ThrottleNormalized = 1.0;
                command.RequestWeaponRelease = false;
            }
            if (evasionMode && evasionAoAProtection > 0.0)
            {
                input.PitchNormalized = Math.Min(
                    input.PitchNormalized,
                    Lerp(0.32, 0.0, evasionAoAProtection));
                input.RollNormalized *= 1.0 - evasionAoAProtection;
                input.SpeedBrakeNormalized *= 1.0 - evasionAoAProtection;
            }
            command.Input = input;
            return command;
        }

        private static double InitialBearingRad(double lat1, double lon1, double lat2, double lon2)
        {
            double deltaLon = lon2 - lon1;
            return NormalizePositiveAngle(Math.Atan2(
                Math.Sin(deltaLon) * Math.Cos(lat2),
                Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(deltaLon)));
        }

        private static double NormalizeSignedAngle(double angleRad)
        {
            double twoPi = Math.PI * 2.0;
            double value = (angleRad + Math.PI) % twoPi;
            if (value < 0.0) value += twoPi;
            return value - Math.PI;
        }

        private static double NormalizePositiveAngle(double angleRad)
        {
            double twoPi = Math.PI * 2.0;
            double value = angleRad % twoPi;
            return value < 0.0 ? value + twoPi : value;
        }

        private static double Lerp(double from, double to, double amount)
        {
            return from + (to - from) * Clamp(amount, 0.0, 1.0);
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            return value < minimum ? minimum : value > maximum ? maximum : value;
        }
    }
}
