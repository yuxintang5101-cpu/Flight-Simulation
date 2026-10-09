using NUnit.Framework;
using UnityEngine;

namespace FlightSim.Platform.Unity.Tests.EditMode
{
    public sealed class PilotInputConditionerTests
    {
        [Test]
        public void MouseDeadZoneProducesCenteredAxes()
        {
            var conditioner = new PilotInputConditioner(PilotInputConditionerSettings.Default);
            PilotInputAxes axes = conditioner.Update(0f, 0f, 0f, 0.01f, -0.01f, true, 1f / 60f);
            Assert.That(axes.Pitch, Is.EqualTo(0f).Within(1e-6f));
            Assert.That(axes.Roll, Is.EqualTo(0f).Within(1e-6f));
        }

        [Test]
        public void OneFrameMouseImpulseIsSlewLimitedAndReturnsToCenter()
        {
            var conditioner = new PilotInputConditioner(PilotInputConditionerSettings.Default);
            PilotInputAxes impulse = conditioner.Update(0f, 0f, 0f, 1f, -1f, true, 1f / 60f);
            Assert.That(Mathf.Abs(impulse.Pitch), Is.LessThanOrEqualTo(2.5f / 60f + 1e-5f));
            Assert.That(Mathf.Abs(impulse.Roll), Is.LessThanOrEqualTo(2.5f / 60f + 1e-5f));

            PilotInputAxes settled = impulse;
            for (int frame = 0; frame < 30; frame++)
                settled = conditioner.Update(0f, 0f, 0f, 0f, 0f, true, 1f / 60f);
            Assert.That(Mathf.Abs(settled.Pitch), Is.LessThan(1e-4f));
            Assert.That(Mathf.Abs(settled.Roll), Is.LessThan(1e-4f));
        }

        [Test]
        public void FreeCameraRejectsMouseButRetainsKeyboardFlightInput()
        {
            var conditioner = new PilotInputConditioner(PilotInputConditionerSettings.Default);
            PilotInputAxes axes = conditioner.Update(1f, -1f, 0.5f, 20f, 20f, false, 1f);
            Assert.That(axes.Pitch, Is.EqualTo(1f).Within(1e-6f));
            Assert.That(axes.Roll, Is.EqualTo(-1f).Within(1e-6f));
            Assert.That(axes.Yaw, Is.EqualTo(0.5f).Within(1e-6f));
        }
    }
}
