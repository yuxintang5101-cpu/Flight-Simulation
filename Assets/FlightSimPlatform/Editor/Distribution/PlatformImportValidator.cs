using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using CesiumForUnity;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Integration;
using FlightSim.Platform.Missions;
using FlightSim.Platform.Presentation;
using FlightSim.Platform.Unity;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace FlightSim.Platform.Editor
{
    public static class PlatformImportValidator
    {
        private const string F35PrefabPath = "Assets/FlightSimPlatform/Prefabs/F35_Visual.prefab";
        private const string FlightDataHubTypeName = "FlightSim.Platform.Data.FlightDataHub, FlightSim.Data";
        private const string FlightDataDomainTypeName = "FlightSim.Platform.Data.FlightDataDomain, FlightSim.Data";

        private static readonly string[] ExpectedMissionIds =
        {
            "KTEX_SCRAMBLE_01",
            "KTEX_CAP_01",
            "KTEX_ESCORT_01",
            "KTEX_EMERGENCY_RTB_01"
        };

        private static readonly string[] ExpectedHubDomains =
        {
            "None",
            "Simulation",
            "Mission",
            "Engagement"
        };

        private static readonly Type[] RequiredGatewayTypes =
        {
            typeof(UdpIntegrationEndpoint),
            typeof(UdpPacketCodec),
            typeof(NdjsonTelemetryServer),
            typeof(ReadOnlyTelemetryPublisher)
        };

        [MenuItem("FlightSim/Distribution/Validate Imported Platform")]
        public static void ValidateFromMenu()
        {
            ValidateCli();
        }

        public static void ValidateCli()
        {
            string outputDirectory = Path.Combine(GetProjectRoot(), "Artifacts", "Validation");
            ValidateCli(CreateDefaultContext(), outputDirectory);
        }

        public static void ValidateCli(ValidationContext context, string outputDirectory)
        {
            ImportValidationReport report = Validate(context);
            report.WriteToDirectory(outputDirectory);

            if (report.HasFailures)
                throw new InvalidOperationException("PLATFORM_IMPORT_VALIDATION_FAILED");

            Debug.Log("FlightSim platform validation passed. Reports: " + outputDirectory);
        }

        public static ValidationContext CreateDefaultContext()
        {
            return new ValidationContext(new UnityPlatformImportProbe());
        }

        public static ImportValidationReport Validate(ValidationContext context)
        {
            IPlatformImportProbe probe = context == null ? null : context.Probe;
            var checks = new List<ImportValidationCheck>();
            var report = new ImportValidationReport
            {
                GeneratedUtc = DateTime.UtcNow.ToString("o")
            };

            Run(checks, "ENV-UNITY-001", "Unity 版本", "使用指定的 Unity 版本重新导入平台。", () =>
                EqualsExpected(probe.UnityVersion, FlightSimDistributionBuilder.RequiredUnityVersion, "Unity 版本不符合要求。"));
            Run(checks, "ENV-PIPELINE-001", "渲染管线", "将项目切换为 Built-in 渲染管线。", () =>
                EqualsExpected(probe.RenderPipeline, "Built-in", "项目未使用 Built-in 渲染管线。"));
            Run(checks, "ENV-WINDOWS-001", "目标平台", "将活动构建目标切换到 Windows x86-64。", () =>
                EqualsExpected(probe.TargetPlatform, "Windows x86-64", "活动构建目标不是 Windows x86-64。"));
            Run(checks, "ENV-INPUT-001", "输入处理", "在 Player Settings 中将 Active Input Handling 设置为 Both。", () =>
                EqualsExpected(probe.InputHandling, "Both", "Active Input Handling 未设置为 Both。"));
            Run(checks, "PKG-CESIUM-001", "Cesium 包版本", "将 com.cesium.unity 固定为 1.24.0。", () =>
                EqualsExpected(probe.CesiumVersion, FlightSimDistributionBuilder.RequiredCesiumVersion, "Cesium 包版本不符合要求。"));
            Run(checks, "PKG-CESIUM-002", "Cesium 原生加载", "重新解析 Cesium 包并确认原生插件可加载。", () =>
                BooleanResult(probe.CesiumNativeLoadable, "Cesium 原生类型未加载。"));
            Run(checks, "SEC-TOKEN-001", "Cesium 访问令牌", "配置专用 Cesium Ion Server 的访问令牌。", () =>
            {
                string token = probe.Token;
                if (string.IsNullOrWhiteSpace(token))
                    return ValidationResult.Failed("未检测到 Cesium 访问令牌。", true);
                report.TokenFingerprint = ComputeTokenFingerprint(token);
                return ValidationResult.Passed();
            });
            Run(checks, "SCENE-CESIUM-001", "Cesium 地形和影像", "恢复场景中的 World Terrain 和影像覆盖层。", () =>
            {
                CesiumSceneAssetProbe assets = probe.CesiumSceneAssets;
                return BooleanResult(
                    assets.TerrainIonAssetId == PlatformAssetLayout.TerrainIonAssetId &&
                    assets.ImageryIonAssetId == PlatformAssetLayout.ImageryIonAssetId,
                    "场景未使用发行版指定的 Cesium World Terrain 与影像资产。");
            });
            Run(checks, "ASSET-F35-001", "F-35 材质", "重新导入 F-35 预制件和 Built-in Standard 材质。", () =>
            {
                F35AssetProbe assets = probe.F35Assets;
                bool valid = assets.RendererCount > 0 &&
                             assets.AllMaterialSlotsAssigned &&
                             assets.AllShadersStandard &&
                             assets.AllMaterialsInF35Root;
                return BooleanResult(valid, "F-35 预制件或材质不完整。");
            });
            Run(checks, "SCENE-FLIGHT-001", "飞行场景锚点", "恢复飞行主机、HUD、F-35 视觉对象和相机原点偏移。", () =>
                BooleanResult(probe.SceneReady, "飞行场景缺少必需锚点或 HUD。"));
            Run(checks, "MISSION-FOUR-001", "默认任务", "导入四个默认任务定义。", () =>
            {
                string[] missionIds = probe.MissionIds ?? Array.Empty<string>();
                return BooleanResult(
                    HasExactValues(missionIds, ExpectedMissionIds),
                    "默认任务 ID 与要求的四个 KTEX 任务不一致。");
            });
            Run(checks, "DATA-HUB-001", "FlightDataHub", "恢复 FlightDataHub 及其仿真、任务和交战数据域。", () =>
            {
                string[] domains = probe.HubDomains ?? Array.Empty<string>();
                bool valid = probe.FlightDataHubAvailable && HasExactValues(domains, ExpectedHubDomains);
                return BooleanResult(valid, "FlightDataHub 或其数据域不完整。");
            });
            Run(checks, "SEC-READONLY-001", "网络只读表面", "移除网络 API 的控制、写入或命令入口。", () =>
                BooleanResult(probe.NetworkSurfaceReadOnly, "检测到可写的网络控制表面。"));

            report.Checks = checks.ToArray();
            return report;
        }

        private static void Run(
            ICollection<ImportValidationCheck> checks,
            string id,
            string summary,
            string remediation,
            Func<ValidationResult> validation)
        {
            try
            {
                ValidationResult result = validation();
                checks.Add(new ImportValidationCheck
                {
                    Id = id,
                    Status = result.IsPassed ? ImportValidationStatus.Passed : ImportValidationStatus.Failed,
                    Summary = summary,
                    Detail = result.Detail,
                    Remediation = result.IsPassed ? string.Empty : remediation
                });
            }
            catch (Exception exception)
            {
                checks.Add(new ImportValidationCheck
                {
                    Id = id,
                    Status = ImportValidationStatus.Failed,
                    Summary = summary,
                    Detail = "PROBE-EXCEPTION-" + id + "|" +
                             (exception.GetType().FullName ?? exception.GetType().Name),
                    Remediation = remediation
                });
            }
        }

        private static bool HasExactValues(IEnumerable<string> actual, IEnumerable<string> expected)
        {
            string[] actualValues = actual
                .OrderBy(value => value, StringComparer.Ordinal)
                .ToArray();
            string[] expectedValues = expected.OrderBy(value => value, StringComparer.Ordinal).ToArray();
            return actualValues.SequenceEqual(expectedValues, StringComparer.Ordinal);
        }

        private static bool IsReadOnlyNetworkSurface(Assembly integrationAssembly)
        {
            if (integrationAssembly == null ||
                !string.Equals(integrationAssembly.GetName().Name, "FlightSim.Integration", StringComparison.Ordinal))
            {
                return false;
            }

            if (RequiredGatewayTypes.Any(type => type.Assembly != integrationAssembly || !IsPublicType(type)))
                return false;

            Type commandInterface = typeof(ISimulationCommand);
            foreach (Type type in RequiredGatewayTypes)
            {
                if (IsForbiddenNetworkName(type.Name) || UsesCommandType(type, commandInterface))
                    return false;

                BindingFlags publicFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
                foreach (MemberInfo member in type.GetMembers(publicFlags))
                {
                    if (IsForbiddenNetworkName(member.Name) || MemberUsesCommandType(member, commandInterface))
                        return false;
                }

                BindingFlags allFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                        BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
                foreach (MemberInfo member in type.GetMembers(allFlags))
                {
                    if (MemberUsesConcreteCommandType(member, commandInterface))
                        return false;
                }
            }

            return true;
        }

        private static bool IsPublicType(Type type)
        {
            if (type.IsPublic)
                return true;
            return type.IsNestedPublic && IsPublicType(type.DeclaringType);
        }

        private static bool IsForbiddenNetworkName(string name)
        {
            return name.IndexOf("Command", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("Acknowledgement", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("Acknowledgment", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   string.Equals(name, "Ack", StringComparison.OrdinalIgnoreCase) ||
                   name.StartsWith("Ack", StringComparison.OrdinalIgnoreCase) ||
                   name.EndsWith("Ack", StringComparison.OrdinalIgnoreCase) ||
                   name.StartsWith("Submit", StringComparison.OrdinalIgnoreCase);
        }

        private static bool MemberUsesCommandType(MemberInfo member, Type commandInterface)
        {
            if (member is FieldInfo field)
                return UsesCommandType(field.FieldType, commandInterface);
            if (member is PropertyInfo property)
                return UsesCommandType(property.PropertyType, commandInterface);
            if (member is EventInfo eventInfo)
                return UsesCommandType(eventInfo.EventHandlerType, commandInterface);
            if (member is ConstructorInfo constructor)
                return constructor.GetParameters().Any(parameter => UsesCommandType(parameter.ParameterType, commandInterface));
            if (!(member is MethodInfo method))
                return false;

            if (UsesCommandType(method.ReturnType, commandInterface) ||
                method.GetParameters().Any(parameter => UsesCommandType(parameter.ParameterType, commandInterface)))
            {
                return true;
            }

            return method.IsGenericMethodDefinition && method.GetGenericArguments()
                .SelectMany(argument => argument.GetGenericParameterConstraints())
                .Any(constraint => UsesCommandType(constraint, commandInterface));
        }

        private static bool MemberUsesConcreteCommandType(MemberInfo member, Type commandInterface)
        {
            if (member is FieldInfo field)
                return UsesConcreteCommandType(field.FieldType, commandInterface);
            if (member is MethodInfo method)
            {
                return UsesConcreteCommandType(method.ReturnType, commandInterface) ||
                       method.GetParameters().Any(parameter => UsesConcreteCommandType(parameter.ParameterType, commandInterface));
            }
            if (member is ConstructorInfo constructor)
                return constructor.GetParameters().Any(parameter => UsesConcreteCommandType(parameter.ParameterType, commandInterface));
            return false;
        }

        private static bool UsesCommandType(Type type, Type commandInterface)
        {
            if (type == null)
                return false;
            if (type.IsByRef || type.IsArray || type.IsPointer)
                return UsesCommandType(type.GetElementType(), commandInterface);
            if (type == commandInterface || commandInterface.IsAssignableFrom(type))
                return true;
            return type.IsGenericType &&
                   type.GetGenericArguments().Any(argument => UsesCommandType(argument, commandInterface));
        }

        private static bool UsesConcreteCommandType(Type type, Type commandInterface)
        {
            if (type == null)
                return false;
            if (type.IsByRef || type.IsArray || type.IsPointer)
                return UsesConcreteCommandType(type.GetElementType(), commandInterface);
            if (type != commandInterface && commandInterface.IsAssignableFrom(type))
                return true;
            return type.IsGenericType && type.GetGenericArguments()
                .Any(argument => UsesConcreteCommandType(argument, commandInterface));
        }

        private static ValidationResult EqualsExpected(string actual, string expected, string failureDetail)
        {
            return string.Equals(actual, expected, StringComparison.Ordinal)
                ? ValidationResult.Passed()
                : ValidationResult.Failed(failureDetail, false);
        }

        private static ValidationResult BooleanResult(bool passed, string failureDetail)
        {
            return passed ? ValidationResult.Passed() : ValidationResult.Failed(failureDetail, false);
        }

        private static string ComputeTokenFingerprint(string token)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(token));
                return string.Concat(hash.Take(6).Select(value => value.ToString("x2")));
            }
        }

        private static string GetProjectRoot()
        {
            return Directory.GetParent(Application.dataPath).FullName;
        }

        private readonly struct ValidationResult
        {
            private ValidationResult(bool isPassed, string detail)
            {
                IsPassed = isPassed;
                Detail = detail;
            }

            public bool IsPassed { get; }
            public string Detail { get; }

            public static ValidationResult Passed()
            {
                return new ValidationResult(true, string.Empty);
            }

            public static ValidationResult Failed(string detail, bool isSensitive)
            {
                return new ValidationResult(false, isSensitive ? "敏感值未通过验证。" : detail);
            }
        }

        private sealed class UnityPlatformImportProbe : IPlatformImportProbe
        {
            public string UnityVersion => Application.unityVersion;

            public string RenderPipeline => GraphicsSettings.currentRenderPipeline == null ? "Built-in" : GraphicsSettings.currentRenderPipeline.GetType().Name;

            public string TargetPlatform => EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64
                ? "Windows x86-64"
                : EditorUserBuildSettings.activeBuildTarget.ToString();

            public string InputHandling => ReadInputHandling();

            public string CesiumVersion => UnityEditor.PackageManager.PackageInfo.FindForAssetPath("Packages/com.cesium.unity")?.version ?? string.Empty;

            public bool CesiumNativeLoadable => IsCesiumNativePluginReady();

            public string Token
            {
                get
                {
                    CesiumIonServer server = AssetDatabase.LoadAssetAtPath<CesiumIonServer>(PlatformAssetLayout.CesiumServerPath);
                    return server == null ? string.Empty : server.defaultIonAccessToken;
                }
            }

            public CesiumSceneAssetProbe CesiumSceneAssets
            {
                get
                {
                    Scene scene = OpenValidationScene();
                    try
                    {
                        Cesium3DTileset terrain = FindComponents<Cesium3DTileset>(scene)
                            .FirstOrDefault(tileset => tileset.ionAssetID == PlatformAssetLayout.TerrainIonAssetId);
                        CesiumIonRasterOverlay[] overlays = FindComponents<CesiumIonRasterOverlay>(scene).ToArray();
                        long imageryId = overlays.Length > 0 && overlays.All(overlay =>
                            overlay.ionAssetID == PlatformAssetLayout.ImageryIonAssetId)
                            ? PlatformAssetLayout.ImageryIonAssetId
                            : 0;
                        return new CesiumSceneAssetProbe
                        {
                            TerrainIonAssetId = terrain == null ? 0 : terrain.ionAssetID,
                            ImageryIonAssetId = imageryId
                        };
                    }
                    finally
                    {
                        EditorSceneManager.CloseScene(scene, true);
                    }
                }
            }

            public F35AssetProbe F35Assets
            {
                get
                {
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(F35PrefabPath);
                    if (prefab == null)
                        return default(F35AssetProbe);

                    Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>(true);
                    Material[][] rendererMaterials = renderers.Select(renderer => renderer.sharedMaterials).ToArray();
                    Material[] materials = rendererMaterials
                        .Where(items => items != null)
                        .SelectMany(items => items)
                        .ToArray();
                    bool allSlotsAssigned = renderers.Length > 0 &&
                                            rendererMaterials.All(items => items != null && items.Length > 0 && items.All(material => material != null));
                    bool allShadersStandard = allSlotsAssigned && materials.All(material =>
                        material.shader != null && string.Equals(material.shader.name, "Standard", StringComparison.Ordinal));
                    bool allMaterialsInRoot = allSlotsAssigned && materials.All(material =>
                    {
                        string path = AssetDatabase.GetAssetPath(material);
                        return string.Equals(path, PlatformAssetLayout.F35MaterialRoot, StringComparison.Ordinal) ||
                               path.StartsWith(PlatformAssetLayout.F35MaterialRoot + "/", StringComparison.Ordinal);
                    });

                    return new F35AssetProbe
                    {
                        RendererCount = renderers.Length,
                        AllMaterialSlotsAssigned = allSlotsAssigned,
                        AllShadersStandard = allShadersStandard,
                        AllMaterialsInF35Root = allMaterialsInRoot
                    };
                }
            }

            public bool SceneReady
            {
                get
                {
                    Scene scene = OpenValidationScene();
                    try
                    {
                        FlightSimulationHost host = FindComponents<FlightSimulationHost>(scene).FirstOrDefault();
                        FlightCameraRig cameraRig = FindComponents<FlightCameraRig>(scene).FirstOrDefault();
                        CesiumAircraftView aircraftView = FindComponents<CesiumAircraftView>(scene).FirstOrDefault();
                        F16HudController hud = FindComponents<F16HudController>(scene).FirstOrDefault();
                        return host != null && cameraRig != null && aircraftView != null && hud != null &&
                               cameraRig.GetComponent<CesiumOriginShift>() != null &&
                               FindDeepChild(aircraftView.transform, "F35_Visual") != null;
                    }
                    finally
                    {
                        EditorSceneManager.CloseScene(scene, true);
                    }
                }
            }

            public string[] MissionIds => DefaultMissionCatalog.CreateAll().Select(mission => mission.MissionId).ToArray();

            public bool FlightDataHubAvailable => Type.GetType(FlightDataHubTypeName, false) != null;

            public string[] HubDomains
            {
                get
                {
                    Type domainType = Type.GetType(FlightDataDomainTypeName, false);
                    return domainType == null || !domainType.IsEnum ? Array.Empty<string>() : Enum.GetNames(domainType);
                }
            }

            public bool NetworkSurfaceReadOnly
            {
                get { return IsReadOnlyNetworkSurface(typeof(UdpPacketCodec).Assembly); }
            }

            private static bool IsCesiumNativePluginReady()
            {
                string editorExtension = GetCurrentEditorNativeExtension();
                if (string.IsNullOrEmpty(editorExtension))
                    return false;

                string[] candidates = AssetDatabase.GetAllAssetPaths()
                    .Where(path => path.StartsWith("Packages/com.cesium.unity/", StringComparison.Ordinal))
                    .Where(IsCesiumNativeLibrary)
                    .ToArray();
                bool editorCompatible = candidates.Any(path =>
                {
                    PluginImporter importer = AssetImporter.GetAtPath(path) as PluginImporter;
                    return importer != null &&
                           path.EndsWith(editorExtension, StringComparison.OrdinalIgnoreCase) &&
                           importer.GetCompatibleWithEditor() &&
                           NativeLibraryFileExists(path);
                });
                bool targetCompatible = candidates.Any(path =>
                {
                    PluginImporter importer = AssetImporter.GetAtPath(path) as PluginImporter;
                    return importer != null &&
                           importer.GetCompatibleWithPlatform(EditorUserBuildSettings.activeBuildTarget) &&
                           NativeLibraryFileExists(path);
                });
                return editorCompatible && targetCompatible;
            }

            private static bool IsCesiumNativeLibrary(string assetPath)
            {
                string fileName = Path.GetFileNameWithoutExtension(assetPath);
                return string.Equals(fileName, "CesiumForUnityNative", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(fileName, "libCesiumForUnityNative", StringComparison.OrdinalIgnoreCase);
            }

            private static bool NativeLibraryFileExists(string assetPath)
            {
                UnityEditor.PackageManager.PackageInfo package =
                    UnityEditor.PackageManager.PackageInfo.FindForAssetPath(assetPath);
                if (package == null || string.IsNullOrEmpty(package.resolvedPath))
                    return false;

                string prefix = "Packages/" + package.name + "/";
                if (!assetPath.StartsWith(prefix, StringComparison.Ordinal))
                    return false;
                string relativePath = assetPath.Substring(prefix.Length).Replace('/', Path.DirectorySeparatorChar);
                return File.Exists(Path.Combine(package.resolvedPath, relativePath));
            }

            private static string GetCurrentEditorNativeExtension()
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.WindowsEditor:
                        return ".dll";
                    case RuntimePlatform.OSXEditor:
                        return ".dylib";
                    case RuntimePlatform.LinuxEditor:
                        return ".so";
                    default:
                        return string.Empty;
                }
            }

            private static string ReadInputHandling()
            {
                string projectSettings = Path.Combine(GetProjectRoot(), "ProjectSettings", "ProjectSettings.asset");
                if (!File.Exists(projectSettings))
                    return string.Empty;

                foreach (string line in File.ReadLines(projectSettings))
                {
                    if (line.Trim() == "activeInputHandler: 2")
                        return "Both";
                }
                return string.Empty;
            }

            private static Scene OpenValidationScene()
            {
                if (AssetDatabase.LoadAssetAtPath<SceneAsset>(PlatformAssetLayout.ScenePath) == null)
                    throw new FileNotFoundException("The FlightSim sample scene is missing.", PlatformAssetLayout.ScenePath);
                return EditorSceneManager.OpenScene(PlatformAssetLayout.ScenePath, OpenSceneMode.Additive);
            }

            private static IEnumerable<T> FindComponents<T>(Scene scene) where T : Component
            {
                foreach (GameObject root in scene.GetRootGameObjects())
                {
                    foreach (T component in root.GetComponentsInChildren<T>(true))
                        yield return component;
                }
            }

            private static Transform FindDeepChild(Transform parent, string name)
            {
                if (parent.name == name)
                    return parent;
                for (int index = 0; index < parent.childCount; index++)
                {
                    Transform found = FindDeepChild(parent.GetChild(index), name);
                    if (found != null)
                        return found;
                }
                return null;
            }
        }
    }
}
