using System;
using System.Collections.Generic;
using CesiumForUnity;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Presentation;
using FlightSim.Platform.Unity;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace FlightSim.Platform.Editor
{
    public static class FlightSimV2SceneBuilder
    {
        public const string ScenePath = PlatformAssetLayout.ScenePath;

        private const string LegacyScenePath = "Assets/Scenes/FlightSim_KTEX.unity";
        private const string PrefabFolder = "Assets/FlightSimPlatform/Prefabs";
        private const string VisualPrefabPath = F35AssetPipeline.VisualPrefabPath;
        private const string HudPrefabPath = PrefabFolder + "/F16C_HUD_Runtime.prefab";
        private const string RunwayTextMaterialPath = "Assets/FlightSimPlatform/Materials/Runway_Text_Depth.mat";
        private const string TerrainLayerName = FlightTerrainSampler.DefaultQueryLayerName;

        [MenuItem("FlightSim/Platform/Create or Open KTEX V2 Scene")]
        public static void CreateOrOpenScene()
        {
            EnsureAssetFolder("Assets/FlightSimPlatform", "Prefabs");
            F35AssetPipeline.PrepareAssets();

            SceneAsset existing = AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath);
            if (existing != null)
            {
                Scene existingScene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
                UpgradeScene(existingScene);
                EditorSceneManager.MarkSceneDirty(existingScene);
                if (!EditorSceneManager.SaveScene(existingScene, ScenePath))
                    throw new InvalidOperationException($"Unable to save '{ScenePath}'.");

                AddSceneToBuildSettings();
                AssetDatabase.SaveAssets();
                Selection.activeObject = existing;
                return;
            }

            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(LegacyScenePath) == null)
            {
                throw new InvalidOperationException(
                    $"The preserved source scene '{LegacyScenePath}' is required to create the V2 scene.");
            }

            if (!AssetDatabase.CopyAsset(LegacyScenePath, ScenePath))
                throw new InvalidOperationException($"Unable to copy '{LegacyScenePath}' to '{ScenePath}'.");

            AssetDatabase.Refresh();
            Scene scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            UpgradeScene(scene);
            EditorSceneManager.MarkSceneDirty(scene);
            if (!EditorSceneManager.SaveScene(scene, ScenePath))
                throw new InvalidOperationException($"Unable to save '{ScenePath}'.");

            AddSceneToBuildSettings();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath);
            Debug.Log($"Created platform flight scene at {ScenePath}. The legacy scene was left unchanged.");
        }

        public static void CreateSceneBatch()
        {
            CreateOrOpenScene();
            ValidateOpenScene();
        }

        private static void UpgradeScene(Scene scene)
        {
            RemoveLegacyRuntime(scene);

            GameObject georeferenceObject = FindByName(scene, "CesiumGeoreference");
            GameObject aircraft = FindByName(scene, "Player_Aircraft_F35") ??
                                  FindByName(scene, "Player_Aircraft_F16C");
            GameObject cameraObject = FindByName(scene, "Main Camera");
            if (georeferenceObject == null || aircraft == null || cameraObject == null)
                throw new InvalidOperationException("The source scene is missing its georeference, aircraft, or Main Camera.");

            ConfigureGeoreference(georeferenceObject);
            string terrainLayer = ConfigureTerrain(scene);
            ConfigureRunwayText(scene);
            aircraft.name = "Player_Aircraft_F35";
            ConfigureAircraftAuthority(aircraft);

            GameObject visual = InstallVisualPrefab(aircraft);
            Transform exterior = FindDeepChild(visual.transform, "F35_Exterior");
            Transform cockpit = FindDeepChild(visual.transform, "F35_Cockpit");
            Transform cockpitEye = FindDeepChild(visual.transform, "CockpitEye");
            if (exterior == null || cockpit == null || cockpitEye == null)
                throw new InvalidOperationException("The F-35 visual prefab is missing its camera visibility roots or cockpit eye.");

            FlightCameraRig cameraRig = ConfigureCamera(cameraObject, aircraft.transform, cockpitEye, exterior, cockpit);
            FlightSimulationHost host = ConfigureSimulationHost(georeferenceObject.transform, cameraRig);
            ConfigureFlightDataRecorder(host);
            CesiumAircraftView view = ConfigureAircraftView(aircraft, host);
            ConfigureTerrainSampler(aircraft, view, host, georeferenceObject, terrainLayer);
            ConfigureMissionRuntime(host, view);
            InstallHud(
                georeferenceObject.transform,
                host,
                cameraRig,
                cameraObject.GetComponent<Camera>());

            GameObject routeMarkers = FindByName(scene, "KTEX_TestRoute_WestValley");
            if (routeMarkers != null)
                UnityEngine.Object.DestroyImmediate(routeMarkers);

            GameObject oldHud = FindByName(scene, "Flight_HUD");
            if (oldHud != null)
                UnityEngine.Object.DestroyImmediate(oldHud);
        }

        private static void RemoveLegacyRuntime(Scene scene)
        {
            foreach (GameObject root in scene.GetRootGameObjects())
            {
                MonoBehaviour[] behaviours = root.GetComponentsInChildren<MonoBehaviour>(true);
                for (int i = 0; i < behaviours.Length; i++)
                {
                    MonoBehaviour behaviour = behaviours[i];
                    if (behaviour == null)
                        continue;

                    string typeName = behaviour.GetType().Name;
                    if (typeName == "KtexAircraftController" ||
                        typeName == "KtexFlightCamera" ||
                        typeName == "KtexFlightHud")
                    {
                        UnityEngine.Object.DestroyImmediate(behaviour);
                    }
                }
            }
        }

        private static void ConfigureGeoreference(GameObject georeferenceObject)
        {
            CesiumGeoreference georeference = GetOrAdd<CesiumGeoreference>(georeferenceObject);
            georeference.originPlacement = CesiumGeoreferenceOriginPlacement.CartographicOrigin;
            georeference.originAuthority = CesiumGeoreferenceOriginAuthority.LongitudeLatitudeHeight;
            georeference.latitude = 37.9538;
            georeference.longitude = -107.9087;
            georeference.height = 2765.0;
            georeference.scale = 1.0;

            CesiumCameraManager cameraManager = GetOrAdd<CesiumCameraManager>(georeferenceObject);
            cameraManager.useMainCamera = true;
            cameraManager.useSceneViewCameraInEditor = true;
        }

        private static string ConfigureTerrain(Scene scene)
        {
            int terrainLayer = EnsureLayer(TerrainLayerName);
            CesiumIonServer ionServer = AssetDatabase.LoadAssetAtPath<CesiumIonServer>(PlatformAssetLayout.CesiumServerPath);
            if (ionServer == null)
                throw new InvalidOperationException($"The dedicated Cesium server is missing at '{PlatformAssetLayout.CesiumServerPath}'.");
            Cesium3DTileset[] tilesets = FindComponentsInScene<Cesium3DTileset>(scene);
            for (int i = 0; i < tilesets.Length; i++)
            {
                Cesium3DTileset tileset = tilesets[i];
                tileset.ionServer = ionServer;
                if (tileset.ionAssetID == 1)
                {
                    tileset.maximumScreenSpaceError = 12f;
                    tileset.maximumCachedBytes = 1024L * 1024L * 1024L;
                    tileset.maximumSimultaneousTileLoads = 20;
                    tileset.preloadAncestors = true;
                    tileset.preloadSiblings = true;
                    tileset.createPhysicsMeshes = true;
                    SetLayerRecursively(tileset.gameObject, terrainLayer);

                    CesiumIonRasterOverlay imagery = tileset.GetComponent<CesiumIonRasterOverlay>();
                    if (imagery != null)
                    {
                        imagery.ionServer = ionServer;
                        imagery.maximumScreenSpaceError = 2f;
                        imagery.maximumTextureSize = 2048;
                        imagery.maximumSimultaneousTileLoads = 20;
                    }
                }
                else if (tileset.ionAssetID == 96188 ||
                         tileset.gameObject.name.IndexOf("OSM", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    tileset.gameObject.SetActive(false);
                }
            }

            return LayerMask.LayerToName(terrainLayer);
        }

        private static void ConfigureRunwayText(Scene scene)
        {
            TextMesh[] textMeshes = FindComponentsInScene<TextMesh>(scene);
            Renderer firstRenderer = null;
            for (int i = 0; i < textMeshes.Length; i++)
            {
                if (textMeshes[i].name.StartsWith("Runway_Number_", StringComparison.Ordinal))
                {
                    firstRenderer = textMeshes[i].GetComponent<Renderer>();
                    if (firstRenderer != null)
                        break;
                }
            }

            if (firstRenderer == null)
                return;

            Shader shader = Shader.Find("FlightSim/Runway Text Depth");
            if (shader == null)
                throw new InvalidOperationException("The runway depth-tested text shader is unavailable.");

            EnsureAssetFolder("Assets/FlightSimPlatform", "Materials");
            Material material = AssetDatabase.LoadAssetAtPath<Material>(RunwayTextMaterialPath);
            if (material == null)
            {
                material = new Material(shader) { name = "Runway_Text_Depth" };
                AssetDatabase.CreateAsset(material, RunwayTextMaterialPath);
            }
            else
            {
                material.shader = shader;
            }

            Texture fontAtlas = firstRenderer.sharedMaterial != null
                ? firstRenderer.sharedMaterial.mainTexture
                : null;
            material.mainTexture = fontAtlas;
            material.color = Color.white;
            material.SetFloat("_Cutoff", 0.25f);
            material.renderQueue = (int)RenderQueue.AlphaTest;
            EditorUtility.SetDirty(material);

            for (int i = 0; i < textMeshes.Length; i++)
            {
                if (!textMeshes[i].name.StartsWith("Runway_Number_", StringComparison.Ordinal))
                    continue;

                Renderer renderer = textMeshes[i].GetComponent<Renderer>();
                if (renderer == null)
                    continue;
                renderer.sharedMaterial = material;
                renderer.shadowCastingMode = ShadowCastingMode.Off;
                renderer.receiveShadows = false;
            }
        }

        private static void ConfigureAircraftAuthority(GameObject aircraft)
        {
            Rigidbody body = aircraft.GetComponent<Rigidbody>();
            if (body != null)
                UnityEngine.Object.DestroyImmediate(body);

            CesiumOriginShift originShift = aircraft.GetComponent<CesiumOriginShift>();
            if (originShift != null)
                UnityEngine.Object.DestroyImmediate(originShift);

            Collider[] colliders = aircraft.GetComponentsInChildren<Collider>(true);
            for (int i = 0; i < colliders.Length; i++)
                UnityEngine.Object.DestroyImmediate(colliders[i]);

            GameObject colliderRoot = FindDeepChild(aircraft.transform, "Simplified_Physics_Colliders")?.gameObject;
            if (colliderRoot != null)
                UnityEngine.Object.DestroyImmediate(colliderRoot);
        }

        private static GameObject InstallVisualPrefab(GameObject aircraft)
        {
            GameObject visualPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(VisualPrefabPath);
            if (visualPrefab == null)
                throw new InvalidOperationException($"The prepared F-35 visual prefab is missing at '{VisualPrefabPath}'.");

            DestroyDirectChild(aircraft.transform, "F16C_Exterior");
            DestroyDirectChild(aircraft.transform, "F16C_Cockpit");
            DestroyDeepChild(aircraft.transform, "F16C_Prototype_Visual");
            DestroyDeepChild(aircraft.transform, "F35_Visual");

            GameObject visual = (GameObject)PrefabUtility.InstantiatePrefab(visualPrefab);
            visual.name = "F35_Visual";
            visual.transform.SetParent(aircraft.transform, false);
            return visual;
        }

        private static FlightCameraRig ConfigureCamera(
            GameObject cameraObject,
            Transform aircraft,
            Transform cockpitEye,
            Transform exterior,
            Transform cockpit)
        {
            cameraObject.tag = "MainCamera";
            Camera camera = GetOrAdd<Camera>(cameraObject);
            camera.nearClipPlane = 0.025f;
            camera.farClipPlane = 500000f;
            camera.fieldOfView = 72f;
            GetOrAdd<AudioListener>(cameraObject);

            CesiumGlobeAnchor anchor = GetOrAdd<CesiumGlobeAnchor>(cameraObject);
            anchor.detectTransformChanges = false;
            anchor.adjustOrientationForGlobeWhenMoving = false;
            CesiumOriginShift shift = GetOrAdd<CesiumOriginShift>(cameraObject);
            shift.distance = 4000.0;

            FlightCameraRig rig = GetOrAdd<FlightCameraRig>(cameraObject);
            SetObjectReference(rig, "aircraftTarget", aircraft);
            SetObjectReference(rig, "cockpitEye", cockpitEye);
            SetObjectReference(rig, "exteriorModelRoot", exterior);
            SetObjectReference(rig, "cockpitModelRoot", cockpit);
            SetEnum(rig, "mode", (int)FlightCameraMode.Cockpit);
            return rig;
        }

        private static FlightSimulationHost ConfigureSimulationHost(Transform parent, FlightCameraRig cameraRig)
        {
            GameObject platform = FindDeepChild(parent, "FlightSim_Platform")?.gameObject;
            if (platform == null)
            {
                platform = new GameObject("FlightSim_Platform");
                platform.transform.SetParent(parent, false);
            }

            FlightSimulationHost host = GetOrAdd<FlightSimulationHost>(platform);
            SetString(host, "localAircraftId", "VIPER-01");
            SetEnum(host, "initialPreset", (int)StartupPreset.RunwayReady);
            SetFloat(host, "initialThrottleNormalized", 0f);
            SetObjectReference(host, "cameraRig", cameraRig);
            return host;
        }

        private static CesiumAircraftView ConfigureAircraftView(GameObject aircraft, FlightSimulationHost host)
        {
            CesiumGlobeAnchor anchor = GetOrAdd<CesiumGlobeAnchor>(aircraft);
            anchor.detectTransformChanges = false;
            anchor.adjustOrientationForGlobeWhenMoving = false;
            CesiumAircraftView view = GetOrAdd<CesiumAircraftView>(aircraft);
            SetObjectReference(view, "simulationHost", host);
            return view;
        }

        private static void ConfigureFlightDataRecorder(FlightSimulationHost host)
        {
            FlightDataRecorder recorder = GetOrAdd<FlightDataRecorder>(host.gameObject);
            SetObjectReference(recorder, "simulationHost", host);
            SetBool(recorder, "autoStart", true);
            SetEnum(recorder, "toggleRecordingKey", (int)KeyCode.F9);
            SetEnum(recorder, "markerKey", (int)KeyCode.F10);
            SetLong(recorder, "telemetryFileBytes", 268435456L);
            MissionDataRecorder missionRecorder = GetOrAdd<MissionDataRecorder>(host.gameObject);
            SetObjectReference(missionRecorder, "simulationHost", host);
            SetObjectReference(missionRecorder, "flightRecorder", recorder);
        }

        private static void ConfigureTerrainSampler(
            GameObject aircraft,
            CesiumAircraftView view,
            FlightSimulationHost host,
            GameObject georeference,
            string terrainLayer)
        {
            FlightTerrainSampler sampler = GetOrAdd<FlightTerrainSampler>(aircraft);
            SetObjectReference(sampler, "simulationHost", host);
            SetObjectReference(sampler, "aircraftTransform", view.transform);
            SetObjectReference(sampler, "georeference", georeference.GetComponent<CesiumGeoreference>());
            SetString(sampler, "terrainQueryLayerName", terrainLayer);
        }

        private static void ConfigureMissionRuntime(FlightSimulationHost host, CesiumAircraftView playerView)
        {
            GameObject visualPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(VisualPrefabPath);
            MissionAircraftViewManager views = GetOrAdd<MissionAircraftViewManager>(host.gameObject);
            views.Configure(host, playerView, visualPrefab);

            MissionBriefingPanel briefing = GetOrAdd<MissionBriefingPanel>(host.gameObject);
            SetObjectReference(briefing, "simulationHost", host);
        }

        private static void InstallHud(
            Transform parent,
            FlightSimulationHost host,
            FlightCameraRig cameraRig,
            Camera cockpitCamera)
        {
            GameObject hudPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(HudPrefabPath);
            if (hudPrefab == null)
            {
                GameObject source = new GameObject("F16C_HUD_Runtime");
                source.AddComponent<F16HudController>();
                source.AddComponent<FlightHudBridge>();
                hudPrefab = PrefabUtility.SaveAsPrefabAsset(source, HudPrefabPath);
                UnityEngine.Object.DestroyImmediate(source);
                if (hudPrefab == null)
                    throw new InvalidOperationException($"Unable to save '{HudPrefabPath}'.");
            }

            GameObject existing = FindDeepChild(parent, "F16C_HUD_Runtime")?.gameObject;
            if (existing != null)
                UnityEngine.Object.DestroyImmediate(existing);

            GameObject hud = (GameObject)PrefabUtility.InstantiatePrefab(hudPrefab);
            hud.name = "F16C_HUD_Runtime";
            hud.transform.SetParent(parent, false);
            FlightHudBridge bridge = GetOrAdd<FlightHudBridge>(hud);
            SetObjectReference(bridge, "simulationHost", host);
            SetObjectReference(bridge, "cameraRig", cameraRig);
            SetObjectReference(bridge, "cockpitCamera", cockpitCamera);
        }

        private static void ValidateOpenScene()
        {
            FlightSimulationHost host = UnityEngine.Object.FindObjectOfType<FlightSimulationHost>();
            CesiumAircraftView view = UnityEngine.Object.FindObjectOfType<CesiumAircraftView>();
            FlightCameraRig cameraRig = UnityEngine.Object.FindObjectOfType<FlightCameraRig>();
            FlightTerrainSampler sampler = UnityEngine.Object.FindObjectOfType<FlightTerrainSampler>();
            FlightDataRecorder recorder = UnityEngine.Object.FindObjectOfType<FlightDataRecorder>();
            MissionDataRecorder missionRecorder = UnityEngine.Object.FindObjectOfType<MissionDataRecorder>();
            F16HudController hud = UnityEngine.Object.FindObjectOfType<F16HudController>();
            MissionAircraftViewManager missionViews = UnityEngine.Object.FindObjectOfType<MissionAircraftViewManager>();
            MissionBriefingPanel briefing = UnityEngine.Object.FindObjectOfType<MissionBriefingPanel>();
            if (host == null || view == null || cameraRig == null || sampler == null || recorder == null || missionRecorder == null || hud == null || missionViews == null || briefing == null)
                throw new InvalidOperationException("The V2 scene is missing one or more platform components.");
            if (view.GetComponent<Rigidbody>() != null || view.GetComponent<CesiumOriginShift>() != null)
                throw new InvalidOperationException("The aircraft visual still contains legacy physics/origin authority.");
            if (cameraRig.GetComponent<CesiumOriginShift>() == null)
                throw new InvalidOperationException("CesiumOriginShift must be attached to the Main Camera.");
            if (FindDeepChild(view.transform, "F35_Visual") == null)
                throw new InvalidOperationException("The aircraft view is missing the prepared F-35 visual.");
        }

        private static T GetOrAdd<T>(GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }

        private static T[] FindComponentsInScene<T>(Scene scene) where T : Component
        {
            var result = new List<T>();
            foreach (GameObject root in scene.GetRootGameObjects())
                result.AddRange(root.GetComponentsInChildren<T>(true));
            return result.ToArray();
        }

        private static GameObject FindByName(Scene scene, string name)
        {
            foreach (GameObject root in scene.GetRootGameObjects())
            {
                Transform found = FindDeepChild(root.transform, name);
                if (found != null)
                    return found.gameObject;
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

        private static void DestroyDirectChild(Transform parent, string name)
        {
            Transform child = parent.Find(name);
            if (child != null)
                UnityEngine.Object.DestroyImmediate(child.gameObject);
        }

        private static void DestroyDeepChild(Transform parent, string name)
        {
            Transform child = FindDeepChild(parent, name);
            if (child != null)
                UnityEngine.Object.DestroyImmediate(child.gameObject);
        }

        private static void SetLayerRecursively(GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            for (int i = 0; i < gameObject.transform.childCount; i++)
                SetLayerRecursively(gameObject.transform.GetChild(i).gameObject, layer);
        }

        private static int EnsureLayer(string layerName)
        {
            int existing = LayerMask.NameToLayer(layerName);
            if (existing >= 0)
                return existing;

            SerializedObject tagManager = new SerializedObject(
                AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layers = tagManager.FindProperty("layers");
            for (int i = 8; i < 32; i++)
            {
                SerializedProperty layer = layers.GetArrayElementAtIndex(i);
                if (string.IsNullOrEmpty(layer.stringValue))
                {
                    layer.stringValue = layerName;
                    tagManager.ApplyModifiedPropertiesWithoutUndo();
                    return i;
                }
            }

            throw new InvalidOperationException($"No free user layer is available for '{layerName}'.");
        }

        private static void SetObjectReference(UnityEngine.Object target, string name, UnityEngine.Object value)
        {
            SerializedObject serialized = new SerializedObject(target);
            serialized.FindProperty(name).objectReferenceValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetString(UnityEngine.Object target, string name, string value)
        {
            SerializedObject serialized = new SerializedObject(target);
            serialized.FindProperty(name).stringValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetFloat(UnityEngine.Object target, string name, float value)
        {
            SerializedObject serialized = new SerializedObject(target);
            serialized.FindProperty(name).floatValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetLong(UnityEngine.Object target, string name, long value)
        {
            SerializedObject serialized = new SerializedObject(target);
            serialized.FindProperty(name).longValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetBool(UnityEngine.Object target, string name, bool value)
        {
            SerializedObject serialized = new SerializedObject(target);
            serialized.FindProperty(name).boolValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetEnum(UnityEngine.Object target, string name, int value)
        {
            SerializedObject serialized = new SerializedObject(target);
            serialized.FindProperty(name).intValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void EnsureAssetFolder(string parent, string name)
        {
            string path = parent + "/" + name;
            if (!AssetDatabase.IsValidFolder(path))
                AssetDatabase.CreateFolder(parent, name);
        }

        private static void AddSceneToBuildSettings()
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            var ordered = new List<EditorBuildSettingsScene>(scenes.Length + 1)
            {
                new EditorBuildSettingsScene(ScenePath, true)
            };
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i].path != ScenePath)
                    ordered.Add(scenes[i]);
            }

            EditorBuildSettings.scenes = ordered.ToArray();
        }
    }
}
