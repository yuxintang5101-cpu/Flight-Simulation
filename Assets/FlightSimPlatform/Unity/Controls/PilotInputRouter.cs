using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

namespace FlightSim.Platform.Unity.Controls
{
    [DisallowMultipleComponent]
    public sealed class PilotInputRouter : MonoBehaviour
    {
        [Serializable] private class HidIds { public int vendorId; public int productId; }
        public const int ThrustmasterVendor = 0x044f;
        public const int WarthogStick = 0x0402;
        public const int WarthogThrottle = 0x0404;
        public PilotControlProfile Profile { get; private set; }
        public string Message { get; private set; } = "";
        public event Action Changed;
        public string ProfilePath => PilotProfileStorage.DefaultPath;
        public bool IsListening => listenIndex >= 0;
        public bool IsCalibrating => calibrationIndex >= 0;
        public bool DisplayFocused { get; private set; }
        public bool DisplayPointerCaptured { get; set; }
        public bool DisplayInputCaptured => DisplayFocused || DisplayPointerCaptured;
        private int displayReleaseFrame=-1;
        public void SetDisplayFocus(bool focused) { if(DisplayFocused&&!focused)displayReleaseFrame=Time.frameCount;DisplayFocused = focused; }
        public bool HardwareEnabled => Profile != null && Profile.Mode != PilotInputMode.KeyboardMouse;
        public bool StickConnected => Resolve(Profile?.Axes[(int)FlightAxis.Pitch]) != null && Resolve(Profile?.Axes[(int)FlightAxis.Roll]) != null;
        public bool ThrottleConnected => Resolve(Profile?.Axes[(int)FlightAxis.LeftThrottle]) != null || Resolve(Profile?.Axes[(int)FlightAxis.RightThrottle]) != null;
        public bool UseMouseFlight => Profile != null && Profile.MouseFlightControl && (!HardwareEnabled || !StickConnected);
        public string ConnectionSummary => Profile.Mode == PilotInputMode.KeyboardMouse ? "键盘控制" : $"WARTHOG  ·  摇杆 {(StickConnected ? "已连接" : "未连接")}  /  油门 {(ThrottleConnected ? "已连接" : "未连接")}";
        private readonly Dictionary<string, InputControl> cache = new Dictionary<string, InputControl>();
        private readonly Dictionary<AxisControl, float> listenBaseline = new Dictionary<AxisControl, float>();
        private int listenIndex = -1;
        private bool listenButton;
        private bool listenDisplay;
        private float listenDeadline;
        private int calibrationIndex = -1;
        private float calibrationMin, calibrationMax;
        private bool hadStick, hadThrottle;

