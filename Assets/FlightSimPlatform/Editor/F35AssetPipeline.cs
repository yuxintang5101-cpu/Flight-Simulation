using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace FlightSim.Platform.Editor
{
    public static class F35AssetPipeline
    {
        public const string SourceModelPath = PlatformAssetLayout.F35Root + "/F35战机_FBX.fbx";
        public const string MaterialFolder = PlatformAssetLayout.F35MaterialRoot;
        public const string VisualPrefabPath = "Assets/FlightSimPlatform/Prefabs/F35_Visual.prefab";

        private const float F35LengthMeters = 15.67f;
        private const string ForwardCanopyFrameName = "LP_Exterior_Canopy_frame_current_f35_int_1_0";

        [MenuItem("FlightSim/Platform/Prepare F-35 Visual")]
        public static void PrepareAssets()
        {
            GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(SourceModelPath);
            if (model == null)
                throw new InvalidOperationException($"F-35 source model was not found at '{SourceModelPath}'.");

            ConvertMaterialsToBuiltIn();
            ConfigureNormalMaps();
            RemapModelMaterials();
            CreateVisualPrefab();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"Prepared Built-in F-35 materials and visual prefab at {VisualPrefabPath}.");
        }

        private static void ConvertMaterialsToBuiltIn()
        {
            Shader standard = Shader.Find("Standard");
            if (standard == null)
                throw new InvalidOperationException("The Built-in Standard shader is unavailable.");

            string[] materialPaths = AssetDatabase.FindAssets("t:Material", new[] { MaterialFolder })
                .Select(AssetDatabase.GUIDToAssetPath)
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToArray();
            if (materialPaths.Length == 0)
                throw new InvalidOperationException($"No F-35 materials were found in '{MaterialFolder}'.");

            for (int i = 0; i < materialPaths.Length; i++)
            {
                Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPaths[i]);
                if (material == null)
                    continue;

                SavedMaterialProperties saved = SavedMaterialProperties.Read(material);
                material.shader = standard;

                Texture baseMap = saved.GetTexture("_BaseMap") ?? saved.GetTexture("_MainTex");
                Texture normalMap = saved.GetTexture("_BumpMap");
                Texture emissionMap = saved.GetTexture("_EmissionMap");
                Color baseColor = saved.GetColor("_BaseColor", saved.GetColor("_Color", Color.white));
                Color emissionColor = saved.GetColor("_EmissionColor", Color.black);

                material.SetTexture("_MainTex", baseMap);
                material.SetTextureScale("_MainTex", saved.GetTextureScale("_BaseMap", Vector2.one));
                material.SetTextureOffset("_MainTex", saved.GetTextureOffset("_BaseMap", Vector2.zero));
                material.SetColor("_Color", baseColor);
                material.SetFloat("_Metallic", Mathf.Clamp01(saved.GetFloat("_Metallic", 0f)));
                material.SetFloat("_Glossiness", Mathf.Clamp01(saved.GetFloat("_Smoothness", saved.GetFloat("_Glossiness", 0.5f))));
                material.SetTexture("_BumpMap", normalMap);
                material.SetFloat("_BumpScale", saved.GetFloat("_BumpScale", 1f));
                material.SetTexture("_EmissionMap", emissionMap);
                material.SetColor("_EmissionColor", emissionColor);

                SetKeyword(material, "_NORMALMAP", normalMap != null);
                bool hasEmission = emissionMap != null || emissionColor.maxColorComponent > 0.001f;
                SetKeyword(material, "_EMISSION", hasEmission);
                material.globalIlluminationFlags = hasEmission
                    ? MaterialGlobalIlluminationFlags.BakedEmissive
                    : MaterialGlobalIlluminationFlags.EmissiveIsBlack;

                if (IsCanopyMaterial(material.name))
                    ConfigureTransparentCanopy(material, baseColor);
                else
                    ConfigureOpaqueMaterial(material);

                EditorUtility.SetDirty(material);
            }
        }

        private static void ConfigureNormalMaps()
        {
            string[] texturePaths = AssetDatabase.FindAssets("t:Texture", new[] { PlatformAssetLayout.F35Root })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => Path.GetFileNameWithoutExtension(path)
                    .IndexOf("_NRM", StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();

            for (int i = 0; i < texturePaths.Length; i++)
            {
                TextureImporter importer = AssetImporter.GetAtPath(texturePaths[i]) as TextureImporter;
                if (importer == null || importer.textureType == TextureImporterType.NormalMap)
                    continue;

                importer.textureType = TextureImporterType.NormalMap;
                importer.SaveAndReimport();
            }
        }

        private static void RemapModelMaterials()
        {
            ModelImporter importer = AssetImporter.GetAtPath(SourceModelPath) as ModelImporter;
            if (importer == null)
                throw new InvalidOperationException($"A ModelImporter was not found for '{SourceModelPath}'.");

            importer.materialImportMode = ModelImporterMaterialImportMode.ImportStandard;
            importer.materialName = ModelImporterMaterialName.BasedOnMaterialName;
            importer.materialSearch = ModelImporterMaterialSearch.Local;

            string[] materialPaths = AssetDatabase.FindAssets("t:Material", new[] { MaterialFolder })
                .Select(AssetDatabase.GUIDToAssetPath)
                .ToArray();
            for (int i = 0; i < materialPaths.Length; i++)
            {
                Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPaths[i]);
                if (material == null)
                    continue;

                var identifier = new AssetImporter.SourceAssetIdentifier(typeof(Material), material.name);
                importer.AddRemap(identifier, material);
            }

            importer.SaveAndReimport();
        }

        private static void CreateVisualPrefab()
        {
            EnsureAssetFolder("Assets/FlightSimPlatform", "Prefabs");
            GameObject modelAsset = AssetDatabase.LoadAssetAtPath<GameObject>(SourceModelPath);
            if (modelAsset == null)
                throw new InvalidOperationException($"Unable to load '{SourceModelPath}' after material remapping.");

            GameObject root = new GameObject("F35_Visual");
            try
            {
                GameObject exteriorRoot = CreateChild(root.transform, "F35_Exterior");
                GameObject cockpitRoot = CreateChild(root.transform, "F35_Cockpit");
                GameObject exteriorModel = InstantiateModel(modelAsset, exteriorRoot.transform);
                GameObject cockpitModel = InstantiateModel(modelAsset, cockpitRoot.transform);

                NormalizeModel(exteriorModel, cockpitModel, root.transform);
                EnableAllRenderers(exteriorModel);
                EnableAllRenderers(cockpitModel);
                HideForwardCanopyFrame(cockpitModel);

                Bounds cockpitBounds = CalculateLocalBounds(
                    root.transform,
                    cockpitModel.GetComponentsInChildren<Renderer>(true)
                        .Where(IsCockpitRenderer)
                        .ToArray());
                if (cockpitBounds.size.sqrMagnitude < 0.001f)
                    cockpitBounds = CalculateLocalBounds(root.transform, cockpitModel.GetComponentsInChildren<Renderer>(true));

                Vector3 eyePosition = cockpitBounds.center;
                eyePosition.y = Mathf.Lerp(cockpitBounds.min.y, cockpitBounds.max.y, 0.72f);
                eyePosition.z = Mathf.Lerp(cockpitBounds.min.z, cockpitBounds.max.z, 0.40f);
                CreateMarker(root.transform, "CockpitEye", eyePosition, Vector3.zero);
                CreateMarker(root.transform, "HudCombinerMount", eyePosition + new Vector3(0f, 0.06f, 0.75f), Vector3.zero);

                exteriorRoot.SetActive(true);
                cockpitRoot.SetActive(false);
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(root, VisualPrefabPath);
                if (prefab == null)
                    throw new InvalidOperationException($"Unable to save '{VisualPrefabPath}'.");
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(root);
            }
        }

        private static void NormalizeModel(GameObject exteriorModel, GameObject cockpitModel, Transform visualRoot)
        {
            Renderer[] allRenderers = exteriorModel.GetComponentsInChildren<Renderer>(true);
            Renderer[] cockpitRenderers = allRenderers.Where(IsCockpitRenderer).ToArray();
            Bounds allBounds = CalculateLocalBounds(visualRoot, allRenderers);
            Bounds cockpitBounds = CalculateLocalBounds(visualRoot, cockpitRenderers);
            if (allBounds.size.sqrMagnitude < 0.001f)
                throw new InvalidOperationException("The F-35 model contains no usable renderer bounds.");

            bool longitudinalIsX = allBounds.size.x > allBounds.size.z;
            float longitudinalLength = longitudinalIsX ? allBounds.size.x : allBounds.size.z;
            Vector3 cockpitCenter = cockpitBounds.size.sqrMagnitude > 0.001f ? cockpitBounds.center : allBounds.center;
            float yaw;
            if (longitudinalIsX)
                yaw = cockpitCenter.x >= allBounds.center.x ? -90f : 90f;
            else
                yaw = cockpitCenter.z >= allBounds.center.z ? 0f : 180f;

            float scale = F35LengthMeters / Mathf.Max(0.001f, longitudinalLength);
            Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);
            ApplyNormalizedTransform(exteriorModel.transform, rotation, scale);
            ApplyNormalizedTransform(cockpitModel.transform, rotation, scale);

            Bounds normalizedBounds = CalculateLocalBounds(visualRoot, exteriorModel.GetComponentsInChildren<Renderer>(true));
            Vector3 offset = new Vector3(-normalizedBounds.center.x, -normalizedBounds.min.y, -normalizedBounds.center.z);
            exteriorModel.transform.localPosition += offset;
            cockpitModel.transform.localPosition += offset;
        }

        private static void ApplyNormalizedTransform(Transform model, Quaternion rotation, float scale)
        {
            model.localPosition = Vector3.zero;
            model.localRotation = rotation;
            model.localScale = Vector3.one * scale;
        }

        private static void EnableAllRenderers(GameObject model)
        {
            Renderer[] renderers = model.GetComponentsInChildren<Renderer>(true);
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = true;
        }

        private static void HideForwardCanopyFrame(GameObject cockpitModel)
        {
            Renderer frame = cockpitModel.GetComponentsInChildren<Renderer>(true)
                .SingleOrDefault(renderer => string.Equals(
                    renderer.name,
                    ForwardCanopyFrameName,
                    StringComparison.Ordinal));
            if (frame == null)
            {
                throw new InvalidOperationException(
                    $"The cockpit renderer '{ForwardCanopyFrameName}' was not found in the F-35 source model.");
            }

            frame.enabled = false;
        }

        private static bool IsCockpitRenderer(Renderer renderer)
        {
            Material[] materials = renderer.sharedMaterials;
            for (int i = 0; i < materials.Length; i++)
            {
                Material material = materials[i];
                if (material != null && IsCockpitMaterial(material.name))
                    return true;
            }

            string rendererName = renderer.name.ToLowerInvariant();
            return rendererName.Contains("cockpit") || rendererName.Contains("interior") || rendererName.Contains("canopy");
        }

        private static bool IsCockpitMaterial(string name)
        {
            string lower = name.ToLowerInvariant();
            return lower.StartsWith("f35_int_") ||
                   lower.StartsWith("f35_mfd") ||
                   lower.StartsWith("f35_pfd") ||
                   IsCanopyMaterial(lower);
        }

        private static bool IsCanopyMaterial(string name)
        {
            return name.IndexOf("canopy", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("glass", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void ConfigureOpaqueMaterial(Material material)
        {
            material.SetOverrideTag("RenderType", "Opaque");
            material.SetFloat("_Mode", 0f);
            material.SetInt("_SrcBlend", (int)BlendMode.One);
            material.SetInt("_DstBlend", (int)BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            SetKeyword(material, "_ALPHATEST_ON", false);
            SetKeyword(material, "_ALPHABLEND_ON", false);
            SetKeyword(material, "_ALPHAPREMULTIPLY_ON", false);
            material.renderQueue = -1;
        }

        private static void ConfigureTransparentCanopy(Material material, Color originalColor)
        {
            originalColor.a = Mathf.Clamp(originalColor.a <= 0.05f ? 0.18f : originalColor.a, 0.08f, 0.35f);
            material.SetColor("_Color", originalColor);
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetFloat("_Mode", 3f);
            material.SetInt("_SrcBlend", (int)BlendMode.One);
            material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            SetKeyword(material, "_ALPHATEST_ON", false);
            SetKeyword(material, "_ALPHABLEND_ON", false);
            SetKeyword(material, "_ALPHAPREMULTIPLY_ON", true);
            material.renderQueue = (int)RenderQueue.Transparent;
        }

        private static void SetKeyword(Material material, string keyword, bool enabled)
        {
            if (enabled)
                material.EnableKeyword(keyword);
            else
                material.DisableKeyword(keyword);
        }

        private static Bounds CalculateLocalBounds(Transform root, Renderer[] renderers)
        {
            Bounds bounds = default;
            bool initialized = false;
            for (int i = 0; i < renderers.Length; i++)
            {
                Bounds world = renderers[i].bounds;
                Vector3 min = world.min;
                Vector3 max = world.max;
                for (int corner = 0; corner < 8; corner++)
                {
                    Vector3 worldCorner = new Vector3(
                        (corner & 1) == 0 ? min.x : max.x,
                        (corner & 2) == 0 ? min.y : max.y,
                        (corner & 4) == 0 ? min.z : max.z);
                    Vector3 localCorner = root.InverseTransformPoint(worldCorner);
                    if (!initialized)
                    {
                        bounds = new Bounds(localCorner, Vector3.zero);
                        initialized = true;
                    }
                    else
                    {
                        bounds.Encapsulate(localCorner);
                    }
                }
            }

            return bounds;
        }

        private static GameObject InstantiateModel(GameObject modelAsset, Transform parent)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(modelAsset, parent);
            instance.name = "F35_Model";
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;
            return instance;
        }

        private static GameObject CreateChild(Transform parent, string name)
        {
            GameObject child = new GameObject(name);
            child.transform.SetParent(parent, false);
            return child;
        }

        private static void CreateMarker(Transform parent, string name, Vector3 position, Vector3 euler)
        {
            GameObject marker = new GameObject(name);
            marker.transform.SetParent(parent, false);
            marker.transform.localPosition = position;
            marker.transform.localRotation = Quaternion.Euler(euler);
        }

        private static void EnsureAssetFolder(string parent, string name)
        {
            string path = parent + "/" + name;
            if (!AssetDatabase.IsValidFolder(path))
                AssetDatabase.CreateFolder(parent, name);
        }

        private sealed class SavedMaterialProperties
        {
            private readonly Dictionary<string, TextureValue> textures = new Dictionary<string, TextureValue>();
            private readonly Dictionary<string, Color> colors = new Dictionary<string, Color>();
            private readonly Dictionary<string, float> floats = new Dictionary<string, float>();

            public static SavedMaterialProperties Read(Material material)
            {
                var result = new SavedMaterialProperties();
                SerializedObject serialized = new SerializedObject(material);
                SerializedProperty saved = serialized.FindProperty("m_SavedProperties");
                result.ReadTextures(saved.FindPropertyRelative("m_TexEnvs"));
                result.ReadColors(saved.FindPropertyRelative("m_Colors"));
                result.ReadFloats(saved.FindPropertyRelative("m_Floats"));
                SerializedProperty ints = saved.FindPropertyRelative("m_Ints");
                if (ints != null)
                    result.ReadFloats(ints);
                return result;
            }

            public Texture GetTexture(string name)
            {
                return textures.TryGetValue(name, out TextureValue value) ? value.Texture : null;
            }

            public Vector2 GetTextureScale(string name, Vector2 fallback)
            {
                return textures.TryGetValue(name, out TextureValue value) ? value.Scale : fallback;
            }

            public Vector2 GetTextureOffset(string name, Vector2 fallback)
            {
                return textures.TryGetValue(name, out TextureValue value) ? value.Offset : fallback;
            }

            public Color GetColor(string name, Color fallback)
            {
                return colors.TryGetValue(name, out Color value) ? value : fallback;
            }

            public float GetFloat(string name, float fallback)
            {
                return floats.TryGetValue(name, out float value) ? value : fallback;
            }

            private void ReadTextures(SerializedProperty array)
            {
                if (array == null)
                    return;
                for (int i = 0; i < array.arraySize; i++)
                {
                    SerializedProperty entry = array.GetArrayElementAtIndex(i);
                    string name = entry.FindPropertyRelative("first").stringValue;
                    SerializedProperty value = entry.FindPropertyRelative("second");
                    textures[name] = new TextureValue(
                        value.FindPropertyRelative("m_Texture").objectReferenceValue as Texture,
                        value.FindPropertyRelative("m_Scale").vector2Value,
                        value.FindPropertyRelative("m_Offset").vector2Value);
                }
            }

            private void ReadColors(SerializedProperty array)
            {
                if (array == null)
                    return;
                for (int i = 0; i < array.arraySize; i++)
                {
                    SerializedProperty entry = array.GetArrayElementAtIndex(i);
                    colors[entry.FindPropertyRelative("first").stringValue] =
                        entry.FindPropertyRelative("second").colorValue;
                }
            }

            private void ReadFloats(SerializedProperty array)
            {
                if (array == null)
                    return;
                for (int i = 0; i < array.arraySize; i++)
                {
                    SerializedProperty entry = array.GetArrayElementAtIndex(i);
                    SerializedProperty value = entry.FindPropertyRelative("second");
                    floats[entry.FindPropertyRelative("first").stringValue] =
                        value.propertyType == SerializedPropertyType.Integer ? value.intValue : value.floatValue;
                }
            }

            private readonly struct TextureValue
            {
                public TextureValue(Texture texture, Vector2 scale, Vector2 offset)
                {
                    Texture = texture;
                    Scale = scale;
                    Offset = offset;
                }

                public Texture Texture { get; }
                public Vector2 Scale { get; }
                public Vector2 Offset { get; }
            }
        }
    }
}
