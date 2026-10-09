using FlightSim.Platform.Contracts;
using UnityEngine;

namespace FlightSim.Platform.Presentation
{
    public readonly struct HudProjection
    {
        public readonly Vector2 Position;
        public readonly bool IsClipped;

        public HudProjection(Vector2 position, bool isClipped)
        {
            Position = position;
            IsClipped = isClipped;
        }
    }

    public static class F16HudLayoutMath
    {
        public const float ReferenceWidth = 1920f;
        public const float ReferenceHeight = 1080f;
        public const float CombinerWidth = 760f;
        public const float CombinerHeight = 640f;
        public const float CombinerRenderedWidth = 440f;
        public const float CombinerRenderedHeight = 410f;
        public const float CombinerVerticalOffset = 205f;
        public const float CombinerFieldOfViewDegrees = 25f;

        private const double KnotsPerMeterPerSecond = 1.94384449;
        private const double FeetPerMeter = 3.28083989501312;
        private const double FeetPerMinutePerMeterPerSecond = 196.850393700787;
        private const double NauticalMilesPerMeter = 1.0 / 1852.0;
        private const float SafeEdgePadding = 24f;

        public static bool IsDisplayable(HudState state)
        {
            if (!state.IsValid || state.Mode == HudMode.Off)
                return false;

            if (!IsFinite(state.CalibratedAirspeedMps) ||
                !IsFinite(state.TrueAirspeedMps) ||
                !IsFinite(state.BarometricAltitudeM) ||
                !IsFinite(state.HeadingRad) ||
                !IsFinite(state.PitchRad) ||
                !IsFinite(state.RollRad) ||
                !IsFinite(state.FlightPathAzimuthRad) ||
                !IsFinite(state.FlightPathElevationRad) ||
                !IsFinite(state.AngleOfAttackRad) ||
                !IsFinite(state.NormalLoadFactorG) ||
                !IsFinite(state.Mach) ||
                !IsFinite(state.VerticalSpeedMps))
            {
                return false;
            }

            if (state.RadarAltitudeValid && !IsFinite(state.RadarAltitudeM))
                return false;

            return state.SelectedSteerpointIndex <= 0 ||
                   IsFinite(state.SteerpointBearingRad) &&
                   IsFinite(state.SteerpointElevationRad) &&
                   IsFinite(state.SteerpointDistanceM);
        }

        public static Rect CalculateSafeRect(float availableWidth, float availableHeight)
        {
            float usableWidth = Mathf.Max(1f, availableWidth - SafeEdgePadding * 2f);
            float usableHeight = Mathf.Max(1f, availableHeight - SafeEdgePadding * 2f);
            float scale = Mathf.Min(1f, usableWidth / CombinerWidth, usableHeight / CombinerHeight);
            Vector2 size = new Vector2(CombinerWidth * scale, CombinerHeight * scale);
            return new Rect(size * -0.5f, size);
        }

        public static HudProjection ProjectAngle(
            double azimuthRad,
            double elevationRad,
            Rect safeRect,
            float inset)
        {
            float halfFovRad = CombinerFieldOfViewDegrees * 0.5f * Mathf.Deg2Rad;
            float x = (float)(System.Math.Tan(azimuthRad) / Mathf.Tan(halfFovRad)) * safeRect.width * 0.5f;
            float y = (float)(System.Math.Tan(elevationRad) / Mathf.Tan(halfFovRad)) * safeRect.height * 0.5f;

            float minX = safeRect.xMin + inset;
            float maxX = safeRect.xMax - inset;
            float minY = safeRect.yMin + inset;
            float maxY = safeRect.yMax - inset;
            Vector2 unclamped = new Vector2(x, y);
            Vector2 clamped = new Vector2(
                Mathf.Clamp(unclamped.x, minX, maxX),
                Mathf.Clamp(unclamped.y, minY, maxY));
            bool clipped = !Mathf.Approximately(unclamped.x, clamped.x) ||
                           !Mathf.Approximately(unclamped.y, clamped.y);
            return new HudProjection(clamped, clipped);
        }

        public static float WrapDegrees(float degrees)
        {
            return Mathf.Repeat(degrees, 360f);
        }

        public static double MetersPerSecondToKnots(double metersPerSecond)
        {
            return metersPerSecond * KnotsPerMeterPerSecond;
        }

        public static double MetersToFeet(double meters)
        {
            return meters * FeetPerMeter;
        }

        public static double MetersPerSecondToFeetPerMinute(double metersPerSecond)
        {
            return metersPerSecond * FeetPerMinutePerMeterPerSecond;
        }

        public static double MetersToNauticalMiles(double meters)
        {
            return meters * NauticalMilesPerMeter;
        }

        public static Vector2 Rotate(Vector2 point, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);
            return new Vector2(point.x * cos - point.y * sin, point.x * sin + point.y * cos);
        }

        public static bool IsFinite(double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value);
        }

    }
}
