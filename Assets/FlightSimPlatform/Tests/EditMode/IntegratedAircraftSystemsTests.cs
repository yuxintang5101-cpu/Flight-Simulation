using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class IntegratedAircraftSystemsTests
    {
        [Test]
        public void RunwayReadyPresetPowersFlightCriticalSystems()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.RunwayReady);

            Assert.That(simulation.Electrical.EssentialBusPowered, Is.True);
            Assert.That(simulation.Hydraulics.SystemAOnline, Is.True);
            Assert.That(simulation.Hydraulics.SystemBOnline, Is.True);
            Assert.That(simulation.LandingGear.NoseGearPositionNormalized, Is.EqualTo(1.0));
            Assert.That(simulation.Propulsion.EngineRunning, Is.True);
        }

        [Test]
        public void ReleasedStoreImmediatelyReducesAuthoritativeMassAndDrag()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.Airborne);
            StoreLoadoutCollection loadout = new StoreLoadoutCollection();
            loadout[2] = new StoreLoadoutConfiguration
            {
                StationIndex = 2,
                StoreType = "AIM-120C",
                Quantity = 2
            };
            simulation.ConfigureLoadout(in loadout);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 0.75;
            simulation.Step(in input, 0.01);
            double massBeforeKg = simulation.State.TotalMassKg;
            double dragBefore = simulation.StoresDragCoefficient;

            bool released = simulation.TryReleaseStore(2, 1, false, out StoreReleaseResult result);

            Assert.That(released, Is.True);
            Assert.That(result.ReleasedQuantity, Is.EqualTo(1));
            Assert.That(simulation.State.TotalMassKg, Is.LessThan(massBeforeKg));
            Assert.That(simulation.StoresDragCoefficient, Is.LessThan(dragBefore));
        }

        [Test]
        public void HydraulicFailureRemovesFlightControlAuthorityInIntegratedTick()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.Airborne);
            PilotControlInput input = PilotControlInput.Neutral;
            input.PitchNormalized = 1.0;
            input.RollNormalized = 1.0;
            simulation.InjectFailure(FailureType.Hydraulic, 1.0, 30.0);

            simulation.Step(in input, 0.01);

            Assert.That(simulation.FlightControls.ElevatorDeflectionRad, Is.EqualTo(0.0).Within(1e-9));
            Assert.That(simulation.FlightControls.AileronDeflectionRad, Is.EqualTo(0.0).Within(1e-9));
            Assert.That(simulation.Warnings.HydraulicWarning, Is.True);
            Assert.That(simulation.Warnings.MasterCaution, Is.True);
        }

        [Test]
        public void ColdAndDarkHasNoThrustUntilPoweredStartSequence()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.ColdAndDark);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 1.0;
            Assert.That(simulation.Fuel.FuelPumpEnabled, Is.False);
            for (int tick = 0; tick < 200; tick++)
            {
                simulation.Step(in input, 0.01);
            }

            Assert.That(simulation.Propulsion.ThrustN, Is.EqualTo(0.0).Within(0.001));
            Assert.That(simulation.Propulsion.EngineRunning, Is.False);

            simulation.SetSystemSwitch(AircraftSystemSwitch.Battery, true);
            simulation.SetSystemSwitch(AircraftSystemSwitch.FuelPump, true);
            input.EngineStartCommand = true;
            for (int tick = 0; tick < 800; tick++)
            {
                simulation.Step(in input, 0.01);
            }

            Assert.That(simulation.Propulsion.EngineRunning, Is.True);
            Assert.That(simulation.Propulsion.ThrustN, Is.GreaterThan(0.0));
        }

        [Test]
        public void StarterDoesNotPowerGeneratorOrHydraulicsBeforeCoreSpool()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.ColdAndDark);
            simulation.SetSystemSwitch(AircraftSystemSwitch.Battery, true);
            simulation.SetSystemSwitch(AircraftSystemSwitch.Generator, true);
            simulation.SetSystemSwitch(AircraftSystemSwitch.FuelPump, true);
            simulation.SetSystemSwitch(AircraftSystemSwitch.HydraulicSystemA, true);
            simulation.SetSystemSwitch(AircraftSystemSwitch.HydraulicSystemB, true);
            PilotControlInput input = PilotControlInput.Neutral;
            input.EngineStartCommand = true;

            simulation.Step(in input, 0.01);

            Assert.That(simulation.Propulsion.Mode, Is.EqualTo(EngineMode.Starting));
            Assert.That(simulation.Electrical.GeneratorOnline, Is.False);
            Assert.That(simulation.Hydraulics.SystemAOnline, Is.False);
            Assert.That(simulation.Hydraulics.SystemBOnline, Is.False);
        }

        [Test]
        public void HydraulicLossPreventsWheelBrakeForce()
        {
            F16AircraftSimulation unbraked = F16AircraftSimulation.CreateKtex(StartupPreset.RunwayReady);
            F16AircraftSimulation brakeCommanded = F16AircraftSimulation.CreateKtex(StartupPreset.RunwayReady);
            unbraked.InjectFailure(FailureType.Hydraulic, 1.0, 30.0);
            brakeCommanded.InjectFailure(FailureType.Hydraulic, 1.0, 30.0);
            PilotControlInput coast = PilotControlInput.Neutral;
            PilotControlInput brake = PilotControlInput.Neutral;
            for (int tick = 0; tick < 200; tick++)
            {
                unbraked.Step(in coast, 0.01);
                brakeCommanded.Step(in coast, 0.01);
            }

            coast.ThrottleNormalized = 1.0;
            brake.ThrottleNormalized = 1.0;
            brake.WheelBrakeNormalized = 1.0;
            for (int tick = 0; tick < 500; tick++)
            {
                unbraked.Step(in coast, 0.01);
                brakeCommanded.Step(in brake, 0.01);
            }

            Assert.That(
                brakeCommanded.State.GroundSpeedMps,
                Is.EqualTo(unbraked.State.GroundSpeedMps).Within(0.25));
        }
    }
}
