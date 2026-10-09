using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CesiumForUnity;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Editor.Tests
{
    public sealed class DistributionAssetLayoutTests
    {
        [Test]
        public void CanonicalRuntimeAssetsExistBelowPlatformRoot()
        {
            Assert.That(AssetDatabase.IsValidFolder(PlatformAssetLayout.F35Root), Is.True);
            Assert.That(AssetDatabase.LoadAssetAtPath<SceneAsset>(PlatformAssetLayout.ScenePath), Is.Not.Null);
            Assert.That(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(PlatformAssetLayout.CesiumServerPath), Is.Not.Null);
            Assert.That(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("Assets/FlightSimPlatform/Prefabs/F16C_Prototype_Visual.prefab"), Is.Null);
        }

        [Test]
        public void DedicatedTokenIsNotDuplicatedIntoReadableDeliveryFiles()
        {
            CesiumIonServer server = AssetDatabase.LoadAssetAtPath<CesiumIonServer>(PlatformAssetLayout.CesiumServerPath);
            Assert.That(server, Is.Not.Null);
            Assert.That(server.defaultIonAccessToken, Is.Not.Empty);

            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string serverFullPath = Path.GetFullPath(Path.Combine(projectRoot, PlatformAssetLayout.CesiumServerPath));
            string[] roots =
            {
                Path.Combine(projectRoot, "Distribution"),
                Path.Combine(projectRoot, "Tools"),
                Path.Combine(projectRoot, "docs"),
                Path.Combine(projectRoot, "Assets", "FlightSimPlatform")
            };
            var readableExtensions = new HashSet<string>(new[]
            {
                ".cs", ".md", ".json", ".csv", ".ps1", ".txt", ".xml", ".unity", ".prefab", ".mat", ".asset", ".asmdef"
            }, StringComparer.OrdinalIgnoreCase);

            string[] leaks = roots.Where(Directory.Exists)
                .SelectMany(root => Directory.GetFiles(root, "*", SearchOption.AllDirectories))
                .Where(path => !string.Equals(Path.GetFullPath(path), serverFullPath, StringComparison.OrdinalIgnoreCase))
                .Where(path => readableExtensions.Contains(Path.GetExtension(path)))
                .Where(path => File.ReadAllText(path).Contains(server.defaultIonAccessToken))
                .Select(path => path.Substring(projectRoot.Length + 1).Replace('\\', '/'))
                .ToArray();

            Assert.That(leaks, Is.Empty, string.Join("\n", leaks));
        }

        [Test]
        public void SampleSceneHasNoForbiddenProjectAssetDependencies()
        {
            string[] dependencies = AssetDatabase.GetDependencies(PlatformAssetLayout.ScenePath, true);
            string[] forbidden = dependencies
                .Where(path => path.StartsWith("Assets/", StringComparison.Ordinal))
                .Where(path => !PlatformAssetLayout.IsAllowedRuntimeAsset(path))
                .OrderBy(path => path, StringComparer.Ordinal)
                .ToArray();

            Assert.That(forbidden, Is.Empty, string.Join("\n", forbidden));
            Assert.That(dependencies.Any(path => path.StartsWith(PlatformAssetLayout.F35Root, StringComparison.Ordinal)), Is.True);
            Assert.That(dependencies.Any(path => path == PlatformAssetLayout.CesiumServerPath), Is.True);
        }

        [Test]
        public void F35MaterialsUseValidBuiltInShaders()
        {
            string[] materialPaths = AssetDatabase.FindAssets("t:Material", new[] { PlatformAssetLayout.F35MaterialRoot })
                .Select(AssetDatabase.GUIDToAssetPath)
                .ToArray();

            Assert.That(materialPaths.Length, Is.GreaterThan(0));
            foreach (string path in materialPaths)
            {
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                Assert.That(material, Is.Not.Null, path);
                Assert.That(material.shader, Is.Not.Null, path);
                Assert.That(material.shader.name, Is.Not.EqualTo("Hidden/InternalErrorShader"), path);
            }
        }

        [Test]
        public void ForbiddenRootsAreRejected()
        {
            Assert.That(PlatformAssetLayout.IsAllowedRuntimeAsset("Assets/External/F16C/model.fbx"), Is.False);
            Assert.That(PlatformAssetLayout.IsAllowedRuntimeAsset("Assets/External/F35/model.fbx"), Is.False);
            Assert.That(PlatformAssetLayout.IsAllowedRuntimeAsset("Assets/Scenes/FlightSim_KTEX_V2.unity"), Is.False);
            Assert.That(PlatformAssetLayout.IsAllowedRuntimeAsset("Assets/FlightSimPlatform/Core/FuelModel.cs"), Is.True);
        }
    }
}
