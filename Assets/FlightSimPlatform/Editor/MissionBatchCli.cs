using System;
using System.IO;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using FlightSim.Platform.Unity;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Editor
{
    public static class MissionBatchCli
    {
        public static void Run()
        {
            int exitCode = 0;
            try
            {
                string[] arguments = Environment.GetCommandLineArgs();
                string profile = ReadArgument(arguments, "-missionProfile", "smoke").ToLowerInvariant();
                string missionFilter = ReadArgument(arguments, "-mission", "all");
                int defaultRuns = profile == "dataset" ? 20 : profile == "regression" ? 5 : 1;
                int runs = ReadIntArgument(arguments, "-runs", defaultRuns, 1, 1000);
                int seedStart = ReadIntArgument(arguments, "-seedStart", 1000, int.MinValue, int.MaxValue);
                string output = ReadArgument(arguments, "-missionOutput", "Artifacts/Missions");
                if (!Path.IsPathRooted(output))
                    output = Path.Combine(Directory.GetParent(Application.dataPath).FullName, output);

                MissionDefinition[] missions = ResolveMissions(missionFilter);
                MissionTerrainCache terrain = RequireTerrainCacheForBatch(LoadTerrainCache());
                MissionRunReport[] reports = new MissionBatchRunner().RunBatch(
                    missions,
                    profile,
                    runs,
                    seedStart,
                    output,
                    terrain);

                for (int index = 0; index < reports.Length; index++)
                {
                    MissionRunReport report = reports[index];
                    Debug.Log(
                        $"MISSION {report.MissionId} seed={report.Seed} success={report.Succeeded} " +
                        $"time={report.FinalSimulationTimeS:F1}s rate={report.SimulationRate:F1}x " +
                        $"hash={report.DeterminismHash:X16} reason='{report.CompletionReason}'");
                    if (MissionBatchPolicy.IsFailure(profile, in report))
                        exitCode = 1;
                }

                if (profile == "regression")
                    exitCode = Math.Max(exitCode, ValidateDeterminism(reports, terrain));
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                exitCode = 2;
            }
            finally
            {
                AssetDatabase.SaveAssets();
                EditorApplication.Exit(exitCode);
            }
        }

        private static MissionDefinition[] ResolveMissions(string filter)
        {
            if (string.Equals(filter, "all", StringComparison.OrdinalIgnoreCase))
                return FlightMissionCatalog.Load().Missions.ToArray();
            MissionDefinition mission = FlightMissionCatalog.Load().Find(filter);
            if (mission == null) throw new ArgumentException($"Unknown mission '{filter}'.");
            return new[] { mission };
        }

        private static MissionTerrainCache LoadTerrainCache()
        {
            const string path = "Assets/FlightSimPlatform/Missions/Terrain/KTEX_TerrainCache.json";
            if (!File.Exists(path))
            {
                Debug.LogWarning("KTEX terrain cache is not present; batch runner is using the analytic runway elevation fallback.");
                return null;
            }
            MissionTerrainCache cache = JsonUtility.FromJson<MissionTerrainCache>(File.ReadAllText(path));
            if (cache == null || cache.HeightM == null || cache.HeightM.Length != cache.ColumnCount * cache.RowCount)
                throw new InvalidDataException($"Terrain cache '{path}' is invalid.");
            return cache;
        }

        public static MissionTerrainCache RequireTerrainCacheForBatch(MissionTerrainCache cache)
        {
            if (cache == null)
            {
                throw new FileNotFoundException(
                    "KTEX mission terrain cache is required. Run '.\\Tools\\UnityCli.ps1 terrain-cache' before mission-batch.");
            }
            if (cache.Source != TerrainSource.MissionCache || cache.ValidSampleCount <= 0)
                throw new InvalidDataException("Batch terrain must be a populated MissionCache, not an analytic fallback.");
            return cache;
        }

        private static int ValidateDeterminism(MissionRunReport[] reports, ITerrainQuery terrain)
        {
            var runner = new MissionBatchRunner();
            bool deterministic = ValidateReplayDeterminism(
                reports,
                (missionId, seed) =>
                {
                    MissionDefinition mission = FlightMissionCatalog.Load().Find(missionId);
                    if (mission == null)
                    {
                        return new MissionRunReport
                        {
                            MissionId = missionId,
                            Seed = seed,
                            HasException = true,
                            ExceptionMessage = "Mission definition was not found during deterministic replay."
                        };
                    }
                    MissionRunOptions options = MissionRunOptions.Create(seed);
                    options.WriteDetailedLogs = false;
                    return runner.RunSingle(mission, in options, terrain);
                },
                out string failure);
            if (deterministic) return 0;
            Debug.LogError(failure);
            return 1;
        }

        public static bool ValidateReplayDeterminism(
            MissionRunReport[] reports,
            Func<string, int, MissionRunReport> replay,
            out string failure)
        {
            if (reports == null || replay == null)
            {
                failure = "Determinism validation requires reports and a replay function.";
                return false;
            }
            for (int index = 0; index < reports.Length; index++)
            {
                MissionRunReport expected = reports[index];
                MissionRunReport actual = replay(expected.MissionId, expected.Seed);
                if (actual.MissionId != expected.MissionId ||
                    actual.Seed != expected.Seed ||
                    actual.Succeeded != expected.Succeeded ||
                    actual.TimedOut != expected.TimedOut ||
                    actual.HasException != expected.HasException ||
                    actual.FinalSimulationTimeS != expected.FinalSimulationTimeS ||
                    actual.DeterminismHash != expected.DeterminismHash)
                {
                    failure =
                        $"Determinism mismatch for {expected.MissionId}, seed {expected.Seed}: " +
                        $"expected {expected.DeterminismHash:X16} at {expected.FinalSimulationTimeS:R}s, " +
                        $"replayed {actual.DeterminismHash:X16} at {actual.FinalSimulationTimeS:R}s.";
                    return false;
                }
            }
            failure = string.Empty;
            return true;
        }

        private static string ReadArgument(string[] arguments, string name, string fallback)
        {
            for (int index = 0; index < arguments.Length - 1; index++)
                if (string.Equals(arguments[index], name, StringComparison.OrdinalIgnoreCase))
                    return arguments[index + 1];
            return fallback;
        }

        private static int ReadIntArgument(string[] arguments, string name, int fallback, int minimum, int maximum)
        {
            string value = ReadArgument(arguments, name, fallback.ToString());
            if (!int.TryParse(value, out int result) || result < minimum || result > maximum)
                throw new ArgumentOutOfRangeException(name, value, $"Expected an integer from {minimum} to {maximum}.");
            return result;
        }
    }
}
