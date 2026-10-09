using System.Linq;
using CesiumForUnity;
using FlightSim.Platform.Presentation;
using FlightSim.Platform.Unity;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace FlightSim.Platform.Editor.Tests
{
    public sealed class FlightSimV2SceneBuilderTests
    {
        [TearDown]
        public void TearDown()
        {
            Cesium3DTileset[] tilesets = Object.FindObjectsOfType<Cesium3DTileset>(true);
            for (int index = 0; index < tilesets.Length; index++)
                tilesets[index].enabled = false;
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        }

        [Test]
        public void GeneratedSceneUsesPlatformAuthorityAndCameraOriginShift()
        {
            FlightSimV2SceneBuilder.CreateOrOpenScene();
            FlightSimV2SceneBuilder.CreateOrOpenScene();
            EditorSceneManager.OpenScene(FlightSimV2SceneBuilder.ScenePath);

            FlightSimulationHost host = Object.FindObjectOfType<FlightSimulationHost>();
            CesiumAircraftView view = Object.FindObjectOfType<CesiumAircraftView>();
            FlightCameraRig cameraRig = Object.FindObjectOfType<FlightCameraRig>();
            FlightTerrainSampler terrainSampler = Object.FindObjectOfType<FlightTerrainSampler>();
            FlightDataRecorder recorder = Object.FindObjectOfType<FlightDataRecorder>();
            F16HudController hud = Object.FindObjectOfType<F16HudController>();

            Assert.That(host, Is.Not.Null);
            Assert.That(view, Is.Not.Null);
            Assert.That(cameraRig, Is.Not.Null);
            Assert.That(terrainSampler, Is.Not.Null);
            Assert.That(Object.FindObjectsOfType<FlightDataRecorder>().Length, Is.EqualTo(1));
            Assert.That(recorder.SimulationHost, Is.SameAs(host));
            var recorderSerialized = new SerializedObject(recorder);
            Assert.That(recorderSerialized.FindProperty("autoStart").boolValue, Is.True);
            Assert.That(recorderSerialized.FindProperty("toggleRecordingKey").intValue, Is.EqualTo((int)KeyCode.F9));
            Assert.That(recorderSerialized.FindProperty("markerKey").intValue, Is.EqualTo((int)KeyCode.F10));
            Assert.That(
                recorderSerialized.FindProperty("telemetryFileBytes").longValue,
                Is.EqualTo(268435456L));
            Assert.That(hud, Is.Not.Null);
            Assert.That(hud.GetComponent("FlightHudBridge"), Is.Not.Null);
            Assert.That(cameraRig.Mode, Is.EqualTo(FlightCameraMode.Cockpit));
            Assert.That(cameraRig.GetComponent<CesiumOriginShift>(), Is.Not.Null);
            Assert.That(view.GetComponent<CesiumOriginShift>(), Is.Null);
            Assert.That(view.GetComponent<Rigidbody>(), Is.Null);
            Assert.That(view.gameObject.name, Is.EqualTo("Player_Aircraft_F35"));
            Assert.That(FindDeepChild(view.transform, "F35_Visual"), Is.Not.Null);
            Assert.That(FindDeepChild(view.transform, "F16C_Prototype_Visual"), Is.Null);
            Assert.That(
                view.GetComponentsInChildren<MonoBehaviour>(true)
                    .Any(component => component.GetType().Name == "KtexAircraftController"),
                Is.False);
            Assert.That(
                cameraRig.GetComponents<MonoBehaviour>()
                    .Any(component => component.GetType().Name == "KtexFlightCamera"),
                Is.False);
            Assert.That(EditorBuildSettings.scenes[0].path, Is.EqualTo(FlightSimV2SceneBuilder.ScenePath));
            Assert.That(EditorBuildSettings.scenes[0].enabled, Is.True);

            AssertRunwayTextUsesDepthTest("Runway_Number_09");
            AssertRunwayTextUsesDepthTest("Runway_Number_27");
        }

        private static void AssertRunwayTextUsesDepthTest(string objectName)
        {
            GameObject runwayText = GameObject.Find(objectName);
            Assert.That(runwayText, Is.Not.Null, objectName);
            Renderer renderer = runwayText.GetComponent<Renderer>();
            Assert.That(renderer, Is.Not.Null, objectName);
            Assert.That(renderer.sharedMaterial, Is.Not.Null, objectName);
            Assert.That(renderer.sharedMaterial.shader.name, Is.EqualTo("FlightSim/Runway Text Depth"), objectName);
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
    }
}
