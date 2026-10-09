using System;
using System.Collections.Generic;
using System.Reflection;
using FlightSim.Platform.Contracts;
using NUnit.Framework;
using UnityEngine;

namespace FlightSim.Platform.Presentation.Tests
{
    public sealed class HudRuntimeContractTests
    {
        private const string BufferTypeName =
            "FlightSim.Platform.Presentation.HudVectorCommandBuffer, FlightSim.Presentation";
        private const string ControllerTypeName =
            "FlightSim.Platform.Presentation.F16HudController, FlightSim.Presentation";

        [Test]
        public void VectorCommandBufferIsBoundedAndReusable()
        {
            Type type = RequireType(BufferTypeName);
            object buffer = Activator.CreateInstance(type, 2);
            MethodInfo addLine = type.GetMethod("AddLine");
            MethodInfo clear = type.GetMethod("Clear");

            Assert.That(addLine, Is.Not.Null);
            Assert.That(clear, Is.Not.Null);
            Assert.That(Invoke<bool>(addLine, buffer, Vector2.zero, Vector2.right, 2f, Color.green), Is.True);
            Assert.That(Invoke<bool>(addLine, buffer, Vector2.up, Vector2.one, 2f, Color.green), Is.True);
            Assert.That(Invoke<bool>(addLine, buffer, Vector2.left, Vector2.down, 2f, Color.green), Is.False);
            Assert.That(ReadProperty<int>(buffer, "Count"), Is.EqualTo(2));
            Assert.That(ReadProperty<bool>(buffer, "Overflowed"), Is.True);

            clear.Invoke(buffer, null);
            Assert.That(ReadProperty<int>(buffer, "Count"), Is.Zero);
            Assert.That(ReadProperty<bool>(buffer, "Overflowed"), Is.False);
        }

        [Test]
        public void ControllerBuildsExpectedCanvasAndFixedLabelPool()
        {
            GameObject host = new GameObject("HUD Test Host");
            try
            {
                Component controller = AddController(host);
                InvokeController(controller, "SetCockpitContext", false, null);
                Transform generated = FindDescendant(host.transform, "F-16C HUD Runtime");

                Assert.That(generated, Is.Not.Null);
                Component scaler = FindComponentByFullName(generated.gameObject, "UnityEngine.UI.CanvasScaler");
                Assert.That(scaler, Is.Not.Null);
                Assert.That(ReadProperty<object>(scaler, "uiScaleMode").ToString(), Is.EqualTo("ScaleWithScreenSize"));
                Assert.That(ReadProperty<Vector2>(scaler, "referenceResolution"), Is.EqualTo(new Vector2(1920f, 1080f)));
                Assert.That(ReadProperty<float>(scaler, "matchWidthOrHeight"), Is.EqualTo(1f));
                Assert.That(CountComponentsByFullName(generated, "TMPro.TextMeshProUGUI"), Is.EqualTo(96));
                Assert.That(controller, Is.Not.Null);
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(host);
            }
        }

        [Test]
        public void PresentationProvidesTmpSettingsWithoutProjectWideEssentials()
        {
            Assert.That(Resources.Load("TMP Settings"), Is.Not.Null);
            Assert.That(Resources.Load("Fonts/F16 HUD Font"), Is.Not.Null);
        }

        [Test]
        public void VisibilityRequiresCockpitCameraAndDisplayableState()
        {
            GameObject host = new GameObject("HUD Test Host");
            GameObject cameraObject = new GameObject("Cockpit Camera");
            try
            {
                Component controller = AddController(host);
                Camera camera = cameraObject.AddComponent<Camera>();
                HudState state = CreateState(HudMode.Nav);

                InvokeController(controller, "SetHudState", state);
                InvokeController(controller, "SetCockpitContext", true, null);
                Assert.That(ReadProperty<bool>(controller, "IsHudVisible"), Is.False);

                InvokeController(controller, "SetCockpitContext", true, camera);
                Assert.That(ReadProperty<bool>(controller, "IsHudVisible"), Is.True);

                state.IsValid = false;
                InvokeController(controller, "SetHudState", state);
                Assert.That(ReadProperty<bool>(controller, "IsHudVisible"), Is.False);

                state = CreateState(HudMode.Off);
                InvokeController(controller, "SetHudState", state);
                Assert.That(ReadProperty<bool>(controller, "IsHudVisible"), Is.False);

                InvokeController(controller, "SetHudState", CreateState(HudMode.Nav));
                InvokeController(controller, "SetCockpitContext", false, camera);
                Assert.That(ReadProperty<bool>(controller, "IsHudVisible"), Is.False);
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(host);
                UnityEngine.Object.DestroyImmediate(cameraObject);
            }
        }

