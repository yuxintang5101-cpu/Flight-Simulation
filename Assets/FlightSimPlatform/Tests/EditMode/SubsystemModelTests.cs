using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class SubsystemModelTests
    {
        [Test]
        public void EngineOffProducesNoThrust()
        {
            F16EngineModel engine = new F16EngineModel(F16AircraftDefinition.CreateDefault());
            engine.Reset(EngineMode.Off);

            engine.Update(1.0, false, true, true, 0.0, 0.0, 1.0);

            Assert.That(engine.State.ThrustN, Is.EqualTo(0.0).Within(0.001));
            Assert.That(engine.State.FuelFlowKgps, Is.EqualTo(0.0).Within(0.001));
        }

        [Test]
        public void StartedEngineSpoolsAndAfterburnerAddsThrustAndFuelFlow()
        {
            F16AircraftDefinition definition = F16AircraftDefinition.CreateDefault();
            F16EngineModel engine = new F16EngineModel(definition);
            engine.Reset(EngineMode.Idle);

            for (int i = 0; i < 3000; i++)
            {
                engine.Update(1.0, false, true, true, 0.0, 0.0, 0.01);
            }

            Assert.That(engine.State.Mode, Is.EqualTo(EngineMode.Afterburner));
            Assert.That(engine.State.AfterburnerActive, Is.True);
            Assert.That(engine.State.ThrustN, Is.GreaterThan(definition.MaximumMilitaryThrustN));
            Assert.That(engine.State.ThrustN, Is.LessThanOrEqualTo(definition.MaximumAfterburnerThrustN * 1.01));
            Assert.That(engine.State.FuelFlowKgps, Is.GreaterThan(1.0));
        }

        [Test]
        public void FuelBurnUpdatesTotalMassAndLowFuelWarning()
        {
            F16FuelSystem fuel = new F16FuelSystem(F16AircraftDefinition.CreateDefault());
            fuel.Reset(100.0, 0.0, 200.0, 300.0);

            fuel.Update(2.0, 20.0);

            Assert.That(fuel.State.TotalFuelKg, Is.EqualTo(60.0).Within(0.001));
            Assert.That(fuel.State.LowFuelWarning, Is.True);
            Assert.That(fuel.State.FuelImbalanceKg, Is.EqualTo(0.0).Within(0.001));
        }

        [Test]
        public void StoreReleaseImmediatelyChangesMassDragAndStationQuantity()
        {
            F16StoresModel stores = new F16StoresModel();
            StoreLoadoutCollection loadout = new StoreLoadoutCollection();
            loadout[2] = new StoreLoadoutConfiguration
            {
                StationIndex = 2,
                StoreType = "AIM-120C",
                Quantity = 2
            };
            stores.Configure(in loadout);
            double massBeforeKg = stores.TotalMassKg;
            double dragBefore = stores.TotalDragCoefficient;

            bool released = stores.TryRelease(2, 1, false, out StoreReleaseResult result);

            Assert.That(released, Is.True);
            Assert.That(result.ReleasedQuantity, Is.EqualTo(1));
            Assert.That(stores.State.Stations[2].Quantity, Is.EqualTo(1));
            Assert.That(stores.TotalMassKg, Is.LessThan(massBeforeKg));
            Assert.That(stores.TotalDragCoefficient, Is.LessThan(dragBefore));
        }

        [Test]
        public void LostHydraulicsRemoveControlAuthorityAndLatchWarning()
        {
            F16ElectromechanicalModel systems = new F16ElectromechanicalModel();
            systems.Reset(StartupPreset.RunwayReady);

            systems.InjectFailure(FailureType.Hydraulic, 1.0, 30.0);
            systems.Update(true, 0.01);

            Assert.That(systems.ActuatorAuthorityNormalized, Is.EqualTo(0.0).Within(0.001));
            Assert.That(systems.Hydraulics.SystemAOnline, Is.False);
            Assert.That(systems.Hydraulics.SystemBOnline, Is.False);
            Assert.That(systems.Warnings.HydraulicWarning, Is.True);
            Assert.That(systems.Warnings.MasterCaution, Is.True);
        }

        [Test]
        public void PartialHydraulicFailurePreservesSystemBAndAlternateBraking()
        {
            F16ElectromechanicalModel systems = new F16ElectromechanicalModel();
            systems.Reset(StartupPreset.RunwayReady);
            systems.InjectFailure(FailureType.Hydraulic, 0.65, 30.0);
            systems.SetBrakeCommand(1.0);

            systems.Update(true, 1.0);

            Assert.That(systems.Hydraulics.SystemAOnline, Is.False);
            Assert.That(systems.Hydraulics.SystemBOnline, Is.True);
            Assert.That(systems.Hydraulics.SystemBPressurePa, Is.GreaterThan(20_000_000.0));
            Assert.That(systems.ActuatorAuthorityNormalized, Is.EqualTo(1.0).Within(0.001));
            Assert.That(systems.LandingGear.LeftBrakePressurePa, Is.GreaterThan(10_000_000.0));
            Assert.That(systems.LandingGear.RightBrakePressurePa, Is.GreaterThan(10_000_000.0));
        }
    }
}
