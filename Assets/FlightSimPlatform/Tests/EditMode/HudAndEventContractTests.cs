using FlightSim.Platform.Contracts;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class HudAndEventContractTests
    {
        [Test]
        public void AvionicsCarriesPresentationReadyHudStateInSiUnits()
        {
            HudState hud = default(HudState);
            hud.Mode = HudMode.Nav;
            hud.CalibratedAirspeedMps = 150.0;
            hud.BarometricAltitudeM = 3500.0;
            hud.FlightPathAzimuthRad = 0.1;
            hud.FlightPathElevationRad = -0.05;

            AvionicsState avionics = default(AvionicsState);
            avionics.Hud = hud;

            Assert.That(avionics.Hud.Mode, Is.EqualTo(HudMode.Nav));
            Assert.That(avionics.Hud.CalibratedAirspeedMps, Is.EqualTo(150.0));
            Assert.That(avionics.Hud.BarometricAltitudeM, Is.EqualTo(3500.0));
        }

        [Test]
        public void EventContractCoversFlightSystemAndFormationTransitions()
        {
            Assert.That(SimulationEventType.EngineStarted, Is.Not.EqualTo(SimulationEventType.EngineStopped));
            Assert.That(SimulationEventType.Takeoff, Is.Not.EqualTo(SimulationEventType.Touchdown));
            Assert.That(SimulationEventType.Crash, Is.Not.EqualTo(SimulationEventType.Warning));
            Assert.That(SimulationEventType.FormationModeChanged, Is.Not.EqualTo(SimulationEventType.StoreReleased));
            Assert.That(SimulationEventType.TerrainDataTimeout, Is.Not.EqualTo(SimulationEventType.TelemetryTimeout));
        }

        [Test]
        public void FormationCommandsCoverRejoinMaintainBreakAndReturn()
        {
            Assert.That(FormationCommandType.Rejoin, Is.Not.EqualTo(FormationCommandType.Maintain));
            Assert.That(FormationCommandType.BreakAway, Is.Not.EqualTo(FormationCommandType.ReturnToBase));
        }
    }
}
