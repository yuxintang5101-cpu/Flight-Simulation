using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace FlightSim.Platform.Editor.Tests
{
    public sealed class PlatformImportValidatorTests
    {
        private string reportDirectory;

        [SetUp]
        public void SetUp()
        {
            reportDirectory = Path.Combine(Path.GetTempPath(), "FlightSimImportValidatorTests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(reportDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(reportDirectory))
                Directory.Delete(reportDirectory, true);
        }

        [Test]
        public void ValidateUsesStableIdsForEveryRequiredPlatformCheck()
        {
            ImportValidationReport report = PlatformImportValidator.Validate(new ValidationContext(CreatePassingProbe()));

            Assert.That(report.HasFailures, Is.False);
            Assert.That(report.Checks.Select(check => check.Id), Is.EqualTo(new[]
            {
                "ENV-UNITY-001",
                "ENV-PIPELINE-001",
                "ENV-WINDOWS-001",
                "ENV-INPUT-001",
                "PKG-CESIUM-001",
                "PKG-CESIUM-002",
                "SEC-TOKEN-001",
                "SCENE-CESIUM-001",
                "ASSET-F35-001",
                "SCENE-FLIGHT-001",
                "MISSION-FOUR-001",
                "DATA-HUB-001",
                "SEC-READONLY-001"
            }));
            Assert.That(report.Checks.All(check => check.Status == ImportValidationStatus.Passed), Is.True);
        }

        [Test]
        public void ValidateConvertsOneProbeExceptionToFailedEntryAndContinuesOtherChecks()
        {
            const string pseudoToken = "pseudo-token-value-123";
            const string url = "https://invalid.example.test/native";
            var probe = CreatePassingProbe();
            probe.F35Assets = () => throw new InvalidOperationException(url + "?token=" + pseudoToken);

            ImportValidationReport report = PlatformImportValidator.Validate(new ValidationContext(probe));

            ImportValidationCheck f35 = Find(report, "ASSET-F35-001");
            Assert.That(f35.Status, Is.EqualTo(ImportValidationStatus.Failed));
            Assert.That(f35.Detail, Is.EqualTo("PROBE-EXCEPTION-ASSET-F35-001|System.InvalidOperationException"));
            Assert.That(f35.Detail, Does.Not.Contain(pseudoToken));
            Assert.That(f35.Detail, Does.Not.Contain(url));
            Assert.That(report.ToJson(), Does.Not.Contain(pseudoToken));
            Assert.That(report.ToJson(), Does.Not.Contain(url));
            Assert.That(report.ToChineseMarkdown(), Does.Not.Contain(pseudoToken));
            Assert.That(report.ToChineseMarkdown(), Does.Not.Contain(url));
            Assert.That(Find(report, "ENV-UNITY-001").Status, Is.EqualTo(ImportValidationStatus.Passed));
            Assert.That(Find(report, "SEC-READONLY-001").Status, Is.EqualTo(ImportValidationStatus.Passed));
        }

        [Test]
        public void ReportsAreJsonAndChineseMarkdownAndNeverRevealTokenPlaintext()
        {
            const string token = "secret-token-never-appears";
            var probe = CreatePassingProbe();
            probe.Token = () => token;
            ImportValidationReport report = PlatformImportValidator.Validate(new ValidationContext(probe));

            report.WriteToDirectory(reportDirectory);

            string json = File.ReadAllText(Path.Combine(reportDirectory, "FlightSim-Import-Report.json"));
            string markdown = File.ReadAllText(Path.Combine(reportDirectory, "FlightSim-Import-Report-zh-CN.md"));
            Assert.That(json, Does.Contain("ENV-UNITY-001"));
            Assert.That(markdown, Does.Contain("# FlightSim 平台导入验证报告"));
            Assert.That(markdown, Does.Contain("通过"));
            Assert.That(json, Does.Not.Contain(token));
            Assert.That(markdown, Does.Not.Contain(token));
            Assert.That(json, Does.Contain("TokenFingerprint"));
        }

        [Test]
        public void EachInvalidProbeResultFailsOnlyItsStableCheck()
        {
            FailureCase[] cases =
            {
                new FailureCase("ENV-UNITY-001", probe => probe.UnityVersion = () => "2022.3.0f1"),
                new FailureCase("ENV-PIPELINE-001", probe => probe.RenderPipeline = () => "Universal"),
                new FailureCase("ENV-WINDOWS-001", probe => probe.TargetPlatform = () => "Linux x86-64"),
                new FailureCase("ENV-INPUT-001", probe => probe.InputHandling = () => "Input System"),
                new FailureCase("PKG-CESIUM-001", probe => probe.CesiumVersion = () => "1.23.1"),
                new FailureCase("PKG-CESIUM-002", probe => probe.CesiumNativeLoadable = () => false),
                new FailureCase("SEC-TOKEN-001", probe => probe.Token = () => string.Empty),
                new FailureCase("SCENE-CESIUM-001", probe => probe.CesiumSceneAssets = () => new CesiumSceneAssetProbe { TerrainIonAssetId = 9, ImageryIonAssetId = PlatformAssetLayout.ImageryIonAssetId }),
                new FailureCase("SCENE-CESIUM-001", probe => probe.CesiumSceneAssets = () => new CesiumSceneAssetProbe { TerrainIonAssetId = PlatformAssetLayout.TerrainIonAssetId, ImageryIonAssetId = 0 }),
                new FailureCase("ASSET-F35-001", probe => probe.F35Assets = () => PassingF35(0)),
                new FailureCase("ASSET-F35-001", probe => probe.F35Assets = () => new F35AssetProbe { RendererCount = 1, AllMaterialSlotsAssigned = false, AllShadersStandard = true, AllMaterialsInF35Root = true }),
                new FailureCase("ASSET-F35-001", probe => probe.F35Assets = () => new F35AssetProbe { RendererCount = 1, AllMaterialSlotsAssigned = true, AllShadersStandard = false, AllMaterialsInF35Root = true }),
                new FailureCase("ASSET-F35-001", probe => probe.F35Assets = () => new F35AssetProbe { RendererCount = 1, AllMaterialSlotsAssigned = true, AllShadersStandard = true, AllMaterialsInF35Root = false }),
                new FailureCase("SCENE-FLIGHT-001", probe => probe.SceneReady = () => false),
                new FailureCase("MISSION-FOUR-001", probe => probe.MissionIds = () => new[] { "KTEX_SCRAMBLE_01", "KTEX_CAP_01", "KTEX_ESCORT_01", "KTEX_WRONG_01" }),
                new FailureCase("MISSION-FOUR-001", probe => probe.MissionIds = () => new[] { "KTEX_SCRAMBLE_01", "KTEX_CAP_01", "KTEX_ESCORT_01", "KTEX_EMERGENCY_RTB_01", string.Empty }),
                new FailureCase("DATA-HUB-001", probe => probe.FlightDataHubAvailable = () => false),
                new FailureCase("DATA-HUB-001", probe => probe.HubDomains = () => new[] { "None", "Simulation", "Mission" }),
                new FailureCase("DATA-HUB-001", probe => probe.HubDomains = () => new[] { "None", "Simulation", "Mission", "Engagement", string.Empty }),
                new FailureCase("SEC-READONLY-001", probe => probe.NetworkSurfaceReadOnly = () => false)
            };

            foreach (FailureCase failureCase in cases)
            {
                TestProbe probe = CreatePassingProbe();
                failureCase.Mutate(probe);
                ImportValidationReport report = PlatformImportValidator.Validate(new ValidationContext(probe));
                string[] failedIds = report.Checks
                    .Where(check => check.Status == ImportValidationStatus.Failed)
                    .Select(check => check.Id)
                    .ToArray();

                Assert.That(failedIds, Is.EqualTo(new[] { failureCase.CheckId }), failureCase.CheckId);
            }
        }

        [Test]
        public void DefaultProbeChecksCesiumNativePluginAndCurrentIntegrationSurface()
        {
            Type[] requiredGatewayTypes = RequiredGatewayTypeNames
                .Select(typeName => Type.GetType(typeName + ", FlightSim.Integration", false))
                .ToArray();
            Assert.That(requiredGatewayTypes, Has.All.Not.Null);
            Assert.That(requiredGatewayTypes, Has.All.Matches<Type>(type => IsPublicType(type)));

            ImportValidationReport report = PlatformImportValidator.Validate(PlatformImportValidator.CreateDefaultContext());

            Assert.That(Find(report, "PKG-CESIUM-002").Status, Is.EqualTo(ImportValidationStatus.Passed));
            Assert.That(
                Find(report, "SEC-READONLY-001").Status,
                Is.EqualTo(ImportValidationStatus.Passed));
            Assert.That(ExpectedReadOnlyNetworkSurface(requiredGatewayTypes[0].Assembly), Is.True);
        }

        [Test]
        public void ValidateCliWritesReportsThenThrowsStableFailure()
        {
            var probe = CreatePassingProbe();
            probe.NetworkSurfaceReadOnly = () => false;

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                PlatformImportValidator.ValidateCli(new ValidationContext(probe), reportDirectory));

            Assert.That(exception.Message, Is.EqualTo("PLATFORM_IMPORT_VALIDATION_FAILED"));
            Assert.That(File.Exists(Path.Combine(reportDirectory, "FlightSim-Import-Report.json")), Is.True);
            Assert.That(File.Exists(Path.Combine(reportDirectory, "FlightSim-Import-Report-zh-CN.md")), Is.True);
            Assert.That(
                File.ReadAllText(Path.Combine(reportDirectory, "FlightSim-Import-Report.json")),
                Does.Contain("SEC-READONLY-001"));
        }

        private static ImportValidationCheck Find(ImportValidationReport report, string id)
        {
            return report.Checks.Single(check => check.Id == id);
        }

        private static TestProbe CreatePassingProbe()
        {
            return new TestProbe
            {
                UnityVersion = () => FlightSimDistributionBuilder.RequiredUnityVersion,
                RenderPipeline = () => "Built-in",
                TargetPlatform = () => "Windows x86-64",
                InputHandling = () => "Both",
                CesiumVersion = () => FlightSimDistributionBuilder.RequiredCesiumVersion,
                CesiumNativeLoadable = () => true,
                Token = () => "test-token",
                CesiumSceneAssets = () => new CesiumSceneAssetProbe { TerrainIonAssetId = PlatformAssetLayout.TerrainIonAssetId, ImageryIonAssetId = PlatformAssetLayout.ImageryIonAssetId },
                F35Assets = () => PassingF35(1),
                SceneReady = () => true,
                MissionIds = () => new[] { "KTEX_SCRAMBLE_01", "KTEX_CAP_01", "KTEX_ESCORT_01", "KTEX_EMERGENCY_RTB_01" },
                FlightDataHubAvailable = () => true,
                HubDomains = () => new[] { "None", "Simulation", "Mission", "Engagement" },
                NetworkSurfaceReadOnly = () => true
            };
        }

        private static F35AssetProbe PassingF35(int rendererCount)
        {
            return new F35AssetProbe
            {
                RendererCount = rendererCount,
                AllMaterialSlotsAssigned = true,
                AllShadersStandard = true,
                AllMaterialsInF35Root = true
            };
        }

        private static bool ExpectedReadOnlyNetworkSurface(Assembly integrationAssembly)
        {
            Type commandInterface = Type.GetType(
                "FlightSim.Platform.Contracts.ISimulationCommand, FlightSim.Contracts",
                false);
            if (commandInterface == null)
                return false;

            Type[] requiredTypes = RequiredGatewayTypeNames
                .Select(name => integrationAssembly.GetType(name, false))
                .ToArray();
            if (requiredTypes.Any(type => type == null))
                return false;

            foreach (Type type in requiredTypes)
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
            if (member is MethodInfo method)
            {
                if (UsesCommandType(method.ReturnType, commandInterface) ||
                    method.GetParameters().Any(parameter => UsesCommandType(parameter.ParameterType, commandInterface)))
                {
                    return true;
                }
                return method.IsGenericMethodDefinition && method.GetGenericArguments()
                    .SelectMany(argument => argument.GetGenericParameterConstraints())
                    .Any(constraint => UsesCommandType(constraint, commandInterface));
            }
            if (member is ConstructorInfo constructor)
                return constructor.GetParameters().Any(parameter => UsesCommandType(parameter.ParameterType, commandInterface));
            return false;
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
            if (type == commandInterface || (type != commandInterface && commandInterface.IsAssignableFrom(type)))
                return true;
            return type.IsGenericType && type.GetGenericArguments().Any(argument => UsesCommandType(argument, commandInterface));
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

        private static readonly string[] RequiredGatewayTypeNames =
        {
            "FlightSim.Platform.Integration.UdpIntegrationEndpoint",
            "FlightSim.Platform.Integration.UdpPacketCodec",
            "FlightSim.Platform.Integration.NdjsonTelemetryServer",
            "FlightSim.Platform.Integration.ReadOnlyTelemetryPublisher"
        };

        private sealed class TestProbe : IPlatformImportProbe
        {
            public Func<string> UnityVersion { get; set; }
            public Func<string> RenderPipeline { get; set; }
            public Func<string> TargetPlatform { get; set; }
            public Func<string> InputHandling { get; set; }
            public Func<string> CesiumVersion { get; set; }
            public Func<bool> CesiumNativeLoadable { get; set; }
            public Func<string> Token { get; set; }
            public Func<CesiumSceneAssetProbe> CesiumSceneAssets { get; set; }
            public Func<F35AssetProbe> F35Assets { get; set; }
            public Func<bool> SceneReady { get; set; }
            public Func<string[]> MissionIds { get; set; }
            public Func<bool> FlightDataHubAvailable { get; set; }
            public Func<string[]> HubDomains { get; set; }
            public Func<bool> NetworkSurfaceReadOnly { get; set; }

            string IPlatformImportProbe.UnityVersion => UnityVersion();
            string IPlatformImportProbe.RenderPipeline => RenderPipeline();
            string IPlatformImportProbe.TargetPlatform => TargetPlatform();
            string IPlatformImportProbe.InputHandling => InputHandling();
            string IPlatformImportProbe.CesiumVersion => CesiumVersion();
            bool IPlatformImportProbe.CesiumNativeLoadable => CesiumNativeLoadable();
            string IPlatformImportProbe.Token => Token();
            CesiumSceneAssetProbe IPlatformImportProbe.CesiumSceneAssets => CesiumSceneAssets();
            F35AssetProbe IPlatformImportProbe.F35Assets => F35Assets();
            bool IPlatformImportProbe.SceneReady => SceneReady();
            string[] IPlatformImportProbe.MissionIds => MissionIds();
            bool IPlatformImportProbe.FlightDataHubAvailable => FlightDataHubAvailable();
            string[] IPlatformImportProbe.HubDomains => HubDomains();
            bool IPlatformImportProbe.NetworkSurfaceReadOnly => NetworkSurfaceReadOnly();
        }

        private sealed class FailureCase
        {
            public FailureCase(string checkId, Action<TestProbe> mutate)
            {
                CheckId = checkId;
                Mutate = mutate;
            }

            public string CheckId { get; }
            public Action<TestProbe> Mutate { get; }
        }
    }
}
