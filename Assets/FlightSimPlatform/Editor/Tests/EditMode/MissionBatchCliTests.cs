using System.IO;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using NUnit.Framework;
using UnityEngine;

namespace FlightSim.Platform.Editor.Tests
{
    public sealed class MissionBatchCliTests
    {
        [Test]
        public void RegressionDeterminismValidationReplaysEveryMissionAndSeed()
        {
            MissionRunReport[] reports =
            {
                new MissionRunReport
                {
                    MissionId = "MISSION-A",
                    Seed = 1000,
                    Succeeded = true,
                    FinalSimulationTimeS = 120.0,
                    DeterminismHash = 10
                },
                new MissionRunReport
                {
                    MissionId = "MISSION-B",
                    Seed = 1001,
                    Succeeded = true,
                    FinalSimulationTimeS = 140.0,
                    DeterminismHash = 20
                }
            };
            int replayCount = 0;

            bool deterministic = MissionBatchCli.ValidateReplayDeterminism(
                reports,
                (missionId, seed) =>
                {
                    replayCount++;
                    MissionRunReport replay = reports[seed - 1000];
                    if (missionId == "MISSION-B") replay.DeterminismHash++;
                    return replay;
                },
                out string failure);

            Assert.That(deterministic, Is.False);
            Assert.That(replayCount, Is.EqualTo(2));
            Assert.That(failure, Does.Contain("MISSION-B"));
            Assert.That(failure, Does.Contain("1001"));
        }

        [Test]
        public void BatchRequiresGeneratedTerrainCacheAndRejectsAnalyticFallback()
        {
            Assert.Throws<FileNotFoundException>(() => MissionBatchCli.RequireTerrainCacheForBatch(null));
            Assert.Throws<InvalidDataException>(() =>
                MissionBatchCli.RequireTerrainCacheForBatch(MissionTerrainCache.CreateKtexFlat()));

            MissionTerrainCache generated = MissionTerrainCache.CreateKtexFlat();
            generated.DeclaredSource = FlightSim.Platform.Contracts.TerrainSource.MissionCache;
            Assert.That(MissionBatchCli.RequireTerrainCacheForBatch(generated), Is.SameAs(generated));
        }

        [Test]
        public void KtexTerrainCacheAssetContainsDenseValidCesiumSamples()
        {
            const string path = "Assets/FlightSimPlatform/Missions/Terrain/KTEX_TerrainCache.json";
            Assert.That(File.Exists(path), Is.True, "Run Tools/UnityCli.ps1 terrain-cache.");
            MissionTerrainCache cache = JsonUtility.FromJson<MissionTerrainCache>(File.ReadAllText(path));

            Assert.That(cache, Is.Not.Null);
            Assert.That(cache.Source, Is.EqualTo(TerrainSource.MissionCache));
            Assert.That(cache.HorizontalSpacingM, Is.EqualTo(250.0));
            Assert.That(cache.HeightM.Length, Is.EqualTo(cache.ColumnCount * cache.RowCount));
            Assert.That(cache.ValidSampleCount, Is.GreaterThanOrEqualTo(cache.HeightM.Length * 0.9));
        }

        [Test]
        public void EmergencyReturnLandsUsingGeneratedKtexTerrainCache()
        {
            const string path = "Assets/FlightSimPlatform/Missions/Terrain/KTEX_TerrainCache.json";
            MissionTerrainCache cache = JsonUtility.FromJson<MissionTerrainCache>(File.ReadAllText(path));
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_EMERGENCY_RTB_01");
            MissionRunOptions options = MissionRunOptions.Create(1000);
            options.WriteDetailedLogs = false;

            MissionRunReport report = new MissionBatchRunner().RunSingle(mission, in options, cache);

            Assert.That(report.HasException, Is.False, report.ExceptionMessage);
            Assert.That(report.Succeeded, Is.True, report.CompletionReason);
            Assert.That(report.Landed, Is.True);
            Assert.That(report.TerrainSource, Is.EqualTo(TerrainSource.MissionCache));
            Assert.That(report.MinimumAglM, Is.GreaterThan(0.0));
        }

        [Test]
        public void EscortMetricsExcludePostNeutralizationAircraftDynamics()
        {
            const string path = "Assets/FlightSimPlatform/Missions/Terrain/KTEX_TerrainCache.json";
            MissionTerrainCache cache = JsonUtility.FromJson<MissionTerrainCache>(File.ReadAllText(path));
            MissionDefinition mission = DefaultMissionCatalog.Create("KTEX_ESCORT_01");
            MissionRunOptions options = MissionRunOptions.Create(1000);
            options.WriteDetailedLogs = false;

            MissionRunReport report = new MissionBatchRunner().RunSingle(mission, in options, cache);

            Assert.That(report.Succeeded, Is.True, report.CompletionReason);
            Assert.That(report.MaximumLoadFactorG, Is.LessThan(15.0));
            Assert.That(report.MinimumAglM, Is.GreaterThan(0.0));
        }
    }
}
