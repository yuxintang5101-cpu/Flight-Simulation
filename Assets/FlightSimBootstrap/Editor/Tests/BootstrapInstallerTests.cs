using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace FlightSim.Bootstrap.Editor.Tests
{
    public sealed class BootstrapInstallerTests
    {
        private string _root;

        [SetUp]
        public void SetUp()
        {
            _root = Path.Combine(Path.GetTempPath(), "FlightSimBootstrapTests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_root);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_root)) Directory.Delete(_root, true);
        }

        [Test]
        public void MergeManifestPinsRequiredPackagesAndLeavesOneCanonicalCesiumRegistry()
        {
            const string source = "{\"scopedRegistries\":[{\"name\":\"Cesium\",\"url\":\"https://old.example\",\"scopes\":[\"com.cesium.unity\"]},{\"name\":\"Other\",\"url\":\"https://other.example\",\"scopes\":[\"com.example\"]},{\"name\":\"Cesium\",\"url\":\"https://unity.pkg.cesium.com\",\"scopes\":[\"com.cesium.unity\"]}],\"dependencies\":{\"com.cesium.unity\":\"1.0.0\",\"com.unity.textmeshpro\":\"2.0.0\"}}";

            string merged = BootstrapJson.MergeManifest(source);
            BootstrapManifestDocument document = BootstrapJson.ParseManifest(merged);

            Assert.That(document.ScopedRegistries.Count, Is.EqualTo(2));
            Assert.That(document.ScopedRegistries[0].Name, Is.EqualTo("Cesium"));
            Assert.That(document.ScopedRegistries[0].Url, Is.EqualTo(BootstrapConstants.CesiumRegistryUrl));
            CollectionAssert.AreEqual(new[] { BootstrapConstants.CesiumScope }, document.ScopedRegistries[0].Scopes);
            Assert.That(document.Dependencies[BootstrapConstants.CesiumPackage], Is.EqualTo(BootstrapConstants.CesiumVersion));
            Assert.That(document.Dependencies[BootstrapConstants.TextMeshProPackage], Is.EqualTo(BootstrapConstants.TextMeshProVersion));
            Assert.That(document.Dependencies[BootstrapConstants.UguiPackage], Is.EqualTo(BootstrapConstants.UguiVersion));
            Assert.That(document.Dependencies[BootstrapConstants.InputSystemPackage], Is.EqualTo(BootstrapConstants.InputSystemVersion));
            Assert.That(BootstrapJson.MergeManifest(merged), Is.EqualTo(merged));
        }

        [Test]
        public void MergeManifestPreservesCanonicalRegistryUnknownFieldsAndExtraScopes()
        {
            const string source = "{\"scopedRegistries\":[{\"name\":\"Cesium\",\"url\":\"https://unity.pkg.cesium.com\",\"scopes\":[\"com.example.extra\",\"com.cesium.unity\"],\"custom\":{\"header\":\"keep\"}},{\"name\":\"Cesium\",\"url\":\"https://old.example\",\"scopes\":[\"com.example.second\"]}],\"dependencies\":{}}";

            string merged = BootstrapJson.MergeManifest(source);
            BootstrapManifestDocument document = BootstrapJson.ParseManifest(merged);

            Assert.That(merged, Does.Contain("\"custom\""));
            Assert.That(merged, Does.Contain("\"header\": \"keep\""));
            CollectionAssert.AreEquivalent(
                new[] { "com.example.extra", BootstrapConstants.CesiumScope, "com.example.second" },
                document.ScopedRegistries[0].Scopes);
        }

        [Test]
        public void EnvironmentValidationRejectsWrongUnityVersionRenderPipelineAndTarget()
        {
            BootstrapEnvironmentSnapshot invalid = new BootstrapEnvironmentSnapshot
            {
                UnityVersion = "2023.2.0f1",
                ActiveBuildTarget = "WebGL",
                RenderPipelineType = "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset"
            };

            BootstrapPreflightReport report = BootstrapEnvironmentEvaluator.Evaluate(invalid);

            Assert.That(report.CanContinue, Is.False);
            Assert.That(report.ContainsFailure(BootstrapCheckId.UnityVersion), Is.True);
            Assert.That(report.ContainsFailure(BootstrapCheckId.RenderPipeline), Is.True);
            Assert.That(report.ContainsFailure(BootstrapCheckId.BuildTarget), Is.True);
        }

        [Test]
        public void EnvironmentValidationRequiresWindowsX64HostAndTarget()
        {
            BootstrapEnvironmentSnapshot invalid = new BootstrapEnvironmentSnapshot
            {
                UnityVersion = BootstrapConstants.UnityVersion,
                ActiveBuildTarget = BootstrapConstants.WindowsX64BuildTarget,
                RenderPipelineType = string.Empty,
                ActiveInputHandler = 2,
                HostPlatform = "OSXEditor",
                Is64BitOperatingSystem = true,
                Is64BitProcess = false
            };

            BootstrapPreflightReport report = BootstrapEnvironmentEvaluator.Evaluate(invalid);

            Assert.That(report.CanContinue, Is.False);
            Assert.That(report.ContainsFailure(BootstrapCheckId.Host), Is.True);
        }

        [Test]
        public void ResolvedCesiumPackageMustUseTheExactPinnedVersion()
        {
            Assert.That(FlightSimBootstrapInstaller.IsRequiredPackageVersion(BootstrapConstants.CesiumPackage, BootstrapConstants.CesiumVersion), Is.True);
            Assert.That(FlightSimBootstrapInstaller.IsRequiredPackageVersion(BootstrapConstants.CesiumPackage, "1.24.1"), Is.False);
            Assert.That(FlightSimBootstrapInstaller.IsRequiredPackageVersion(BootstrapConstants.CesiumPackage, "1.23.9"), Is.False);
        }

        [Test]
        public void ContinueRequiresAllFourResolvedPackageVersions()
        {
            Dictionary<string, string> exact = new Dictionary<string, string>
            {
                [BootstrapConstants.CesiumPackage] = BootstrapConstants.CesiumVersion,
                [BootstrapConstants.TextMeshProPackage] = BootstrapConstants.TextMeshProVersion,
                [BootstrapConstants.UguiPackage] = BootstrapConstants.UguiVersion,
                [BootstrapConstants.InputSystemPackage] = BootstrapConstants.InputSystemVersion
            };

            Assert.DoesNotThrow(() => FlightSimBootstrapInstaller.ValidateRequiredPackageVersions(exact));
            exact[BootstrapConstants.InputSystemPackage] = "1.13.0";
            Assert.Throws<InvalidOperationException>(() => FlightSimBootstrapInstaller.ValidateRequiredPackageVersions(exact));
            exact.Remove(BootstrapConstants.UguiPackage);
            Assert.Throws<InvalidOperationException>(() => FlightSimBootstrapInstaller.ValidateRequiredPackageVersions(exact));
        }

        [Test]
        public void ConflictingAssetPathAndGuidStopsPlatformImport()
        {
            BootstrapAssetIdentity[] incoming = { new BootstrapAssetIdentity { Path = "Assets/FlightSimPlatform/Config.asset", Guid = "expected-guid" } };
            Assert.Throws<InvalidOperationException>(() => FlightSimBootstrapInstaller.EnsureNoConflictingAssetGuids(incoming, path => "receiver-guid"));
            Assert.DoesNotThrow(() => FlightSimBootstrapInstaller.EnsureNoConflictingAssetGuids(incoming, path => "expected-guid"));
        }

        [Test]
        public void DistributionManifestParsingFiltersPlatformAssetsAndRejectsMissingGuids()
        {
            const string json = "{\"DistributionVersion\":\"2.0.0\",\"Assets\":[{\"Path\":\"Assets/FlightSimPlatform/Config.asset\",\"Guid\":\"guid-a\",\"SizeBytes\":1,\"Sha256\":\"aa\"},{\"Path\":\"Assets/FlightSimPlatform/Editor/Tests/OptionalTest.cs\",\"Guid\":\"guid-test\"},{\"Path\":\"Assets/FlightSimBootstrap/Editor/X.cs\",\"Guid\":\"guid-b\"}]}";

            BootstrapAssetIdentity[] assets = BootstrapJson.ParseAssetManifest(json);

            Assert.That(assets, Has.Length.EqualTo(1));
            Assert.That(assets[0].Path, Is.EqualTo("Assets/FlightSimPlatform/Config.asset"));
            Assert.That(assets[0].Guid, Is.EqualTo("guid-a"));
            Assert.Throws<InvalidDataException>(() => BootstrapJson.ParseAssetManifest("{\"Assets\":[{\"Path\":\"Assets/FlightSimPlatform/Bad.asset\",\"Guid\":\"\"}]}"));
        }

        [Test]
        public void PlatformImportPreparationRunsConflictCheckBeforeRecordingOwnedAssets()
        {
            BootstrapInstallState state = new BootstrapInstallState();
            BootstrapAssetIdentity[] incoming =
            {
                new BootstrapAssetIdentity { Path = "Assets/FlightSimPlatform/Existing.asset", Guid = "same" },
                new BootstrapAssetIdentity { Path = "Assets/FlightSimPlatform/New.asset", Guid = "new" }
            };

            Assert.Throws<InvalidOperationException>(() =>
                FlightSimBootstrapInstaller.PreparePlatformImport(state, incoming, path => path.EndsWith("Existing.asset") ? "conflict" : string.Empty));
            Assert.That(state.ImportedAssets, Is.Empty);

            FlightSimBootstrapInstaller.PreparePlatformImport(state, incoming, path => path.EndsWith("Existing.asset") ? "same" : string.Empty);
            Assert.That(state.ImportedAssets, Has.Count.EqualTo(2));
            Assert.That(state.ImportedAssets[0].ExistedBeforeImport, Is.True);
            Assert.That(state.ImportedAssets[1].ExistedBeforeImport, Is.False);
            Assert.That(state.ImportedAssets[1].ImportedByInstaller, Is.True);
        }

        [Test]
        public void ExternalPlatformModeValidatesAssetsButNeverClaimsRollbackOwnership()
        {
            BootstrapInstallState state = new BootstrapInstallState();
            BootstrapAssetIdentity[] incoming = { new BootstrapAssetIdentity { Path = "Assets/FlightSimPlatform/Config.asset", Guid = "expected" } };

            FlightSimBootstrapInstaller.RecordExternallyImportedPlatform(state, incoming, path => "expected");

            Assert.That(state.Phase, Is.EqualTo(BootstrapInstallPhase.PlatformImported));
            Assert.That(state.ImportedAssets, Has.Count.EqualTo(1));
            Assert.That(state.ImportedAssets[0].ImportedByInstaller, Is.False);
            Assert.Throws<InvalidOperationException>(() =>
                FlightSimBootstrapInstaller.RecordExternallyImportedPlatform(new BootstrapInstallState(), incoming, path => string.Empty));
        }

        [Test]
        public void BackupRollbackRestoresMissingAndExistingFilesAtomically()
        {
            string manifest = Path.Combine(_root, "Packages", "manifest.json");
            string lockFile = Path.Combine(_root, "Packages", "packages-lock.json");
            string settings = Path.Combine(_root, "ProjectSettings", "ProjectSettings.asset");
            Directory.CreateDirectory(Path.GetDirectoryName(manifest));
            Directory.CreateDirectory(Path.GetDirectoryName(settings));
            File.WriteAllText(manifest, "before-manifest");
            File.WriteAllText(settings, "before-settings");

            BootstrapBackup backup = FlightSimBootstrapInstaller.CreateBackup(_root, new[] { manifest, lockFile, settings });
            File.WriteAllText(manifest, "after-manifest");
            File.WriteAllText(lockFile, "after-lock");
            File.Delete(settings);

            FlightSimBootstrapInstaller.Rollback(backup);

            Assert.That(File.ReadAllText(manifest), Is.EqualTo("before-manifest"));
            Assert.That(File.Exists(lockFile), Is.False);
            Assert.That(File.ReadAllText(settings), Is.EqualTo("before-settings"));
        }

        [Test]
        public void ResumeFromManifestUpdatedIsIdempotentAndPreservesState()
        {
            string statePath = Path.Combine(_root, "Library", "FlightSimInstall", "install-state.json");
            BootstrapInstallState initial = new BootstrapInstallState
            {
                Phase = BootstrapInstallPhase.ManifestUpdated,
                ProjectPath = _root,
                ManifestPath = Path.Combine(_root, "Packages", "manifest.json")
            };

            FlightSimBootstrapInstaller.SaveState(statePath, initial);
            BootstrapInstallState resumed = FlightSimBootstrapInstaller.LoadState(statePath);
            BootstrapInstallState afterContinue = FlightSimBootstrapInstaller.Resume(resumed, packagesResolved: false);

            Assert.That(afterContinue.Phase, Is.EqualTo(BootstrapInstallPhase.WaitingForPackages));
            Assert.That(FlightSimBootstrapInstaller.Resume(afterContinue, packagesResolved: false).Phase,
                Is.EqualTo(BootstrapInstallPhase.WaitingForPackages));
            Assert.That(FlightSimBootstrapInstaller.LoadState(statePath).Phase, Is.EqualTo(BootstrapInstallPhase.ManifestUpdated));
        }

        [Test]
        public void ManifestCheckpointIsSavedBeforeWriteAndCanResumeAfterFailure()
        {
            BootstrapInstallState state = new BootstrapInstallState { Phase = BootstrapInstallPhase.BackupCreated };
            List<string> events = new List<string>();

            Assert.Throws<IOException>(() => FlightSimBootstrapInstaller.ApplyManifestCheckpoint(
                state,
                () => events.Add("save:" + state.Phase),
                () => { events.Add("write"); throw new IOException("injected"); }));

            CollectionAssert.AreEqual(new[] { "save:ManifestUpdatePending", "write" }, events);
            Assert.That(state.Phase, Is.EqualTo(BootstrapInstallPhase.ManifestUpdatePending));

            FlightSimBootstrapInstaller.ApplyManifestCheckpoint(
                state,
                () => events.Add("save:" + state.Phase),
                () => events.Add("retry-write"));
            Assert.That(state.Phase, Is.EqualTo(BootstrapInstallPhase.ManifestUpdated));
            CollectionAssert.Contains(events, "save:ManifestUpdated");
        }

        [Test]
        public void RollbackDeletesOnlyNewInstallerOwnedAssetsWithMatchingGuid()
        {
            BootstrapInstallState state = new BootstrapInstallState
            {
                ImportedAssets = new List<BootstrapImportedAsset>
                {
                    new BootstrapImportedAsset { Path = "Assets/FlightSimPlatform/New.asset", Guid = "new-guid", ImportedByInstaller = true, ExistedBeforeImport = false },
                    new BootstrapImportedAsset { Path = "Assets/FlightSimPlatform/Existing.asset", Guid = "existing-guid", ImportedByInstaller = true, ExistedBeforeImport = true },
                    new BootstrapImportedAsset { Path = "Assets/FlightSimPlatform/External.asset", Guid = "external-guid", ImportedByInstaller = false, ExistedBeforeImport = false },
                    new BootstrapImportedAsset { Path = "Assets/FlightSimPlatform/Replaced.asset", Guid = "expected-guid", ImportedByInstaller = true, ExistedBeforeImport = false }
                }
            };
            List<string> deleted = new List<string>();

            int count = FlightSimBootstrapInstaller.DeleteImportedAssets(
                state,
                path => path.EndsWith("Replaced.asset") ? "receiver-guid" : state.ImportedAssets.Find(item => item.Path == path).Guid,
                path => { deleted.Add(path); return true; });

            Assert.That(count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] { "Assets/FlightSimPlatform/New.asset" }, deleted);
        }

        [Test]
        public void ReportUsesFingerprintAndNeverWritesPlaintextTokenInEnglishOrChinese()
        {
            const string token = "this-is-a-test-token-that-must-never-appear";
            string output = Path.Combine(_root, "report");
            BootstrapReportWriter.Write(output, new BootstrapPreflightReport
            {
                Entries = new[] { BootstrapReportEntry.Passed("SEC-TOKEN-001", "Token " + token + " detected.", "已检测到令牌 " + token) }
            }, token);

            string json = File.ReadAllText(Path.Combine(output, BootstrapConstants.JsonReportFileName));
            string markdown = File.ReadAllText(Path.Combine(output, BootstrapConstants.ChineseReportFileName));

            Assert.That(json, Does.Not.Contain(token));
            Assert.That(markdown, Does.Not.Contain(token));
            Assert.That(json, Does.Contain(BootstrapReportWriter.Fingerprint(token)));
            Assert.That(markdown, Does.Contain(BootstrapReportWriter.Fingerprint(token)));
            Assert.That(BootstrapReportWriter.Fingerprint(token), Has.Length.EqualTo(12));
        }

        [Test]
        public void DedicatedServerTokenReflectionReturnsOnlyTokenOrExceptionType()
        {
            const string token = "dedicated-server-secret";

            Assert.That(BootstrapTokenReader.TryReadToken(new FakeCesiumServer { defaultIonAccessToken = token }, out string read, out string error), Is.True);
            Assert.That(read, Is.EqualTo(token));
            Assert.That(error, Is.Empty);

            Assert.That(BootstrapTokenReader.TryReadToken(new ThrowingCesiumServer(), out read, out error), Is.False);
            Assert.That(read, Is.Empty);
            Assert.That(error, Is.EqualTo(typeof(InvalidOperationException).Name));
            Assert.That(error, Does.Not.Contain("secret-in-exception"));
        }

        [Test]
        public void ReportWithoutReadableTokenLeavesFingerprintEmpty()
        {
            string output = Path.Combine(_root, "no-token-report");
            BootstrapReportWriter.Write(output, new BootstrapPreflightReport(), null);

            string json = File.ReadAllText(Path.Combine(output, BootstrapConstants.JsonReportFileName));
            Assert.That(json, Does.Contain("\"tokenFingerprint\": \"\""));
            Assert.That(json, Does.Not.Contain("not-present"));
        }

        [Test]
        public void FailedReportEntryRetainsRepairAdviceAndRedactsToken()
        {
            const string token = "failure-path-token";
            BootstrapPreflightReport source = new BootstrapPreflightReport
            {
                Entries = new[] { BootstrapReportEntry.Passed("ENV-OK-001", "Ready", "就绪") }
            };

            BootstrapPreflightReport failed = BootstrapReportWriter.WithFailure(source, "INSTALL-FAIL-001", "Install failed for " + token, "安装失败 " + token);

            Assert.That(failed.CanContinue, Is.False);
            Assert.That(failed.ContainsFailure("INSTALL-FAIL-001"), Is.True);
            string output = Path.Combine(_root, "failed-report");
            BootstrapReportWriter.Write(output, failed, token);
            Assert.That(File.ReadAllText(Path.Combine(output, BootstrapConstants.JsonReportFileName)), Does.Not.Contain(token));
            Assert.That(File.ReadAllText(Path.Combine(output, BootstrapConstants.ChineseReportFileName)), Does.Not.Contain(token));
        }

        [Test]
        public void PlatformPackagePathComesOnlyFromItsExplicitCommandLineArgument()
        {
            Assert.That(FlightSimBootstrapInstaller.GetPlatformPackagePath(new[] { "Unity.exe", "-flightSimPlatformPackage", "C:/delivery/02-platform.unitypackage" }), Is.EqualTo("C:/delivery/02-platform.unitypackage"));
            Assert.That(FlightSimBootstrapInstaller.GetPlatformPackagePath(new[] { "Unity.exe", "-other", "value" }), Is.Empty);
        }

        [Test]
        public void ContinueArgumentsRequireManifestAndExactlyOneImportMode()
        {
            BootstrapContinueOptions import = BootstrapContinueOptions.Parse(new[]
            {
                "Unity.exe", "-flightSimAssetManifest", "C:/delivery/AssetManifest.json",
                "-flightSimPlatformPackage", "C:/delivery/platform.unitypackage"
            });
            Assert.That(import.PlatformAlreadyImported, Is.False);
            Assert.That(import.AssetManifestPath, Does.EndWith("AssetManifest.json"));
            Assert.That(import.PlatformPackagePath, Does.EndWith("platform.unitypackage"));

            BootstrapContinueOptions external = BootstrapContinueOptions.Parse(new[]
            {
                "Unity.exe", "-flightSimAssetManifest", "C:/delivery/AssetManifest.json",
                "-flightSimPlatformAlreadyImported"
            });
            Assert.That(external.PlatformAlreadyImported, Is.True);
            Assert.That(external.PlatformPackagePath, Is.Empty);

            Assert.Throws<ArgumentException>(() => BootstrapContinueOptions.Parse(new[] { "Unity.exe", "-flightSimPlatformAlreadyImported" }));
            Assert.Throws<ArgumentException>(() => BootstrapContinueOptions.Parse(new[]
            {
                "Unity.exe", "-flightSimAssetManifest", "manifest.json",
                "-flightSimPlatformPackage", "platform.unitypackage",
                "-flightSimPlatformAlreadyImported"
            }));
        }

        private sealed class FakeCesiumServer
        {
            public string defaultIonAccessToken;
        }

        private sealed class ThrowingCesiumServer
        {
            public string defaultIonAccessToken => throw new InvalidOperationException("secret-in-exception");
        }
    }
}
