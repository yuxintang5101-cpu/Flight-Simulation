using FlightSim.Platform.Contracts;
using TMPro;
using UnityEngine;

namespace FlightSim.Platform.Presentation
{
    internal readonly struct F16HudStyle
    {
        public readonly Color Primary;
        public readonly Color Dim;
        public readonly Color Warning;
        public readonly Color Caution;
        public readonly float LineWidth;

        public F16HudStyle(Color primary, Color dim, Color warning, Color caution, float lineWidth)
        {
            Primary = primary;
            Dim = dim;
            Warning = warning;
            Caution = caution;
            LineWidth = lineWidth;
        }
    }

    internal static class F16HudComposer
    {
        private const float SmallText = 14f;
        private const float NormalText = 17f;
        private const float ValueText = 22f;
        private const float WarningText = 20f;
        private const float PitchPixelsPerDegree = 9.2f;

        public static void Compose(
            HudState state,
            AircraftCombatState combat,
            Rect safeRect,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            vectors.Clear();
            labels.BeginFrame();

            DrawCombinerCorners(safeRect, vectors, style);

            HudProjection fpmProjection = F16HudLayoutMath.ProjectAngle(
                state.FlightPathAzimuthRad,
                state.FlightPathElevationRad,
                safeRect,
                48f);

            DrawPitchLadder(state, fpmProjection.Position.x, vectors, labels, style);
            DrawRollIndicator(state, vectors, style);
            DrawHeadingTape(state, vectors, labels, style);
            DrawSpeedTape(state, vectors, labels, style);
            DrawAltitudeTape(state, vectors, labels, style);
            DrawVerticalVelocity(state, vectors, labels, style);
            DrawNavigation(state, fpmProjection.Position, safeRect, vectors, labels, style);
            DrawBoresight(vectors, style);
            DrawFlightPathMarker(fpmProjection, vectors, style);

            if (state.Mode == HudMode.Landing)
                DrawLandingCues(state, fpmProjection.Position, vectors, labels, style);
            else if (state.Mode == HudMode.AirToAir)
                DrawAirToAir(combat, safeRect, vectors, labels, style);

            DrawStatusData(state, labels, style);
            DrawWarnings(state, labels, style);

            labels.EndFrame();
        }