        private void Awake()
        {
            Profile = PilotProfileStorage.Load(ProfilePath, out string warning);
            Message = warning;
            ApplyWarthogDefaults(false);
        }
        private void OnEnable() { InputSystem.onDeviceChange += OnDeviceChange; }
        private void OnDisable() { InputSystem.onDeviceChange -= OnDeviceChange; CancelLearning(); DisplayFocused = false; DisplayPointerCaptured = false; }
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            cache.Clear();
            ApplyWarthogDefaults(false);
            CancelLearning();
            SetDisplayFocus(false); DisplayPointerCaptured = false;
            Changed?.Invoke();
        }
        private void Update()
        {
            if (Profile == null) return;
            bool stick = StickConnected, throttle = ThrottleConnected;
            if ((hadStick && !stick) || (hadThrottle && !throttle)) Message = "控制器已断开：操纵轴已释放，油门保留当前值，可用键盘接管。";
            hadStick = stick; hadThrottle = throttle;
            if (IsListening)
            {
                if (Time.unscaledTime >= listenDeadline) { CancelLearning(); Message = "绑定超时，原绑定保持不变。"; Changed?.Invoke(); }
                else PollLearning();
            }
            if (IsCalibrating && TryReadRaw((FlightAxis)calibrationIndex, out float raw))
            {
                calibrationMin = Mathf.Min(calibrationMin, raw);
                calibrationMax = Mathf.Max(calibrationMax, raw);
            }
        }
        public bool TryReadAxis(FlightAxis axis, out float value)
        {
            value = 0;
            if (!HardwareEnabled || !TryReadRaw(axis, out float raw)) return false;
            value = Profile.Axes[(int)axis].Normalize(raw, PilotControlProfile.IsUnipolar(axis));
            return true;
        }
        public bool TryReadRaw(FlightAxis axis, out float value)
        {
            value = 0;
            if (Profile == null || !(Resolve(Profile.Axes[(int)axis]) is AxisControl control)) return false;
            value = control.ReadValue();
            return AxisBinding.IsFinite(value);
        }
        public bool TryReadThrottle(out float value)
        {
            value = 0;
            bool left = TryReadAxis(FlightAxis.LeftThrottle, out float l);
            bool right = TryReadAxis(FlightAxis.RightThrottle, out float r);
            if (Profile.Throttle == ThrottleSelection.Left) { value = l; return left; }
            if (Profile.Throttle == ThrottleSelection.Right) { value = r; return right; }
            if (!left && !right) return false;
            value = left && right ? (l + r) * .5f : left ? l : r;
            return true;
        }
        public bool Pressed(PilotAction action) => HardwareEnabled && !DisplayFocused && Time.frameCount>displayReleaseFrame && !IsListening && !IsCalibrating && Resolve(Profile.Buttons[(int)action]) is ButtonControl c && c.wasPressedThisFrame;
        public bool Held(PilotAction action) => HardwareEnabled && !DisplayFocused && Time.frameCount>displayReleaseFrame && !IsListening && !IsCalibrating && Resolve(Profile.Buttons[(int)action]) is ButtonControl c && c.isPressed;
        public bool DisplayPressed(DisplayAction action) => HardwareEnabled && !IsListening && !IsCalibrating && Resolve(Profile.DisplayButtons[(int)action]) is ButtonControl c && c.wasPressedThisFrame;
        public bool DisplayHeld(DisplayAction action) => HardwareEnabled && !IsListening && !IsCalibrating && Resolve(Profile.DisplayButtons[(int)action]) is ButtonControl c && c.isPressed;
        public bool DisplayBindingConnected(DisplayAction action) => Profile != null && Resolve(Profile.DisplayButtons[(int)action]) != null;
        public void ClearDisplayBinding(int index) { Profile.DisplayButtons[index] = new HardwareBinding { ExplicitlyUnbound = true }; Save(); }
        public void BeginDisplayLearning(int index) { BeginLearning(true, index); listenDisplay = true; }
        public string BindingLabel(HardwareBinding binding)
        {
            if (binding == null || !binding.IsBound) return "未绑定";
            string name = binding.Product ?? "";
            string product = name.Contains("Throttle") ? "油门" : name.Contains("Joystick") ? "摇杆" : name;
            return product + " · " + binding.ControlPath + (Resolve(binding) == null ? "（离线）" : "");
        }
        public void Save()
        {
            try { PilotProfileStorage.Save(ProfilePath, Profile); Message = "控制配置已保存。"; }
            catch (Exception e) { Message = "保存失败：" + e.Message; }
            cache.Clear(); Changed?.Invoke();
        }
        public void SetMode(PilotInputMode mode) { Profile.Mode = mode; CancelLearning(); Save(); }
        public void ClearBinding(bool button, int index)
        {
            if (button) Profile.Buttons[index] = new HardwareBinding { ExplicitlyUnbound = true };
            else Profile.Axes[index] = new AxisBinding { ExplicitlyUnbound = true };
            Save();
        }
        public void BeginLearning(bool button, int index)
        {
            CancelLearning(); calibrationIndex = -1; listenIndex = index; listenButton = button;
            listenDisplay = false;
            listenDeadline = Time.unscaledTime + 12;
            foreach (InputDevice device in FlightDevices())
                foreach (var control in device.allControls)
                    if (control is AxisControl axis && !(axis is ButtonControl) && !axis.synthetic) listenBaseline[axis] = axis.ReadValue();
            Message = button ? "请按一个摇杆或油门按钮（12 秒内）…" : "请只移动要绑定的那一个轴（12 秒内）…";
            Changed?.Invoke();
        }
        public void CancelLearning() { listenIndex = -1; listenBaseline.Clear(); calibrationIndex = -1; }
        private void PollLearning()
        {
            InputControl picked = null;
            foreach (InputDevice device in FlightDevices())
            {
                foreach (InputControl control in device.allControls)
                {
                    if (listenButton && control is ButtonControl button && (!button.synthetic || control.parent is DpadControl) && button.wasPressedThisFrame) { picked = control; break; }
                    if (!listenButton && control is AxisControl axis && !(axis is ButtonControl) && !axis.synthetic && listenBaseline.TryGetValue(axis, out float baseline) && Mathf.Abs(axis.ReadValue() - baseline) > .3f) { picked = axis; break; }
                }
                if (picked != null) break;
            }
            if (picked == null) return;
            HardwareBinding binding = listenButton ? new HardwareBinding() : new AxisBinding();
            CopyIdentity(binding, picked);
            if (listenDisplay) Profile.DisplayButtons[listenIndex] = binding;
            else if (listenButton) Profile.Buttons[listenIndex] = binding;
            else Profile.Axes[listenIndex] = (AxisBinding)binding;
            CancelLearning(); Save();
            Message = "已绑定 " + BindingLabel(binding) + "。请检查方向并校准行程。";
        }
        public void BeginCalibration(int index)
        {
            CancelLearning();
            if (!TryReadRaw((FlightAxis)index, out float raw)) { Message = "请先连接设备并绑定该轴。"; return; }
            calibrationIndex = index; calibrationMin = raw; calibrationMax = raw;
            Message = "缓慢移动至两个端点；摇杆最后回中，再点“完成校准”。";
        }
        public void FinishCalibration()
        {
            if (calibrationIndex < 0) return;
            var binding = Profile.Axes[calibrationIndex];
            bool valid = TryReadRaw((FlightAxis)calibrationIndex, out float center) && calibrationMax - calibrationMin > .2f;
            if (valid) { binding.Minimum = calibrationMin; binding.Maximum = calibrationMax; binding.Center = center; Save(); }
            else Message = "行程不足，校准未保存。请移动轴至两个端点后重试。";
            calibrationIndex = -1; Changed?.Invoke();
        }
        public void ApplyWarthogDefaults(bool replace)
        {
            if (Profile == null) return;
            if (replace) { Profile = new PilotControlProfile(); cache.Clear(); }
            InputDevice stick = FindWarthog(WarthogStick), throttle = FindWarthog(WarthogThrottle);
            BindAxisDefault(stick, FlightAxis.Roll, false, "stick/x", "x");
            BindAxisDefault(stick, FlightAxis.Pitch, true, "stick/y", "y");
            BindAxisDefault(throttle, FlightAxis.LeftThrottle, true, "z");
            BindAxisDefault(throttle, FlightAxis.RightThrottle, true, "rz");
            BindButtonDefault(stick, PilotAction.WeaponRelease, "trigger", "button1");
            BindButtonDefault(stick, PilotAction.Camera, "button3");
            BindButtonDefault(stick, PilotAction.Target, "button4");
            BindButtonDefault(stick, PilotAction.Weapon, "button2");
            BindButtonDefault(stick, PilotAction.YawLeft, "hat/left");
            BindButtonDefault(stick, PilotAction.YawRight, "hat/right");
            BindDisplayDefault(stick, DisplayAction.ToggleFocus, "button5");
            BindDisplayDefault(stick, DisplayAction.Up, "hat/up");
            BindDisplayDefault(stick, DisplayAction.Down, "hat/down");
            BindDisplayDefault(stick, DisplayAction.Left, "hat/left");
            BindDisplayDefault(stick, DisplayAction.Right, "hat/right");
            BindDisplayDefault(stick, DisplayAction.Confirm, "button2");
            BindDisplayDefault(stick, DisplayAction.Back, "button3");
            BindDisplayDefault(stick, DisplayAction.NextPage, "button4");
            BindDisplayDefault(stick, DisplayAction.PreviousPage, "button19");
            BindDisplayDefault(stick, DisplayAction.ZoomIn, "button7");
            BindDisplayDefault(stick, DisplayAction.ZoomOut, "button9");
            // No twist axis or rudder pedals. The POV hat and Q/E provide button-based yaw.
            if (replace) Save();
        }
        private void BindAxisDefault(InputDevice device, FlightAxis axis, bool invert, params string[] paths)
        {
            if (device == null || Profile.Axes[(int)axis].IsBound || Profile.Axes[(int)axis].ExplicitlyUnbound) return;
            InputControl c = paths.Select(p => device.TryGetChildControl<AxisControl>(p)).FirstOrDefault(p => p != null);
            if (c == null) return;
            var binding = new AxisBinding { Invert = invert, DeadZone = PilotControlProfile.IsUnipolar(axis) ? 0 : .04f, Exponent = 1.35f };
            CopyIdentity(binding, c); Profile.Axes[(int)axis] = binding;
        }
        private void BindButtonDefault(InputDevice device, PilotAction action, params string[] paths)
        {
            if (device == null || Profile.Buttons[(int)action].IsBound || Profile.Buttons[(int)action].ExplicitlyUnbound) return;
            InputControl c = paths.Select(p => device.TryGetChildControl<ButtonControl>(p)).FirstOrDefault(p => p != null);
            if (c == null) return;
            var binding = new HardwareBinding(); CopyIdentity(binding, c); Profile.Buttons[(int)action] = binding;
        }
        private void BindDisplayDefault(InputDevice device, DisplayAction action, string path)
        {
            var binding = Profile.DisplayButtons[(int)action];
            if(device == null || binding.IsBound || binding.ExplicitlyUnbound) return;
            var control = device.TryGetChildControl<ButtonControl>(path); if(control == null) return;
            binding = new HardwareBinding(); CopyIdentity(binding, control); Profile.DisplayButtons[(int)action] = binding;
        }
        private InputControl Resolve(HardwareBinding binding)
        {
            if (binding == null || !binding.IsBound) return null;
            string key = binding.VendorId + ":" + binding.ProductId + ":" + binding.Product + ":" + binding.Layout + ":" + binding.ControlPath;
            if (cache.TryGetValue(key, out InputControl cached))
            {
                if (cached == null || (cached.device.added && cached.device.enabled)) return cached;
                cache.Remove(key);
            }
            foreach (InputDevice device in FlightDevices())
            {
                HidIds id = GetIds(device);
                bool matches = binding.VendorId != 0 ? id.vendorId == binding.VendorId && id.productId == binding.ProductId
                    : (!string.IsNullOrEmpty(binding.Product) ? device.description.product == binding.Product : device.layout == binding.Layout);
                if (!matches) continue;
                InputControl control = device.TryGetChildControl(binding.ControlPath);
                if (control != null) { cache[key] = control; return control; }
            }
            cache[key] = null;
            return null;
        }
        private static void CopyIdentity(HardwareBinding binding, InputControl control)
        {
            HidIds ids = GetIds(control.device);
            binding.VendorId = ids.vendorId; binding.ProductId = ids.productId;
            binding.Product = control.device.description.product ?? ""; binding.Layout = control.device.layout;
            binding.ControlPath = control.path.Substring(control.device.path.Length).TrimStart('/');
        }
        private static HidIds GetIds(InputDevice device)
        {
            try { return JsonUtility.FromJson<HidIds>(device.description.capabilities) ?? new HidIds(); }
            catch { return new HidIds(); }
        }
        private static InputDevice FindWarthog(int productId) => FlightDevices().FirstOrDefault(d => { var ids = GetIds(d); return ids.vendorId == ThrustmasterVendor && ids.productId == productId; });
        private static IEnumerable<InputDevice> FlightDevices() => InputSystem.devices.Where(d => d.added && d.enabled && !(d is Keyboard) && !(d is Mouse) && !(d is Touchscreen) && (d is Joystick || d is Gamepad || d.description.interfaceName == "HID"));
    }
}
