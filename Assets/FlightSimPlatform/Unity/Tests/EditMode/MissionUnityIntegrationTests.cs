using System.IO;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using NUnit.Framework;

namespace FlightSim.Platform.Unity.Tests.EditMode
{
    public sealed class MissionUnityIntegrationTests
    {
        [Test]
        public void MissionJsonRoundTripPreservesRosterAndInitialFuel()
        {
            MissionDefinition expected = DefaultMissionCatalog.Create("KTEX_CAP_01");

            string json = MissionJsonCodec.ToJson(expected, true);
            MissionDefinition actual = MissionJsonCodec.FromJson(json);

            Assert.That(actual.MissionId, Is.EqualTo(expected.MissionId));
            Assert.That(actual.Actors.Length, Is.EqualTo(expected.Actors.Length));
            Assert.That(actual.Actors[0].InitialCondition.InternalFuelFraction, Is.EqualTo(0.80));
            Assert.That(MissionDefinitionValidator.Validate(actual).IsValid, Is.True);
        }

        [TestCase("KTEX_SCRAMBLE_01")]
        [TestCase("KTEX_CAP_01")]
        [TestCase("KTEX_ESCORT_01")]
        [TestCase("KTEX_EMERGENCY_RTB_01")]
        public void DocumentedMissionExampleImportsAndValidates(string missionId)
        {
            string path = Path.Combine(
                UnityEngine.Application.dataPath,
                "FlightSimPlatform",
                "Docs",
                "Integration",
                "Missions",
                missionId + ".json");

            MissionDefinition mission = MissionJsonCodec.FromJson(File.ReadAllText(path));
            MissionDefinition catalog = DefaultMissionCatalog.Create(missionId);
            MissionValidationResult validation = MissionDefinitionValidator.Validate(mission);

            Assert.That(mission.MissionId, Is.EqualTo(missionId));
            Assert.That(validation.IsValid, Is.True, validation.FirstError);
            Assert.That(mission.Actors.Length, Is.EqualTo(catalog.Actors.Length));
            foreach (MissionActorDefinition expectedActor in catalog.Actors)
            {
                MissionActorDefinition actualActor = System.Array.Find(
                    mission.Actors,
                    actor => actor.AircraftId == expectedActor.AircraftId);
                Assert.That(actualActor.AircraftId, Is.EqualTo(expectedActor.AircraftId));
                Assert.That(actualActor.InitialCondition.SpawnMode, Is.EqualTo(expectedActor.InitialCondition.SpawnMode));
                Assert.That(actualActor.InitialCondition.InternalFuelFraction,
                    Is.EqualTo(expectedActor.InitialCondition.InternalFuelFraction).Within(1e-9));
                if (expectedActor.InitialCondition.SpawnMode == AircraftSpawnMode.Geodetic)
                {
                    Assert.That(actualActor.InitialCondition.LongitudeRad,
                        Is.EqualTo(expectedActor.InitialCondition.LongitudeRad).Within(1e-5));
                    Assert.That(actualActor.InitialCondition.LatitudeRad,
                        Is.EqualTo(expectedActor.InitialCondition.LatitudeRad).Within(1e-5));
                    Assert.That(actualActor.InitialCondition.EllipsoidHeightM,
                        Is.EqualTo(expectedActor.InitialCondition.EllipsoidHeightM).Within(0.1));
                }
                Assert.That(actualActor.Route.Length, Is.EqualTo(expectedActor.Route.Length));
                for (int routeIndex = 0; routeIndex < expectedActor.Route.Length; routeIndex++)
                {
                    Assert.That(actualActor.Route[routeIndex].AcceptanceRadiusM,
                        Is.EqualTo(expectedActor.Route[routeIndex].AcceptanceRadiusM).Within(0.1));
                }
            }
        }

        [Test]
        public void ViewBindingUsesExplicitAircraftInsteadOfAlwaysLocalPlayer()
        {
            AircraftId wingman = new AircraftId("VIPER-02");
            UnityEngine.GameObject georeferenceObject = new UnityEngine.GameObject("Test Georeference");
            UnityEngine.GameObject viewObject = new UnityEngine.GameObject("Wingman View");
            try
            {
                georeferenceObject.AddComponent<CesiumForUnity.CesiumGeoreference>();
                viewObject.transform.SetParent(georeferenceObject.transform, false);
                viewObject.AddComponent<CesiumForUnity.CesiumGlobeAnchor>();
                CesiumAircraftView view = viewObject.AddComponent<CesiumAircraftView>();

                view.Bind(null, wingman);

                Assert.That(view.BoundAircraft, Is.EqualTo(wingman));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(viewObject);
                UnityEngine.Object.DestroyImmediate(georeferenceObject);
            }
        }

        [Test]
        public void TerrainSamplerBindingUsesExplicitAircraft()
        {
            AircraftId hostile = new AircraftId("BANDIT-01");
            UnityEngine.GameObject sampleObject = new UnityEngine.GameObject("Hostile Terrain Sampler");
            try
            {
                FlightTerrainSampler sampler = sampleObject.AddComponent<FlightTerrainSampler>();

                sampler.Bind(null, sampleObject.transform, hostile);

                Assert.That(sampler.BoundAircraft, Is.EqualTo(hostile));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(sampleObject);
            }
        }
    }
}
