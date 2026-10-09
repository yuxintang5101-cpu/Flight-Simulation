using System;
using System.Reflection;
using FlightSim.Platform.Contracts;
using NUnit.Framework;
using UnityEngine;

namespace FlightSim.Platform.Presentation.Tests
{
    public sealed class F16HudLayoutMathTests
    {
        private const string LayoutMathTypeName =
            "FlightSim.Platform.Presentation.F16HudLayoutMath, FlightSim.Presentation";

        [Test]
        public void PresentationAssemblyDefinesLayoutMathContract()
        {
            Type type = GetLayoutMathType();

            Assert.That(type, Is.Not.Null);
            Assert.That(type.GetMethod("IsDisplayable", BindingFlags.Public | BindingFlags.Static), Is.Not.Null);
            Assert.That(type.GetMethod("CalculateSafeRect", BindingFlags.Public | BindingFlags.Static), Is.Not.Null);
            Assert.That(type.GetMethod("ProjectAngle", BindingFlags.Public | BindingFlags.Static), Is.Not.Null);
        }

        [Test]
        public void DisplayableStateRequiresValidNonOffFiniteData()
        {
            HudState state = CreateValidState();

            Assert.That(Invoke<bool>("IsDisplayable", state), Is.True);

            state.IsValid = false;
            Assert.That(Invoke<bool>("IsDisplayable", state), Is.False);

            state = CreateValidState();
            state.Mode = HudMode.Off;
            Assert.That(Invoke<bool>("IsDisplayable", state), Is.False);

            state = CreateValidState();
            state.PitchRad = double.NaN;
            Assert.That(Invoke<bool>("IsDisplayable", state), Is.False);
        }

        [TestCase(1920f, 1080f)]
        [TestCase(1280f, 720f)]
        [TestCase(3840f, 2160f)]
        [TestCase(2560f, 1080f)]
        [TestCase(1440f, 1080f)]
        public void SafeRectPreservesCombinerAspectAcrossSupportedLayouts(float width, float height)
        {
            Rect rect = Invoke<Rect>("CalculateSafeRect", width, height);

            Assert.That(rect.width / rect.height, Is.EqualTo(760f / 640f).Within(0.0001f));
            Assert.That(rect.width, Is.LessThanOrEqualTo(width - 48f));
            Assert.That(rect.height, Is.LessThanOrEqualTo(height - 48f));
            Assert.That(rect.center, Is.EqualTo(Vector2.zero));
        }

        [Test]
        public void CombinerCalibrationMatchesPrototypeCockpitGlass()
        {
            Type type = GetLayoutMathType();
            FieldInfo verticalOffset = type.GetField(
                "CombinerVerticalOffset",
                BindingFlags.Public | BindingFlags.Static);
            FieldInfo renderedWidth = type.GetField(
                "CombinerRenderedWidth",
                BindingFlags.Public | BindingFlags.Static);
            FieldInfo renderedHeight = type.GetField(
                "CombinerRenderedHeight",
                BindingFlags.Public | BindingFlags.Static);

            Assert.That(verticalOffset, Is.Not.Null);
            Assert.That(renderedWidth, Is.Not.Null);
            Assert.That(renderedHeight, Is.Not.Null);
            Assert.That((float)verticalOffset.GetRawConstantValue(), Is.EqualTo(205f));
            Assert.That((float)renderedWidth.GetRawConstantValue(), Is.EqualTo(440f));
            Assert.That((float)renderedHeight.GetRawConstantValue(), Is.EqualTo(410f));
        }

        [Test]
        public void AngularProjectionReportsAndClampsOffCombinerCues()
        {
            Rect rect = new Rect(-380f, -320f, 760f, 640f);
            object centered = Invoke("ProjectAngle", 0.0, 0.0, rect, 28f);
            object clipped = Invoke("ProjectAngle", 1.0, -1.0, rect, 28f);

            Assert.That(ReadProjectionPosition(centered), Is.EqualTo(Vector2.zero));
            Assert.That(ReadProjectionClipped(centered), Is.False);
            Assert.That(ReadProjectionClipped(clipped), Is.True);

            Vector2 clippedPosition = ReadProjectionPosition(clipped);
            Assert.That(clippedPosition.x, Is.InRange(rect.xMin + 28f, rect.xMax - 28f));
            Assert.That(clippedPosition.y, Is.InRange(rect.yMin + 28f, rect.yMax - 28f));
        }

        [TestCase(-1f, 359f)]
        [TestCase(360f, 0f)]
        [TestCase(721f, 1f)]
        public void HeadingWrapsToUnsignedDegrees(float input, float expected)
        {
            Assert.That(Invoke<float>("WrapDegrees", input), Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void SiValuesConvertToF16DisplayUnits()
        {
            Assert.That(Invoke<double>("MetersPerSecondToKnots", 100.0), Is.EqualTo(194.384449).Within(0.0001));
            Assert.That(Invoke<double>("MetersToFeet", 1000.0), Is.EqualTo(3280.8399).Within(0.001));
            Assert.That(Invoke<double>("MetersPerSecondToFeetPerMinute", 10.0), Is.EqualTo(1968.50394).Within(0.001));
            Assert.That(Invoke<double>("MetersToNauticalMiles", 1852.0), Is.EqualTo(1.0).Within(0.000001));
        }

        private static HudState CreateValidState()
        {
            return new HudState
            {
                IsValid = true,
                Mode = HudMode.Nav,
                CalibratedAirspeedMps = 150.0,
                TrueAirspeedMps = 160.0,
                BarometricAltitudeM = 2500.0,
                RadarAltitudeM = 500.0,
                RadarAltitudeValid = true,
                HeadingRad = 1.0,
                PitchRad = 0.1,
                RollRad = -0.2,
                FlightPathAzimuthRad = 0.02,
                FlightPathElevationRad = -0.01,
                AngleOfAttackRad = 0.15,
                NormalLoadFactorG = 1.0,
                Mach = 0.72,
                VerticalSpeedMps = 2.0,
                SelectedSteerpointIndex = 4,
                SteerpointBearingRad = 1.2,
                SteerpointElevationRad = -0.03,
                SteerpointDistanceM = 18520.0,
                SelectedStoreType = "AIM-120C",
                SelectedStoreQuantity = 2
            };
        }

        private static Type GetLayoutMathType()
        {
            return Type.GetType(LayoutMathTypeName, false);
        }

        private static object Invoke(string methodName, params object[] arguments)
        {
            Type type = GetLayoutMathType();
            Assert.That(type, Is.Not.Null, "Presentation layout math type is missing.");
            MethodInfo method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Assert.That(method, Is.Not.Null, methodName + " is missing.");
            return method.Invoke(null, arguments);
        }

        private static T Invoke<T>(string methodName, params object[] arguments)
        {
            return (T)Invoke(methodName, arguments);
        }

        private static Vector2 ReadProjectionPosition(object projection)
        {
            return (Vector2)projection.GetType().GetField("Position").GetValue(projection);
        }

        private static bool ReadProjectionClipped(object projection)
        {
            return (bool)projection.GetType().GetField("IsClipped").GetValue(projection);
        }
    }
}