        private static void DrawAirToAir(
            AircraftCombatState combat,
            Rect safeRect,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            labels.Acquire("A-A", new Vector2(-336f, -294f), new Vector2(70f, 22f), NormalText, style.Primary);
            string arm = combat.MasterArm == MasterArmState.Arm
                ? "ARM"
                : combat.MasterArm == MasterArmState.Simulate ? "SIM" : "SAFE";
            labels.Acquire(arm, new Vector2(-270f, -294f), new Vector2(62f, 22f), NormalText,
                combat.MasterArm == MasterArmState.Arm ? style.Primary : style.Caution);
            labels.Acquire(
                string.IsNullOrEmpty(combat.SelectedStoreType) ? "NO WPN" : combat.SelectedStoreType,
                new Vector2(-175f, -294f), new Vector2(125f, 22f), NormalText, style.Primary);
            labels.AcquireNumber("{0:0}", Mathf.Max(0, combat.SelectedStoreQuantity),
                new Vector2(-96f, -294f), new Vector2(38f, 22f), NormalText, style.Primary);

            if (!combat.IsValid || string.IsNullOrEmpty(combat.SelectedTarget.Value))
            {
                labels.Acquire("NO RAD", new Vector2(0f, 190f), new Vector2(120f, 24f), NormalText, style.Dim);
                return;
            }

            HudProjection target = F16HudLayoutMath.ProjectAngle(
                combat.TargetBearingRad,
                combat.TargetElevationRad,
                safeRect,
                42f);
            Color trackColor = combat.RadarTrackState == RadarTrackState.Locked ? style.Primary : style.Dim;
            if (target.IsClipped)
            {
                vectors.AddDiamond(target.Position, 15f, style.LineWidth, trackColor);
                Vector2 direction = target.Position.sqrMagnitude > 1f ? -target.Position.normalized : Vector2.down;
                vectors.AddLine(target.Position, target.Position + direction * 22f, style.LineWidth, trackColor);
            }
            else
            {
                float half = combat.RadarTrackState == RadarTrackState.Locked ? 35f : 27f;
                Rect targetBox = new Rect(target.Position - Vector2.one * half, Vector2.one * half * 2f);
                vectors.AddBox(targetBox, style.LineWidth, trackColor);
                if (combat.RadarTrackState == RadarTrackState.Locked)
                    vectors.AddCircle(target.Position, 9f, style.LineWidth * 0.7f, style.Dim, 16);
            }

            double rangeNm = F16HudLayoutMath.MetersToNauticalMiles(combat.TargetRangeM);
            labels.AcquireNumber("R {0:0.0}", (float)rangeNm,
                target.Position + new Vector2(0f, -49f), new Vector2(98f, 22f), NormalText, trackColor);
            labels.AcquireNumber("VC {0:+000;-000;000}",
                (float)F16HudLayoutMath.MetersPerSecondToKnots(combat.ClosureRateMps),
                new Vector2(300f, -252f), new Vector2(122f, 22f), SmallText, style.Primary);

            const float dlzX = 190f;
            const float dlzBottom = -135f;
            const float dlzTop = 130f;
            vectors.AddLine(new Vector2(dlzX, dlzBottom), new Vector2(dlzX, dlzTop), style.LineWidth, style.Dim);
            vectors.AddLine(new Vector2(dlzX - 11f, dlzBottom), new Vector2(dlzX + 11f, dlzBottom), style.LineWidth, style.Dim);
            vectors.AddLine(new Vector2(dlzX - 11f, dlzTop), new Vector2(dlzX + 11f, dlzTop), style.LineWidth, style.Dim);
            float normalizedRange = Mathf.Clamp01((float)(combat.TargetRangeM / 60000.0));
            float rangeY = Mathf.Lerp(dlzTop, dlzBottom, normalizedRange);
            vectors.AddLine(new Vector2(dlzX - 17f, rangeY), new Vector2(dlzX + 17f, rangeY), style.LineWidth * 1.3f,
                combat.InLaunchZone ? style.Primary : style.Caution);
            labels.Acquire("DLZ", new Vector2(dlzX, dlzTop + 19f), new Vector2(54f, 20f), SmallText, style.Dim);

            string lockText = combat.RadarTrackState == RadarTrackState.Locked ? "LOCK" : "TRACK";
            labels.Acquire(lockText, new Vector2(0f, 190f), new Vector2(100f, 24f), NormalText, trackColor);
            if (combat.ShootCue)
                labels.Acquire("SHOOT", new Vector2(0f, 154f), new Vector2(130f, 31f), WarningText, style.Primary);
            if (combat.MissileLaunchWarning)
                labels.Acquire("MISSILE", new Vector2(0f, 229f), new Vector2(150f, 30f), WarningText, style.Warning);
        }

        private static void DrawCombinerCorners(
            Rect safeRect,
            HudVectorCommandBuffer vectors,
            F16HudStyle style)
        {
            const float corner = 25f;
            float width = style.LineWidth * 0.55f;
            Color color = style.Dim;

            vectors.AddLine(new Vector2(safeRect.xMin, safeRect.yMax), new Vector2(safeRect.xMin + corner, safeRect.yMax), width, color);
            vectors.AddLine(new Vector2(safeRect.xMin, safeRect.yMax), new Vector2(safeRect.xMin, safeRect.yMax - corner), width, color);
            vectors.AddLine(new Vector2(safeRect.xMax - corner, safeRect.yMax), new Vector2(safeRect.xMax, safeRect.yMax), width, color);
            vectors.AddLine(new Vector2(safeRect.xMax, safeRect.yMax), new Vector2(safeRect.xMax, safeRect.yMax - corner), width, color);
            vectors.AddLine(new Vector2(safeRect.xMin, safeRect.yMin), new Vector2(safeRect.xMin + corner, safeRect.yMin), width, color);
            vectors.AddLine(new Vector2(safeRect.xMin, safeRect.yMin), new Vector2(safeRect.xMin, safeRect.yMin + corner), width, color);
            vectors.AddLine(new Vector2(safeRect.xMax - corner, safeRect.yMin), new Vector2(safeRect.xMax, safeRect.yMin), width, color);
            vectors.AddLine(new Vector2(safeRect.xMax, safeRect.yMin), new Vector2(safeRect.xMax, safeRect.yMin + corner), width, color);
        }

