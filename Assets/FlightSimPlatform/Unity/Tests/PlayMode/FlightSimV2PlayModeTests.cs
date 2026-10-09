using System.Collections;
using System.IO;
using System.Linq;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using FlightSim.Platform.Presentation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace FlightSim.Platform.Unity.Tests.PlayMode
{
    public sealed class FlightSimV2PlayModeTests
    {
        private const string ForwardCanopyFrameName = "LP_Exterior_Canopy_frame_current_f35_int_1_0";

        private static readonly Vector2Int[] CaptureResolutions =
        {
            new Vector2Int(1280, 720),
            new Vector2Int(1920, 1080),
            new Vector2Int(1024, 768),
            new Vector2Int(2560, 1080),
            new Vector2Int(3840, 2160)
        };

        [UnityTest]
        public IEnumerator V2SceneRunsAuthoritativeSimulationAndCockpitOnlyHud()
        {
            SceneManager.LoadScene("FlightSim_KTEX_V2", LoadSceneMode.Single);
            yield return null;
            yield return null;

            FlightSimulationHost host = Object.FindObjectOfType<FlightSimulationHost>();
            FlightCameraRig cameraRig = Object.FindObjectOfType<FlightCameraRig>();
            F16HudController hud = Object.FindObjectOfType<F16HudController>();
            CesiumAircraftView aircraftView = Object.FindObjectOfType<CesiumAircraftView>();
            Assert.That(host, Is.Not.Null);
            Assert.That(host.UdpEndpoint, Is.Not.Null);
            Assert.That(cameraRig, Is.Not.Null);
            Assert.That(hud, Is.Not.Null);
            Assert.That(aircraftView, Is.Not.Null);
            Assert.That(aircraftView.GetComponent<Rigidbody>(), Is.Null);
            Assert.That(cameraRig.Mode, Is.EqualTo(FlightCameraMode.Cockpit));
            Assert.That(hud.IsHudVisible, Is.True);

            Text missionTitle = Object.FindObjectsOfType<Text>(true)
                .FirstOrDefault(text => text.text == "选择你的下一次飞行");
            Assert.That(missionTitle, Is.Not.Null);
            Assert.That(missionTitle.font.name, Does.Contain("NotoSansSC"));
            Assert.That(missionTitle.font.HasCharacter('紧'), Is.True);

            Transform visual = FindDeepChild(aircraftView.transform, "F35_Visual");
            Assert.That(visual, Is.Not.Null);
            Transform exterior = FindDeepChild(visual, "F35_Exterior");
            Transform cockpit = FindDeepChild(visual, "F35_Cockpit");
            Renderer exteriorFrame = FindRenderer(exterior, ForwardCanopyFrameName);
            Renderer cockpitFrame = FindRenderer(cockpit, ForwardCanopyFrameName);
            Assert.That(exteriorFrame, Is.Not.Null);
            Assert.That(cockpitFrame, Is.Not.Null);
            Assert.That(exteriorFrame.enabled, Is.True);
            Assert.That(cockpitFrame.enabled, Is.False);
            Assert.That(cockpit.GetComponentsInChildren<Renderer>(true).Count(renderer => !renderer.enabled), Is.EqualTo(1));

            cameraRig.SetMode(FlightCameraMode.Chase);
            yield return null;
            Assert.That(hud.IsHudVisible, Is.False);
            cameraRig.SetMode(FlightCameraMode.Cockpit);
            yield return null;
            Assert.That(hud.IsHudVisible, Is.True);

            Assert.That(
                host.SimulationService.TryGetLatest(host.LocalAircraft, out AircraftSnapshot initial),
                Is.True);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 1.0;
            Assert.That(host.SubmitPilotInput(in input), Is.True);
            for (int tick = 0; tick < 1800; tick++)
                host.SimulationService.Tick();

            Assert.That(
                host.SimulationService.TryGetLatest(host.LocalAircraft, out AircraftSnapshot accelerated),
                Is.True);
            Assert.That(
                accelerated.Fast.CalibratedAirspeedMps,
                Is.GreaterThan(initial.Fast.CalibratedAirspeedMps + 45.0));
            Assert.That(accelerated.Fast.CalibratedAirspeedMps, Is.GreaterThan(55.0));
        }

        [UnityTest]
        public IEnumerator RecorderCreatesRowsMarkerAndReleasesSessionFiles()
        {
            string root = Path.Combine(
                Path.GetTempPath(),
                "FlightSim-PlayMode-" + System.Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(root);
            try
            {
                SceneManager.LoadScene("FlightSim_KTEX_V2", LoadSceneMode.Single);
                yield return null;
                yield return null;

                FlightDataRecorder recorder = Object.FindObjectOfType<FlightDataRecorder>();
                FlightSimulationHost host = Object.FindObjectOfType<FlightSimulationHost>();
                Assert.That(recorder, Is.Not.Null);
                Assert.That(host, Is.Not.Null);
                recorder.StopRecording();
                Assert.That(recorder.StartRecording(root), Is.True);

                PilotControlInput recordedInput = new PilotControlInput
                {
                    PitchNormalized = 0.125,
                    RollNormalized = -0.25,
                    YawNormalized = 0.375,
                    ThrottleNormalized = 0.75,
                    WheelBrakeNormalized = 0.5,
                    SpeedBrakeNormalized = 0.25,
                    EngineStartCommand = true
                };
                Assert.That(host.SubmitPilotInput(in recordedInput), Is.True);

                for (int frame = 0; frame < 6; frame++)
                {
                    for (int tick = 0; tick < 10; tick++)
                        host.SimulationService.Tick();
                    yield return null;
                }

                yield return new WaitForSecondsRealtime(1.1f);
                Assert.That(recorder.IsRecording, Is.True);
                Assert.That(
                    Path.GetFullPath(recorder.SessionDirectory).StartsWith(
                        Path.GetFullPath(root) + Path.DirectorySeparatorChar,
                        System.StringComparison.OrdinalIgnoreCase),
                    Is.True);
                Assert.That(
                    File.ReadAllLines(Path.Combine(recorder.SessionDirectory, "telemetry_000.csv")).Length,
                    Is.GreaterThan(1),
                    "Periodic flushing must expose telemetry while the recording remains active.");

                recorder.AddUserMarker("PLAYMODE_MARKER");
                string session = recorder.SessionDirectory;
                recorder.StopRecording();

                Assert.That(recorder.IsRecording, Is.False);
                string[] telemetry = File.ReadAllLines(Path.Combine(session, "telemetry_000.csv"));
                Assert.That(telemetry.Length, Is.GreaterThan(1));
                string[] header = telemetry[0].Split(',');
                int throttleColumn = System.Array.IndexOf(header, "input_throttle");
                int engineStartColumn = System.Array.IndexOf(header, "input_engine_start");
                Assert.That(throttleColumn, Is.GreaterThanOrEqualTo(0));
                Assert.That(engineStartColumn, Is.GreaterThanOrEqualTo(0));
                Assert.That(
                    telemetry.Skip(1).Any(row =>
                    {
                        string[] fields = row.Split(',');
                        return fields[throttleColumn] == "0.75" && fields[engineStartColumn] == "1";
                    }),
                    Is.True,
                    "The recorder must retain all pilot controls, not only conditioned axes.");
                Assert.That(File.ReadAllText(Path.Combine(session, "events.csv")), Does.Contain("PLAYMODE_MARKER"));
                Directory.Delete(root, true);
                Assert.That(Directory.Exists(root), Is.False);
            }
            finally
            {
                if (Directory.Exists(root))
                    Directory.Delete(root, true);
            }
        }

        [UnityTest]
        public IEnumerator RecorderReregistersAfterDisableAndEnable()
        {
            string root = Path.Combine(
                Path.GetTempPath(),
                "FlightSim-Reenable-" + System.Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(root);
            try
            {
                SceneManager.LoadScene("FlightSim_KTEX_V2", LoadSceneMode.Single);
                yield return null;
                yield return null;

                FlightDataRecorder recorder = Object.FindObjectOfType<FlightDataRecorder>();
                FlightSimulationHost host = Object.FindObjectOfType<FlightSimulationHost>();
                Assert.That(recorder, Is.Not.Null);
                Assert.That(host, Is.Not.Null);
                recorder.StopRecording();
                typeof(FlightDataRecorder)
                    .GetField("autoStart", System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic)
                    .SetValue(recorder, false);

                recorder.enabled = false;
                yield return null;
                recorder.enabled = true;
                yield return null;

                Assert.That(recorder.StartRecording(root), Is.True);
                for (int tick = 0; tick < 10; tick++)
                    host.SimulationService.Tick();
                recorder.AddUserMarker("REENABLE_MARKER");
                string session = recorder.SessionDirectory;
                recorder.StopRecording();

                Assert.That(
                    File.ReadAllLines(Path.Combine(session, "telemetry_000.csv")).Length,
                    Is.GreaterThan(1),
                    "A re-enabled recorder must receive telemetry after registering again.");
                Assert.That(
                    File.ReadAllText(Path.Combine(session, "events.csv")),
                    Does.Contain("REENABLE_MARKER"));
                Directory.Delete(root, true);
                Assert.That(Directory.Exists(root), Is.False);
            }
            finally
            {
                if (Directory.Exists(root))
                    Directory.Delete(root, true);
            }
        }

        [UnityTest]
        public IEnumerator RecorderClosesActiveSessionOnApplicationQuit()
        {
            string root = Path.Combine(
                Path.GetTempPath(),
                "FlightSim-ApplicationQuit-" + System.Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(root);
            FlightDataRecorder recorder = null;
            try
            {
                SceneManager.LoadScene("FlightSim_KTEX_V2", LoadSceneMode.Single);
                yield return null;
                yield return null;

                recorder = Object.FindObjectOfType<FlightDataRecorder>();
                FlightSimulationHost host = Object.FindObjectOfType<FlightSimulationHost>();
                Assert.That(recorder, Is.Not.Null);
                Assert.That(host, Is.Not.Null);
                recorder.StopRecording();
                Assert.That(recorder.StartRecording(root), Is.True);
                for (int tick = 0; tick < 10; tick++)
                    host.SimulationService.Tick();

                System.Reflection.MethodInfo onApplicationQuit = typeof(FlightDataRecorder).GetMethod(
                    "OnApplicationQuit",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                Assert.That(onApplicationQuit, Is.Not.Null);
                string session = recorder.SessionDirectory;
                onApplicationQuit.Invoke(recorder, null);

                Assert.That(recorder.IsRecording, Is.False);
                Assert.That(
                    File.ReadAllLines(Path.Combine(session, "telemetry_000.csv")).Length,
                    Is.GreaterThan(1));
                Assert.That(
                    File.ReadAllText(Path.Combine(session, "events.csv")),
                    Does.Contain("RECORDING_STOPPED"));
                Directory.Delete(root, true);
                Assert.That(Directory.Exists(root), Is.False);
            }
            finally
            {
                recorder?.StopRecording();
                if (Directory.Exists(root))
                    Directory.Delete(root, true);
            }
        }

        [UnityTest]
        public IEnumerator HudRendersInsideCombinerAtSupportedResolutions()
        {
            SceneManager.LoadScene("FlightSim_KTEX_V2", LoadSceneMode.Single);
            yield return null;
            yield return null;

            Camera camera = Object.FindObjectOfType<FlightCameraRig>().GetComponent<Camera>();
            F16HudController hud = Object.FindObjectOfType<F16HudController>();
            Assert.That(hud.IsHudVisible, Is.True);
            string outputDirectory = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), "Artifacts/HudCaptures"));
            Directory.CreateDirectory(outputDirectory);

            for (int i = 0; i < CaptureResolutions.Length; i++)
            {
                Vector2Int resolution = CaptureResolutions[i];
                RenderTexture target = new RenderTexture(
                    resolution.x,
                    resolution.y,
                    24,
                    RenderTextureFormat.ARGB32);
                Texture2D image = new Texture2D(
                    resolution.x,
                    resolution.y,
                    TextureFormat.RGB24,
                    false);
                try
                {
                    camera.targetTexture = target;
                    yield return null;
                    Canvas.ForceUpdateCanvases();
                    camera.Render();

                    RenderTexture previous = RenderTexture.active;
                    RenderTexture.active = target;
                    image.ReadPixels(new Rect(0, 0, resolution.x, resolution.y), 0, 0);
                    image.Apply(false, false);
                    RenderTexture.active = previous;
                    Assert.That(CountHudGreenPixels(image), Is.GreaterThan(100), $"HUD was blank at {resolution}.");
                    File.WriteAllBytes(
                        Path.Combine(
                            outputDirectory,
                            $"F16C_HUD_{resolution.x}x{resolution.y}.png"),
                        image.EncodeToPNG());

                    RectTransform combiner = FindCombiner(hud);
                    Assert.That(combiner, Is.Not.Null);
                    Assert.That(
                        combiner.rect.width,
                        Is.EqualTo(F16HudLayoutMath.CombinerWidth).Within(0.1f));
                    Assert.That(
                        combiner.rect.height,
                        Is.EqualTo(F16HudLayoutMath.CombinerHeight).Within(0.1f));
                    Assert.That(
                        combiner.rect.width * combiner.localScale.x,
                        Is.EqualTo(F16HudLayoutMath.CombinerRenderedWidth * hud.HudScale).Within(0.1f));
                    Assert.That(
                        combiner.rect.height * combiner.localScale.y,
                        Is.EqualTo(F16HudLayoutMath.CombinerRenderedHeight * hud.HudScale).Within(0.1f));
                    Assert.That(combiner.rect.width, Is.GreaterThan(0f));
                    Assert.That(combiner.rect.height, Is.GreaterThan(0f));

                    FlightCameraRig cameraRig = Object.FindObjectOfType<FlightCameraRig>();
                    cameraRig.SetMode(FlightCameraMode.Chase);
                    yield return null;
                    Assert.That(hud.IsHudVisible, Is.False, $"HUD leaked into chase view at {resolution}.");
                    cameraRig.SetMode(FlightCameraMode.Cockpit);
                    yield return null;
                    Assert.That(hud.IsHudVisible, Is.True, $"HUD did not return in cockpit view at {resolution}.");
                }
                finally
                {
                    camera.targetTexture = null;
                    Object.DestroyImmediate(target);
                    Object.DestroyImmediate(image);
                }
            }
        }

        [UnityTest]
        public IEnumerator EveryBuiltInMissionStartsVisibleAutomationAndSpawnsAllActors()
        {
            SceneManager.LoadScene("FlightSim_KTEX_V2", LoadSceneMode.Single);
            yield return null;
            yield return null;
            FlightSimulationHost host = Object.FindObjectOfType<FlightSimulationHost>();
            MissionAircraftViewManager viewManager = Object.FindObjectOfType<MissionAircraftViewManager>();
            string[] missionIds =
            {
                "KTEX_SCRAMBLE_01",
                "KTEX_CAP_01",
                "KTEX_ESCORT_01",
                "KTEX_EMERGENCY_RTB_01"
            };

            Assert.That(host, Is.Not.Null);
            Assert.That(viewManager, Is.Not.Null);
            var canonicalHub = host.DataHub;
            Assert.That(canonicalHub, Is.Not.Null);
            for (int index = 0; index < missionIds.Length; index++)
            {
                Assert.That(host.LoadMission(missionIds[index]).Accepted, Is.True, missionIds[index]);
                Assert.That(host.DataHub, Is.SameAs(canonicalHub), missionIds[index]);
                Assert.That(canonicalHub.TryGetMission(out MissionSnapshot briefing), Is.True, missionIds[index]);
                Assert.That(briefing.Mission.MissionId, Is.EqualTo(missionIds[index]), missionIds[index]);
                Assert.That(briefing.Mission.Phase, Is.EqualTo(MissionPhase.Briefing), missionIds[index]);
                Assert.That(host.StartMission(true).Accepted, Is.True, missionIds[index]);
                host.SetSessionPaused(false);
                MissionSnapshot snapshot = default(MissionSnapshot);
                for (int frame = 0; frame < 60; frame++)
                {
                    yield return null;
                    Assert.That(host.TryGetMissionSnapshot(out snapshot), Is.True);
                    if (snapshot.Mission.Tick > 0UL) break;
                }
                Assert.That(snapshot.Mission.Phase, Is.EqualTo(MissionPhase.Running), missionIds[index]);
                Assert.That(snapshot.Automation.Mode, Is.EqualTo(AutomationMode.Visible), missionIds[index]);
                Assert.That(
                    snapshot.Automation.PlayerControlAuthority,
                    Is.EqualTo(ControlAuthority.AutomationPilot),
                    missionIds[index]);
                Assert.That(viewManager.SpawnedViewCount, Is.EqualTo(snapshot.Actors.Length - 1), missionIds[index]);
                Assert.That(snapshot.Mission.Tick, Is.GreaterThan(0UL), missionIds[index]);
            }
        }

        private static int CountHudGreenPixels(Texture2D image)
        {
            Color32[] pixels = image.GetPixels32();
            int count = 0;
            for (int index = 0; index < pixels.Length; index++)
            {
                Color32 pixel = pixels[index];
                if (pixel.g >= 150 && pixel.g >= pixel.r * 1.35f && pixel.g >= pixel.b * 1.10f)
                    count++;
            }
            return count;
        }

        private static RectTransform FindCombiner(F16HudController hud)
        {
            RectTransform[] transforms = hud.GetComponentsInChildren<RectTransform>(true);
            for (int i = 0; i < transforms.Length; i++)
            {
                if (transforms[i].name == "Combiner Safe Area")
                    return transforms[i];
            }

            return null;
        }

        private static Transform FindDeepChild(Transform parent, string name)
        {
            if (parent.name == name)
                return parent;
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform found = FindDeepChild(parent.GetChild(i), name);
                if (found != null)
                    return found;
            }
            return null;
        }

        private static Renderer FindRenderer(Transform parent, string name)
        {
            return parent.GetComponentsInChildren<Renderer>(true)
                .SingleOrDefault(renderer => renderer.name == name);
        }
    }
}
