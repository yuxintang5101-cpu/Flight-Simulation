using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class FlightControlAndDynamicsTests
    {
        [Test]
        public void FlightControlLawLimitsGAndAngleOfAttack()
        {
            F16FlightControlLaw flightControls = new F16FlightControlLaw(F16AircraftDefinition.CreateDefault());
            PilotControlInput input = PilotControlInput.Neutral;
            input.PitchNormalized = 1.0;
            FlightControlSensors sensors = new FlightControlSensors
            {
                AngleOfAttackRad = 28.0 * Math.PI / 180.0,
                NormalLoadFactorG = 8.8,
                CalibratedAirspeedMps = 140.0
            };

            FlightControlOutput output = flightControls.Update(in input, in sensors, 1.0, 0.01);

            Assert.That(output.CommandedNormalLoadFactorG, Is.LessThanOrEqualTo(9.0));
            Assert.That(output.AngleOfAttackLimiterActive, Is.True);
            Assert.That(output.GForceLimiterActive, Is.True);
            Assert.That(Math.Abs(output.ElevatorDeflectionRad), Is.LessThanOrEqualTo(26.0 * Math.PI / 180.0));
        }

        [TestCase(75.0, 3.0)]
        [TestCase(140.0, 9.0)]
        public void PositiveGCommandIsScheduledByAirspeed(double airspeedMps, double expectedLimitG)
        {
            var law = new F16FlightControlLaw(F16AircraftDefinition.CreateDefault());
            PilotControlInput input = PilotControlInput.Neutral;
            input.PitchNormalized = 1.0;
            var sensors = new FlightControlSensors { CalibratedAirspeedMps = airspeedMps, NormalLoadFactorG = 1.0 };
            FlightControlOutput output = law.Update(in input, in sensors, 1.0, 1.0);
            Assert.That(output.CommandedNormalLoadFactorG, Is.EqualTo(expectedLimitG).Within(0.05));
        }

        [Test]
        public void HighAngleOfAttackReducesRollAndPositivePitchAuthority()
        {
            FlightControlOutput normal = EvaluateSettledControl(5.0, 100.0, 1.0, 1.0);
            FlightControlOutput protectedOutput = EvaluateSettledControl(22.0, 100.0, 1.0, 1.0);
            Assert.That(Math.Abs(protectedOutput.AileronDeflectionRad), Is.LessThan(Math.Abs(normal.AileronDeflectionRad)));
            Assert.That(protectedOutput.ElevatorDeflectionRad, Is.GreaterThan(normal.ElevatorDeflectionRad));
            Assert.That(protectedOutput.AngleOfAttackLimiterActive, Is.True);
        }

        [Test]
        public void PositiveDirectPitchScalesContinuouslyThroughAngleOfAttackProtection()
        {
            double maximumElevatorRad = 25.0 * Math.PI / 180.0;
            FlightControlOutput belowOnset = EvaluateSettledControl(17.999, 55.0, 1.0, 0.0);
            FlightControlOutput atOnset = EvaluateSettledControl(18.0, 55.0, 1.0, 0.0);
            FlightControlOutput halfwayProtected = EvaluateSettledControl(21.5, 55.0, 1.0, 0.0);
            FlightControlOutput fullyProtected = EvaluateSettledControl(25.0, 55.0, 1.0, 0.0);

            Assert.That(
                atOnset.ElevatorDeflectionRad,
                Is.EqualTo(belowOnset.ElevatorDeflectionRad).Within(0.01 * Math.PI / 180.0));
            Assert.That(atOnset.ElevatorDeflectionRad, Is.EqualTo(-maximumElevatorRad).Within(1e-9));
            Assert.That(halfwayProtected.ElevatorDeflectionRad, Is.EqualTo(0.0).Within(1e-9));
            Assert.That(fullyProtected.ElevatorDeflectionRad, Is.EqualTo(maximumElevatorRad).Within(1e-9));
        }

        [Test]
        public void AngleOfAttackProtectionPreservesNegativePitchRecoveryAuthority()
        {
            FlightControlOutput unprotected = EvaluateSettledControl(5.0, 55.0, -1.0, 0.0);
            FlightControlOutput protectedOutput = EvaluateSettledControl(22.0, 55.0, -1.0, 0.0);

            Assert.That(
                protectedOutput.ElevatorDeflectionRad,
                Is.EqualTo(unprotected.ElevatorDeflectionRad).Within(1e-9));
        }

        [Test]
        public void BriefTakeoffDisturbanceRemainsInsideRecoverableEnvelope()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.RunwayReady);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 1.0;
            double maximumAngleOfAttackRad = 0.0;

            for (int tick = 0; tick < 6000 && !simulation.State.HasTakenOff; tick++)
            {
                if (simulation.State.CalibratedAirspeedMps > 67.0)
                {
                    input.PitchNormalized = 0.65;
                }

                simulation.Step(in input, 0.01);
                maximumAngleOfAttackRad = Math.Max(maximumAngleOfAttackRad, Math.Abs(simulation.State.AngleOfAttackRad));
            }

            Assert.That(simulation.State.HasTakenOff, Is.True);

            input.PitchNormalized = 0.08;
            for (int tick = 0; tick < 100; tick++)
            {
                simulation.Step(in input, 0.01);
                maximumAngleOfAttackRad = Math.Max(maximumAngleOfAttackRad, Math.Abs(simulation.State.AngleOfAttackRad));
            }

            input.PitchNormalized = 0.30;
            input.RollNormalized = 0.20;
            for (int tick = 0; tick < 25; tick++)
            {
                simulation.Step(in input, 0.01);
                maximumAngleOfAttackRad = Math.Max(maximumAngleOfAttackRad, Math.Abs(simulation.State.AngleOfAttackRad));
            }

            input.PitchNormalized = 0.0;
            input.RollNormalized = 0.0;
            for (int tick = 0; tick < 800; tick++)
            {
                simulation.Step(in input, 0.01);
                maximumAngleOfAttackRad = Math.Max(maximumAngleOfAttackRad, Math.Abs(simulation.State.AngleOfAttackRad));
            }

            Assert.That(maximumAngleOfAttackRad, Is.LessThanOrEqualTo(25.0 * Math.PI / 180.0));
            Assert.That(Math.Abs(simulation.State.BodyAngularVelocityRadps.X), Is.LessThan(15.0 * Math.PI / 180.0));
            Assert.That(Math.Abs(simulation.State.BodyAngularVelocityRadps.Y), Is.LessThan(10.0 * Math.PI / 180.0));
            Assert.That(simulation.State.CalibratedAirspeedMps, Is.Positive);
        }

        [Test]
        public void NoHydraulicAuthorityCentersPrimaryControls()
        {
            F16FlightControlLaw flightControls = new F16FlightControlLaw(F16AircraftDefinition.CreateDefault());
            PilotControlInput input = PilotControlInput.Neutral;
            input.PitchNormalized = 1.0;
            input.RollNormalized = 1.0;
            input.YawNormalized = 1.0;
            FlightControlSensors sensors = new FlightControlSensors { CalibratedAirspeedMps = 100.0 };

            FlightControlOutput output = flightControls.Update(in input, in sensors, 0.0, 0.01);

            Assert.That(output.ElevatorDeflectionRad, Is.EqualTo(0.0).Within(1e-9));
            Assert.That(output.AileronDeflectionRad, Is.EqualTo(0.0).Within(1e-9));
            Assert.That(output.RudderDeflectionRad, Is.EqualTo(0.0).Within(1e-9));
        }

        [Test]
        public void KtexRunwayReadyRollAcceleratesWithoutArtificialSpeedPlateau()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.RunwayReady);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 1.0;
            input.WheelBrakeNormalized = 0.0;
            double speedAtTenSecondsMps = 0.0;

            for (int tick = 0; tick < 2500; tick++)
            {
                simulation.Step(in input, 0.01);
                if (tick == 999)
                {
                    speedAtTenSecondsMps = simulation.State.CalibratedAirspeedMps;
                }
            }

            Assert.That(speedAtTenSecondsMps, Is.GreaterThan(20.0));
            Assert.That(simulation.State.CalibratedAirspeedMps, Is.GreaterThan(speedAtTenSecondsMps + 15.0));
        }

        [Test]
        public void KtexRunwayReadyCanRotateAndTakeOffWithinRunway()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.RunwayReady);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 1.0;
            double takeoffKcas = 0.0;

            for (int tick = 0; tick < 6000 && !simulation.State.HasTakenOff; tick++)
            {
                if (simulation.State.CalibratedAirspeedMps > 67.0)
                {
                    input.PitchNormalized = 0.65;
                }

                simulation.Step(in input, 0.01);
                if (simulation.State.HasTakenOff)
                {
                    takeoffKcas = simulation.State.CalibratedAirspeedMps * 1.9438444924406;
                }
            }

            Assert.That(
                simulation.State.HasTakenOff,
                Is.True,
                $"Final state: {simulation.State.CalibratedAirspeedMps * 1.9438444924406:F1} KCAS, " +
                $"runway {simulation.State.DistanceAlongRunwayM:F1} m, " +
                $"AGL {simulation.State.AboveGroundLevelAltitudeM:F2} m, " +
                $"pitch {simulation.State.PitchRad * 180.0 / Math.PI:F1} deg, " +
                $"AoA {simulation.State.AngleOfAttackRad * 180.0 / Math.PI:F1} deg, " +
                $"WOW {simulation.State.WeightOnWheels}.");
            Assert.That(takeoffKcas, Is.InRange(140.0, 170.0));
            Assert.That(simulation.State.DistanceAlongRunwayM, Is.LessThan(2167.0));

            for (int tick = 0; tick < 100; tick++)
            {
                simulation.Step(in input, 0.01);
            }

            Assert.That(simulation.State.WeightOnWheels, Is.False);
            Assert.That(simulation.State.AboveGroundLevelAltitudeM, Is.GreaterThan(5.0));
            Assert.That(simulation.State.ClimbRateMps, Is.GreaterThan(0.5));
        }

        [Test]
        public void HydraulicDegradationDoesNotTurnMildBankCommandIntoDirectionalDivergence()
        {
            AircraftInitialCondition initial = AircraftInitialCondition.CreateAirborneDefault();
            initial.LongitudeRad = -107.9087 * Math.PI / 180.0;
            initial.LatitudeRad = 37.9538 * Math.PI / 180.0;
            initial.EllipsoidHeightM = 7000.0;
            initial.HeadingRad = 285.0 * Math.PI / 180.0;
            initial.PitchRad = 2.5 * Math.PI / 180.0;
            initial.TrueAirspeedMps = 180.0;
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(in initial);
            simulation.InjectFailure(FailureType.Hydraulic, 0.65, 900.0);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 0.72;
            input.RollNormalized = -0.05;
            double maximumSideslipRad = 0.0;

            for (int tick = 0; tick < 800; tick++)
            {
                simulation.Step(in input, 0.01);
                maximumSideslipRad = Math.Max(maximumSideslipRad, Math.Abs(simulation.State.SideslipRad));
            }

            Assert.That(maximumSideslipRad, Is.LessThan(10.0 * Math.PI / 180.0));
            Assert.That(Math.Abs(simulation.State.RollRad), Is.LessThan(65.0 * Math.PI / 180.0));
            Assert.That(Math.Abs(simulation.State.BodyAngularVelocityRadps.X), Is.LessThan(30.0 * Math.PI / 180.0));
        }

        [Test]
        public void IdenticalInputsProduceDeterministicState()
        {
            F16AircraftSimulation first = F16AircraftSimulation.CreateKtex(StartupPreset.Airborne);
            F16AircraftSimulation second = F16AircraftSimulation.CreateKtex(StartupPreset.Airborne);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 0.78;
            input.RollNormalized = 0.15;

            for (int tick = 0; tick < 10000; tick++)
            {
                simulationStep(first, in input);
                simulationStep(second, in input);
            }

            Assert.That(first.State.EcefPositionM.X, Is.EqualTo(second.State.EcefPositionM.X).Within(1e-9));
            Assert.That(first.State.EcefPositionM.Y, Is.EqualTo(second.State.EcefPositionM.Y).Within(1e-9));
            Assert.That(first.State.EcefPositionM.Z, Is.EqualTo(second.State.EcefPositionM.Z).Within(1e-9));
            Assert.That(first.State.TotalMassKg, Is.EqualTo(second.State.TotalMassKg).Within(1e-9));
        }

        [Test]
        public void WarmedSimulationTickAllocatesNoManagedMemory()
        {
            F16AircraftSimulation simulation = F16AircraftSimulation.CreateKtex(StartupPreset.Airborne);
            PilotControlInput input = PilotControlInput.Neutral;
            input.ThrottleNormalized = 0.75;
            for (int tick = 0; tick < 200; tick++)
            {
                simulation.Step(in input, 0.01);
            }

            long beforeBytes = GC.GetAllocatedBytesForCurrentThread();
            for (int tick = 0; tick < 1000; tick++)
            {
                simulation.Step(in input, 0.01);
            }
            long allocatedBytes = GC.GetAllocatedBytesForCurrentThread() - beforeBytes;

            Assert.That(allocatedBytes, Is.EqualTo(0));
        }

        private static void simulationStep(F16AircraftSimulation simulation, in PilotControlInput input)
        {
            simulation.Step(in input, 0.01);
        }

        private static FlightControlOutput EvaluateSettledControl(
            double angleOfAttackDegrees,
            double airspeedMps,
            double pitchNormalized,
            double rollNormalized)
        {
            F16FlightControlLaw law = new F16FlightControlLaw(F16AircraftDefinition.CreateDefault());
            PilotControlInput input = PilotControlInput.Neutral;
            input.PitchNormalized = pitchNormalized;
            input.RollNormalized = rollNormalized;
            FlightControlSensors sensors = new FlightControlSensors
            {
                AngleOfAttackRad = angleOfAttackDegrees * Math.PI / 180.0,
                CalibratedAirspeedMps = airspeedMps,
                NormalLoadFactorG = 1.0
            };

            return law.Update(in input, in sensors, 1.0, 1.0);
        }

    }
}
