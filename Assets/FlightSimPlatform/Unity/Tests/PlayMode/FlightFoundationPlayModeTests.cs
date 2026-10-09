using System.Collections;
using System.IO;
using CesiumForUnity;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Presentation;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FlightSim.Platform.Unity.Tests.PlayMode
{
    public sealed class FlightFoundationPlayModeTests
    {
        [UnityTest]
        public IEnumerator SessionNavigationPauseRestartReportAndEffectPoolAreFunctional()
        {
            SceneManager.LoadScene("FlightSim_KTEX_V2",LoadSceneMode.Single);
            yield return null;yield return null;
            var session=Object.FindObjectOfType<FlightSessionController>();var host=session.Host;
            Assert.That(session.Screen,Is.EqualTo(FlightScreen.Home));Assert.That(host.IsSessionPaused,Is.True);
            Assert.That(Object.FindObjectOfType<FlightShellView>(),Is.Not.Null);Assert.That(session.Catalog.Missions.Count,Is.GreaterThanOrEqualTo(5));
            session.FreePreset=StartupPreset.Airborne;Assert.That(session.BeginFreeFlight(),Is.True);
            yield return new WaitForSeconds(.1f);
            Assert.That(host.MissionDirector,Is.Null);Assert.That(host.LatestSnapshot.Fast.SimulationTimeS,Is.GreaterThan(0));
            Assert.That(host.GetComponent<LocalTerrainEnvironment>().UsesLocalCache,Is.True);
            Assert.That(host.LatestSnapshot.Fast.TerrainSampleValid,Is.True,"Offline terrain must provide live AGL without online tiles.");
            Assert.That(Object.FindObjectOfType<MissionAircraftViewManager>().SpawnedViewCount,Is.EqualTo(0));
            session.Show(FlightScreen.Paused);double pausedTime=host.LatestSnapshot.Fast.SimulationTimeS;
            yield return new WaitForSeconds(.1f);Assert.That(host.LatestSnapshot.Fast.SimulationTimeS,Is.EqualTo(pausedTime));
            session.OpenControls();Assert.That(session.Screen,Is.EqualTo(FlightScreen.Controls));session.CloseControls();Assert.That(session.Screen,Is.EqualTo(FlightScreen.Paused));
            var custom=TrainingMissionFactory.CreateNavigationTraining();custom.MissionId="CUSTOM_RESTART_TEST";
            Assert.That(session.BeginMission(custom,true),Is.True);yield return new WaitForSeconds(.1f);
            session.Restart();Assert.That(host.LoadedMission.MissionId,Is.EqualTo(custom.MissionId));Assert.That(host.IsMissionRunning,Is.True);
            session.EndFlight();Assert.That(session.Screen,Is.EqualTo(FlightScreen.Debrief));Assert.That(File.Exists(session.ReportPath),Is.True);
            var effects=host.GetComponent<FlightEffectsController>();var location=new double3(host.LatestSnapshot.Fast.EcefPositionXM,host.LatestSnapshot.Fast.EcefPositionYM,host.LatestSnapshot.Fast.EcefPositionZM);
            for(int i=0;i<40;i++)effects.Spawn(location,i%2==0);
            Assert.That(effects.PoolCount,Is.LessThanOrEqualTo(12));Assert.That(effects.ActiveCount,Is.EqualTo(12));
            session.BeginFreeFlight();Assert.That(effects.ActiveCount,Is.EqualTo(0));session.Home();Assert.That(host.IsSessionPaused,Is.True);
            yield return null;
        }
    }
}
