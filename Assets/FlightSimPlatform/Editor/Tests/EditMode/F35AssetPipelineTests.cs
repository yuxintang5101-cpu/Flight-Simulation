using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Editor.Tests
{
    public sealed class F35AssetPipelineTests
    {
        private const string MaterialFolder = PlatformAssetLayout.F35MaterialRoot;
        private const string PrefabPath = "Assets/FlightSimPlatform/Prefabs/F35_Visual.prefab";
        private const string ForwardCanopyFrameName = "LP_Exterior_Canopy_frame_current_f35_int_1_0";

        [OneTimeSetUp]
        public void PrepareF35Assets()
        {
            F35AssetPipeline.PrepareAssets();
        }

        [Test]
        public void AllF35MaterialsUseBuiltInStandardShader()
        {
            Shader standard = Shader.Find("Standard");
            Assert.That(standard, Is.Not.Null);

            string[] materialGuids = AssetDatabase.FindAssets("t:Material", new[] { MaterialFolder });
            Assert.That(materialGuids.Length, Is.EqualTo(78));

            foreach (string path in materialGuids.Select(AssetDatabase.GUIDToAssetPath))
            {
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                Assert.That(material, Is.Not.Null, path);
                Assert.That(material.shader, Is.SameAs(standard), path);
            }
        }

        [Test]
        public void F35NormalTexturesUseNormalMapImportType()
        {
            string[] normalPaths = AssetDatabase.FindAssets("t:Texture", new[] { PlatformAssetLayout.F35Root })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => path.IndexOf("_NRM", System.StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();
            Assert.That(normalPaths.Length, Is.GreaterThan(0));

            foreach (string path in normalPaths)
            {
                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                Assert.That(importer, Is.Not.Null, path);
                Assert.That(importer.textureType, Is.EqualTo(TextureImporterType.NormalMap), path);
            }
        }

        [Test]
        public void CanopyUsesBuiltInTransparentPremultipliedSetup()
        {
            Material canopy = AssetDatabase.LoadAssetAtPath<Material>(MaterialFolder + "/canopy_glass.mat");
            Assert.That(canopy, Is.Not.Null);
            Assert.That(canopy.shader.name, Is.EqualTo("Standard"));
            Assert.That(canopy.GetFloat("_Mode"), Is.EqualTo(3f));
            Assert.That(canopy.GetFloat("_ZWrite"), Is.EqualTo(0f));
            Assert.That(canopy.IsKeywordEnabled("_ALPHAPREMULTIPLY_ON"), Is.True);
            Assert.That(canopy.renderQueue, Is.EqualTo(3000));
            Assert.That(canopy.color.a, Is.InRange(0.08f, 0.35f));
        }

        [Test]
        public void SourceModelRenderersUseConvertedExternalMaterials()
        {
            Shader standard = Shader.Find("Standard");
            GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(F35AssetPipeline.SourceModelPath);
            Assert.That(model, Is.Not.Null);

            Material[] materials = model.GetComponentsInChildren<Renderer>(true)
                .SelectMany(renderer => renderer.sharedMaterials)
                .Where(material => material != null)
                .Distinct()
                .ToArray();
            Assert.That(materials.Length, Is.GreaterThan(0));
            foreach (Material material in materials)
            {
                string materialPath = AssetDatabase.GetAssetPath(material);
                Assert.That(material.shader, Is.SameAs(standard), material.name);
                Assert.That(materialPath, Does.StartWith(MaterialFolder + "/"), material.name);
            }
        }

        [Test]
        public void F35VisualPrefabProvidesCameraVisibilityRootsAndCockpitEye()
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            Assert.That(prefab, Is.Not.Null);
            Assert.That(prefab.name, Is.EqualTo("F35_Visual"));

            Transform exterior = FindDeepChild(prefab.transform, "F35_Exterior");
            Transform cockpit = FindDeepChild(prefab.transform, "F35_Cockpit");
            Transform cockpitEye = FindDeepChild(prefab.transform, "CockpitEye");
            Transform hudMount = FindDeepChild(prefab.transform, "HudCombinerMount");

            Assert.That(exterior, Is.Not.Null);
            Assert.That(cockpit, Is.Not.Null);
            Assert.That(cockpitEye, Is.Not.Null);
            Assert.That(hudMount, Is.Not.Null);
            Assert.That(exterior.gameObject.activeSelf, Is.True);
            Assert.That(cockpit.gameObject.activeSelf, Is.False);
            Renderer[] exteriorRenderers = exterior.GetComponentsInChildren<Renderer>(true);
            Renderer[] cockpitRenderers = cockpit.GetComponentsInChildren<Renderer>(true);
            Assert.That(exteriorRenderers.Length, Is.GreaterThan(0));
            Assert.That(cockpitRenderers.Length, Is.GreaterThan(0));
            Assert.That(exteriorRenderers.Count(renderer => renderer.enabled), Is.EqualTo(exteriorRenderers.Length));
            Assert.That(prefab.GetComponentsInChildren<Camera>(true), Is.Empty);
            Assert.That(prefab.GetComponentsInChildren<AudioListener>(true), Is.Empty);
            Assert.That(prefab.GetComponentsInChildren<Light>(true), Is.Empty);

            GameObject instance = Object.Instantiate(prefab);
            try
            {
                Transform instanceExterior = FindDeepChild(instance.transform, "F35_Exterior");
                Bounds bounds = CalculateBounds(instanceExterior.GetComponentsInChildren<Renderer>(true));
                float length = Mathf.Max(bounds.size.x, bounds.size.z);
                Assert.That(length, Is.InRange(15.0f, 16.3f));

                Transform instanceEye = FindDeepChild(instance.transform, "CockpitEye");
                Vector3 eye = instanceEye.position;
                Assert.That(eye.x, Is.InRange(bounds.min.x, bounds.max.x));
                Assert.That(eye.y, Is.InRange(bounds.min.y, bounds.max.y));
                Assert.That(eye.z, Is.InRange(bounds.min.z, bounds.max.z));
            }
            finally
            {
                Object.DestroyImmediate(instance);
            }
        }

        [Test]
        public void CockpitHidesOnlyForwardCanopyFrameWhileExteriorRemainsComplete()
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            Transform exterior = FindDeepChild(prefab.transform, "F35_Exterior");
            Transform cockpit = FindDeepChild(prefab.transform, "F35_Cockpit");
            Renderer exteriorFrame = FindRenderer(exterior, ForwardCanopyFrameName);
            Renderer cockpitFrame = FindRenderer(cockpit, ForwardCanopyFrameName);

            Assert.That(exteriorFrame, Is.Not.Null);
            Assert.That(cockpitFrame, Is.Not.Null);
            Assert.That(exteriorFrame.enabled, Is.True);
            Assert.That(cockpitFrame.enabled, Is.False);
            Assert.That(cockpit.GetComponentsInChildren<Renderer>(true).Count(renderer => !renderer.enabled), Is.EqualTo(1));
        }

        private static Bounds CalculateBounds(Renderer[] renderers)
        {
            Assert.That(renderers.Length, Is.GreaterThan(0));
            Bounds bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
                bounds.Encapsulate(renderers[i].bounds);
            return bounds;
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