        [Test]
        public void NavCompositionCoversFlightNavigationWarningAndStoreDataWithoutGrowingPool()
        {
            GameObject host = new GameObject("HUD Test Host");
            GameObject cameraObject = new GameObject("Cockpit Camera");
            try
            {
                Component controller = AddController(host);
                Camera camera = cameraObject.AddComponent<Camera>();
                HudState state = CreateState(HudMode.Nav);
                state.MasterArmEnabled = true;
                state.MasterWarning = true;
                state.MasterCaution = true;

                InvokeController(controller, "SetCockpitContext", true, camera);
                InvokeController(controller, "SetHudState", state);

                Transform generated = FindDescendant(host.transform, "F-16C HUD Runtime");
                int poolSize = CountComponentsByFullName(generated, "TMPro.TextMeshProUGUI");
                HashSet<string> labels = ReadActiveTmpLabels(generated);

                Assert.That(labels, Does.Contain("NAV"));
                Assert.That(labels, Does.Contain("ARM"));
                Assert.That(labels, Does.Contain("AIM-120C"));
                Assert.That(labels, Does.Contain("QTY 2"));
                Assert.That(labels, Does.Contain("STPT 04"));
                Assert.That(labels, Does.Contain("WARN"));
                Assert.That(labels, Does.Contain("CAUTION"));
                Assert.That(labels, Does.Contain("G 1.2"));
                Assert.That(labels, Does.Contain("M 0.78"));
                Assert.That(labels, Does.Contain("R 01640"));

                InvokeController(controller, "SetHudState", state);
                Assert.That(CountComponentsByFullName(generated, "TMPro.TextMeshProUGUI"), Is.EqualTo(poolSize));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(host);
                UnityEngine.Object.DestroyImmediate(cameraObject);
            }
        }

        [Test]
        public void LandingModeAddsAoABracketAndGearStatus()
        {
            GameObject host = new GameObject("HUD Test Host");
            GameObject cameraObject = new GameObject("Cockpit Camera");
            try
            {
                Component controller = AddController(host);
                Camera camera = cameraObject.AddComponent<Camera>();
                InvokeController(controller, "SetCockpitContext", true, camera);

                HudState state = CreateState(HudMode.Nav);
                InvokeController(controller, "SetHudState", state);
                int navCommands = ReadVectorCommandCount(host.transform);

                state.Mode = HudMode.Landing;
                state.LandingGearDown = true;
                InvokeController(controller, "SetHudState", state);
                int landingCommands = ReadVectorCommandCount(host.transform);
                HashSet<string> labels = ReadActiveTmpLabels(
                    FindDescendant(host.transform, "F-16C HUD Runtime"));

                Assert.That(landingCommands, Is.GreaterThan(navCommands));
                Assert.That(labels, Does.Contain("LAND"));
                Assert.That(labels, Does.Contain("GEAR"));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(host);
                UnityEngine.Object.DestroyImmediate(cameraObject);
            }
        }

        [Test]
        public void AirToAirModePublishesTargetWeaponDlzShootAndThreatCues()
        {
            GameObject host = new GameObject("HUD Test Host");
            GameObject cameraObject = new GameObject("Cockpit Camera");
            try
            {
                Component controller = AddController(host);
                Camera camera = cameraObject.AddComponent<Camera>();
                InvokeController(controller, "SetCockpitContext", true, camera);
                HudState state = CreateState(HudMode.AirToAir);
                AircraftCombatState combat = new AircraftCombatState
                {
                    Aircraft = new AircraftId("VIPER-01"),
                    MasterArm = MasterArmState.Arm,
                    SelectedStoreType = "AIM-120C",
                    SelectedStoreQuantity = 2,
                    SelectedTarget = new AircraftId("BANDIT-01"),
                    RadarTrackState = RadarTrackState.Locked,
                    TargetRangeM = 18520.0,
                    ClosureRateMps = 250.0,
                    TargetBearingRad = 2.0 * Mathf.Deg2Rad,
                    TargetElevationRad = 1.0 * Mathf.Deg2Rad,
                    InLaunchZone = true,
                    ShootCue = true,
                    MissileLaunchWarning = true,
                    IsValid = true
                };

                InvokeController(controller, "SetCombatState", combat);
                InvokeController(controller, "SetHudState", state);
                HashSet<string> labels = ReadActiveTmpLabels(
                    FindDescendant(host.transform, "F-16C HUD Runtime"));

                Assert.That(labels, Does.Contain("A-A"));
                Assert.That(labels, Does.Contain("ARM"));
                Assert.That(labels, Does.Contain("AIM-120C"));
                Assert.That(labels, Does.Contain("DLZ"));
                Assert.That(labels, Does.Contain("LOCK"));
                Assert.That(labels, Does.Contain("SHOOT"));
                Assert.That(labels, Does.Contain("MISSILE"));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(host);
                UnityEngine.Object.DestroyImmediate(cameraObject);
            }
        }