        private static void DrawPitchLadder(
            HudState state,
            float driftX,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            float pitchDegrees = (float)state.PitchRad * Mathf.Rad2Deg;
            float rollDegrees = (float)state.RollRad * Mathf.Rad2Deg;
            var clip = new Rect(-214f, -205f, 428f, 410f);

            for (int pitchLine = -90; pitchLine <= 90; pitchLine += 5)
            {
                float y = (pitchLine - pitchDegrees) * PitchPixelsPerDegree;
                if (Mathf.Abs(y) > 275f)
                    continue;

                bool horizon = pitchLine == 0;
                bool major = pitchLine % 10 == 0;
                float outer = horizon ? 116f : major ? 78f : 57f;
                float inner = horizon ? 16f : 21f;
                float lineWidth = horizon || major ? style.LineWidth : style.LineWidth * 0.65f;
                Color color = horizon ? style.Primary : style.Dim;

                Vector2 center = new Vector2(driftX, y);
                Vector2 leftOuter = F16HudLayoutMath.Rotate(center + Vector2.left * outer, -rollDegrees);
                Vector2 leftInner = F16HudLayoutMath.Rotate(center + Vector2.left * inner, -rollDegrees);
                Vector2 rightInner = F16HudLayoutMath.Rotate(center + Vector2.right * inner, -rollDegrees);
                Vector2 rightOuter = F16HudLayoutMath.Rotate(center + Vector2.right * outer, -rollDegrees);

                bool dashed = pitchLine < 0;
                AddClippedLine(vectors, leftOuter, leftInner, clip, lineWidth, color, dashed);
                AddClippedLine(vectors, rightInner, rightOuter, clip, lineWidth, color, dashed);

                if (!horizon && major)
                {
                    Vector2 leftLabel = F16HudLayoutMath.Rotate(center + Vector2.left * (outer + 24f), -rollDegrees);
                    Vector2 rightLabel = F16HudLayoutMath.Rotate(center + Vector2.right * (outer + 24f), -rollDegrees);
                    if (clip.Contains(leftLabel))
                        labels.AcquireNumber("{0:0}", Mathf.Abs(pitchLine), leftLabel, new Vector2(38f, 20f), SmallText, color);
                    if (clip.Contains(rightLabel))
                        labels.AcquireNumber("{0:0}", Mathf.Abs(pitchLine), rightLabel, new Vector2(38f, 20f), SmallText, color);
                }
            }
        }

        private static void DrawRollIndicator(HudState state, HudVectorCommandBuffer vectors, F16HudStyle style)
        {
            Vector2 center = new Vector2(0f, 119f);
            const float radius = 76f;
            vectors.AddArc(center, radius, 42f, 138f, style.LineWidth * 0.55f, style.Dim, 20);

            for (int bank = -30; bank <= 30; bank += 10)
            {
                float angle = (90f + bank) * Mathf.Deg2Rad;
                float tick = bank == 0 ? 12f : 8f;
                Vector2 outer = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                Vector2 inner = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * (radius - tick);
                vectors.AddLine(inner, outer, style.LineWidth * 0.65f, style.Dim);
            }

            float roll = Mathf.Clamp((float)state.RollRad * Mathf.Rad2Deg, -30f, 30f);
            float markerAngle = (90f - roll) * Mathf.Deg2Rad;
            Vector2 marker = center + new Vector2(Mathf.Cos(markerAngle), Mathf.Sin(markerAngle)) * (radius + 2f);
            Vector2 inward = (center - marker).normalized;
            Vector2 side = new Vector2(-inward.y, inward.x) * 6f;
            vectors.AddLine(marker, marker + inward * 11f + side, style.LineWidth, style.Primary);
            vectors.AddLine(marker, marker + inward * 11f - side, style.LineWidth, style.Primary);
        }

