using System;
using CesiumForUnity;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlightSim.Platform.Editor
{
    public static class FlightSimDistributionMigration
    {
        private const string LegacyF35Root = "Assets/External/F35";
        private const string LegacyScenePath = "Assets/Scenes/FlightSim_KTEX_V2.unity";
        private const string DefaultIonServerPath = "Assets/CesiumSettings/Resources/CesiumIonServers/ion.cesium.com.asset";

        private static readonly string[] RunwayAssets =
        {
            "MAT_Runway_Asphalt.mat",
            "MAT_Runway_Marking_White.mat",
            "PMAT_LowFriction_Runway.asset"
        };

        [MenuItem("FlightSim/Distribution/Migrate Assets to Canonical Layout")]
        public static void Apply()
        {
            EnsureFolder(PlatformAssetLayout.F35Root.Substring(0, PlatformAssetLayout.F35Root.LastIndexOf('/')));
            MoveAssetIfNeeded(LegacyF35Root, PlatformAssetLayout.F35Root);

            EnsureFolder(PlatformAssetLayout.EnvironmentMaterialRoot);
            for (int i = 0; i < RunwayAssets.Length; i++)
            {
                MoveAssetIfNeeded(
                    "Assets/FlightSim/Materials/" + RunwayAssets[i],
                    PlatformAssetLayout.EnvironmentMaterialRoot + "/" + RunwayAssets[i]);
            }

            EnsureFolder(PlatformAssetLayout.ScenePath.Substring(0, PlatformAssetLayout.ScenePath.LastIndexOf('/')));
            MoveAssetIfNeeded(LegacyScenePath, PlatformAssetLayout.ScenePath);

            EnsureDedicatedIonServer();
            ConfigureSceneIonServer();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("FlightSim distribution asset migration completed.");
        }

        public static void ApplyCli()
        {
            Apply();
        }

        private static void EnsureDedicatedIonServer()
        {
            EnsureFolder(PlatformAssetLayout.CesiumServerPath.Substring(0, PlatformAssetLayout.CesiumServerPath.LastIndexOf('/')));
            CesiumIonServer existing = AssetDatabase.LoadAssetAtPath<CesiumIonServer>(PlatformAssetLayout.CesiumServerPath);
            if (existing != null)
                return;

            CesiumIonServer source = AssetDatabase.LoadAssetAtPath<CesiumIonServer>(DefaultIonServerPath);
            if (source == null)
                throw new InvalidOperationException("The current Cesium ion server asset could not be found.");
            if (string.IsNullOrWhiteSpace(source.defaultIonAccessToken))
                throw new InvalidOperationException("The current Cesium ion server has no default access token.");

            CesiumIonServer dedicated = UnityEngine.Object.Instantiate(source);
            dedicated.name = "FlightSimCesiumIonServer";
            AssetDatabase.CreateAsset(dedicated, PlatformAssetLayout.CesiumServerPath);
        }

        private static void ConfigureSceneIonServer()
        {
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(PlatformAssetLayout.ScenePath);
            CesiumIonServer server = AssetDatabase.LoadAssetAtPath<CesiumIonServer>(PlatformAssetLayout.CesiumServerPath);
            if (sceneAsset == null || server == null)
                throw new InvalidOperationException("The sample scene or dedicated Cesium ion server is missing after migration.");

            Scene scene = EditorSceneManager.OpenScene(PlatformAssetLayout.ScenePath, OpenSceneMode.Single);
            Cesium3DTileset[] tilesets = FindComponentsInScene<Cesium3DTileset>(scene);
            for (int i = 0; i < tilesets.Length; i++)
            {
                tilesets[i].ionServer = server;
                EditorUtility.SetDirty(tilesets[i]);
            }

            CesiumIonRasterOverlay[] overlays = FindComponentsInScene<CesiumIonRasterOverlay>(scene);
            for (int i = 0; i < overlays.Length; i++)
            {
                overlays[i].ionServer = server;
                EditorUtility.SetDirty(overlays[i]);
            }

            EditorSceneManager.MarkSceneDirty(scene);
            if (!EditorSceneManager.SaveScene(scene, PlatformAssetLayout.ScenePath))
                throw new InvalidOperationException("The migrated sample scene could not be saved.");
        }

        private static T[] FindComponentsInScene<T>(Scene scene) where T : Component
        {
            var result = new System.Collections.Generic.List<T>();
            foreach (GameObject root in scene.GetRootGameObjects())
                result.AddRange(root.GetComponentsInChildren<T>(true));
            return result.ToArray();
        }

        private static void MoveAssetIfNeeded(string source, string target)
        {
            bool sourceExists = AssetDatabase.LoadMainAssetAtPath(source) != null || AssetDatabase.IsValidFolder(source);
            bool targetExists = AssetDatabase.LoadMainAssetAtPath(target) != null || AssetDatabase.IsValidFolder(target);
            if (!sourceExists && targetExists)
                return;
            if (!sourceExists)
                throw new InvalidOperationException($"Required migration source is missing: {source}");
            if (targetExists)
                throw new InvalidOperationException($"Migration target already exists while source still exists: {target}");

            string error = AssetDatabase.MoveAsset(source, target);
            if (!string.IsNullOrEmpty(error))
                throw new InvalidOperationException($"Failed to move '{source}' to '{target}': {error}");
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
                return;
            int separator = path.LastIndexOf('/');
            if (separator <= 0)
                throw new InvalidOperationException($"Invalid asset folder path: {path}");
            string parent = path.Substring(0, separator);
            string name = path.Substring(separator + 1);
            EnsureFolder(parent);
            if (!AssetDatabase.IsValidFolder(path))
                AssetDatabase.CreateFolder(parent, name);
        }
    }
}
