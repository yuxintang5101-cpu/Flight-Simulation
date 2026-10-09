using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FlightSim.Platform.Contracts;
using NUnit.Framework;
using UnityEngine;

namespace FlightSim.Platform.Editor.Tests
{
    public sealed class DistributionDocumentationTests
    {
        private static string ProjectRoot => Directory.GetParent(Application.dataPath).FullName;
        private static string DocsRoot => Path.Combine(ProjectRoot, "Distribution", "Docs-zh-CN");
        private static string ExamplesRoot => Path.Combine(ProjectRoot, "Distribution", "Examples");
        private static string ImportedIntegrationDocsRoot => Path.Combine(ProjectRoot, "Assets", "FlightSimPlatform", "Docs", "Integration");

        [Test]
        public void RequiredChineseDocumentsExistAndDescribeThePlatform()
        {
            RequireSourceDeliveryDirectory(DocsRoot, "Chinese delivery documents");
            string[] required =
            {
                "00-项目简介.md", "01-安装与导入.md", "02-快速验收.md", "03-Unity二次开发.md",
                "04-外部Agent接入.md", "05-完整数据字典.md", "05-完整数据字典.csv", "06-任务与自动运行.md",
                "07-协议说明.md", "08-故障排查.md", "09-更新与卸载.md", "10-授权与第三方.md", "CHANGELOG.md"
            };
            foreach (string name in required)
                Assert.That(File.Exists(Path.Combine(DocsRoot, name)), Is.True, name);

            string introduction = File.ReadAllText(Path.Combine(DocsRoot, "00-项目简介.md"));
            Assert.That(introduction, Does.Contain("Unity"));
            Assert.That(introduction, Does.Contain("Cesium"));
            Assert.That(introduction, Does.Contain("半真实战术飞行仿真与数据平台"));
            Assert.That(introduction, Does.Contain("不是单一飞行游戏"));
        }

        [Test]
        public void InstallationGuideProvidesACompleteBeginnerWorkflow()
        {
            RequireSourceDeliveryDirectory(DocsRoot, "Chinese delivery documents");
            string guide = File.ReadAllText(Path.Combine(DocsRoot, "01-安装与导入.md"));
            string[] requiredSections =
            {
                "先看这里：你只需要完成什么",
                "第一步：确认 Unity 版本",
                "第二步：放置交付包",
                "第三步：关闭 Unity",
                "第四步：打开 PowerShell",
                "第五步：执行安装命令",
                "第六步：判断安装是否成功",
                "第七步：执行完整验收",
                "第八步：打开示例场景",
                "最常见问题",
                "给接收方的最终检查表"
            };
            foreach (string section in requiredSections)
                Assert.That(guide, Does.Contain(section), section);

            Assert.That(guide, Does.Contain("不要把交付包放进 Assets"));
            Assert.That(guide, Does.Contain("Install-FlightSim.ps1"));
            Assert.That(guide, Does.Contain("Verify-FlightSim.ps1"));
            Assert.That(guide, Does.Contain("FlightSim_KTEX_V2.unity"));
        }