        private static void DrawHeadingTape(
            HudState state,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            const float y = 276f;
            const float halfWidth = 210f;
            const float pixelsPerDegree = 7.2f;
            float heading = F16HudLayoutMath.WrapDegrees((float)state.HeadingRad * Mathf.Rad2Deg);

            vectors.AddLine(new Vector2(-halfWidth, y), new Vector2(halfWidth, y), style.LineWidth * 0.65f, style.Primary);
            for (int offset = -25; offset <= 25; offset += 5)
            {
                float x = offset * pixelsPerDegree;
                bool major = offset % 10 == 0;
                float tick = major ? 13f : 8f;
                vectors.AddLine(new Vector2(x, y), new Vector2(x, y - tick), style.LineWidth * 0.65f, major ? style.Primary : style.Dim);
                if (major)
                {
                    float tickHeading = F16HudLayoutMath.WrapDegrees(heading + offset);
                    int headingTens = Mathf.RoundToInt(tickHeading / 10f);
                    labels.AcquireNumber(
                        "{0:00}",
                        headingTens == 0 ? 36f : headingTens,
                        new Vector2(x, y - 25f),
                        new Vector2(42f, 20f),
                        SmallText,
                        style.Primary);
                }
            }

            labels.AcquireNumber(
                "{0:000}",
                Mathf.RoundToInt(heading),
                new Vector2(0f, 305f),
                new Vector2(82f, 26f),
                ValueText,
                style.Primary);

            vectors.AddLine(new Vector2(-9f, y + 12f), new Vector2(0f, y + 2f), style.LineWidth, style.Primary);
            vectors.AddLine(new Vector2(9f, y + 12f), new Vector2(0f, y + 2f), style.LineWidth, style.Primary);

            if (state.SelectedSteerpointIndex > 0)
            {
                float bearing = (float)state.SteerpointBearingRad * Mathf.Rad2Deg;
                float delta = Mathf.DeltaAngle(heading, bearing);
                float cueX = Mathf.Clamp(delta, -25f, 25f) * pixelsPerDegree;
                vectors.AddLine(new Vector2(cueX - 7f, y + 22f), new Vector2(cueX, y + 14f), style.LineWidth, style.Dim);
                vectors.AddLine(new Vector2(cueX + 7f, y + 22f), new Vector2(cueX, y + 14f), style.LineWidth, style.Dim);
            }
        }

        private static void DrawSpeedTape(
            HudState state,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            const float spineX = -244f;
            const float centerX = -300f;
            const float halfHeight = 145f;
            const float pixelsPerKnot = 2f;
            float speed = Mathf.Max(0f, (float)F16HudLayoutMath.MetersPerSecondToKnots(state.CalibratedAirspeedMps));

            vectors.AddLine(new Vector2(spineX, -halfHeight), new Vector2(spineX, halfHeight), style.LineWidth * 0.55f, style.Dim);
            float first = Mathf.Floor((speed - 80f) / 10f) * 10f;
            for (float value = first; value <= speed + 80f; value += 10f)
            {
                if (value < 0f)
                    continue;
                float y = (value - speed) * pixelsPerKnot;
                if (Mathf.Abs(y) > halfHeight || Mathf.Abs(y) < 24f)
                    continue;

                bool major = Mathf.RoundToInt(value) % 20 == 0;
                float tick = major ? 18f : 10f;
                vectors.AddLine(new Vector2(spineX, y), new Vector2(spineX - tick, y), style.LineWidth * 0.65f, major ? style.Primary : style.Dim);
                if (major)
                {
                    labels.AcquireNumber(
                        "{0:0}",
                        Mathf.RoundToInt(value),
                        new Vector2(centerX - 2f, y),
                        new Vector2(76f, 19f),
                        SmallText,
                        style.Primary,
                        TextAlignmentOptions.Right);
                }
            }

            Rect valueBox = new Rect(centerX - 44f, -18f, 88f, 36f);
            vectors.AddBox(valueBox, style.LineWidth, style.Primary);
            vectors.AddLine(new Vector2(valueBox.xMax, 12f), new Vector2(spineX, 0f), style.LineWidth, style.Primary);
            vectors.AddLine(new Vector2(valueBox.xMax, -12f), new Vector2(spineX, 0f), style.LineWidth, style.Primary);
            labels.AcquireNumber(
                "{0:000}",
                Mathf.RoundToInt(speed),
                new Vector2(centerX, 0f),
                valueBox.size,
                ValueText,
                style.Primary);
            labels.Acquire("KCAS", new Vector2(centerX, 174f), new Vector2(90f, 20f), SmallText, style.Dim);
        }

