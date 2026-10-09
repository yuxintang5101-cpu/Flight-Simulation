using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FlightSim.Platform.Editor
{
    public enum ImportValidationStatus
    {
        Passed,
        Warning,
        Failed
    }

    [Serializable]
    public sealed class ImportValidationCheck
    {
        public string Id = string.Empty;
        public ImportValidationStatus Status;
        public string Summary = string.Empty;
        public string Detail = string.Empty;
        public string Remediation = string.Empty;
    }

    [Serializable]
    public sealed class ImportValidationReport
    {
        public string GeneratedUtc = string.Empty;
        public string TokenFingerprint = string.Empty;
        public ImportValidationCheck[] Checks = Array.Empty<ImportValidationCheck>();

        public bool HasFailures => Checks != null && Checks.Any(check => check.Status == ImportValidationStatus.Failed);

        public string ToJson()
        {
            return JsonUtility.ToJson(this, true);
        }

        public string ToChineseMarkdown()
        {
            var builder = new StringBuilder();
            builder.AppendLine("# FlightSim 平台导入验证报告");
            builder.AppendLine();
            builder.AppendLine("| 检查 ID | 状态 | 摘要 | 处理建议 |");
            builder.AppendLine("| --- | --- | --- | --- |");
            foreach (ImportValidationCheck check in Checks ?? Array.Empty<ImportValidationCheck>())
            {
                builder.Append("| ").Append(Escape(check.Id))
                    .Append(" | ").Append(ToChineseStatus(check.Status))
                    .Append(" | ").Append(Escape(check.Summary))
                    .Append(" | ").Append(Escape(check.Remediation))
                    .AppendLine(" |");
            }

            builder.AppendLine();
            builder.AppendLine("## 说明");
            builder.AppendLine();
            builder.AppendLine("验证报告只记录令牌指纹，不记录令牌明文。");
            if (!string.IsNullOrEmpty(TokenFingerprint))
                builder.AppendLine("令牌指纹: `" + TokenFingerprint + "`");
            return builder.ToString();
        }

        public void WriteToDirectory(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
                throw new ArgumentException("A report directory is required.", nameof(directory));

            Directory.CreateDirectory(directory);
            Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(Path.Combine(directory, "FlightSim-Import-Report.json"), ToJson(), utf8);
            File.WriteAllText(Path.Combine(directory, "FlightSim-Import-Report-zh-CN.md"), ToChineseMarkdown(), utf8);
        }

        private static string Escape(string value)
        {
            return (value ?? string.Empty).Replace("|", "\\|").Replace("\r", " ").Replace("\n", " ");
        }

        private static string ToChineseStatus(ImportValidationStatus status)
        {
            switch (status)
            {
                case ImportValidationStatus.Passed:
                    return "通过";
                case ImportValidationStatus.Warning:
                    return "警告";
                default:
                    return "失败";
            }
        }
    }

    public sealed class ValidationContext
    {
        public ValidationContext(IPlatformImportProbe probe)
        {
            Probe = probe;
        }

        public IPlatformImportProbe Probe { get; }
    }

    public struct CesiumSceneAssetProbe
    {
        public long TerrainIonAssetId;
        public long ImageryIonAssetId;
    }

    public struct F35AssetProbe
    {
        public int RendererCount;
        public bool AllMaterialSlotsAssigned;
        public bool AllShadersStandard;
        public bool AllMaterialsInF35Root;
    }

    public interface IPlatformImportProbe
    {
        string UnityVersion { get; }
        string RenderPipeline { get; }
        string TargetPlatform { get; }
        string InputHandling { get; }
        string CesiumVersion { get; }
        bool CesiumNativeLoadable { get; }
        string Token { get; }
        CesiumSceneAssetProbe CesiumSceneAssets { get; }
        F35AssetProbe F35Assets { get; }
        bool SceneReady { get; }
        string[] MissionIds { get; }
        bool FlightDataHubAvailable { get; }
        string[] HubDomains { get; }
        bool NetworkSurfaceReadOnly { get; }
    }
}
