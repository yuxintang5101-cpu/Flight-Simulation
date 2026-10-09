using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FlightSim.Platform.Contracts;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Editor
{
    public sealed class DataDictionaryRow
    {
        public readonly string[] Columns;

        public DataDictionaryRow(params string[] columns)
        {
            if (columns == null || columns.Length != DataDictionaryGenerator.ColumnCount)
                throw new ArgumentException("A data dictionary row must contain exactly 22 columns.", nameof(columns));
            Columns = columns;
        }
    }

    public static class DataDictionaryGenerator
    {
        public const int ColumnCount = 22;

        private static readonly string[] Headers =
        {
            "数据域", "中文名称", "英文字段路径", "C# 类型", "JSON 类型", "UDP 编码类型", "单位",
            "最小值", "最大值", "枚举值", "是否可空", "有效性字段", "更新频率", "数据来源子系统",
            "Unity 内部访问接口", "UDP 消息类型与字段", "NDJSON 类型与字段", "CSV 文件与列", "示例值",
            "首次引入版本", "中文语义说明", "注意事项"
        };

        private static readonly Dictionary<string, string> WordTranslations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Aircraft"] = "飞机", ["Identity"] = "身份", ["Id"] = "标识", ["Callsign"] = "呼号",
            ["Side"] = "阵营", ["Role"] = "角色", ["Simulation"] = "仿真", ["Visual"] = "视觉",
            ["Model"] = "模型", ["Tick"] = "节拍", ["Time"] = "时间", ["Position"] = "位置",
            ["Velocity"] = "速度", ["Angular"] = "角", ["Quaternion"] = "四元数", ["Longitude"] = "经度",
            ["Latitude"] = "纬度", ["Height"] = "高度", ["Heading"] = "航向", ["Pitch"] = "俯仰",
            ["Roll"] = "滚转", ["True"] = "真", ["Calibrated"] = "校准", ["Air"] = "空中",
            ["Ground"] = "地面", ["Speed"] = "速度", ["Mach"] = "马赫数", ["Angle"] = "角度",
            ["Attack"] = "迎角", ["Sideslip"] = "侧滑", ["Normal"] = "法向", ["Load"] = "载荷",
            ["Factor"] = "系数", ["Sea"] = "海平面", ["Level"] = "级别", ["Altitude"] = "高度",
            ["Above"] = "高于", ["Climb"] = "爬升", ["Terrain"] = "地形", ["Sample"] = "采样",
            ["Valid"] = "有效", ["Age"] = "时效", ["Flight"] = "飞行", ["Control"] = "控制",
            ["Active"] = "当前", ["Authority"] = "控制权", ["Command"] = "指令", ["Aileron"] = "副翼",
            ["Elevator"] = "升降舵", ["Rudder"] = "方向舵", ["Deflection"] = "偏转", ["Leading"] = "前缘",
            ["Trailing"] = "后缘", ["Flap"] = "襟翼", ["Limiter"] = "限制器", ["Computer"] = "计算机",
            ["Enabled"] = "启用", ["Autopilot"] = "自动驾驶", ["Engaged"] = "接通", ["Propulsion"] = "推进",
            ["Engine"] = "发动机", ["Running"] = "运行", ["Afterburner"] = "加力", ["Exhaust"] = "排气",
            ["Gas"] = "燃气", ["Temperature"] = "温度", ["Fuel"] = "燃油", ["Flow"] = "流量",
            ["Nozzle"] = "喷口", ["Thrust"] = "推力", ["Internal"] = "内部", ["External"] = "外部",
            ["Left"] = "左", ["Right"] = "右", ["Total"] = "总", ["Transfer"] = "传输",
            ["Imbalance"] = "不平衡", ["Bingo"] = "返航油量", ["Joker"] = "预警油量", ["Center"] = "中心",
            ["Gravity"] = "重力", ["Low"] = "低", ["Warning"] = "告警", ["Pump"] = "泵",
            ["Electrical"] = "电气", ["Bus"] = "母线", ["Voltage"] = "电压", ["Essential"] = "关键",
            ["Avionics"] = "航电", ["Battery"] = "电池", ["Generator"] = "发电机", ["Current"] = "当前",
            ["Powered"] = "供电", ["Online"] = "在线", ["Hydraulic"] = "液压", ["Hydraulics"] = "液压",
            ["System"] = "系统", ["Systems"] = "系统", ["State"] = "状态", ["States"] = "状态",
            ["Pressure"] = "压力", ["Brake"] = "刹车", ["Landing"] = "着陆", ["Gear"] = "起落架",
            ["Nose"] = "前", ["Main"] = "主", ["Weight"] = "重量", ["Wheel"] = "机轮",
            ["Steering"] = "转向", ["Store"] = "挂载", ["Stores"] = "挂载", ["Station"] = "挂点", ["Selected"] = "已选",
            ["Master"] = "主", ["Arm"] = "保险", ["Ready"] = "就绪", ["Quantity"] = "数量",
            ["Type"] = "类型", ["Ins"] = "惯导", ["Aligned"] = "对准", ["Waypoint"] = "航路点",
            ["Radar"] = "雷达", ["Data"] = "数据", ["Link"] = "链路", ["Navigation"] = "导航",
            ["Sensor"] = "传感器", ["Caution"] = "注意", ["Stall"] = "失速", ["Overspeed"] = "超速",
            ["Code"] = "代码", ["Latched"] = "锁存", ["Tactical"] = "态势", ["Track"] = "航迹",
            ["Count"] = "数量", ["Formation"] = "编队", ["Leader"] = "长机", ["Slot"] = "槽位",
            ["Mode"] = "模式", ["Error"] = "误差", ["Combat"] = "空战", ["Target"] = "目标",
            ["Range"] = "距离", ["Closure"] = "闭合", ["Bearing"] = "方位", ["Elevation"] = "仰角",
            ["Aspect"] = "进入角", ["Lock"] = "锁定", ["Quality"] = "质量", ["Launch"] = "发射",
            ["Zone"] = "区", ["Shoot"] = "发射", ["Cue"] = "提示", ["Missile"] = "导弹",
            ["Mission"] = "任务", ["Run"] = "运行", ["Schema"] = "架构", ["Seed"] = "随机种子",
            ["Phase"] = "阶段", ["Result"] = "结果", ["Elapsed"] = "已用", ["Remaining"] = "剩余",
            ["Objective"] = "目标", ["Succeeded"] = "成功", ["Failed"] = "失败", ["Completion"] = "完成",
            ["Reason"] = "原因", ["Actor"] = "参与方", ["Status"] = "状态", ["Ai"] = "人工智能",
            ["Skill"] = "能力", ["Detected"] = "已探测", ["Progress"] = "进度", ["Deadline"] = "截止时间",
            ["Automation"] = "自动运行", ["Batch"] = "批次", ["Wall"] = "墙钟", ["Rate"] = "倍率",
            ["Source"] = "来源", ["Report"] = "报告", ["Event"] = "事件", ["Message"] = "消息",
            ["Shooter"] = "发射方", ["Weapon"] = "武器", ["Impact"] = "命中", ["Outcome"] = "结果",
            ["Sequence"] = "序号", ["Health"] = "健康", ["Dropped"] = "丢弃", ["Last"] = "最后",
            ["Updated"] = "更新", ["Connected"] = "连接", ["Domain"] = "数据域", ["Snapshot"] = "快照",
            ["Is"] = "是否", ["Has"] = "是否包含", ["Of"] = "", ["To"] = "至", ["Index"] = "序号",
            ["Path"] = "路径", ["Normalized"] = "归一化值", ["Startup"] = "启动", ["Preset"] = "预设",
            ["Hud"] = "HUD", ["Warnings"] = "告警", ["Controls"] = "控制", ["Objectives"] = "目标",
            ["Actors"] = "参与方", ["Fast"] = "快速状态", ["Aoa"] = "迎角", ["Agl"] = "离地高度",
            ["Msl"] = "平均海平面", ["Ecef"] = "地心地固坐标", ["Frd"] = "机体前右下坐标",
            ["Kcas"] = "校准空速", ["Tas"] = "真空速", ["Cas"] = "校准空速", ["Gps"] = "GPS",
            ["Rad"] = "弧度", ["Radps"] = "弧度每秒", ["Mps"] = "米每秒", ["Kg"] = "千克",
            ["Kgps"] = "千克每秒", ["Pa"] = "帕", ["Percent"] = "百分比", ["Bytes"] = "字节",
            ["M"] = "米", ["S"] = "秒", ["A"] = "安培", ["V"] = "伏特", ["N"] = "牛顿",
            ["XM"] = "X（米）", ["YM"] = "Y（米）", ["ZM"] = "Z（米）", ["Body"] = "机体",
            ["Airspeed"] = "空速", ["Steerpoint"] = "导航点", ["Desired"] = "期望", ["Offset"] = "偏移",
            ["Available"] = "可用", ["Canopy"] = "座舱盖", ["Capacity"] = "容量", ["Cg"] = "重心",
            ["Deviation"] = "偏差", ["Edge"] = "缘", ["Ellipsoid"] = "椭球", ["Engagement"] = "交战",
            ["Glideslope"] = "下滑道", ["Lateral"] = "横向", ["Localizer"] = "航向道", ["Name"] = "名称",
            ["On"] = "接通", ["Tcp"] = "TCP", ["Udp"] = "UDP", ["Vertical"] = "垂直", ["Wheels"] = "机轮",
            ["Affiliation"] = "敌我属性", ["Armed"] = "已武装", ["Azimuth"] = "方位角", ["Barometric"] = "气压",
            ["Brightness"] = "亮度", ["Circle"] = "圆", ["Classification"] = "分类", ["Client"] = "客户端",
            ["Clock"] = "时钟", ["Coefficient"] = "系数", ["Confidence"] = "置信度", ["Continuous"] = "连续",
            ["Cutout"] = "抑制", ["Declutter"] = "去杂波", ["Disconnects"] = "断开次数", ["Display"] = "显示",
            ["Distance"] = "距离", ["Down"] = "放下", ["Drag"] = "阻力", ["Drift"] = "偏流", ["Failure"] = "故障",
            ["Fire"] = "发射", ["Force"] = "力", ["Forward"] = "前向", ["Frames"] = "帧", ["Go"] = "可用",
            ["Great"] = "大", ["Ils"] = "ILS", ["In"] = "处于", ["Locked"] = "已锁定", ["Longitudinal"] = "纵向",
            ["Mac"] = "平均气动弦", ["Marker"] = "标记", ["Mass"] = "质量", ["Maximum"] = "最大", ["Mean"] = "平均",
            ["Newest"] = "最新", ["Oldest"] = "最早", ["Overflow"] = "溢出", ["Player"] = "玩家",
            ["Recorded"] = "记录值", ["Rejected"] = "拒绝", ["Released"] = "已释放", ["Required"] = "要求",
            ["Scale"] = "比例", ["Show"] = "显示", ["Slant"] = "斜距", ["Stations"] = "挂点", ["Tracks"] = "航迹",
            ["Up"] = "上", ["Version"] = "版本", ["Waypoints"] = "航路点", ["Yaw"] = "偏航"
        };

        private static readonly Dictionary<string, string> ExactFieldTranslations = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["ActiveControlAuthority"] = "当前控制权",
            ["AiMode"] = "AI 模式",
            ["AiSkillNormalized"] = "AI 能力归一化值",
            ["AboveGroundLevelAltitudeM"] = "离地高度（米）",
            ["AngleOfAttackRad"] = "迎角（弧度）",
            ["AngleOfAttackLimiterActive"] = "迎角限制器是否生效",
            ["CurrentWaypointIndex"] = "当前航路点序号",
            ["GeneratorCurrentA"] = "发电机电流（安培）",
            ["GForceLimiterActive"] = "过载限制器是否生效",
            ["Hud"] = "HUD 状态",
            ["HudEnabled"] = "HUD 是否启用",
            ["HudMode"] = "HUD 模式",
            ["IsDetected"] = "是否已探测",
            ["IsEngaged"] = "是否已交战",
            ["IsValid"] = "数据是否有效",
            ["InLaunchZone"] = "是否在发射区内",
            ["LeaderAircraft"] = "长机标识",
            ["MasterModeAirToAir"] = "空对空主模式",
            ["CenterOfGravityPercentMac"] = "重心位置（% MAC）",
            ["PlayerControlAuthority"] = "玩家飞机控制权",
            ["ReportPath"] = "报告路径",
            ["SimulationTimeS"] = "仿真时间（秒）",
            ["StartupState"] = "启动状态",
            ["WallClockTimeS"] = "墙钟时间（秒）"
        };

        [MenuItem("FlightSim/Distribution/Generate Chinese Data Dictionary")]
        public static void Generate()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            WriteMarkdownAndCsv(Path.Combine(projectRoot, "Distribution", "Docs-zh-CN"));
            WriteMarkdownAndCsv(Path.Combine(projectRoot, "Assets", "FlightSimPlatform", "Docs", "Integration"));
            AssetDatabase.Refresh();
        }

        public static void GenerateCli()
        {
            Generate();
        }

        public static IReadOnlyList<DataDictionaryRow> BuildRows()
        {
            Type[] roots = GetRootTypes().Where(type => type != null).ToArray();
            var contexts = BuildTypeContexts(roots);
            var rows = new List<DataDictionaryRow>();
            foreach (KeyValuePair<Type, DataTypeContext> pair in contexts.OrderBy(pair => pair.Key.Name, StringComparer.Ordinal))
            {
                Type type = pair.Key;
                DataTypeContext context = pair.Value;
                foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public).OrderBy(field => field.MetadataToken))
                    rows.Add(BuildRow(type, field, context));
            }
            return rows.OrderBy(row => row.Columns[0], StringComparer.Ordinal)
                .ThenBy(row => row.Columns[2], StringComparer.Ordinal)
                .ToArray();
        }

        public static void WriteMarkdownAndCsv(string outputDirectory)
        {
            Directory.CreateDirectory(outputDirectory);
            IReadOnlyList<DataDictionaryRow> rows = BuildRows();
            string csv = BuildCsv(rows);
            string markdown = BuildMarkdown(rows);
            var utf8 = new UTF8Encoding(false);
            File.WriteAllText(Path.Combine(outputDirectory, "05-完整数据字典.csv"), csv, utf8);
            File.WriteAllText(Path.Combine(outputDirectory, "05-完整数据字典.md"), markdown, utf8);
            File.WriteAllText(Path.Combine(outputDirectory, "data-dictionary-v2.csv"), csv, utf8);
            File.WriteAllText(Path.Combine(outputDirectory, "data-dictionary-v2.md"), markdown, utf8);
        }

        private static Type[] GetRootTypes()
        {
            var roots = new List<Type>
            {
                typeof(AircraftIdentityState), typeof(AircraftSnapshot), typeof(AircraftFastState), typeof(AircraftSystemsState),
                typeof(TacticalPictureState), typeof(MissionState), typeof(MissionActorState),
                typeof(MissionObjectiveState), typeof(AircraftCombatState), typeof(AutomationRunState),
                typeof(MissionSnapshot), typeof(SimulationEvent), typeof(MissionEvent), typeof(WeaponEngagementEvent)
            };
            AddOptionalType(roots, "FlightSim.Platform.Data.AircraftDataSnapshot, FlightSim.Data");
            AddOptionalType(roots, "FlightSim.Platform.Data.FlightDataHealth, FlightSim.Data");
            AddOptionalType(roots, "FlightSim.Platform.Data.PlatformDataEvent, FlightSim.Data");
            AddOptionalType(roots, "FlightSim.Platform.Integration.TelemetryPublisherHealth, FlightSim.Integration");
            return roots.ToArray();
        }

        private static void AddOptionalType(ICollection<Type> roots, string assemblyQualifiedName)
        {
            Type type = Type.GetType(assemblyQualifiedName, false);
            if (type != null)
                roots.Add(type);
        }

        private static Dictionary<Type, DataTypeContext> BuildTypeContexts(IEnumerable<Type> roots)
        {
            var contexts = new Dictionary<Type, DataTypeContext>();
            var queue = new Queue<KeyValuePair<Type, DataTypeContext>>();
            foreach (Type root in roots)
            {
                DataTypeContext context = CreateRootContext(root);
                queue.Enqueue(new KeyValuePair<Type, DataTypeContext>(root, context));
            }

            while (queue.Count > 0)
            {
                KeyValuePair<Type, DataTypeContext> item = queue.Dequeue();
                if (contexts.ContainsKey(item.Key))
                    continue;
                contexts[item.Key] = item.Value;
                foreach (FieldInfo field in item.Key.GetFields(BindingFlags.Instance | BindingFlags.Public))
                {
                    Type child = field.FieldType.IsArray ? field.FieldType.GetElementType() : field.FieldType;
                    if (child != null && IsStructuredPlatformType(child))
                        queue.Enqueue(new KeyValuePair<Type, DataTypeContext>(child, item.Value));
                }
            }
            return contexts;
        }

        private static bool IsStructuredPlatformType(Type type)
        {
            return !type.IsEnum && type != typeof(AircraftId) &&
                   (type.Namespace == typeof(AircraftFastState).Namespace || type.Namespace == "FlightSim.Platform.Data");
        }

        private static DataTypeContext CreateRootContext(Type type)
        {
            string name = type.Name;
            if (name.Contains("Fast")) return new DataTypeContext("flight", "50 Hz", "飞行动力学", "FastState", "flight.csv", "IFlightDataHub.TryGetAircraft");
            if (name.Contains("Systems")) return new DataTypeContext("systems", "10 Hz", "机电与飞机系统", "SystemsState", "systems.csv", "IFlightDataHub.TryGetAircraft");
            if (name.Contains("Tactical")) return new DataTypeContext("tactical", "5 Hz", "战术态势", "TacticalPictureState", "tactical.csv", "IFlightDataHub.TryGetAircraft");
            if (name.Contains("Combat")) return new DataTypeContext("combat", "10 Hz", "空战航电", "AircraftCombatState", "combat.csv", "IFlightDataHub.TryGetCombat");
            if (name.Contains("MissionActor")) return new DataTypeContext("actors", "5 Hz", "任务编排", "MissionActorState", "actors.csv", "IFlightDataHub.TryGetMission");
            if (name.Contains("Objective")) return new DataTypeContext("objectives", "5 Hz", "任务编排", "MissionObjectiveState", "objectives.csv", "IFlightDataHub.TryGetMission");
            if (name.Contains("MissionState")) return new DataTypeContext("mission", "5 Hz", "任务编排", "MissionState", "mission.csv", "IFlightDataHub.TryGetMission");
            if (name.Contains("Automation")) return new DataTypeContext("automation", "5 Hz", "自动运行", "AutomationRunState", "automation.csv", "IFlightDataHub.TryGetMission");
            if (name.Contains("Identity")) return new DataTypeContext("identity", "变化时 / 5 Hz", "飞机注册", "IdentityState", "identity.csv", "IFlightDataHub.TryGetAircraft");
            if (name.Contains("Health")) return new DataTypeContext("health", "5 Hz", "平台监控", "Health", "health.csv", "IFlightDataHub.GetHealth");
            if (name.Contains("Event")) return new DataTypeContext("events", "即时", "事件总线", name, "events.csv", "IFlightDataHub.TryReadEvent");
            return new DataTypeContext("snapshot", "随数据域", "统一数据中心", "Snapshot", "snapshots.csv", "IFlightDataHub");
        }

        private static DataDictionaryRow BuildRow(Type type, FieldInfo field, DataTypeContext context)
        {
            Type valueType = field.FieldType;
            Type scalar = valueType.IsArray ? valueType.GetElementType() : valueType;
            string fieldPath = type.Name + "." + field.Name;
            string chineseName = TranslateFieldName(field.Name);
            string unit = InferUnit(field.Name, scalar);
            string[] range = InferRange(field.Name, scalar);
            string enumValues = scalar != null && scalar.IsEnum ? string.Join(" | ", Enum.GetNames(scalar)) : "-";
            string udp = IsUdpDomain(context.UdpMessageType)
                ? context.UdpMessageType + "." + field.Name + " / " + GetUdpEncoding(valueType)
                : "未编码（仅 NDJSON/CSV）";
            string ndjsonMapping = GetNdjsonMapping(type, field, context);
            return new DataDictionaryRow(
                context.Domain,
                chineseName,
                fieldPath,
                GetCSharpTypeName(valueType),
                GetJsonType(valueType),
                GetUdpEncoding(valueType),
                unit,
                range[0],
                range[1],
                enumValues,
                valueType.IsValueType ? "否" : "是",
                ResolveValidity(type, field),
                context.Frequency,
                context.Source,
                context.UnityApi,
                udp,
                ndjsonMapping,
                context.CsvFile + ":" + field.Name,
                GetExample(field.Name, scalar),
                "2.0.0 / Contract 2",
                $"{chineseName}（{fieldPath}）由{context.Source}发布，用于只读仿真状态、界面展示或外部 Agent 分析。",
                "SI 单位；仅在有效性条件满足时使用。外部接口不可据此发送控制命令。"
            );
        }

        private static bool IsUdpDomain(string messageType)
        {
            switch (messageType)
            {
                case "FastState":
                case "SystemsState":
                case "TacticalPictureState":
                case "SimulationEvent":
                case "MissionState":
                case "MissionActorState":
                case "MissionObjectiveState":
                case "AircraftCombatState":
                case "AutomationRunState":
                case "MissionEvent":
                case "WeaponEngagementEvent":
                    return true;
                default:
                    return false;
            }
        }

        private static string GetNdjsonMapping(Type type, FieldInfo field, DataTypeContext context)
        {
            if (context.UdpMessageType == "Snapshot" || type.Name == "FlightDataHealth" || type.Name == "PlatformDataEvent")
                return "未直接发布（Unity 进程内）";

            string domain;
            switch (context.UdpMessageType)
            {
                case "IdentityState": domain = "identity"; break;
                case "FastState": domain = "fastState"; break;
                case "SystemsState": domain = "systemsState"; break;
                case "TacticalPictureState": domain = "tacticalPicture"; break;
                case "AircraftCombatState": domain = "combatState"; break;
                case "MissionState": domain = "missionState"; break;
                case "MissionActorState": domain = "missionActor"; break;
                case "MissionObjectiveState": domain = "missionObjective"; break;
                case "AutomationRunState": domain = "automationState"; break;
                case "SimulationEvent": domain = "simulationEvent"; break;
                case "MissionEvent": domain = "missionEvent"; break;
                case "WeaponEngagementEvent": domain = "weaponEngagementEvent"; break;
                case "Health": domain = "health"; break;
                default: return "未直接发布（Unity 进程内）";
            }
            string envelope = context.Frequency == "即时" ? "event" : "data";
            return envelope + "[domain=" + domain + "].payload." + field.Name;
        }

        private static string ResolveValidity(Type type, FieldInfo field)
        {
            if (field.Name.StartsWith("IsValid", StringComparison.Ordinal))
                return "自身布尔值";
            if (type.GetField("IsValid", BindingFlags.Instance | BindingFlags.Public) != null)
                return type.Name + ".IsValid";
            return "上级快照 valid / 字段定义域";
        }

        private static string GetCSharpTypeName(Type type)
        {
            if (type == typeof(double)) return "double";
            if (type == typeof(float)) return "float";
            if (type == typeof(int)) return "int";
            if (type == typeof(uint)) return "uint";
            if (type == typeof(ulong)) return "ulong";
            if (type == typeof(ushort)) return "ushort";
            if (type == typeof(bool)) return "bool";
            if (type == typeof(string)) return "string";
            if (type.IsArray) return GetCSharpTypeName(type.GetElementType()) + "[]";
            return type.Name;
        }

        private static string GetJsonType(Type type)
        {
            if (type.IsArray) return "array";
            if (type == typeof(bool)) return "boolean";
            if (type == typeof(string) || type == typeof(AircraftId) || type.IsEnum) return "string";
            if (type.IsPrimitive || type == typeof(decimal)) return "number";
            return "object";
        }

        private static string GetUdpEncoding(Type type)
        {
            if (type.IsArray) return "定长容量集合 / 元素顺序编码";
            if (type == typeof(double)) return "float64 little-endian";
            if (type == typeof(float)) return "float32 little-endian";
            if (type == typeof(ulong)) return "uint64 little-endian";
            if (type == typeof(uint)) return "uint32 little-endian";
            if (type == typeof(int) || type.IsEnum) return "int32 little-endian";
            if (type == typeof(ushort)) return "uint16 little-endian";
            if (type == typeof(bool)) return "uint8 (0/1)";
            if (type == typeof(string) || type == typeof(AircraftId)) return "UTF-8 定长或长度前缀";
            return "嵌套结构顺序编码";
        }

        private static string InferUnit(string fieldName, Type type)
        {
            if (type == typeof(bool)) return "布尔";
            if (type != null && type.IsEnum) return "枚举";
            if (fieldName.EndsWith("Radps", StringComparison.Ordinal)) return "rad/s";
            if (fieldName.EndsWith("Rad", StringComparison.Ordinal)) return "rad";
            if (fieldName.EndsWith("Mps", StringComparison.Ordinal)) return "m/s";
            if (fieldName.EndsWith("Kgps", StringComparison.Ordinal)) return "kg/s";
            if (fieldName.EndsWith("Kg", StringComparison.Ordinal)) return "kg";
            if (fieldName.EndsWith("Pa", StringComparison.Ordinal)) return "Pa";
            if (fieldName.EndsWith("VoltageV", StringComparison.Ordinal) || fieldName.EndsWith("V", StringComparison.Ordinal)) return "V";
            if (fieldName.EndsWith("CurrentA", StringComparison.Ordinal) || fieldName.EndsWith("A", StringComparison.Ordinal)) return "A";
            if (fieldName.EndsWith("ThrustN", StringComparison.Ordinal) || fieldName.EndsWith("N", StringComparison.Ordinal)) return "N";
            if (fieldName.EndsWith("TemperatureC", StringComparison.Ordinal)) return "degC";
            if (fieldName.EndsWith("Percent", StringComparison.Ordinal)) return "%";
            if (fieldName.EndsWith("Normalized", StringComparison.Ordinal)) return "0..1";
            if (fieldName.EndsWith("TimeS", StringComparison.Ordinal) || fieldName.EndsWith("DurationS", StringComparison.Ordinal) || fieldName.EndsWith("AgeS", StringComparison.Ordinal)) return "s";
            if (fieldName.EndsWith("M", StringComparison.Ordinal)) return "m";
            if (fieldName.EndsWith("G", StringComparison.Ordinal)) return "g";
            if (fieldName.EndsWith("Bytes", StringComparison.Ordinal)) return "byte";
            if (fieldName.EndsWith("Id", StringComparison.Ordinal) || fieldName.Contains("Aircraft")) return "标识";
            return "无量纲";
        }

        private static string[] InferRange(string fieldName, Type type)
        {
            if (type == typeof(bool)) return new[] { "false", "true" };
            if (type != null && type.IsEnum) return new[] { "见枚举", "见枚举" };
            if (fieldName.EndsWith("Normalized", StringComparison.Ordinal)) return new[] { "0", "1" };
            if (fieldName.Contains("CommandNormalized")) return new[] { "-1", "1" };
            if (fieldName.Contains("Count") || fieldName.Contains("Quantity") || fieldName.Contains("Index")) return new[] { "0 或 -1（未选择）", "由容量定义" };
            return new[] { "由模型定义", "由模型定义" };
        }

        private static string GetExample(string fieldName, Type type)
        {
            if (type == typeof(bool)) return "true";
            if (type != null && type.IsEnum) return Enum.GetNames(type).FirstOrDefault() ?? "0";
            if (type == typeof(string) || type == typeof(AircraftId)) return fieldName.Contains("Aircraft") ? "VIPER-01" : "示例";
            if (fieldName.EndsWith("Rad", StringComparison.Ordinal)) return "0.523599";
            if (fieldName.EndsWith("Mps", StringComparison.Ordinal)) return "150.0";
            if (fieldName.EndsWith("M", StringComparison.Ordinal)) return "2765.0";
            if (fieldName.EndsWith("Normalized", StringComparison.Ordinal)) return "0.75";
            return "0";
        }

        private static string TranslateFieldName(string fieldName)
        {
            if (ExactFieldTranslations.TryGetValue(fieldName, out string exact))
                return exact;

            string normalized = fieldName.Replace("AngleOfAttack", "Aoa");
            string[] words = SplitPascalCase(normalized);
            var builder = new StringBuilder();
            for (int i = 0; i < words.Length; i++)
            {
                if (WordTranslations.TryGetValue(words[i], out string translated)) builder.Append(translated);
                else builder.Append(words[i]);
            }
            return builder.ToString();
        }

        private static string[] SplitPascalCase(string value)
        {
            if (string.IsNullOrEmpty(value)) return Array.Empty<string>();
            var words = new List<string>();
            int start = 0;
            for (int i = 1; i < value.Length; i++)
            {
                bool boundary = char.IsUpper(value[i]) &&
                                (!char.IsUpper(value[i - 1]) || (i + 1 < value.Length && char.IsLower(value[i + 1])));
                if (!boundary) continue;
                words.Add(value.Substring(start, i - start));
                start = i;
            }
            words.Add(value.Substring(start));
            return words.ToArray();
        }

        private static string BuildCsv(IReadOnlyList<DataDictionaryRow> rows)
        {
            var builder = new StringBuilder();
            AppendCsvRow(builder, Headers);
            for (int i = 0; i < rows.Count; i++) AppendCsvRow(builder, rows[i].Columns);
            return builder.ToString();
        }

        private static void AppendCsvRow(StringBuilder builder, IEnumerable<string> columns)
        {
            builder.AppendLine(string.Join(",", columns.Select(EscapeCsv)));
        }

        private static string EscapeCsv(string value)
        {
            return "\"" + (value ?? string.Empty).Replace("\"", "\"\"") + "\"";
        }

        private static string BuildMarkdown(IReadOnlyList<DataDictionaryRow> rows)
        {
            var builder = new StringBuilder();
            builder.AppendLine("# FlightSim 完整数据字典");
            builder.AppendLine();
            builder.AppendLine("本文档由公开契约反射生成。所有数值默认使用 SI 单位；Unity 内部 UI、UDP v2、TCP NDJSON 与离线 CSV 应按同一字段语义读取。外部 Agent 仅允许读取。完整 22 列明细见同目录 CSV。\n");
            builder.AppendLine("| 数据域 | 中文名称 | 英文字段路径 | C# 类型 | JSON 类型 | 单位 | 频率 | Unity API | NDJSON | 中文说明 |");
            builder.AppendLine("|---|---|---|---|---|---|---|---|---|---|");
            for (int i = 0; i < rows.Count; i++)
            {
                string[] c = rows[i].Columns;
                builder.Append("| ").Append(EscapeMarkdown(c[0])).Append(" | ")
                    .Append(EscapeMarkdown(c[1])).Append(" | `").Append(c[2]).Append("` | `")
                    .Append(c[3]).Append("` | ").Append(c[4]).Append(" | ").Append(c[6]).Append(" | ")
                    .Append(c[12]).Append(" | `").Append(c[14]).Append("` | `").Append(c[16]).Append("` | ")
                    .Append(EscapeMarkdown(c[20])).AppendLine(" |");
            }
            return builder.ToString();
        }

        private static string EscapeMarkdown(string value)
        {
            return (value ?? string.Empty).Replace("|", "\\|").Replace("\r", " ").Replace("\n", " ");
        }

        private static string ToCamelCase(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }

        private readonly struct DataTypeContext
        {
            public DataTypeContext(string domain, string frequency, string source, string udpMessageType, string csvFile, string unityApi)
            {
                Domain = domain;
                Frequency = frequency;
                Source = source;
                UdpMessageType = udpMessageType;
                CsvFile = csvFile;
                UnityApi = unityApi;
            }

            public string Domain { get; }
            public string Frequency { get; }
            public string Source { get; }
            public string UdpMessageType { get; }
            public string CsvFile { get; }
            public string UnityApi { get; }
        }
    }
}