        private static void DrawAltitudeTape(
            HudState state,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            const float spineX = 242f;
            const float centerX = 292f;
            const float halfHeight = 145f;
            const float pixelsPerFoot = 0.18f;
            float altitude = Mathf.Max(0f, (float)F16HudLayoutMath.MetersToFeet(state.BarometricAltitudeM));

            vectors.AddLine(new Vector2(spineX, -halfHeight), new Vector2(spineX, halfHeight), style.LineWidth * 0.55f, style.Dim);
            float first = Mathf.Floor((altitude - 900f) / 100f) * 100f;
            for (float value = first; value <= altitude + 900f; value += 100f)
            {
                if (value < 0f)
                    continue;
                float y = (value - altitude) * pixelsPerFoot;
                if (Mathf.Abs(y) > halfHeight || Mathf.Abs(y) < 24f)
                    continue;

                bool major = Mathf.RoundToInt(value) % 500 == 0;
                float tick = major ? 18f : 10f;
                vectors.AddLine(new Vector2(spineX, y), new Vector2(spineX + tick, y), style.LineWidth * 0.65f, major ? style.Primary : style.Dim);
                if (major)
                {
                    labels.AcquireNumber(
                        "{0:0}",
                        Mathf.RoundToInt(value),
                        new Vector2(centerX + 2f, y),
                        new Vector2(88f, 19f),
                        SmallText,
                        style.Primary,
                        TextAlignmentOptions.Left);
                }
            }

            Rect valueBox = new Rect(centerX - 49f, -18f, 98f, 36f);
            vectors.AddBox(valueBox, style.LineWidth, style.Primary);
            vectors.AddLine(new Vector2(valueBox.xMin, 12f), new Vector2(spineX, 0f), style.LineWidth, style.Primary);
            vectors.AddLine(new Vector2(valueBox.xMin, -12f), new Vector2(spineX, 0f), style.LineWidth, style.Primary);
            labels.AcquireNumber(
                "{0:00000}",
                Mathf.RoundToInt(altitude),
                new Vector2(centerX, 0f),
                valueBox.size,
                ValueText,
                style.Primary);
            labels.Acquire("ALT FT", new Vector2(centerX, 174f), new Vector2(100f, 20f), SmallText, style.Dim);
        }

        private static void DrawVerticalVelocity(
            HudState state,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            const float x = 356f;
            const float halfHeight = 126f;
            const float pixelsPerThousand = 21f;
            float feetPerMinute = (float)F16HudLayoutMath.MetersPerSecondToFeetPerMinute(state.VerticalSpeedMps);

            vectors.AddLine(new Vector2(x, -halfHeight), new Vector2(x, halfHeight), style.LineWidth * 0.55f, style.Dim);
            for (int thousands = -6; thousands <= 6; thousands++)
            {
                float y = thousands * pixelsPerThousand;
                float tick = thousands == 0 || thousands % 2 == 0 ? 10f : 6f;
                vectors.AddLine(new Vector2(x - tick, y), new Vector2(x + tick, y), style.LineWidth * 0.55f, thousands == 0 ? style.Primary : style.Dim);
            }

            float markerY = Mathf.Clamp(feetPerMinute / 1000f, -6f, 6f) * pixelsPerThousand;
            vectors.AddLine(new Vector2(x - 16f, markerY), new Vector2(x + 16f, markerY), style.LineWidth * 1.2f, style.Primary);
            labels.Acquire("VVI", new Vector2(x, 174f), new Vector2(48f, 20f), SmallText, style.Dim);
            labels.AcquireNumber(
                "{0:+0000;-0000;00000}",
                Mathf.RoundToInt(feetPerMinute),
                new Vector2(x - 4f, -168f),
                new Vector2(74f, 20f),
                SmallText,
                style.Primary);
        }

        private static void DrawNavigation(
            HudState state,
            Vector2 fpm,
            Rect safeRect,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            if (state.SelectedSteerpointIndex <= 0)
                return;

            float heading = (float)state.HeadingRad * Mathf.Rad2Deg;
            float bearing = (float)state.SteerpointBearingRad * Mathf.Rad2Deg;
            float relativeBearingDegrees = Mathf.DeltaAngle(heading, bearing);
            HudProjection steerpoint = F16HudLayoutMath.ProjectAngle(
                relativeBearingDegrees * Mathf.Deg2Rad,
                state.SteerpointElevationRad,
                safeRect,
                36f);
            vectors.AddDiamond(steerpoint.Position, 13f, style.LineWidth, style.Primary);
            if (steerpoint.IsClipped)
                vectors.AddCross(steerpoint.Position, 7f, style.LineWidth * 0.8f, style.Primary);

            Vector2 steeringCue = fpm + new Vector2(
                Mathf.Clamp(relativeBearingDegrees * 2.4f, -80f, 80f),
                64f);
            steeringCue.x = Mathf.Clamp(steeringCue.x, -180f, 180f);
            steeringCue.y = Mathf.Clamp(steeringCue.y, -155f, 165f);
            vectors.AddCircle(steeringCue, 7f, style.LineWidth, style.Primary, 16);
            float directionRadians = (90f - relativeBearingDegrees) * Mathf.Deg2Rad;
            Vector2 tailDirection = new Vector2(Mathf.Cos(directionRadians), Mathf.Sin(directionRadians));
            vectors.AddLine(steeringCue, steeringCue + tailDirection * 19f, style.LineWidth, style.Primary);

            labels.AcquireNumber(
                "STPT {0:00}",
                state.SelectedSteerpointIndex,
                new Vector2(52f, -294f),
                new Vector2(100f, 22f),
                NormalText,
                style.Primary);
            labels.AcquireNumber(
                "{0:0.0} NM",
                (float)F16HudLayoutMath.MetersToNauticalMiles(state.SteerpointDistanceM),
                new Vector2(149f, -294f),
                new Vector2(94f, 22f),
                NormalText,
                style.Primary);
        }

