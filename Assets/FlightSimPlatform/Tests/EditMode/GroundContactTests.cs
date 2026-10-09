using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class GroundContactTests
    {
        [Test]
        public void RetractedGearCannotGenerateRunwayContactForce()
        {
            F16AircraftSimulation aircraft = F16AircraftSimulation.CreateKtex(StartupPreset.RunwayReady);
            GroundContactModel ground = new GroundContactModel();
            F16AircraftState state = aircraft.State;

            GroundContactLoads retracted = ground.Evaluate(
                in state.EcefPositionM,
                in state.EcefVelocityMps,
                in state.BodyToEcefOrientation,
                in state.BodyAngularVelocityRadps,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0);
            GroundContactLoads extended = ground.Evaluate(
                in state.EcefPositionM,
                in state.EcefVelocityMps,
                in state.BodyToEcefOrientation,
                in state.BodyAngularVelocityRadps,
                1.0,
                1.0,
                1.0,
                0.0,
                0.0);

            Assert.That(retracted.WeightOnWheels, Is.False);
            Assert.That(retracted.ForceBodyN, Is.EqualTo(DVector3.Zero));
            Assert.That(extended.WeightOnWheels, Is.True);
            Assert.That(extended.ForceBodyN.Length, Is.GreaterThan(0.0));
        }
    }
}
