using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Missions
{
    [Serializable]
    public sealed class MissionTerrainCache : ITerrainQuery
    {
        public int SchemaVersion = 1;
        public string MissionId = string.Empty;
        public double LongitudeMinRad;
        public double LongitudeMaxRad;
        public double LatitudeMinRad;
        public double LatitudeMaxRad;
        public double HorizontalSpacingM = 250.0;
        public int ColumnCount;
        public int RowCount;
        public double[] HeightM = Array.Empty<double>();
        public bool[] SampleValid = Array.Empty<bool>();
        public int ValidSampleCount;
        public TerrainSource DeclaredSource = TerrainSource.MissionCache;

        public TerrainSource Source => DeclaredSource;

        public bool TryGetHeightM(double longitudeRad, double latitudeRad, out double ellipsoidHeightM)
        {
            ellipsoidHeightM = 0.0;
            if (ColumnCount < 2 || RowCount < 2 || HeightM == null ||
                HeightM.Length != ColumnCount * RowCount ||
                !(LongitudeMaxRad > LongitudeMinRad) || !(LatitudeMaxRad > LatitudeMinRad) ||
                longitudeRad < LongitudeMinRad || longitudeRad > LongitudeMaxRad ||
                latitudeRad < LatitudeMinRad || latitudeRad > LatitudeMaxRad)
            {
                return false;
            }

            double normalizedX = (longitudeRad - LongitudeMinRad) / (LongitudeMaxRad - LongitudeMinRad);
            double normalizedY = (latitudeRad - LatitudeMinRad) / (LatitudeMaxRad - LatitudeMinRad);
            double gridX = Math.Min(ColumnCount - 1.0, normalizedX * (ColumnCount - 1));
            double gridY = Math.Min(RowCount - 1.0, normalizedY * (RowCount - 1));
            int x0 = Math.Min(ColumnCount - 2, Math.Max(0, (int)Math.Floor(gridX)));
            int y0 = Math.Min(RowCount - 2, Math.Max(0, (int)Math.Floor(gridY)));
            int x1 = x0 + 1;
            int y1 = y0 + 1;
            double tx = gridX - x0;
            double ty = gridY - y0;
            double h00 = HeightM[y0 * ColumnCount + x0];
            double h10 = HeightM[y0 * ColumnCount + x1];
            double h01 = HeightM[y1 * ColumnCount + x0];
            double h11 = HeightM[y1 * ColumnCount + x1];
            if (SampleValid != null && SampleValid.Length == HeightM.Length &&
                (!SampleValid[y0 * ColumnCount + x0] || !SampleValid[y0 * ColumnCount + x1] ||
                 !SampleValid[y1 * ColumnCount + x0] || !SampleValid[y1 * ColumnCount + x1]))
                return false;
            if (!IsFinite(h00) || !IsFinite(h10) || !IsFinite(h01) || !IsFinite(h11))
                return false;
            ellipsoidHeightM = Lerp(Lerp(h00, h10, tx), Lerp(h01, h11, tx), ty);
            return IsFinite(ellipsoidHeightM);
        }

        public static MissionTerrainCache CreateKtexFlat(double ellipsoidHeightM = 2765.0)
        {
            const double radians = Math.PI / 180.0;
            return new MissionTerrainCache
            {
                MissionId = "KTEX",
                LongitudeMinRad = -107.93 * radians,
                LongitudeMaxRad = -107.88 * radians,
                LatitudeMinRad = 37.94 * radians,
                LatitudeMaxRad = 37.97 * radians,
                HorizontalSpacingM = 250.0,
                ColumnCount = 2,
                RowCount = 2,
                HeightM = new[] { ellipsoidHeightM, ellipsoidHeightM, ellipsoidHeightM, ellipsoidHeightM },
                SampleValid = new[] { true, true, true, true },
                ValidSampleCount = 4,
                DeclaredSource = TerrainSource.AnalyticRunway
            };
        }

        private static double Lerp(double first, double second, double amount)
        {
            return first + (second - first) * amount;
        }

        private static bool IsFinite(double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value);
        }
    }
}
