using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace FlightSim.Bootstrap.Editor
{
    public static class BootstrapReportWriter
    {
        public static BootstrapPreflightReport WithFailure(BootstrapPreflightReport report, string checkId, string message, string chineseMessage)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));
            BootstrapReportEntry[] entries = new BootstrapReportEntry[report.Entries.Length + 1];
            Array.Copy(report.Entries, entries, report.Entries.Length);
            entries[entries.Length - 1] = BootstrapReportEntry.Failed(checkId, message, chineseMessage, "Restore the backup and correct the reported prerequisite.", "请恢复备份并修正报告中的前置条件。");
            return new BootstrapPreflightReport { Entries = entries };
        }

        public static void Write(string outputDirectory, BootstrapPreflightReport report, string token)
        {
            if (outputDirectory == null) throw new ArgumentNullException(nameof(outputDirectory));
            if (report == null) throw new ArgumentNullException(nameof(report));
            Directory.CreateDirectory(outputDirectory);
            string fingerprint = string.IsNullOrEmpty(token) ? string.Empty : Fingerprint(token);
            File.WriteAllText(Path.Combine(outputDirectory, BootstrapConstants.JsonReportFileName), BuildJson(report, token, fingerprint), new UTF8Encoding(false));
            File.WriteAllText(Path.Combine(outputDirectory, BootstrapConstants.ChineseReportFileName), BuildChineseMarkdown(report, token, fingerprint), new UTF8Encoding(false));
        }

        public static string Fingerprint(string token)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(token ?? string.Empty));
                StringBuilder builder = new StringBuilder(12);
                for (int index = 0; index < 6; index++) builder.Append(hash[index].ToString("x2"));
                return builder.ToString();
            }
        }

        private static string BuildJson(BootstrapPreflightReport report, string token, string fingerprint)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\n  \"tokenFingerprint\": \"").Append(BootstrapJson.EscapeString(fingerprint)).Append("\",\n  \"entries\": [");
            for (int index = 0; index < report.Entries.Length; index++)
            {
                BootstrapReportEntry entry = report.Entries[index];
                builder.Append("\n    {\"status\": \"").Append(entry.Status).Append("\", \"checkId\": \"").Append(BootstrapJson.EscapeString(entry.CheckId)).Append("\", \"message\": \"").Append(BootstrapJson.EscapeString(Sanitize(entry.Message, token))).Append("\", \"repairAdvice\": \"").Append(BootstrapJson.EscapeString(Sanitize(entry.RepairAdvice, token))).Append("\"}");
                if (index < report.Entries.Length - 1) builder.Append(',');
            }
            return builder.Append("\n  ]\n}\n").ToString();
        }

        private static string BuildChineseMarkdown(BootstrapPreflightReport report, string token, string fingerprint)
        {
            StringBuilder builder = new StringBuilder("# FlightSim 导入报告\n\n令牌指纹：`").Append(fingerprint).Append("`\n\n");
            foreach (BootstrapReportEntry entry in report.Entries)
            {
                builder.Append("## ").Append(ChineseStatus(entry.Status)).Append(" · ").Append(entry.CheckId).Append("\n\n");
                builder.Append(Sanitize(entry.ChineseMessage, token)).Append("\n\n");
                if (!string.IsNullOrEmpty(entry.ChineseRepairAdvice)) builder.Append("修复建议：").Append(Sanitize(entry.ChineseRepairAdvice, token)).Append("\n\n");
            }
            return builder.ToString();
        }

        private static string Sanitize(string value, string token)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(token)) return value ?? string.Empty;
            return value.Replace(token, "***REDACTED***");
        }

        private static string ChineseStatus(BootstrapReportStatus status)
        {
            switch (status)
            {
                case BootstrapReportStatus.Passed: return "通过";
                case BootstrapReportStatus.Warning: return "警告";
                default: return "失败";
            }
        }
    }

    public static class BootstrapTokenReader
    {
        public static bool TryReadToken(object server, out string token, out string errorType)
        {
            token = string.Empty;
            errorType = string.Empty;
            if (server == null)
            {
                errorType = "DedicatedServerMissing";
                return false;
            }
            try
            {
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                Type type = server.GetType();
                PropertyInfo property = type.GetProperty("defaultIonAccessToken", flags);
                object value = property != null ? property.GetValue(server, null) : null;
                if (property == null)
                {
                    FieldInfo field = type.GetField("defaultIonAccessToken", flags);
                    if (field == null)
                    {
                        errorType = "TokenMemberMissing";
                        return false;
                    }
                    value = field.GetValue(server);
                }
                token = value as string ?? string.Empty;
                if (string.IsNullOrWhiteSpace(token))
                {
                    token = string.Empty;
                    errorType = "TokenMissing";
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                Exception safeException = exception is TargetInvocationException && exception.InnerException != null
                    ? exception.InnerException
                    : exception;
                token = string.Empty;
                errorType = safeException.GetType().Name;
                return false;
            }
        }
    }
}