        private static HudState CreateState(HudMode mode)
        {
            return new HudState
            {
                IsValid = mode != HudMode.Off,
                Mode = mode,
                CalibratedAirspeedMps = 128.611,
                TrueAirspeedMps = 140.0,
                BarometricAltitudeM = 3048.0,
                RadarAltitudeM = 500.0,
                RadarAltitudeValid = true,
                HeadingRad = 270f * Mathf.Deg2Rad,
                PitchRad = 3f * Mathf.Deg2Rad,
                RollRad = -12f * Mathf.Deg2Rad,
                FlightPathAzimuthRad = 1.5f * Mathf.Deg2Rad,
                FlightPathElevationRad = -2f * Mathf.Deg2Rad,
                AngleOfAttackRad = 11f * Mathf.Deg2Rad,
                NormalLoadFactorG = 1.2,
                Mach = 0.78,
                VerticalSpeedMps = -2.54,
                SelectedSteerpointIndex = 4,
                SteerpointBearingRad = 276f * Mathf.Deg2Rad,
                SteerpointElevationRad = -1f * Mathf.Deg2Rad,
                SteerpointDistanceM = 18520.0,
                LandingGearDown = mode == HudMode.Landing,
                SelectedStoreType = "AIM-120C",
                SelectedStoreQuantity = 2
            };
        }

        private static Component AddController(GameObject host)
        {
            return host.AddComponent(RequireType(ControllerTypeName));
        }

        private static Type RequireType(string assemblyQualifiedName)
        {
            Type type = Type.GetType(assemblyQualifiedName, false);
            Assert.That(type, Is.Not.Null, assemblyQualifiedName + " is missing.");
            return type;
        }

        private static void InvokeController(Component controller, string methodName, params object[] arguments)
        {
            MethodInfo method = controller.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            Assert.That(method, Is.Not.Null, methodName + " is missing.");
            method.Invoke(controller, arguments);
        }

        private static T Invoke<T>(MethodInfo method, object target, params object[] arguments)
        {
            return (T)method.Invoke(target, arguments);
        }

        private static T ReadProperty<T>(object target, string propertyName)
        {
            PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            Assert.That(property, Is.Not.Null, propertyName + " is missing.");
            return (T)property.GetValue(target);
        }

        private static Transform FindDescendant(Transform root, string name)
        {
            Transform[] transforms = root.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < transforms.Length; i++)
            {
                if (transforms[i].name == name)
                    return transforms[i];
            }

            return null;
        }

        private static Component FindComponentByFullName(GameObject root, string fullName)
        {
            Component[] components = root.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] != null && components[i].GetType().FullName == fullName)
                    return components[i];
            }

            return null;
        }

        private static int CountComponentsByFullName(Transform root, string fullName)
        {
            int count = 0;
            Component[] components = root.GetComponentsInChildren<Component>(true);
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] != null && components[i].GetType().FullName == fullName)
                    count++;
            }

            return count;
        }

        private static HashSet<string> ReadActiveTmpLabels(Transform root)
        {
            var labels = new HashSet<string>();
            Component[] components = root.GetComponentsInChildren<Component>(false);
            for (int i = 0; i < components.Length; i++)
            {
                Component component = components[i];
                if (component == null || component.GetType().FullName != "TMPro.TextMeshProUGUI")
                    continue;

                PropertyInfo textProperty = component.GetType().GetProperty("text");
                labels.Add((string)textProperty.GetValue(component));
            }

            return labels;
        }

        private static int ReadVectorCommandCount(Transform root)
        {
            Component[] components = root.GetComponentsInChildren<Component>(true);
            for (int i = 0; i < components.Length; i++)
            {
                Component component = components[i];
                if (component != null &&
                    component.GetType().FullName == "FlightSim.Platform.Presentation.HudVectorGraphic")
                {
                    return ReadProperty<int>(component, "CommandCount");
                }
            }

            Assert.Fail("HUD vector graphic is missing.");
            return 0;
        }
    }
}
