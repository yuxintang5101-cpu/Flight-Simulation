using System;
using System.IO;
using UnityEngine;

namespace FlightSim.Platform.Unity.Controls
{
    public enum PilotInputMode { Automatic, KeyboardMouse, Hotas }
    public enum ThrottleSelection { Average, Left, Right }
    public enum FlightAxis { Pitch, Roll, Yaw, LeftThrottle, RightThrottle, WheelBrake }
    public enum PilotAction { Camera, LandingGear, SpeedBrake, WheelBrake, EngineStart, Target, Weapon, MasterArm, WeaponRelease, Automation, YawLeft, YawRight }
    public enum DisplayAction { ToggleFocus, Up, Down, Left, Right, Confirm, Back, NextPage, PreviousPage, ZoomIn, ZoomOut }

    [Serializable]
    public class HardwareBinding
    {
        public int VendorId;
        public int ProductId;
        public string Product = "";
        public string Layout = "";
        public string ControlPath = "";
        public bool ExplicitlyUnbound;
        public bool IsBound => !string.IsNullOrWhiteSpace(ControlPath);
    }

    [Serializable]
    public sealed class AxisBinding : HardwareBinding
    {
        public float Minimum = -1f;
        public float Maximum = 1f;
        public float Center;
        public float DeadZone = .04f;
        public float Exponent = 1.35f;
        public bool Invert;

        public float Normalize(float raw, bool unipolar)
        {
            if (!IsFinite(raw) || !IsFinite(Minimum) || !IsFinite(Maximum) || Maximum - Minimum < .0001f) return 0;
            float value;
            if (unipolar)
            {
                value = Mathf.Clamp01((raw - Minimum) / (Maximum - Minimum));
                if (Invert) value = 1f - value;
                return value;
            }
            float center = Mathf.Clamp(IsFinite(Center) ? Center : 0, Minimum + .00001f, Maximum - .00001f);
            value = raw >= center ? (raw - center) / (Maximum - center) : (raw - center) / (center - Minimum);
            value = Mathf.Clamp(value, -1, 1);
            float deadZone = Mathf.Clamp(IsFinite(DeadZone) ? DeadZone : .04f, 0, .45f);
            float magnitude = Mathf.Max(0, (Mathf.Abs(value) - deadZone) / (1 - deadZone));
            value = Mathf.Sign(value) * Mathf.Pow(magnitude, Mathf.Clamp(IsFinite(Exponent) ? Exponent : 1, .5f, 3));
            return Invert ? -value : value;
        }
        public static bool IsFinite(float v) => !float.IsNaN(v) && !float.IsInfinity(v);
    }

    [Serializable]
    public sealed class PilotControlProfile
    {
        public int SchemaVersion = 1;
        public string Name = "HOTAS WARTHOG";
        public PilotInputMode Mode = PilotInputMode.Automatic;
        public ThrottleSelection Throttle = ThrottleSelection.Average;
        public bool MouseFlightControl;
        public AxisBinding[] Axes = CreateAxes();
        public HardwareBinding[] Buttons = CreateButtons();
        // Additive schema extension: existing axis calibration and pilot bindings remain intact.
        public HardwareBinding[] DisplayButtons = CreateDisplayButtons();
        public static bool IsUnipolar(FlightAxis axis) => axis == FlightAxis.LeftThrottle || axis == FlightAxis.RightThrottle || axis == FlightAxis.WheelBrake;
        public void EnsureShape()
        {
            if (Axes == null || Axes.Length != 6) Axes = CreateAxes();
            if (Buttons == null || Buttons.Length != 12) Buttons = CreateButtons();
            if (DisplayButtons == null || DisplayButtons.Length != 11) {
                var previous = DisplayButtons; DisplayButtons = CreateDisplayButtons();
                if(previous != null) Array.Copy(previous, DisplayButtons, Math.Min(previous.Length, DisplayButtons.Length));
            }
            for (int i = 0; i < Axes.Length; i++) if (Axes[i] == null) Axes[i] = new AxisBinding();
            for (int i = 0; i < Buttons.Length; i++) if (Buttons[i] == null) Buttons[i] = new HardwareBinding();
            for (int i = 0; i < DisplayButtons.Length; i++) if (DisplayButtons[i] == null) DisplayButtons[i] = new HardwareBinding();
            if (!Enum.IsDefined(typeof(PilotInputMode), Mode)) Mode = PilotInputMode.Automatic;
            if (!Enum.IsDefined(typeof(ThrottleSelection), Throttle)) Throttle = ThrottleSelection.Average;
        }
        private static AxisBinding[] CreateAxes() { var a = new AxisBinding[6]; for (int i = 0; i < a.Length; i++) a[i] = new AxisBinding(); return a; }
        private static HardwareBinding[] CreateButtons() { var a = new HardwareBinding[12]; for (int i = 0; i < a.Length; i++) a[i] = new HardwareBinding(); return a; }
        private static HardwareBinding[] CreateDisplayButtons() { var a = new HardwareBinding[11]; for (int i = 0; i < a.Length; i++) a[i] = new HardwareBinding(); return a; }
    }

    public static class PilotProfileStorage
    {
        public static string DefaultPath => Path.Combine(Application.persistentDataPath, "FlightSim", "controls-v1.json");
        public static PilotControlProfile Load(string path, out string warning)
        {
            warning = "";
            if (!File.Exists(path)) return new PilotControlProfile();
            try
            {
                var value = JsonUtility.FromJson<PilotControlProfile>(File.ReadAllText(path));
                if (value == null || value.SchemaVersion != 1) throw new InvalidDataException("Unsupported control profile version");
                value.EnsureShape();
                return value;
            }
            catch (Exception e) { warning = "控制配置无法读取，已使用默认值：" + e.Message; return new PilotControlProfile(); }
        }
        public static void Save(string path, PilotControlProfile profile)
        {
            profile.EnsureShape();
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string temporary = path + ".tmp";
            File.WriteAllText(temporary, JsonUtility.ToJson(profile, true));
            if (File.Exists(path)) File.Replace(temporary, path, path + ".bak");
            else File.Move(temporary, path);
        }
    }
}
