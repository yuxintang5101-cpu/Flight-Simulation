using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CesiumForUnity;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Editor
{
    public static class FlightSimDistributionBuilder
    {
        public const string DistributionVersion = "2.0.0";
        public const string RequiredUnityVersion = "2022.3.62f2c1";
        public const string RequiredCesiumVersion = "1.24.0";
        public const string DistributionDirectoryName = "FlightSim-Platform-v" + DistributionVersion;

        private const string BootstrapRoot = "Assets/FlightSimBootstrap";
        private const string SourceDistributionRoot = "Distribution";
        private const string ChecksumsFileName = "Checksums-SHA256.txt";

        [MenuItem("FlightSim/Distribution/Build v2.0.0")]
        public static void BuildFromMenu()
        {
            string projectRoot = GetProjectRoot();
            Build(Path.Combine(projectRoot, "Artifacts", "Distribution"));
        }

        public static void BuildCli()
        {
            string output = ReadCommandLineValue("-distributionOutput");
            if (string.IsNullOrWhiteSpace(output))
                output = Path.Combine(GetProjectRoot(), "Artifacts", "Distribution");
            else if (!Path.IsPathRooted(output))
                output = Path.Combine(GetProjectRoot(), output);
            Build(output);
        }

        public static string Build(string outputRoot)
        {
            ValidatePreflight();
            string projectRoot = GetProjectRoot();
            string fullOutputRoot = Path.GetFullPath(outputRoot);
            string finalDirectory = Path.Combine(fullOutputRoot, DistributionDirectoryName);
            string stagingDirectory = Path.Combine(fullOutputRoot, ".staging-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(fullOutputRoot);
            if (Directory.Exists(finalDirectory))
                throw new InvalidOperationException($"Distribution output already exists and is immutable: {finalDirectory}");

            try
            {
                Directory.CreateDirectory(stagingDirectory);
                string[] runtimeAssets = GetPlatformRuntimeAssetPaths();
                string[] bootstrapAssets = GetBootstrapProductionAssetPaths();
                string[] testAssets = GetDeveloperTestAssetPaths();
                ValidateDependencies(runtimeAssets);

                AssetDatabase.ExportPackage(
                    bootstrapAssets,
                    Path.Combine(stagingDirectory, $"01-FlightSim-Bootstrap-v{DistributionVersion}.unitypackage"),
                    ExportPackageOptions.Default);
                AssetDatabase.ExportPackage(
                    runtimeAssets,
                    Path.Combine(stagingDirectory, $"02-FlightSim-Platform-With-F35-v{DistributionVersion}.unitypackage"),
                    ExportPackageOptions.IncludeDependencies);
                AssetDatabase.ExportPackage(
                    testAssets,
                    Path.Combine(stagingDirectory, $"03-FlightSim-DeveloperTests-v{DistributionVersion}.unitypackage"),
                    ExportPackageOptions.Default);

                DataDictionaryGenerator.WriteMarkdownAndCsv(Path.Combine(projectRoot, SourceDistributionRoot, "Docs-zh-CN"));
                CopyDeliverySources(projectRoot, stagingDirectory);
                WriteVersionFile(stagingDirectory);
                // This manifest is consumed while importing package 02. It must describe
                // exactly that package, otherwise optional test assets would be reported as
                // missing before package 03 is imported.
                WriteAssetManifest(projectRoot, stagingDirectory, runtimeAssets);
                WriteLicenseNotice(stagingDirectory);
                File.WriteAllText(
                    Path.Combine(stagingDirectory, ChecksumsFileName),
                    DistributionFileUtility.BuildChecksumText(stagingDirectory, ChecksumsFileName),
                    new System.Text.UTF8Encoding(false));

                SelfVerify(stagingDirectory);
                Directory.Move(stagingDirectory, finalDirectory);
                Debug.Log($"FlightSim distribution created at {finalDirectory}");
                return finalDirectory;
            }
            catch
            {
                if (Directory.Exists(stagingDirectory))
                    Directory.Delete(stagingDirectory, true);
                throw;
            }
        }

        public static string[] FindForbiddenPaths(IEnumerable<string> paths)
        {
            if (paths == null)
                return Array.Empty<string>();
            return paths
                .Where(path => PlatformAssetLayout.ForbiddenRoots.Any(root =>
                    path.StartsWith(root, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(path => path, StringComparer.Ordinal)
                .ToArray();
        }

        private static void ValidatePreflight()
        {
            if (!string.Equals(Application.unityVersion, RequiredUnityVersion, StringComparison.Ordinal))
                throw new InvalidOperationException($"Unity {RequiredUnityVersion} is required; current version is {Application.unityVersion}.");
            if (!File.Exists(Path.Combine(GetProjectRoot(), "Packages", "manifest.json")))
                throw new InvalidOperationException("Packages/manifest.json is missing.");
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(PlatformAssetLayout.ScenePath) == null)
                throw new InvalidOperationException($"Sample scene is missing: {PlatformAssetLayout.ScenePath}");
            if (AssetDatabase.LoadAssetAtPath<CesiumIonServer>(PlatformAssetLayout.CesiumServerPath) == null)
                throw new InvalidOperationException($"Dedicated Cesium server is missing: {PlatformAssetLayout.CesiumServerPath}");
            if (!AssetDatabase.IsValidFolder(BootstrapRoot))
                throw new InvalidOperationException("Bootstrap assets are missing.");

            string manifest = File.ReadAllText(Path.Combine(GetProjectRoot(), "Packages", "manifest.json"));
            if (!manifest.Contains("\"com.cesium.unity\": \"" + RequiredCesiumVersion + "\""))
                throw new InvalidOperationException($"Cesium for Unity must be pinned to {RequiredCesiumVersion}.");
        }

        private static string[] GetPlatformRuntimeAssetPaths()
        {
            return EnumerateAssetFiles(PlatformAssetLayout.PlatformRoot)
                .Where(path => !IsTestAsset(path))
                .OrderBy(path => path, StringComparer.Ordinal)
                .ToArray();
        }

        private static string[] GetBootstrapProductionAssetPaths()
        {
            return EnumerateAssetFiles(BootstrapRoot)
                .Where(path => !IsTestAsset(path))
                .OrderBy(path => path, StringComparer.Ordinal)
                .ToArray();
        }

        private static string[] GetDeveloperTestAssetPaths()
        {
            return EnumerateAssetFiles(PlatformAssetLayout.PlatformRoot)
                .Concat(EnumerateAssetFiles(BootstrapRoot))
                .Where(IsTestAsset)
                .Distinct(StringComparer.Ordinal)
                .OrderBy(path => path, StringComparer.Ordinal)
                .ToArray();
        }

        private static IEnumerable<string> EnumerateAssetFiles(string assetRoot)
        {
            string fullRoot = Path.Combine(GetProjectRoot(), assetRoot.Replace('/', Path.DirectorySeparatorChar));
            if (!Directory.Exists(fullRoot))
                yield break;
            foreach (string file in Directory.GetFiles(fullRoot, "*", SearchOption.AllDirectories))
            {
                if (file.EndsWith(".meta", StringComparison.OrdinalIgnoreCase))
                    continue;
                string relative = file.Substring(GetProjectRoot().Length + 1).Replace('\\', '/');
                yield return relative;
            }
        }

        private static bool IsTestAsset(string path)
        {
            return path.IndexOf("/Tests/", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   path.EndsWith("Tests.asmdef", StringComparison.OrdinalIgnoreCase);
        }

        private static void ValidateDependencies(string[] runtimeAssets)
        {
            var invalid = new SortedSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < runtimeAssets.Length; i++)
            {
                string[] dependencies = AssetDatabase.GetDependencies(runtimeAssets[i], true);
                for (int j = 0; j < dependencies.Length; j++)
                {
                    string dependency = dependencies[j];
                    if (dependency.StartsWith("Assets/", StringComparison.Ordinal) &&
                        !PlatformAssetLayout.IsAllowedRuntimeAsset(dependency))
                    {
                        invalid.Add(dependency);
                    }
                }
            }

            string[] forbidden = FindForbiddenPaths(runtimeAssets.Concat(invalid));
            if (invalid.Count > 0 || forbidden.Length > 0)
            {
                throw new InvalidOperationException(
                    "Distribution dependency scan rejected these paths:\n" +
                    string.Join("\n", invalid.Concat(forbidden).Distinct().OrderBy(path => path, StringComparer.Ordinal)));
            }
        }

        private static void CopyDeliverySources(string projectRoot, string stagingDirectory)
        {
            string sourceRoot = Path.Combine(projectRoot, SourceDistributionRoot);
            string[] rootFiles = { "Install-FlightSim.ps1", "Verify-FlightSim.ps1" };
            for (int i = 0; i < rootFiles.Length; i++)
            {
                string source = Path.Combine(sourceRoot, rootFiles[i]);
                if (!File.Exists(source))
                    throw new FileNotFoundException("Distribution script is missing.", source);
                File.Copy(source, Path.Combine(stagingDirectory, rootFiles[i]));
            }

            DistributionFileUtility.CopyDirectory(Path.Combine(sourceRoot, "Docs-zh-CN"), Path.Combine(stagingDirectory, "Docs-zh-CN"));
            DistributionFileUtility.CopyDirectory(Path.Combine(sourceRoot, "Examples"), Path.Combine(stagingDirectory, "Examples"));
        }

        private static void WriteVersionFile(string stagingDirectory)
        {
            CesiumIonServer server = AssetDatabase.LoadAssetAtPath<CesiumIonServer>(PlatformAssetLayout.CesiumServerPath);
            if (server == null || string.IsNullOrWhiteSpace(server.defaultIonAccessToken))
                throw new InvalidOperationException("Dedicated Cesium token is empty.");
            var info = new DistributionVersionInfo
            {
                TokenSha256Prefix = DistributionFileUtility.ComputeTokenFingerprint(server.defaultIonAccessToken)
            };
            File.WriteAllText(
                Path.Combine(stagingDirectory, "VERSION.json"),
                JsonUtility.ToJson(info, true) + "\n",
                new System.Text.UTF8Encoding(false));
        }

        private static void WriteAssetManifest(string projectRoot, string stagingDirectory, IEnumerable<string> assetPaths)
        {
            string[] fullPaths = assetPaths
                .Distinct(StringComparer.Ordinal)
                .Select(path => Path.Combine(projectRoot, path.Replace('/', Path.DirectorySeparatorChar)))
                .ToArray();
            AssetManifest manifest = AssetManifestBuilder.BuildFileManifest(
                projectRoot,
                fullPaths,
                relative => AssetDatabase.AssetPathToGUID(relative));
            File.WriteAllText(
                Path.Combine(stagingDirectory, "AssetManifest.json"),
                JsonUtility.ToJson(manifest, true) + "\n",
                new System.Text.UTF8Encoding(false));
        }

        private static void WriteLicenseNotice(string stagingDirectory)
        {
            string licenses = Path.Combine(stagingDirectory, "LICENSES");
            Directory.CreateDirectory(licenses);
            string text =
                "# 授权说明\n\n" +
                "F-35 视觉资源按交付方确认的再分发授权随本包提供。接收方应保留其内部授权记录。\n\n" +
                "Cesium for Unity 由 Cesium GS 提供，其软件许可与在线服务条款由 Cesium 官方文件约束。\n\n" +
                "本包不包含 Assets/External/F16C 及其 GPL 资源。\n";
            File.WriteAllText(Path.Combine(licenses, "README-zh-CN.md"), text, new System.Text.UTF8Encoding(false));
        }

        private static void SelfVerify(string stagingDirectory)
        {
            string[] requiredFiles =
            {
                $"01-FlightSim-Bootstrap-v{DistributionVersion}.unitypackage",
                $"02-FlightSim-Platform-With-F35-v{DistributionVersion}.unitypackage",
                $"03-FlightSim-DeveloperTests-v{DistributionVersion}.unitypackage",
                "Install-FlightSim.ps1",
                "Verify-FlightSim.ps1",
                "VERSION.json",
                "AssetManifest.json",
                ChecksumsFileName
            };
            for (int i = 0; i < requiredFiles.Length; i++)
            {
                string path = Path.Combine(stagingDirectory, requiredFiles[i]);
                if (!File.Exists(path) || new FileInfo(path).Length == 0)
                    throw new InvalidOperationException($"Distribution self-verification failed: {requiredFiles[i]}");
            }

            string manifestText = File.ReadAllText(Path.Combine(stagingDirectory, "AssetManifest.json"));
            string[] forbidden = FindForbiddenPaths(PlatformAssetLayout.ForbiddenRoots.Where(manifestText.Contains));
            if (forbidden.Length > 0 || manifestText.Contains("defaultIonAccessToken"))
                throw new InvalidOperationException("Distribution manifest contains a forbidden path or token field.");
        }

        private static string GetProjectRoot()
        {
            return Directory.GetParent(Application.dataPath).FullName;
        }

        private static string ReadCommandLineValue(string key)
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (string.Equals(args[i], key, StringComparison.OrdinalIgnoreCase))
                    return args[i + 1];
            }
            return string.Empty;
        }
    }
}
