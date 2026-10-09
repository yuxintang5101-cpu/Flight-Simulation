using FlightSim.Platform.Contracts;
using FlightSim.Platform.Integration;
using FlightSim.Platform.Missions;
using NUnit.Framework;

namespace FlightSim.Platform.Unity.Tests.EditMode
{
    public sealed class NavigationRouteTests
    {
        [Test]
        public void ManualPilotCannotSkipToLastWaypoint()
        {
            var mission=TrainingMissionFactory.CreateNavigationTraining();
            var last=mission.Actors[0].Route[2];
            mission.Actors[0].InitialCondition.LongitudeRad=last.LongitudeRad;
            mission.Actors[0].InitialCondition.LatitudeRad=last.LatitudeRad;
            var director=new MissionDirector(new FlightSimulationService());
            Assert.That(director.Load(mission,1).Accepted,Is.True);Assert.That(director.Start().Accepted,Is.True);
            for(int i=0;i<10;i++)director.Tick();
            director.TryGetLatest(out var state);
            Assert.That(state.Mission.Phase,Is.EqualTo(MissionPhase.Running));
            Assert.That(state.Objectives[0].CurrentCount,Is.EqualTo(0));
            Assert.That(state.Actors[0].CurrentWaypointIndex,Is.EqualTo(0));
        }
    }
}
