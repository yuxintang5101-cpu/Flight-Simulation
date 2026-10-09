using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Integration
{
    internal static class DynamicWarningEvaluator
    {
        private const double StallWarningAngleOfAttackRad = 22.0 * Math.PI / 180.0;

        internal static WarningState Evaluate(
            in WarningState source,
            bool terrainSampleValid,
            bool hasTakenOff,
            bool weightOnWheels,
            double aglM,
            double climbRateMps,
            bool gearDown,
            double mach,
            double angleOfAttackRad)
        {
            WarningState warnings = source;
            warnings.LowAltitudeWarning = terrainSampleValid &&
                                          hasTakenOff &&
                                          !weightOnWheels &&
                                          aglM < 120.0 &&
                                          climbRateMps < -1.0;
            warnings.OverspeedWarning = mach > 2.05;
            warnings.StallWarning = angleOfAttackRad > StallWarningAngleOfAttackRad;
            warnings.LandingGearWarning = warnings.LandingGearWarning ||
                                          (hasTakenOff &&
                                           terrainSampleValid &&
                                           aglM < 180.0 &&
                                           climbRateMps < -0.5 &&
                                           !gearDown);
            warnings.MasterWarning = warnings.MasterWarning || warnings.FireWarning;
            warnings.MasterCaution = warnings.MasterCaution ||
                                     warnings.HydraulicWarning ||
                                     warnings.ElectricalWarning ||
                                     warnings.FuelWarning ||
                                     warnings.LowAltitudeWarning ||
                                     warnings.OverspeedWarning ||
                                     warnings.StallWarning ||
                                     warnings.LandingGearWarning ||
                                     warnings.CanopyWarning;
            return warnings;
        }
    }
}
