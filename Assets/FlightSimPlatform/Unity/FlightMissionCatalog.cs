using System;
using System.Collections.Generic;
using System.IO;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    public sealed class FlightMissionCatalog
    {
        private readonly List<MissionDefinition> missions = new List<MissionDefinition>();
        private readonly List<string> issues = new List<string>();
        public IReadOnlyList<MissionDefinition> Missions => missions;
        public IReadOnlyList<string> Issues => issues;
        public static string UserMissionDirectory => Path.Combine(Application.persistentDataPath, "FlightSim", "Missions");

        public static FlightMissionCatalog Load(IEnumerable<MissionDefinitionAsset> configured = null)
        {
            var catalog = new FlightMissionCatalog();
            foreach (var mission in DefaultMissionCatalog.CreateAll()) catalog.Add(mission, false, "Built-in");
            var library = Resources.Load<FlightMissionLibrary>("FlightSim/MissionLibrary");
            if (configured != null) foreach (var asset in configured) if (asset != null) catalog.Add(asset.Definition, true, asset.name);
            if (library != null) foreach (var asset in library.Missions) if (asset != null) catalog.Add(asset.Definition, true, asset.name);
            catalog.LoadDirectory(Path.Combine(Application.streamingAssetsPath, "FlightSim", "Missions"));
            catalog.LoadDirectory(UserMissionDirectory);
            return catalog;
        }
        public bool Add(MissionDefinition definition, bool replace, string source)
        {
            MissionValidationResult check = MissionDefinitionValidator.Validate(definition);
            if (!check.IsValid) { issues.Add(source + ": " + check.FirstError); return false; }
            int index = missions.FindIndex(m => m.MissionId == definition.MissionId);
            if (index >= 0 && !replace) { issues.Add(source + ": 重复任务 ID " + definition.MissionId); return false; }
            // Runtime always owns its own definition; editor assets are never mutated by a flight.
            var copy = MissionJsonCodec.FromJson(MissionJsonCodec.ToJson(definition));
            if (index >= 0) missions[index] = copy; else missions.Add(copy);
            return true;
        }
        public MissionDefinition Find(string id) => missions.Find(m => m.MissionId == id);
        public void LoadDirectory(string directory)
        {
            if (!Directory.Exists(directory)) return;
            try
            {
                string[] files = Directory.GetFiles(directory, "*.json");
                Array.Sort(files, StringComparer.Ordinal);
                for (int i = 0; i < files.Length && i < 64; i++)
                {
                    string file = files[i];
                    try
                    {
                        if (new FileInfo(file).Length > 2 * 1024 * 1024) throw new InvalidDataException("任务文件超过 2 MB");
                        Add(MissionJsonCodec.FromJson(File.ReadAllText(file)), false, Path.GetFileName(file));
                    }
                    catch (Exception e) { issues.Add(Path.GetFileName(file) + ": " + e.Message); }
                }
                if (files.Length > 64) issues.Add("单目录最多载入 64 个任务文件：" + directory);
            }
            catch (Exception e) { issues.Add("无法读取任务目录：" + e.Message); }
        }
    }

    public static class TrainingMissionFactory
    {
        public static MissionDefinition CreateNavigationTraining()
        {
            const double rad = Math.PI / 180;
            var initial = AircraftInitialCondition.CreateAirborneDefault();
            initial.EllipsoidHeightM = 5200;
            var actor = MissionActorDefinition.Create("TRAINER-01", true, AircraftSide.Friendly, AircraftRole.Player, initial);
            actor.InitialAiMode = MissionAiMode.Navigate;
            actor.AiSkillNormalized = .8;
            actor.Route = new[] {
                Point("WP-01", -108.02, 37.98), Point("WP-02", -108.18, 38.02), Point("WP-03", -108.36, 38.07)
            };
            return new MissionDefinition {
                MissionId = "TRAINING_NAV_01", DisplayName = "基础航线训练", DefaultSeed = 4101, MaximumDurationS = 900,
                Briefing = "从 KTEX 上空进入训练航线，依次经过三个导航点。保持平稳转弯和速度；可以人工操纵，也可自动运行检查任务。此任务是可复制扩展的入门模板。",
                Actors = new[] { actor },
                Objectives = new[] { new MissionObjectiveDefinition {
                    ObjectiveId = "NAV_ROUTE", DisplayName = "依次通过三个航路点", Type = MissionObjectiveType.ReachWaypoint,
                    SubjectAircraftId = actor.AircraftId, RequiredCount = 3, IsRequired = true, TimeLimitS = 850
                } }
            };
            MissionWaypointDefinition Point(string id, double lon, double lat) => new MissionWaypointDefinition {
                WaypointId = id, LongitudeRad = lon * rad, LatitudeRad = lat * rad, EllipsoidHeightM = 5200,
                TargetTrueAirspeedMps = 230, AcceptanceRadiusM = 2500
            };
        }
    }
}
