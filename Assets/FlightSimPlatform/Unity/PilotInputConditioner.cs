using System;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    [Serializable]
    public struct PilotInputConditionerSettings
    {
        public float MousePitchSensitivity;
        public float MouseRollSensitivity;
        public float MouseDeadZone;
        public float MouseExponent;
        public float CommandRiseRatePerS;
        public float CommandCenterRatePerS;

        public static PilotInputConditionerSettings Default => new PilotInputConditionerSettings
        {
            MousePitchSensitivity = 0.10f,
            MouseRollSensitivity = 0.12f,
            MouseDeadZone = 0.02f,
            MouseExponent = 1.6f,
            CommandRiseRatePerS = 2.5f,
            CommandCenterRatePerS = 4.0f
        };
    }

    public readonly struct PilotInputAxes
    {
        public readonly float Pitch;
        public readonly float Roll;
        public readonly float Yaw;

        public PilotInputAxes(float pitch, float roll, float yaw)
        {
            Pitch = pitch;
            Roll = roll;
            Yaw = yaw;
        }
    }

    public sealed class PilotInputConditioner
    {
        private readonly PilotInputConditionerSettings settings;
        private float pitch;
        private float roll;
        private float yaw;

        public PilotInputConditioner(PilotInputConditionerSettings settings)
        {
            this.settings = settings;
        }

        public PilotInputAxes Update(
            float keyboardPitch,
            float keyboardRoll,
            float keyboardYaw,
            float mouseDeltaX,
            float mouseDeltaY,
            bool acceptMouse,
            float deltaTimeS)
        {
            float mousePitch = acceptMouse
                ? ShapeMouse(mouseDeltaY * settings.MousePitchSensitivity)
                : 0f;
            float mouseRoll = acceptMouse
                ? ShapeMouse(mouseDeltaX * settings.MouseRollSensitivity)
                : 0f;

            float targetPitch = Mathf.Clamp(keyboardPitch - mousePitch, -1f, 1f);
            float targetRoll = Mathf.Clamp(keyboardRoll + mouseRoll, -1f, 1f);
            float targetYaw = Mathf.Clamp(keyboardYaw, -1f, 1f);
            float deltaTime = Mathf.Max(0f, deltaTimeS);

            pitch = MoveTowardsTarget(pitch, targetPitch, deltaTime);
            roll = MoveTowardsTarget(roll, targetRoll, deltaTime);
            yaw = MoveTowardsTarget(yaw, targetYaw, deltaTime);
            return new PilotInputAxes(pitch, roll, yaw);
        }

        public void Reset()
        {
            pitch = 0f;
            roll = 0f;
            yaw = 0f;
        }

        private float ShapeMouse(float value)
        {
            float magnitude = Mathf.Abs(value);
            float shapedMagnitude = Mathf.Pow(
                Mathf.InverseLerp(settings.MouseDeadZone, 1f, magnitude),
                settings.MouseExponent);
            return Mathf.Sign(value) * shapedMagnitude;
        }

        private float MoveTowardsTarget(float current, float target, float deltaTimeS)
        {
            float rate = Mathf.Abs(target) <= settings.MouseDeadZone
                ? settings.CommandCenterRatePerS
                : settings.CommandRiseRatePerS;
            return Mathf.MoveTowards(current, target, rate * deltaTimeS);
        }
    }
}