        private static void DrawBoresight(HudVectorCommandBuffer vectors, F16HudStyle style)
        {
            vectors.AddLine(new Vector2(-25f, 0f), new Vector2(-7f, 0f), style.LineWidth, style.Primary);
            vectors.AddLine(new Vector2(7f, 0f), new Vector2(25f, 0f), style.LineWidth, style.Primary);
            vectors.AddLine(new Vector2(0f, -18f), new Vector2(0f, -7f), style.LineWidth, style.Primary);
            vectors.AddLine(new Vector2(0f, 7f), new Vector2(0f, 18f), style.LineWidth, style.Primary);
        }

        private static void DrawFlightPathMarker(
            HudProjection projection,
            HudVectorCommandBuffer vectors,
            F16HudStyle style)
        {
            Vector2 center = projection.Position;
            vectors.AddCircle(center, 12f, style.LineWidth, style.Primary, 24);
            vectors.AddLine(center + new Vector2(-39f, 0f), center + new Vector2(-12f, 0f), style.LineWidth, style.Primary);
            vectors.AddLine(center + new Vector2(12f, 0f), center + new Vector2(39f, 0f), style.LineWidth, style.Primary);
            vectors.AddLine(center + new Vector2(0f, 12f), center + new Vector2(0f, 31f), style.LineWidth, style.Primary);
            if (projection.IsClipped)
                vectors.AddCross(center, 9f, style.LineWidth, style.Primary);
        }

        private static void DrawLandingCues(
            HudState state,
            Vector2 fpm,
            HudVectorCommandBuffer vectors,
            HudLabelPool labels,
            F16HudStyle style)
        {
            float angleOfAttackDegrees = (float)state.AngleOfAttackRad * Mathf.Rad2Deg;
            Vector2 bracketCenter = fpm + new Vector2(
                -58f,
                Mathf.Clamp((11f - angleOfAttackDegrees) * 6f, -45f, 45f));
            const float halfHeight = 28f;
            const float arm = 16f;

            vectors.AddLine(bracketCenter + new Vector2(0f, -halfHeight), bracketCenter + new Vector2(0f, halfHeight), style.LineWidth, style.Primary);
            vectors.AddLine(bracketCenter + new Vector2(0f, halfHeight), bracketCenter + new Vector2(arm, halfHeight), style.LineWidth, style.Primary);
            vectors.AddLine(bracketCenter, bracketCenter + new Vector2(arm, 0f), style.LineWidth, style.Primary);
            vectors.AddLine(bracketCenter + new Vector2(0f, -halfHeight), bracketCenter + new Vector2(arm, -halfHeight), style.LineWidth, style.Primary);

            labels.Acquire("LAND", new Vector2(-336f, -294f), new Vector2(70f, 22f), NormalText, style.Primary);
            labels.Acquire(
                state.LandingGearDown ? "GEAR" : "GEAR X",
                new Vector2(222f, -294f),
                new Vector2(72f, 22f),
                NormalText,
                state.LandingGearDown ? style.Primary : style.Warning);
        }