        [Test]
        public void DataDictionaryHasTwentyTwoCompleteColumnsAndCoversContractFields()
        {
            string deliveryCsvPath = Path.Combine(DocsRoot, "05-完整数据字典.csv");
            string csvPath = File.Exists(deliveryCsvPath)
                ? deliveryCsvPath
                : Path.Combine(ImportedIntegrationDocsRoot, "05-完整数据字典.csv");
            Assert.That(File.Exists(csvPath), Is.True, "The generated data dictionary is missing from both source delivery and imported integration docs.");
            List<string[]> rows = ParseCsv(File.ReadAllText(csvPath));
            Assert.That(rows.Count, Is.GreaterThan(1));
            Assert.That(rows[0].Length, Is.EqualTo(22));
            for (int i = 1; i < rows.Count; i++)
            {
                Assert.That(rows[i].Length, Is.EqualTo(22), $"row {i + 1}");
                Assert.That(rows[i][0], Is.Not.Empty, $"domain at row {i + 1}");
                Assert.That(rows[i][1], Is.Not.Empty, $"Chinese name at row {i + 1}");
                Assert.That(rows[i][2], Is.Not.Empty, $"field path at row {i + 1}");
                Assert.That(rows[i][3], Is.Not.Empty, $"C# type at row {i + 1}");
                Assert.That(rows[i][4], Is.Not.Empty, $"JSON type at row {i + 1}");
                Assert.That(rows[i][6], Is.Not.Empty, $"unit at row {i + 1}");
                Assert.That(rows[i][12], Is.Not.Empty, $"frequency at row {i + 1}");
                Assert.That(rows[i][14], Is.Not.Empty, $"Unity API at row {i + 1}");
                Assert.That(rows[i][16], Is.Not.Empty, $"NDJSON mapping at row {i + 1}");
                Assert.That(rows[i][17], Is.Not.Empty, $"CSV mapping at row {i + 1}");
                Assert.That(rows[i][20], Does.Match("[\\u3400-\\u9fff]"), $"Chinese semantics at row {i + 1}");
            }

            var dictionaryPaths = new HashSet<string>(rows.Skip(1).Select(row => row[2]), StringComparer.Ordinal);
            var roots = new List<Type>
            {
                typeof(AircraftIdentityState), typeof(AircraftSnapshot), typeof(AircraftFastState), typeof(AircraftSystemsState),
                typeof(TacticalPictureState), typeof(MissionState), typeof(MissionActorState),
                typeof(MissionObjectiveState), typeof(AircraftCombatState), typeof(AutomationRunState),
                typeof(MissionSnapshot), typeof(SimulationEvent), typeof(MissionEvent), typeof(WeaponEngagementEvent)
            };
            string[] optionalTypes =
            {
                "FlightSim.Platform.Data.AircraftDataSnapshot, FlightSim.Data",
                "FlightSim.Platform.Data.FlightDataHealth, FlightSim.Data",
                "FlightSim.Platform.Data.PlatformDataEvent, FlightSim.Data",
                "FlightSim.Platform.Integration.TelemetryPublisherHealth, FlightSim.Integration"
            };
            foreach (string typeName in optionalTypes)
            {
                Type optional = Type.GetType(typeName, false);
                Assert.That(optional, Is.Not.Null, typeName);
                roots.Add(optional);
            }
            foreach (Type type in ExpandContractTypes(roots))
            {
                foreach (System.Reflection.FieldInfo field in type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                {
                    string path = type.Name + "." + field.Name;
                    Assert.That(dictionaryPaths.Contains(path), Is.True, path);
                }
            }

            AssertDictionaryMapping(rows, "MissionActorState.AiMode", "MissionActorState", "data[domain=missionActor].payload.AiMode");
            AssertDictionaryMapping(rows, "TacticalPictureState.Aircraft", "TacticalPictureState", "data[domain=tacticalPicture].payload.Aircraft");
            AssertDictionaryMapping(rows, "AircraftCombatState.Aircraft", "AircraftCombatState", "data[domain=combatState].payload.Aircraft");
            AssertDictionaryMapping(rows, "AircraftDataSnapshot.Fast", "未编码（仅 NDJSON/CSV）", "未直接发布（Unity 进程内）");
            AssertDictionaryMapping(rows, "PlatformDataEvent.Sequence", "未编码（仅 NDJSON/CSV）", "未直接发布（Unity 进程内）");

            string integrationRoot = Path.Combine(ProjectRoot, "Assets", "FlightSimPlatform", "Docs", "Integration");
            Assert.That(
                File.ReadAllText(Path.Combine(integrationRoot, "data-dictionary-v2.csv")),
                Is.EqualTo(File.ReadAllText(Path.Combine(integrationRoot, "05-完整数据字典.csv"))));
            Assert.That(
                File.ReadAllText(Path.Combine(integrationRoot, "data-dictionary-v2.md")),
                Is.EqualTo(File.ReadAllText(Path.Combine(integrationRoot, "05-完整数据字典.md"))));
        }

        [Test]
        public void ExternalExamplesExposeNoOutboundControlCalls()
        {
            RequireSourceDeliveryDirectory(ExamplesRoot, "external read-only examples");
            string[] files = Directory.GetFiles(ExamplesRoot, "*", SearchOption.AllDirectories);
            Assert.That(files.Length, Is.GreaterThanOrEqualTo(4));
            string combined = string.Join("\n", files.Select(File.ReadAllText));
            Assert.That(combined, Does.Not.Contain("sendall("));
            Assert.That(combined, Does.Not.Contain(".send("));
            Assert.That(combined, Does.Not.Contain(".WriteAsync("));
            Assert.That(combined, Does.Not.Contain("SubmitCommand"));
            Assert.That(combined, Does.Not.Contain("WeaponReleaseCommand"));
            Assert.That(combined, Does.Not.Contain("FaultInjectionCommand"));

            string[] clients =
            {
                Path.Combine(ExamplesRoot, "CSharp", "FlightSimReadOnlyClient.cs"),
                Path.Combine(ExamplesRoot, "Python", "flightsim_readonly_client.py"),
                Path.Combine(ExamplesRoot, "NodeJs", "flightsim-readonly-client.mjs")
            };
            foreach (string client in clients)
            {
                string source = File.ReadAllText(client);
                Assert.That(source, Does.Contain("hello"), client);
                Assert.That(source, Does.Contain("event"), client);
                Assert.That(source, Does.Contain("payload"), client);
            }
        }

        private static IEnumerable<Type> ExpandContractTypes(IEnumerable<Type> roots)
        {
            var queue = new Queue<Type>(roots);
            var seen = new HashSet<Type>();
            while (queue.Count > 0)
            {
                Type type = queue.Dequeue();
                if (!seen.Add(type))
                    continue;
                yield return type;
                foreach (System.Reflection.FieldInfo field in type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                {
                    Type child = field.FieldType.IsArray ? field.FieldType.GetElementType() : field.FieldType;
                    if (child != null && child.Namespace == typeof(AircraftFastState).Namespace && !child.IsEnum && child != typeof(AircraftId))
                        queue.Enqueue(child);
                }
            }
        }

        private static void RequireSourceDeliveryDirectory(string path, string description)
        {
            if (!Directory.Exists(path))
                Assert.Ignore(description + " are validated before packaging and are not imported into receiver projects.");
        }

        private static void AssertDictionaryMapping(IReadOnlyList<string[]> rows, string fieldPath, string udpContains, string ndjson)
        {
            string[] row = rows.Skip(1).Single(item => item[2] == fieldPath);
            Assert.That(row[15], Does.Contain(udpContains), fieldPath + " UDP");
            Assert.That(row[16], Is.EqualTo(ndjson), fieldPath + " NDJSON");
        }

        private static List<string[]> ParseCsv(string text)
        {
            var rows = new List<string[]>();
            var row = new List<string>();
            var field = new StringBuilder();
            bool quoted = false;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (quoted)
                {
                    if (c == '"' && i + 1 < text.Length && text[i + 1] == '"')
                    {
                        field.Append('"');
                        i++;
                    }
                    else if (c == '"') quoted = false;
                    else field.Append(c);
                }
                else if (c == '"') quoted = true;
                else if (c == ',')
                {
                    row.Add(field.ToString());
                    field.Length = 0;
                }
                else if (c == '\n')
                {
                    row.Add(field.ToString().TrimEnd('\r'));
                    field.Length = 0;
                    rows.Add(row.ToArray());
                    row.Clear();
                }
                else field.Append(c);
            }
            if (field.Length > 0 || row.Count > 0)
            {
                row.Add(field.ToString());
                rows.Add(row.ToArray());
            }
            return rows;
        }
    }
}
