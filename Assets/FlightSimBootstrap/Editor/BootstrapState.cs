using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace FlightSim.Bootstrap.Editor
{
    public enum BootstrapInstallPhase
    {
        Preflight,
        BackupCreated,
        ManifestUpdatePending,
        ManifestUpdated,
        WaitingForPackages,
        ReadyForPlatformImport,
        PlatformImportPending,
        PlatformImported,
        Validating,
        Completed,
        Failed,
        RolledBack
    }

    public sealed class BootstrapBackupEntry
    {
        public string SourcePath;
        public string BackupPath;
        public bool Existed;
    }

    public sealed class BootstrapBackup
    {
        public string DirectoryPath;
        public List<BootstrapBackupEntry> Entries = new List<BootstrapBackupEntry>();
    }

    public sealed class BootstrapInstallState
    {
        public BootstrapInstallPhase Phase;
        public string ProjectPath;
        public string ManifestPath;
        public string AssetManifestPath;
        public string PlatformPackagePath;
        public bool PlatformAlreadyImported;
        public bool PlatformImportRequested;
        public List<BootstrapImportedAsset> ImportedAssets = new List<BootstrapImportedAsset>();
        public BootstrapBackup Backup;
        public string FailureMessage;
        public long UpdatedUtcTicks;
    }

    public sealed class BootstrapImportedAsset
    {
        public string Path;
        public string Guid;
        public bool ExistedBeforeImport;
        public bool ImportedByInstaller;
    }

    public sealed class BootstrapContinueOptions
    {
        public string AssetManifestPath = string.Empty;
        public string PlatformPackagePath = string.Empty;
        public bool PlatformAlreadyImported;

        public static BootstrapContinueOptions Parse(string[] arguments)
        {
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            BootstrapContinueOptions options = new BootstrapContinueOptions
            {
                AssetManifestPath = ReadValue(arguments, BootstrapConstants.AssetManifestArgument),
                PlatformPackagePath = ReadValue(arguments, BootstrapConstants.PlatformPackageArgument),
                PlatformAlreadyImported = ContainsSwitch(arguments, BootstrapConstants.PlatformAlreadyImportedArgument)
            };
            options.Validate();
            return options;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(AssetManifestPath))
                throw new ArgumentException(BootstrapConstants.AssetManifestArgument + " <AssetManifest.json> is required.");
            bool hasPackage = !string.IsNullOrWhiteSpace(PlatformPackagePath);
            if (hasPackage == PlatformAlreadyImported)
                throw new ArgumentException("Specify exactly one of " + BootstrapConstants.PlatformPackageArgument + " <platform.unitypackage> or " + BootstrapConstants.PlatformAlreadyImportedArgument + ".");
        }

        private static string ReadValue(string[] arguments, string name)
        {
            for (int index = 0; index < arguments.Length; index++)
            {
                if (!string.Equals(arguments[index], name, StringComparison.Ordinal)) continue;
                if (index + 1 >= arguments.Length || string.IsNullOrWhiteSpace(arguments[index + 1]) || arguments[index + 1].StartsWith("-", StringComparison.Ordinal))
                    throw new ArgumentException(name + " requires a value.");
                return arguments[index + 1];
            }
            return string.Empty;
        }

        private static bool ContainsSwitch(string[] arguments, string name)
        {
            for (int index = 0; index < arguments.Length; index++)
            {
                if (string.Equals(arguments[index], name, StringComparison.Ordinal)) return true;
            }
            return false;
        }
    }

    public static class BootstrapCheckId
    {
        public const string UnityVersion = "ENV-UNITY-001";
        public const string RenderPipeline = "ENV-RENDER-001";
        public const string BuildTarget = "ENV-TARGET-001";
        public const string Host = "ENV-HOST-001";
        public const string InputHandling = "ENV-INPUT-001";
    }

    public sealed class BootstrapEnvironmentSnapshot
    {
        public string UnityVersion;
        public string ActiveBuildTarget;
        public string RenderPipelineType;
        public int ActiveInputHandler;
        public string HostPlatform;
        public bool Is64BitOperatingSystem;
        public bool Is64BitProcess;
    }

    public enum BootstrapReportStatus { Passed, Warning, Failed }

    public sealed class BootstrapReportEntry
    {
        public BootstrapReportStatus Status;
        public string CheckId;
        public string Message;
        public string ChineseMessage;
        public string RepairAdvice;
        public string ChineseRepairAdvice;

        public static BootstrapReportEntry Passed(string checkId, string message, string chineseMessage)
        {
            return new BootstrapReportEntry { Status = BootstrapReportStatus.Passed, CheckId = checkId, Message = message, ChineseMessage = chineseMessage };
        }

        public static BootstrapReportEntry Warning(string checkId, string message, string chineseMessage, string repairAdvice, string chineseRepairAdvice)
        {
            return new BootstrapReportEntry { Status = BootstrapReportStatus.Warning, CheckId = checkId, Message = message, ChineseMessage = chineseMessage, RepairAdvice = repairAdvice, ChineseRepairAdvice = chineseRepairAdvice };
        }

        public static BootstrapReportEntry Failed(string checkId, string message, string chineseMessage, string repairAdvice, string chineseRepairAdvice)
        {
            return new BootstrapReportEntry { Status = BootstrapReportStatus.Failed, CheckId = checkId, Message = message, ChineseMessage = chineseMessage, RepairAdvice = repairAdvice, ChineseRepairAdvice = chineseRepairAdvice };
        }
    }

    public sealed class BootstrapPreflightReport
    {
        public BootstrapReportEntry[] Entries = Array.Empty<BootstrapReportEntry>();
        public bool CanContinue
        {
            get
            {
                foreach (BootstrapReportEntry entry in Entries)
                {
                    if (entry.Status == BootstrapReportStatus.Failed) return false;
                }
                return true;
            }
        }

        public bool ContainsFailure(string checkId)
        {
            foreach (BootstrapReportEntry entry in Entries)
            {
                if (entry.Status == BootstrapReportStatus.Failed && string.Equals(entry.CheckId, checkId, StringComparison.Ordinal)) return true;
            }
            return false;
        }
    }

    public static class BootstrapEnvironmentEvaluator
    {
        public static BootstrapPreflightReport Evaluate(BootstrapEnvironmentSnapshot environment)
        {
            if (environment == null) throw new ArgumentNullException(nameof(environment));
            List<BootstrapReportEntry> entries = new List<BootstrapReportEntry>();
            entries.Add(string.Equals(environment.UnityVersion, BootstrapConstants.UnityVersion, StringComparison.Ordinal)
                ? BootstrapReportEntry.Passed(BootstrapCheckId.UnityVersion, "Unity Editor version is exact.", "Unity Editor 版本精确匹配。")
                : BootstrapReportEntry.Failed(BootstrapCheckId.UnityVersion, "Unity Editor must be exactly " + BootstrapConstants.UnityVersion + ".", "Unity Editor 必须精确为 " + BootstrapConstants.UnityVersion + "。", "Install or select the required Unity Editor.", "请安装或选择要求的 Unity Editor。"));

            entries.Add(string.IsNullOrEmpty(environment.RenderPipelineType)
                ? BootstrapReportEntry.Passed(BootstrapCheckId.RenderPipeline, "Built-in Render Pipeline detected.", "已检测到 Built-in Render Pipeline。")
                : BootstrapReportEntry.Failed(BootstrapCheckId.RenderPipeline, "Only Built-in Render Pipeline is supported.", "仅支持 Built-in Render Pipeline。", "Create a Built-in receiver project; do not convert materials automatically.", "请创建 Built-in 接收方工程；不得自动转换材质。"));

            entries.Add(string.Equals(environment.ActiveBuildTarget, BootstrapConstants.WindowsX64BuildTarget, StringComparison.Ordinal)
                ? BootstrapReportEntry.Passed(BootstrapCheckId.BuildTarget, "Windows x86-64 target detected.", "已检测到 Windows x86-64 目标平台。")
                : BootstrapReportEntry.Failed(BootstrapCheckId.BuildTarget, "Target must be Windows x86-64.", "目标平台必须为 Windows x86-64。", "Switch the active build target to Standalone Windows 64-bit.", "请将活动构建目标切换为 Standalone Windows 64-bit。"));

            bool windowsX64Host = string.Equals(environment.HostPlatform, "WindowsEditor", StringComparison.Ordinal) &&
                                  environment.Is64BitOperatingSystem && environment.Is64BitProcess;
            entries.Add(windowsX64Host
                ? BootstrapReportEntry.Passed(BootstrapCheckId.Host, "Windows x64 editor host detected.", "已检测到 Windows x64 Editor 主机。")
                : BootstrapReportEntry.Failed(BootstrapCheckId.Host, "Installer host must be Windows x64.", "安装器主机必须为 Windows x64。", "Run the exact Unity Editor in a 64-bit Windows process.", "请在 64 位 Windows 进程中运行指定 Unity Editor。"));

            entries.Add(environment.ActiveInputHandler == 2
                ? BootstrapReportEntry.Passed(BootstrapCheckId.InputHandling, "Active Input Handling is Both.", "Active Input Handling 已设为 Both。")
                : BootstrapReportEntry.Warning(BootstrapCheckId.InputHandling, "Active Input Handling will be set to Both.", "Active Input Handling 将设为 Both。", "Restart the editor after the setting is applied.", "设置应用后请重启编辑器。"));
            return new BootstrapPreflightReport { Entries = entries.ToArray() };
        }

        public static BootstrapEnvironmentSnapshot CaptureCurrent()
        {
            return new BootstrapEnvironmentSnapshot
            {
                UnityVersion = Application.unityVersion,
                ActiveBuildTarget = UnityEditor.EditorUserBuildSettings.activeBuildTarget.ToString(),
                RenderPipelineType = GraphicsSettings.renderPipelineAsset == null ? string.Empty : GraphicsSettings.renderPipelineAsset.GetType().FullName,
                ActiveInputHandler = FlightSimBootstrapInstaller.ReadActiveInputHandler(),
                HostPlatform = Application.platform.ToString(),
                Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,
                Is64BitProcess = Environment.Is64BitProcess
            };
        }
    }
}
