using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace FlightSim.Bootstrap.Editor
{
    public sealed class BootstrapAssetIdentity
    {
        public string Path;
        public string Guid;
    }

    public static class FlightSimBootstrapInstaller
    {
        private static readonly string[] RequiredPackageNames =
        {
            BootstrapConstants.CesiumPackage,
            BootstrapConstants.TextMeshProPackage,
            BootstrapConstants.UguiPackage,
            BootstrapConstants.InputSystemPackage
        };

        [MenuItem("FlightSim/Installation/Preflight")]
        public static void PreflightMenu() { PreflightCli(); }

        [MenuItem("FlightSim/Installation/Continue Installation")]
        public static void ContinueMenu() { ContinueCli(); }

        [MenuItem("FlightSim/Installation/Validate Imported Platform")]
        public static void ValidateMenu() { ValidateCli(); }

        [MenuItem("FlightSim/Installation/Rollback Last Installation")]
        public static void RollbackMenu() { RollbackCli(); }

        public static void PreflightCli()
        {
            string projectPath = GetProjectPath();
            string statePath = GetStatePath(projectPath);
            BootstrapPreflightReport report = BootstrapEnvironmentEvaluator.Evaluate(BootstrapEnvironmentEvaluator.CaptureCurrent());
            BootstrapInstallState state = LoadRecoverablePreflightState(statePath) ?? new BootstrapInstallState
            {
                Phase = BootstrapInstallPhase.Preflight,
                ProjectPath = projectPath,
                ManifestPath = Path.Combine(projectPath, "Packages", "manifest.json")
            };
            try
            {
                if (!report.CanContinue) throw new InvalidOperationException("Preflight environment requirements were not met.");
                if (state.Phase == BootstrapInstallPhase.Preflight)
                {
                    string lockFile = Path.Combine(projectPath, "Packages", "packages-lock.json");
                    string projectSettings = Path.Combine(projectPath, "ProjectSettings", "ProjectSettings.asset");
                    string buildSettings = Path.Combine(projectPath, "ProjectSettings", "EditorBuildSettings.asset");
                    state.Backup = CreateBackup(projectPath, new[] { state.ManifestPath, lockFile, projectSettings, buildSettings });
                    state.Phase = BootstrapInstallPhase.BackupCreated;
                    SaveState(statePath, state);
                }

                ApplyManifestCheckpoint(state, () => SaveState(statePath, state), () => ApplyManifestAndInputSettings(state));
                MoveToWaitingForPackages(statePath, state);
                BootstrapReportWriter.Write(projectPath, report, null);
            }
            catch (Exception exception)
            {
                FailAndRollback(statePath, state, exception, report);
                throw;
            }
        }

        public static void PreparePlatformImportCli()
        {
            string projectPath = GetProjectPath();
            string statePath = GetStatePath(projectPath);
            BootstrapInstallState state = LoadState(statePath);
            BootstrapPreflightReport report = BootstrapEnvironmentEvaluator.Evaluate(BootstrapEnvironmentEvaluator.CaptureCurrent());
            try
            {
                if (!report.CanContinue) throw new InvalidOperationException("Platform import preparation environment requirements were not met.");
                if (state.Phase == BootstrapInstallPhase.PlatformImportPending)
                {
                    BootstrapReportWriter.Write(projectPath, report, null);
                    return;
                }
                if (state.Phase == BootstrapInstallPhase.WaitingForPackages)
                {
                    Dictionary<string, string> resolved = GetResolvedRequiredPackageVersions();
                    if (resolved.Count < RequiredPackageNames.Length)
                        throw new InvalidOperationException("Required packages are still resolving.");
                    ValidateRequiredPackageVersions(resolved);
                    state.Phase = BootstrapInstallPhase.ReadyForPlatformImport;
                    SaveState(statePath, state);
                }
                if (state.Phase != BootstrapInstallPhase.ReadyForPlatformImport)
                    throw new InvalidOperationException("Platform import can only be prepared from phase " + state.Phase + ".");

                ValidateRequiredPackageVersions(GetResolvedRequiredPackageVersions());
                BootstrapContinueOptions options = BootstrapContinueOptions.Parse(Environment.GetCommandLineArgs());
                if (options.PlatformAlreadyImported)
                    throw new InvalidOperationException("PreparePlatformImportCli requires a platform package path.");

                state.AssetManifestPath = options.AssetManifestPath;
                state.PlatformPackagePath = options.PlatformPackagePath;
                state.PlatformAlreadyImported = false;
                BootstrapAssetIdentity[] manifestAssets = LoadAssetManifest(options.AssetManifestPath);
                PreparePlatformImport(state, manifestAssets, AssetDatabase.AssetPathToGUID);
                state.Phase = BootstrapInstallPhase.PlatformImportPending;
                state.PlatformImportRequested = true;
                SaveState(statePath, state);
                BootstrapReportWriter.Write(projectPath, report, null);
            }
            catch (Exception exception)
            {
                FailAndRollback(statePath, state, exception, report);
                throw;
            }
        }

        public static void ContinueCli()
        {
            string projectPath = GetProjectPath();
            string statePath = GetStatePath(projectPath);
            BootstrapInstallState state = LoadState(statePath);
            BootstrapPreflightReport report = BootstrapEnvironmentEvaluator.Evaluate(BootstrapEnvironmentEvaluator.CaptureCurrent());
            try
            {
                if (!report.CanContinue) throw new InvalidOperationException("Continue environment requirements were not met.");
                if (state.Phase == BootstrapInstallPhase.BackupCreated || state.Phase == BootstrapInstallPhase.ManifestUpdatePending)
                {
                    ApplyManifestCheckpoint(state, () => SaveState(statePath, state), () => ApplyManifestAndInputSettings(state));
                    MoveToWaitingForPackages(statePath, state);
                }
                else if (state.Phase == BootstrapInstallPhase.ManifestUpdated)
                {
                    MoveToWaitingForPackages(statePath, state);
                }

                if (state.Phase == BootstrapInstallPhase.WaitingForPackages)
                {
                    Dictionary<string, string> resolved = GetResolvedRequiredPackageVersions();
                    if (resolved.Count < RequiredPackageNames.Length)
                    {
                        BootstrapPreflightReport waitingReport = AppendWarning(
                            report,
                            "PKG-WAIT-001",
                            "Required packages are still resolving.",
                            "所需包仍在解析中。",
                            "Run Continue Installation again after Package Manager completes.",
                            "Package Manager 完成后请再次运行 Continue Installation。");
                        SaveState(statePath, state);
                        BootstrapReportWriter.Write(projectPath, waitingReport, null);
                        return;
                    }
                    ValidateRequiredPackageVersions(resolved);
                    state.Phase = BootstrapInstallPhase.ReadyForPlatformImport;
                    SaveState(statePath, state);
                }

                if (state.Phase == BootstrapInstallPhase.ReadyForPlatformImport)
                {
                    BootstrapContinueOptions options = ResolveContinueOptions(state, Environment.GetCommandLineArgs());
                    state.AssetManifestPath = options.AssetManifestPath;
                    state.PlatformPackagePath = options.PlatformPackagePath;
                    state.PlatformAlreadyImported = options.PlatformAlreadyImported;
                    BootstrapAssetIdentity[] manifestAssets = LoadAssetManifest(options.AssetManifestPath);
                    if (options.PlatformAlreadyImported)
                    {
                        RecordExternallyImportedPlatform(state, manifestAssets, AssetDatabase.AssetPathToGUID);
                        SaveState(statePath, state);
                    }
                    else
                    {
                        if (!File.Exists(options.PlatformPackagePath)) throw new FileNotFoundException("Platform package does not exist.", options.PlatformPackagePath);
                        PreparePlatformImport(state, manifestAssets, AssetDatabase.AssetPathToGUID);
                        state.Phase = BootstrapInstallPhase.PlatformImportPending;
                        state.PlatformImportRequested = true;
                        SaveState(statePath, state);
                        AssetDatabase.ImportPackage(options.PlatformPackagePath, false);
                        return;
                    }
                }

                if (state.Phase == BootstrapInstallPhase.PlatformImportPending)
                {
                    ValidateImportedPlatformAssets(state, AssetDatabase.AssetPathToGUID);
                    state.Phase = BootstrapInstallPhase.PlatformImported;
                    SaveState(statePath, state);
                }

                if (state.Phase == BootstrapInstallPhase.PlatformImported || state.Phase == BootstrapInstallPhase.Validating)
                {
                    ValidateRequiredPackageVersions(GetResolvedRequiredPackageVersions());
                    state.Phase = BootstrapInstallPhase.Validating;
                    AppendSampleSceneIfPresent();
                    state.Phase = BootstrapInstallPhase.Completed;
                    SaveState(statePath, state);
                    WriteReportWithDedicatedToken(projectPath, report);
                    return;
                }
                if (state.Phase == BootstrapInstallPhase.Completed)
                {
                    WriteReportWithDedicatedToken(projectPath, report);
                    return;
                }
                SaveState(statePath, state);
                BootstrapReportWriter.Write(projectPath, report, null);
            }
            catch (Exception exception)
            {
                FailAndRollback(statePath, state, exception, report);
                throw;
            }
        }

        public static void ValidateCli()
        {
            string projectPath = GetProjectPath();
            string statePath = GetStatePath(projectPath);
            BootstrapInstallState state = File.Exists(statePath) ? LoadState(statePath) : new BootstrapInstallState { ProjectPath = projectPath, Phase = BootstrapInstallPhase.Validating };
            BootstrapPreflightReport report = BootstrapEnvironmentEvaluator.Evaluate(BootstrapEnvironmentEvaluator.CaptureCurrent());
            try
            {
                if (!report.CanContinue) throw new InvalidOperationException("Validation environment requirements were not met.");
                ValidateRequiredPackageVersions(GetResolvedRequiredPackageVersions());
                if (state.ImportedAssets.Count > 0) ValidateImportedPlatformAssets(state, AssetDatabase.AssetPathToGUID);
                state.Phase = BootstrapInstallPhase.Validating;
                AppendSampleSceneIfPresent();
                state.Phase = BootstrapInstallPhase.Completed;
                SaveState(statePath, state);
                WriteReportWithDedicatedToken(projectPath, report);
            }
            catch (Exception exception)
            {
                FailAndRollback(statePath, state, exception, report);
                throw;
            }
        }

        public static void RollbackCli()
        {
            string statePath = GetStatePath(GetProjectPath());
            BootstrapInstallState state = LoadState(statePath);
            Exception deletionFailure = null;
            Exception restoreFailure = null;
            try
            {
                DeleteImportedAssets(state, AssetDatabase.AssetPathToGUID, AssetDatabase.DeleteAsset);
            }
            catch (Exception exception)
            {
                deletionFailure = exception;
            }
            try
            {
                if (state.Backup != null) Rollback(state.Backup);
            }
            catch (Exception exception)
            {
                restoreFailure = exception;
            }

            if (deletionFailure != null || restoreFailure != null)
            {
                state.Phase = BootstrapInstallPhase.Failed;
                state.FailureMessage = (deletionFailure ?? restoreFailure).GetType().Name;
                SaveState(statePath, state);
                BootstrapReportWriter.Write(state.ProjectPath, BootstrapReportWriter.WithFailure(
                    new BootstrapPreflightReport(), "INSTALL-ROLLBACK-FAIL-001", "Rollback failed: " + state.FailureMessage, "回滚失败：" + state.FailureMessage), null);
                throw new InvalidOperationException("Rollback failed: " + state.FailureMessage);
            }

            state.Phase = BootstrapInstallPhase.RolledBack;
            state.FailureMessage = string.Empty;
            SaveState(statePath, state);
            BootstrapReportWriter.Write(state.ProjectPath, new BootstrapPreflightReport { Entries = new[] { BootstrapReportEntry.Passed("INSTALL-ROLLBACK-001", "Installation changes were restored.", "安装变更已恢复。") } }, null);
            AssetDatabase.Refresh();
        }

        public static BootstrapBackup CreateBackup(string projectPath, IEnumerable<string> sourcePaths)
        {
            if (projectPath == null) throw new ArgumentNullException(nameof(projectPath));
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            string directory = Path.Combine(projectPath, "Library", BootstrapConstants.InstallDirectoryName, "backups", DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + "-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(directory);
            BootstrapBackup backup = new BootstrapBackup { DirectoryPath = directory };
            int index = 0;
            foreach (string sourcePath in sourcePaths.Distinct(StringComparer.OrdinalIgnoreCase))
            {
                bool exists = File.Exists(sourcePath);
                string backupPath = Path.Combine(directory, index.ToString("D2") + ".backup");
                if (exists) File.Copy(sourcePath, backupPath, false);
                backup.Entries.Add(new BootstrapBackupEntry { SourcePath = sourcePath, BackupPath = backupPath, Existed = exists });
                index++;
            }
            return backup;
        }

        public static void Rollback(BootstrapBackup backup)
        {
            if (backup == null) throw new ArgumentNullException(nameof(backup));
            foreach (BootstrapBackupEntry entry in backup.Entries)
            {
                if (entry.Existed)
                {
                    if (!File.Exists(entry.BackupPath)) throw new FileNotFoundException("Backup file is missing.", entry.BackupPath);
                    WriteAtomically(entry.SourcePath, File.ReadAllText(entry.BackupPath));
                }
                else if (File.Exists(entry.SourcePath))
                {
                    File.Delete(entry.SourcePath);
                }
            }
        }

        public static void SaveState(string statePath, BootstrapInstallState state)
        {
            if (statePath == null) throw new ArgumentNullException(nameof(statePath));
            if (state == null) throw new ArgumentNullException(nameof(state));
            state.UpdatedUtcTicks = DateTime.UtcNow.Ticks;
            WriteAtomically(statePath, BootstrapJson.SerializeInstallState(state));
        }

        public static BootstrapInstallState LoadState(string statePath)
        {
            if (!File.Exists(statePath)) throw new FileNotFoundException("No FlightSim installation state was found.", statePath);
            return BootstrapJson.ParseInstallState(File.ReadAllText(statePath));
        }

        public static BootstrapInstallState Resume(BootstrapInstallState state, bool packagesResolved)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (state.Phase == BootstrapInstallPhase.ManifestUpdated) state.Phase = BootstrapInstallPhase.WaitingForPackages;
            if (state.Phase != BootstrapInstallPhase.WaitingForPackages || !packagesResolved) return state;
            state.Phase = BootstrapInstallPhase.ReadyForPlatformImport;
            return state;
        }

        public static bool IsRequiredPackageVersion(string packageName, string version)
        {
            if (string.Equals(packageName, BootstrapConstants.CesiumPackage, StringComparison.Ordinal)) return string.Equals(version, BootstrapConstants.CesiumVersion, StringComparison.Ordinal);
            if (string.Equals(packageName, BootstrapConstants.TextMeshProPackage, StringComparison.Ordinal)) return string.Equals(version, BootstrapConstants.TextMeshProVersion, StringComparison.Ordinal);
            if (string.Equals(packageName, BootstrapConstants.UguiPackage, StringComparison.Ordinal)) return string.Equals(version, BootstrapConstants.UguiVersion, StringComparison.Ordinal);
            if (string.Equals(packageName, BootstrapConstants.InputSystemPackage, StringComparison.Ordinal)) return string.Equals(version, BootstrapConstants.InputSystemVersion, StringComparison.Ordinal);
            return false;
        }

        public static void ValidateRequiredPackageVersions(IReadOnlyDictionary<string, string> resolvedVersions)
        {
            if (resolvedVersions == null) throw new ArgumentNullException(nameof(resolvedVersions));
            foreach (string packageName in RequiredPackageNames)
            {
                string version;
                if (!resolvedVersions.TryGetValue(packageName, out version))
                    throw new InvalidOperationException("Required package is not resolved: " + packageName + ".");
                if (!IsRequiredPackageVersion(packageName, version))
                    throw new InvalidOperationException("Required package version mismatch: " + packageName + ".");
            }
        }

        public static void ApplyManifestCheckpoint(BootstrapInstallState state, Action saveState, Action applyChanges)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (saveState == null) throw new ArgumentNullException(nameof(saveState));
            if (applyChanges == null) throw new ArgumentNullException(nameof(applyChanges));
            if (state.Phase == BootstrapInstallPhase.BackupCreated)
            {
                state.Phase = BootstrapInstallPhase.ManifestUpdatePending;
                saveState();
            }
            if (state.Phase == BootstrapInstallPhase.ManifestUpdatePending)
            {
                applyChanges();
                state.Phase = BootstrapInstallPhase.ManifestUpdated;
                saveState();
            }
            else if (state.Phase != BootstrapInstallPhase.ManifestUpdated)
            {
                throw new InvalidOperationException("Manifest checkpoint cannot run from phase " + state.Phase + ".");
            }
        }

        public static void PreparePlatformImport(
            BootstrapInstallState state,
            IEnumerable<BootstrapAssetIdentity> incomingAssets,
            Func<string, string> currentGuidAtPath)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            BootstrapAssetIdentity[] assets = incomingAssets == null ? throw new ArgumentNullException(nameof(incomingAssets)) : incomingAssets.ToArray();
            EnsureNoConflictingAssetGuids(assets, currentGuidAtPath);
            List<BootstrapImportedAsset> ledger = new List<BootstrapImportedAsset>();
            foreach (BootstrapAssetIdentity asset in assets)
            {
                string currentGuid = currentGuidAtPath(asset.Path) ?? string.Empty;
                ledger.Add(new BootstrapImportedAsset
                {
                    Path = asset.Path,
                    Guid = asset.Guid,
                    ExistedBeforeImport = !string.IsNullOrEmpty(currentGuid),
                    ImportedByInstaller = true
                });
            }
            state.ImportedAssets = ledger;
            state.PlatformAlreadyImported = false;
        }

        public static void RecordExternallyImportedPlatform(
            BootstrapInstallState state,
            IEnumerable<BootstrapAssetIdentity> incomingAssets,
            Func<string, string> currentGuidAtPath)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (incomingAssets == null) throw new ArgumentNullException(nameof(incomingAssets));
            if (currentGuidAtPath == null) throw new ArgumentNullException(nameof(currentGuidAtPath));
            List<BootstrapImportedAsset> ledger = new List<BootstrapImportedAsset>();
            foreach (BootstrapAssetIdentity asset in incomingAssets)
            {
                string currentGuid = currentGuidAtPath(asset.Path) ?? string.Empty;
                if (!string.Equals(currentGuid, asset.Guid, StringComparison.Ordinal))
                    throw new InvalidOperationException("Externally imported asset is missing or has a different GUID at " + asset.Path + ".");
                ledger.Add(new BootstrapImportedAsset
                {
                    Path = asset.Path,
                    Guid = asset.Guid,
                    ExistedBeforeImport = true,
                    ImportedByInstaller = false
                });
            }
            state.ImportedAssets = ledger;
            state.PlatformAlreadyImported = true;
            state.PlatformImportRequested = false;
            state.Phase = BootstrapInstallPhase.PlatformImported;
        }

        public static void ValidateImportedPlatformAssets(BootstrapInstallState state, Func<string, string> currentGuidAtPath)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (currentGuidAtPath == null) throw new ArgumentNullException(nameof(currentGuidAtPath));
            foreach (BootstrapImportedAsset asset in state.ImportedAssets ?? new List<BootstrapImportedAsset>())
            {
                string currentGuid = currentGuidAtPath(asset.Path) ?? string.Empty;
                if (!string.Equals(currentGuid, asset.Guid, StringComparison.Ordinal))
                    throw new InvalidOperationException("Imported asset is missing or has a different GUID at " + asset.Path + ".");
            }
        }

        public static int DeleteImportedAssets(
            BootstrapInstallState state,
            Func<string, string> currentGuidAtPath,
            Func<string, bool> deleteAsset)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (currentGuidAtPath == null) throw new ArgumentNullException(nameof(currentGuidAtPath));
            if (deleteAsset == null) throw new ArgumentNullException(nameof(deleteAsset));
            int deleted = 0;
            IEnumerable<BootstrapImportedAsset> assets = state.ImportedAssets ?? new List<BootstrapImportedAsset>();
            foreach (BootstrapImportedAsset asset in assets.OrderByDescending(item => item.Path, StringComparer.Ordinal))
            {
                if (!asset.ImportedByInstaller || asset.ExistedBeforeImport) continue;
                string currentGuid = currentGuidAtPath(asset.Path) ?? string.Empty;
                if (!string.Equals(currentGuid, asset.Guid, StringComparison.Ordinal)) continue;
                if (!deleteAsset(asset.Path)) throw new IOException("Failed to delete imported asset at " + asset.Path + ".");
                deleted++;
            }
            return deleted;
        }

        public static string GetPlatformPackagePath(string[] arguments)
        {
            if (arguments == null) return string.Empty;
            for (int index = 0; index < arguments.Length - 1; index++)
            {
                if (string.Equals(arguments[index], BootstrapConstants.PlatformPackageArgument, StringComparison.Ordinal)) return arguments[index + 1] ?? string.Empty;
            }
            return string.Empty;
        }

        public static void EnsureNoConflictingAssetGuids(IEnumerable<BootstrapAssetIdentity> incomingAssets, Func<string, string> currentGuidAtPath)
        {
            if (incomingAssets == null) throw new ArgumentNullException(nameof(incomingAssets));
            if (currentGuidAtPath == null) throw new ArgumentNullException(nameof(currentGuidAtPath));
            foreach (BootstrapAssetIdentity incoming in incomingAssets)
            {
                if (incoming == null || string.IsNullOrEmpty(incoming.Path) || string.IsNullOrEmpty(incoming.Guid)) throw new InvalidDataException("Incoming asset identity is incomplete.");
                string currentGuid = currentGuidAtPath(incoming.Path);
                if (!string.IsNullOrEmpty(currentGuid) && !string.Equals(currentGuid, incoming.Guid, StringComparison.Ordinal))
                {
                    throw new InvalidOperationException("Asset path/GUID conflict at " + incoming.Path + ".");
                }
            }
        }

        public static int ReadActiveInputHandler()
        {
            SerializedObject settings = GetProjectSettingsObject();
            SerializedProperty property = settings.FindProperty("activeInputHandler");
            return property == null ? -1 : property.intValue;
        }

        private static void SetActiveInputHandlingBoth()
        {
            SerializedObject settings = GetProjectSettingsObject();
            SerializedProperty property = settings.FindProperty("activeInputHandler");
            if (property == null) throw new InvalidOperationException("ProjectSettings.activeInputHandler was not found.");
            if (property.intValue == 2) return;
            property.intValue = 2;
            if (!settings.ApplyModifiedPropertiesWithoutUndo()) throw new InvalidOperationException("Failed to serialize Active Input Handling.");
            AssetDatabase.SaveAssets();
        }

        private static SerializedObject GetProjectSettingsObject()
        {
            UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/ProjectSettings.asset");
            if (assets == null || assets.Length == 0) throw new InvalidOperationException("ProjectSettings.asset could not be loaded for serialized editing.");
            return new SerializedObject(assets[0]);
        }

        private static void AppendSampleSceneIfPresent()
        {
            if (!File.Exists(Path.Combine(GetProjectPath(), BootstrapConstants.SampleScenePath))) return;
            EditorBuildSettingsScene[] current = EditorBuildSettings.scenes;
            if (current.Any(scene => string.Equals(scene.path, BootstrapConstants.SampleScenePath, StringComparison.Ordinal))) return;
            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(current) { new EditorBuildSettingsScene(BootstrapConstants.SampleScenePath, true) };
            EditorBuildSettings.scenes = scenes.ToArray();
        }

        private static BootstrapInstallState LoadRecoverablePreflightState(string statePath)
        {
            if (!File.Exists(statePath)) return null;
            BootstrapInstallState state = LoadState(statePath);
            return state.Phase == BootstrapInstallPhase.BackupCreated ||
                   state.Phase == BootstrapInstallPhase.ManifestUpdatePending ||
                   state.Phase == BootstrapInstallPhase.ManifestUpdated
                ? state
                : null;
        }

        private static void ApplyManifestAndInputSettings(BootstrapInstallState state)
        {
            WriteAtomically(state.ManifestPath, BootstrapJson.MergeManifest(File.ReadAllText(state.ManifestPath)));
            SetActiveInputHandlingBoth();
        }

        private static void MoveToWaitingForPackages(string statePath, BootstrapInstallState state)
        {
            if (state.Phase != BootstrapInstallPhase.ManifestUpdated) return;
            AssetDatabase.Refresh();
            state.Phase = BootstrapInstallPhase.WaitingForPackages;
            SaveState(statePath, state);
        }

        private static BootstrapContinueOptions ResolveContinueOptions(BootstrapInstallState state, string[] arguments)
        {
            bool hasExplicitArguments = arguments != null && arguments.Any(argument =>
                string.Equals(argument, BootstrapConstants.AssetManifestArgument, StringComparison.Ordinal) ||
                string.Equals(argument, BootstrapConstants.PlatformPackageArgument, StringComparison.Ordinal) ||
                string.Equals(argument, BootstrapConstants.PlatformAlreadyImportedArgument, StringComparison.Ordinal));
            if (hasExplicitArguments) return BootstrapContinueOptions.Parse(arguments);
            BootstrapContinueOptions persisted = new BootstrapContinueOptions
            {
                AssetManifestPath = state.AssetManifestPath ?? string.Empty,
                PlatformPackagePath = state.PlatformPackagePath ?? string.Empty,
                PlatformAlreadyImported = state.PlatformAlreadyImported
            };
            persisted.Validate();
            return persisted;
        }

        private static BootstrapAssetIdentity[] LoadAssetManifest(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) throw new FileNotFoundException("Distribution AssetManifest.json does not exist.", path);
            return BootstrapJson.ParseAssetManifest(File.ReadAllText(path));
        }

        private static Dictionary<string, string> GetResolvedRequiredPackageVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (string packageName in RequiredPackageNames)
            {
                UnityEditor.PackageManager.PackageInfo package = UnityEditor.PackageManager.PackageInfo.FindForPackageName(packageName);
                if (package != null) versions[packageName] = package.version;
            }
            return versions;
        }

        private static BootstrapPreflightReport AppendWarning(
            BootstrapPreflightReport report,
            string checkId,
            string message,
            string chineseMessage,
            string repairAdvice,
            string chineseRepairAdvice)
        {
            BootstrapReportEntry[] entries = new BootstrapReportEntry[report.Entries.Length + 1];
            Array.Copy(report.Entries, entries, report.Entries.Length);
            entries[entries.Length - 1] = BootstrapReportEntry.Warning(checkId, message, chineseMessage, repairAdvice, chineseRepairAdvice);
            return new BootstrapPreflightReport { Entries = entries };
        }

        private static void WriteReportWithDedicatedToken(string projectPath, BootstrapPreflightReport report)
        {
            string token = string.Empty;
            string errorType = string.Empty;
            object server = null;
            try
            {
                server = AssetDatabase.LoadMainAssetAtPath(BootstrapConstants.DedicatedCesiumServerPath);
            }
            catch (Exception exception)
            {
                errorType = exception.GetType().Name;
            }

            BootstrapReportEntry tokenEntry;
            if (string.IsNullOrEmpty(errorType) && BootstrapTokenReader.TryReadToken(server, out token, out errorType))
            {
                tokenEntry = BootstrapReportEntry.Passed("SEC-TOKEN-001", "Dedicated Cesium token fingerprint recorded.", "已记录专用 Cesium 令牌指纹。");
            }
            else
            {
                token = string.Empty;
                tokenEntry = BootstrapReportEntry.Failed(
                    "SEC-TOKEN-001",
                    "Dedicated Cesium token could not be read: " + errorType + ".",
                    "无法读取专用 Cesium 令牌：" + errorType + "。",
                    "Verify the dedicated Cesium server asset and token member.",
                    "请检查专用 Cesium server 资产及其令牌成员。");
            }
            BootstrapReportEntry[] entries = new BootstrapReportEntry[report.Entries.Length + 1];
            Array.Copy(report.Entries, entries, report.Entries.Length);
            entries[entries.Length - 1] = tokenEntry;
            BootstrapReportWriter.Write(projectPath, new BootstrapPreflightReport { Entries = entries }, token);
        }

        private static string GetProjectPath()
        {
            return Directory.GetParent(Application.dataPath).FullName;
        }

        private static string GetStatePath(string projectPath)
        {
            return Path.Combine(projectPath, "Library", BootstrapConstants.InstallDirectoryName, BootstrapConstants.InstallStateFileName);
        }

        private static void FailAndRollback(string statePath, BootstrapInstallState state, Exception exception, BootstrapPreflightReport report)
        {
            Exception rollbackFailure = null;
            try
            {
                DeleteImportedAssets(state, AssetDatabase.AssetPathToGUID, AssetDatabase.DeleteAsset);
            }
            catch (Exception deleteException)
            {
                rollbackFailure = deleteException;
            }
            try
            {
                if (state.Backup != null) Rollback(state.Backup);
            }
            catch (Exception rollbackException)
            {
                rollbackFailure = rollbackFailure ?? rollbackException;
            }
            state.Phase = rollbackFailure == null ? BootstrapInstallPhase.RolledBack : BootstrapInstallPhase.Failed;
            state.FailureMessage = rollbackFailure == null ? exception.GetType().Name : rollbackFailure.GetType().Name;
            SaveState(statePath, state);
            BootstrapReportWriter.Write(state.ProjectPath, BootstrapReportWriter.WithFailure(report, "INSTALL-FAIL-001", "Installation failed: " + exception.GetType().Name, "安装失败：" + exception.GetType().Name), null);
        }

        private static void WriteAtomically(string path, string content)
        {
            string directory = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(directory)) throw new InvalidOperationException("A destination directory is required.");
            Directory.CreateDirectory(directory);
            string temporaryPath = Path.Combine(directory, "." + Path.GetFileName(path) + "." + Guid.NewGuid().ToString("N") + ".tmp");
            File.WriteAllText(temporaryPath, content, new UTF8Encoding(false));
            if (File.Exists(path)) File.Replace(temporaryPath, path, null);
            else File.Move(temporaryPath, path);
        }
    }
}
