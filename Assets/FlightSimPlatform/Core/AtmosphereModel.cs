using System;

namespace FlightSim.Platform.Core
{
    [Serializable]
    public readonly struct AtmosphereSample
    {
        public readonly double TemperatureK;
        public readonly double PressurePa;
        public readonly double DensityKgpm3;
        public readonly double SpeedOfSoundMps;

        public AtmosphereSample(
            double temperatureK,
            double pressurePa,
            double densityKgpm3,
            double speedOfSoundMps)
        {
            TemperatureK = temperatureK;
            PressurePa = pressurePa;
            DensityKgpm3 = densityKgpm3;
            SpeedOfSoundMps = speedOfSoundMps;
        }
    }

    public static class IsaAtmosphere
    {
        public const double SeaLevelTemperatureK = 288.15;
        public const double SeaLevelPressurePa = 101325.0;
        public const double SeaLevelDensityKgpm3 = 1.225;
        public const double SeaLevelSpeedOfSoundMps = 340.294;

        private const double EarthGeopotentialRadiusM = 6356766.0;
        private const double MinimumGeometricAltitudeM = -5000.0;
        private const double MaximumGeometricAltitudeM = 86000.0;
        private const double StandardGravityMps2 = 9.80665;
        private const double SpecificGasConstantJpkgK = 287.05287;
        private const double RatioOfSpecificHeats = 1.4;

        public static AtmosphereSample Sample(double geometricAltitudeM)
        {
            double boundedAltitudeM = ClampFinite(
                geometricAltitudeM,
                MinimumGeometricAltitudeM,
                MaximumGeometricAltitudeM);
            double geopotentialAltitudeM = EarthGeopotentialRadiusM * boundedAltitudeM /
                (EarthGeopotentialRadiusM + boundedAltitudeM);

            double baseAltitudeM;
            double baseTemperatureK;
            double basePressurePa;
            double lapseRateKpm;
            SelectLayer(
                geopotentialAltitudeM,
                out baseAltitudeM,
                out baseTemperatureK,
                out basePressurePa,
                out lapseRateKpm);

            double altitudeInLayerM = geopotentialAltitudeM - baseAltitudeM;
            double temperatureK = baseTemperatureK + lapseRateKpm * altitudeInLayerM;
            double pressurePa;
            if (lapseRateKpm == 0.0)
            {
                pressurePa = basePressurePa * Math.Exp(
                    -StandardGravityMps2 * altitudeInLayerM /
                    (SpecificGasConstantJpkgK * baseTemperatureK));
            }
            else
            {
                pressurePa = basePressurePa * Math.Pow(
                    temperatureK / baseTemperatureK,
                    -StandardGravityMps2 / (lapseRateKpm * SpecificGasConstantJpkgK));
            }

            double densityKgpm3 = pressurePa / (SpecificGasConstantJpkgK * temperatureK);
            double speedOfSoundMps = Math.Sqrt(RatioOfSpecificHeats * SpecificGasConstantJpkgK * temperatureK);
            return new AtmosphereSample(temperatureK, pressurePa, densityKgpm3, speedOfSoundMps);
        }

        private static void SelectLayer(
            double geopotentialAltitudeM,
            out double baseAltitudeM,
            out double baseTemperatureK,
            out double basePressurePa,
            out double lapseRateKpm)
        {
            if (geopotentialAltitudeM < 11000.0)
            {
                baseAltitudeM = 0.0;
                baseTemperatureK = SeaLevelTemperatureK;
                basePressurePa = SeaLevelPressurePa;
                lapseRateKpm = -0.0065;
            }
            else if (geopotentialAltitudeM < 20000.0)
            {
                baseAltitudeM = 11000.0;
                baseTemperatureK = 216.65;
                basePressurePa = 22632.06;
                lapseRateKpm = 0.0;
            }
            else if (geopotentialAltitudeM < 32000.0)
            {
                baseAltitudeM = 20000.0;
                baseTemperatureK = 216.65;
                basePressurePa = 5474.889;
                lapseRateKpm = 0.001;
            }
            else if (geopotentialAltitudeM < 47000.0)
            {
                baseAltitudeM = 32000.0;
                baseTemperatureK = 228.65;
                basePressurePa = 868.0187;
                lapseRateKpm = 0.0028;
            }
            else if (geopotentialAltitudeM < 51000.0)
            {
                baseAltitudeM = 47000.0;
                baseTemperatureK = 270.65;
                basePressurePa = 110.9063;
                lapseRateKpm = 0.0;
            }
            else if (geopotentialAltitudeM < 71000.0)
            {
                baseAltitudeM = 51000.0;
                baseTemperatureK = 270.65;
                basePressurePa = 66.93887;
                lapseRateKpm = -0.0028;
            }
            else
            {
                baseAltitudeM = 71000.0;
                baseTemperatureK = 214.65;
                basePressurePa = 3.956420;
                lapseRateKpm = -0.002;
            }
        }

        private static double ClampFinite(double value, double minimum, double maximum)
        {
            if (double.IsNaN(value))
            {
                return 0.0;
            }

            if (value <= minimum || double.IsNegativeInfinity(value))
            {
                return minimum;
            }

            if (value >= maximum || double.IsPositiveInfinity(value))
            {
                return maximum;
            }

            return value;
        }
    }
}
