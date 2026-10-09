using System;
using System.IO;
using System.Threading.Tasks;
using CesiumForUnity;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlightSim.Platform.Editor
{
    public static class MissionTerrainCacheBuilder
    {
        public const string OutputPath = "Assets/FlightSimPlatform/Missions/Terrain/KTEX_TerrainCache.json";
        private const double EarthRadiusM = 6371008.8;
        private const double MarginM = 5000.0;
        private const double SpacingM = 250.0;
        private const int SamplesPerRequest = 4096;
        private const int ConcurrentRequests = 4;

        [MenuItem("FlightSim/Platform/Generate KTEX Mission Terrain Cache")]
        public static async void GenerateFromEditor()
        {
            await GenerateAsync(false);
        }

        public static async void GenerateCli()
        {
            await GenerateAsync(true);
        }

        private static async Task GenerateAsync(bool exitWhenDone)
        {
            int exitCode = 0;
            try
            {
                if (!File.Exists(FlightSimV2SceneBuilder.ScenePath))
                    FlightSimV2SceneBuilder.CreateOrOpenScene();
                else
                    EditorSceneManager.OpenScene(FlightSimV2SceneBuilder.ScenePath, OpenSceneMode.Single);

                Cesium3DTileset terrain = FindWorldTerrain();
                if (terrain == null)
                    throw new InvalidOperationException("Cesium World Terrain (ion asset 1) is missing from the KTEX scene.");

                ComputeBounds(out double lonMin, out double lonMax, out double latMin, out double latMax);
                double centerLatitude = (latMin + latMax) * 0.5;
                double lonMetersPerRad = EarthRadiusM * Math.Max(0.2, Math.Cos(centerLatitude));
                int columns = Math.Max(2, (int)Math.Ceiling((lonMax - lonMin) * lonMetersPerRad / SpacingM) + 1);
                int rows = Math.Max(2, (int)Math.Ceiling((latMax - latMin) * EarthRadiusM / SpacingM) + 1);
                var positions = new double3[columns * rows];
                for (int row = 0; row < rows; row++)
                {
                    double latitude = Lerp(latMin, latMax, row / (double)(rows - 1));
                    for (int column = 0; column < columns; column++)
                    {
                        double longitude = Lerp(lonMin, lonMax, column / (double)(columns - 1));
                        positions[row * columns + column] = new double3(
                            longitude * 180.0 / Math.PI,
                            latitude * 180.0 / Math.PI,
                            0.0);
                    }
                }

                var cache = new MissionTerrainCache
                {
                    MissionId = "KTEX_MULTI_MISSION_V1",
                    LongitudeMinRad = lonMin,
                    LongitudeMaxRad = lonMax,
                    LatitudeMinRad = latMin,
                    LatitudeMaxRad = latMax,
                    HorizontalSpacingM = SpacingM,
                    ColumnCount = columns,
                    RowCount = rows,
                    HeightM = new double[positions.Length],
                    SampleValid = new bool[positions.Length],
                    DeclaredSource = TerrainSource.MissionCache
                };
                Debug.Log(
                    $"Sampling {positions.Length} Cesium terrain points in requests of {SamplesPerRequest} " +
                    $"({ConcurrentRequests} concurrent)...");
                for (int waveStart = 0; waveStart < positions.Length; waveStart += SamplesPerRequest * ConcurrentRequests)
                {
                    int remaining = positions.Length - waveStart;
                    int requestCount = Math.Min(
                        ConcurrentRequests,
                        (remaining + SamplesPerRequest - 1) / SamplesPerRequest);
                    var tasks = new Task<CesiumSampleHeightResult>[requestCount];
                    var offsets = new int[requestCount];
                    for (int requestIndex = 0; requestIndex < requestCount; requestIndex++)
                    {
                        int offset = waveStart + requestIndex * SamplesPerRequest;
                        int count = Math.Min(SamplesPerRequest, positions.Length - offset);
                        var requestPositions = new double3[count];
                        Array.Copy(positions, offset, requestPositions, 0, count);
                        offsets[requestIndex] = offset;
                        tasks[requestIndex] = terrain.SampleHeightMostDetailed(requestPositions);
                    }

                    CesiumSampleHeightResult[] results = await Task.WhenAll(tasks);
                    for (int requestIndex = 0; requestIndex < results.Length; requestIndex++)
                    {
                        CesiumSampleHeightResult sampled = results[requestIndex];
                        int offset = offsets[requestIndex];
                        int count = tasks[requestIndex].Result.longitudeLatitudeHeightPositions.Length;
                        for (int sampleIndex = 0; sampleIndex < count; sampleIndex++)
                        {
                            int destination = offset + sampleIndex;
                            bool valid = sampled.sampleSuccess != null &&
                                         sampleIndex < sampled.sampleSuccess.Length &&
                                         sampled.sampleSuccess[sampleIndex];
                            cache.SampleValid[destination] = valid;
                            cache.HeightM[destination] = valid
                                ? sampled.longitudeLatitudeHeightPositions[sampleIndex].z
                                : 2765.0;
                            if (valid) cache.ValidSampleCount++;
                        }
                    }
                    Debug.Log(
                        $"KTEX terrain sampling progress: {Math.Min(positions.Length, waveStart + SamplesPerRequest * requestCount)}/" +
                        $"{positions.Length} ({cache.ValidSampleCount} valid). ");
                }
                if (cache.ValidSampleCount < positions.Length * 0.9)
                    throw new InvalidOperationException($"Only {cache.ValidSampleCount}/{positions.Length} terrain samples succeeded.");

                string directory = Path.GetDirectoryName(OutputPath);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                File.WriteAllText(OutputPath, JsonUtility.ToJson(cache, true));
                AssetDatabase.ImportAsset(OutputPath, ImportAssetOptions.ForceUpdate);
                AssetDatabase.SaveAssets();
                Debug.Log($"Wrote KTEX terrain cache to {OutputPath} ({cache.ValidSampleCount} valid samples). ");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                exitCode = 1;
            }
            finally
            {
                if (exitWhenDone) EditorApplication.Exit(exitCode);
            }
        }

        private static Cesium3DTileset FindWorldTerrain()
        {
            Cesium3DTileset[] tilesets = UnityEngine.Object.FindObjectsOfType<Cesium3DTileset>();
            for (int index = 0; index < tilesets.Length; index++)
                if (tilesets[index].ionAssetID == 1) return tilesets[index];
            return null;
        }

        private static void ComputeBounds(out double lonMin, out double lonMax, out double latMin, out double latMax)
        {
            lonMin = double.MaxValue; lonMax = double.MinValue;
            latMin = double.MaxValue; latMax = double.MinValue;
            MissionDefinition[] missions = DefaultMissionCatalog.CreateAll();
            for (int missionIndex = 0; missionIndex < missions.Length; missionIndex++)
            {
                MissionActorDefinition[] actors = missions[missionIndex].Actors;
                for (int actorIndex = 0; actorIndex < actors.Length; actorIndex++)
                {
                    AircraftInitialCondition initial = actors[actorIndex].InitialCondition;
                    Include(initial.LongitudeRad, initial.LatitudeRad, ref lonMin, ref lonMax, ref latMin, ref latMax);
                    MissionWaypointDefinition[] route = actors[actorIndex].Route ?? Array.Empty<MissionWaypointDefinition>();
                    for (int waypointIndex = 0; waypointIndex < route.Length; waypointIndex++)
                        Include(route[waypointIndex].LongitudeRad, route[waypointIndex].LatitudeRad, ref lonMin, ref lonMax, ref latMin, ref latMax);
                }
            }
            double centerLatitude = (latMin + latMax) * 0.5;
            double latMargin = MarginM / EarthRadiusM;
            double lonMargin = MarginM / (EarthRadiusM * Math.Max(0.2, Math.Cos(centerLatitude)));
            lonMin -= lonMargin; lonMax += lonMargin; latMin -= latMargin; latMax += latMargin;
        }

        private static void Include(double longitude, double latitude, ref double lonMin, ref double lonMax, ref double latMin, ref double latMax)
        {
            lonMin = Math.Min(lonMin, longitude); lonMax = Math.Max(lonMax, longitude);
            latMin = Math.Min(latMin, latitude); latMax = Math.Max(latMax, latitude);
        }

        private static double Lerp(double first, double second, double amount) => first + (second - first) * amount;
    }
}
