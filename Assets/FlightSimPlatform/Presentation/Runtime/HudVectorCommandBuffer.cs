using UnityEngine;

namespace FlightSim.Platform.Presentation
{
    internal readonly struct HudLineCommand
    {
        public readonly Vector2 Start;
        public readonly Vector2 End;
        public readonly float Width;
        public readonly Color32 Color;

        public HudLineCommand(Vector2 start, Vector2 end, float width, Color32 color)
        {
            Start = start;
            End = end;
            Width = width;
            Color = color;
        }
    }

    public sealed class HudVectorCommandBuffer
    {
        private readonly HudLineCommand[] _commands;

        public int Count { get; private set; }
        public int Capacity => _commands.Length;
        public bool Overflowed { get; private set; }

        public HudVectorCommandBuffer(int capacity)
        {
            _commands = new HudLineCommand[Mathf.Max(1, capacity)];
        }

        public void Clear()
        {
            Count = 0;
            Overflowed = false;
        }

        public bool AddLine(Vector2 start, Vector2 end, float width, Color color)
        {
            if (Count >= _commands.Length)
            {
                Overflowed = true;
                return false;
            }

            if ((end - start).sqrMagnitude < 0.0001f)
                return true;

            _commands[Count++] = new HudLineCommand(start, end, Mathf.Max(0.25f, width), color);
            return true;
        }

        public bool AddDashedLine(
            Vector2 start,
            Vector2 end,
            float width,
            Color color,
            float dashLength = 10f,
            float gapLength = 7f)
        {
            Vector2 delta = end - start;
            float length = delta.magnitude;
            if (length < 0.01f)
                return true;

            Vector2 direction = delta / length;
            float stride = Mathf.Max(0.5f, dashLength) + Mathf.Max(0f, gapLength);
            bool added = true;
            for (float distance = 0f; distance < length; distance += stride)
            {
                Vector2 dashStart = start + direction * distance;
                Vector2 dashEnd = start + direction * Mathf.Min(distance + dashLength, length);
                added &= AddLine(dashStart, dashEnd, width, color);
            }

            return added;
        }

        public bool AddCircle(
            Vector2 center,
            float radius,
            float width,
            Color color,
            int segments = 24)
        {
            int count = Mathf.Max(8, segments);
            Vector2 previous = center + Vector2.right * radius;
            bool added = true;
            for (int i = 1; i <= count; i++)
            {
                float angle = i / (float)count * Mathf.PI * 2f;
                Vector2 next = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                added &= AddLine(previous, next, width, color);
                previous = next;
            }

            return added;
        }

        public bool AddArc(
            Vector2 center,
            float radius,
            float startDegrees,
            float endDegrees,
            float width,
            Color color,
            int segments)
        {
            int count = Mathf.Max(1, segments);
            float startRadians = startDegrees * Mathf.Deg2Rad;
            Vector2 previous = center + new Vector2(Mathf.Cos(startRadians), Mathf.Sin(startRadians)) * radius;
            bool added = true;
            for (int i = 1; i <= count; i++)
            {
                float angle = Mathf.Lerp(startDegrees, endDegrees, i / (float)count) * Mathf.Deg2Rad;
                Vector2 next = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                added &= AddLine(previous, next, width, color);
                previous = next;
            }

            return added;
        }

        public bool AddBox(Rect rect, float width, Color color)
        {
            bool added = AddLine(new Vector2(rect.xMin, rect.yMin), new Vector2(rect.xMax, rect.yMin), width, color);
            added &= AddLine(new Vector2(rect.xMax, rect.yMin), new Vector2(rect.xMax, rect.yMax), width, color);
            added &= AddLine(new Vector2(rect.xMax, rect.yMax), new Vector2(rect.xMin, rect.yMax), width, color);
            added &= AddLine(new Vector2(rect.xMin, rect.yMax), new Vector2(rect.xMin, rect.yMin), width, color);
            return added;
        }

        public bool AddDiamond(Vector2 center, float radius, float width, Color color)
        {
            Vector2 top = center + Vector2.up * radius;
            Vector2 right = center + Vector2.right * radius;
            Vector2 bottom = center + Vector2.down * radius;
            Vector2 left = center + Vector2.left * radius;
            bool added = AddLine(top, right, width, color);
            added &= AddLine(right, bottom, width, color);
            added &= AddLine(bottom, left, width, color);
            added &= AddLine(left, top, width, color);
            return added;
        }

        public bool AddCross(Vector2 center, float radius, float width, Color color)
        {
            bool added = AddLine(
                center + new Vector2(-radius, -radius),
                center + new Vector2(radius, radius),
                width,
                color);
            added &= AddLine(
                center + new Vector2(-radius, radius),
                center + new Vector2(radius, -radius),
                width,
                color);
            return added;
        }

        internal HudLineCommand GetLine(int index)
        {
            return _commands[index];
        }
    }
}