        private static void DrawStatusData(HudState state, HudLabelPool labels, F16HudStyle style)
        {
            labels.AcquireNumber(
                "G {0:0.0}",
                (float)state.NormalLoadFactorG,
                new Vector2(-330f, -228f),
                new Vector2(86f, 22f),
                NormalText,
                style.Primary,
                TextAlignmentOptions.Left);
            labels.AcquireNumber(
                "M {0:0.00}",
                (float)state.Mach,
                new Vector2(-330f, -252f),
                new Vector2(86f, 22f),
                NormalText,
                style.Primary,
                TextAlignmentOptions.Left);

            if (state.Mode == HudMode.Nav)
                labels.Acquire("NAV", new Vector2(-336f, -294f), new Vector2(70f, 22f), NormalText, style.Primary);

            if (state.Mode != HudMode.AirToAir)
            {
                labels.Acquire(
                    state.MasterArmEnabled ? "ARM" : "SAFE",
                    new Vector2(-272f, -294f),
                    new Vector2(66f, 22f),
                    NormalText,
                    state.MasterArmEnabled ? style.Primary : style.Caution);

                labels.Acquire(
                    string.IsNullOrEmpty(state.SelectedStoreType) ? "NO STORE" : state.SelectedStoreType,
                    new Vector2(-174f, -294f),
                    new Vector2(124f, 22f),
                    NormalText,
                    style.Primary);
                labels.AcquireNumber(
                    "QTY {0:0}",
                    Mathf.Max(0, state.SelectedStoreQuantity),
                    new Vector2(-91f, -294f),
                    new Vector2(56f, 22f),
                    SmallText,
                    style.Primary);
            }

            if (state.RadarAltitudeValid)
            {
                labels.AcquireNumber(
                    "R {0:00000}",
                    Mathf.Max(0, Mathf.RoundToInt((float)F16HudLayoutMath.MetersToFeet(state.RadarAltitudeM))),
                    new Vector2(298f, -228f),
                    new Vector2(118f, 22f),
                    NormalText,
                    style.Primary);
            }
            else
            {
                labels.Acquire(
                    "R -----",
                    new Vector2(298f, -228f),
                    new Vector2(118f, 22f),
                    NormalText,
                    style.Primary);
            }
        }

        private static void DrawWarnings(HudState state, HudLabelPool labels, F16HudStyle style)
        {
            if (state.MasterWarning && state.MasterCaution)
            {
                labels.Acquire("WARN", new Vector2(-59f, 228f), new Vector2(104f, 27f), WarningText, style.Warning);
                labels.Acquire("CAUTION", new Vector2(65f, 228f), new Vector2(122f, 27f), WarningText, style.Caution);
            }
            else if (state.MasterWarning)
            {
                labels.Acquire("WARN", new Vector2(0f, 228f), new Vector2(120f, 27f), WarningText, style.Warning);
            }
            else if (state.MasterCaution)
            {
                labels.Acquire("CAUTION", new Vector2(0f, 228f), new Vector2(150f, 27f), WarningText, style.Caution);
            }
        }

        private static void AddClippedLine(
            HudVectorCommandBuffer vectors,
            Vector2 start,
            Vector2 end,
            Rect clip,
            float width,
            Color color,
            bool dashed)
        {
            if (!TryClipLine(clip, ref start, ref end))
                return;

            if (dashed)
                vectors.AddDashedLine(start, end, width, color, 10f, 7f);
            else
                vectors.AddLine(start, end, width, color);
        }

        private static bool TryClipLine(Rect rect, ref Vector2 start, ref Vector2 end)
        {
            Vector2 delta = end - start;
            float t0 = 0f;
            float t1 = 1f;

            if (!ClipTest(-delta.x, start.x - rect.xMin, ref t0, ref t1) ||
                !ClipTest(delta.x, rect.xMax - start.x, ref t0, ref t1) ||
                !ClipTest(-delta.y, start.y - rect.yMin, ref t0, ref t1) ||
                !ClipTest(delta.y, rect.yMax - start.y, ref t0, ref t1))
            {
                return false;
            }

            Vector2 originalStart = start;
            start = originalStart + delta * t0;
            end = originalStart + delta * t1;
            return true;
        }

        private static bool ClipTest(float p, float q, ref float t0, ref float t1)
        {
            if (Mathf.Approximately(p, 0f))
                return q >= 0f;

            float ratio = q / p;
            if (p < 0f)
            {
                if (ratio > t1)
                    return false;
                if (ratio > t0)
                    t0 = ratio;
            }
            else
            {
                if (ratio < t0)
                    return false;
                if (ratio < t1)
                    t1 = ratio;
            }

            return true;
        }
    }
}
