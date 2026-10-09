using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Core
{
    [Serializable]
    public struct PilotControlInput
    {
        public double PitchNormalized;
        public double RollNormalized;
        public double YawNormalized;
        public double ThrottleNormalized;
        public double WheelBrakeNormalized;
        public double SpeedBrakeNormalized;
        public bool EngineStartCommand;

        public static PilotControlInput Neutral => default(PilotControlInput);
    }

    public struct F16AircraftState
    {
        public ulong Tick;
        public double SimulationTimeS;
        public DVector3 EcefPositionM;
        public DVector3 EcefVelocityMps;
        public DVector3 BodyVelocityMps;
        public DVector3 BodyAngularVelocityRadps;
        public DQuaternion BodyToEcefOrientation;
        public double LongitudeRad;
        public double LatitudeRad;
        public double EllipsoidHeightM;
        public double HeadingRad;
        public double PitchRad;
        public double RollRad;
        public double TrueAirspeedMps;
        public double CalibratedAirspeedMps;
        public double GroundSpeedMps;
        public double Mach;
        public double AngleOfAttackRad;
        public double SideslipRad;
        public double DynamicPressurePa;
        public double NormalLoadFactorG;
        public double MeanSeaLevelAltitudeM;
        public double AboveGroundLevelAltitudeM;
        public double ClimbRateMps;
        public double TotalMassKg;
        public double DistanceAlongRunwayM;
        public double DistanceRightOfRunwayCenterlineM;
        public bool WeightOnWheels;
        public bool HasTakenOff;

        public DQuaternion BodyToEcefQuaternion => BodyToEcefOrientation;
        public DVector3 BodyVelocityFrdMps => BodyVelocityMps;
        public DVector3 BodyRatesFrdRadps => BodyAngularVelocityRadps;
    }

    /// <summary>
    /// Allocation-free deterministic F-16 rigid-body simulation with no Unity physics dependency.
    /// </summary>
    public sealed class F16AircraftSimulation
    {
        private const double RunwaySpawnDistanceM = 85.0;
        private const double RunwayCenterOfMassHeightM = 1.545;
        private const double AirborneSpawnHeightAboveKtexM = 1500.0;
        private const double AirborneSpawnTrueAirspeedMps = 180.0;
        private const double AirborneSpawnPitchRad = 2.5 * Math.PI / 180.0;
        private const double InitialFuelFraction = 0.70;
        private const double ReferenceGravityMps2 = 9.80665;
        private const double Wgs84EccentricitySquared = 6.69437999014e-3;
        private const double Wgs84GravityEquatorMps2 = 9.7803253359;
        private const double Wgs84GravityFormulaK = 0.00193185265241;
        private const double MaximumAngularAccelerationRadps2 = 8.0;
        private const double NominalBrakePressurePa = 20_684_271.9;
        private const double TakeoffConfirmationTimeS = 0.25;

        private readonly F16AircraftDefinition definition;
        private readonly AerodynamicsModel aerodynamics;
        private readonly GroundContactModel groundContact;
        private readonly F16FlightControlLaw flightControlLaw;
        private readonly F16EngineModel engine;
        private readonly F16FuelSystem fuel;
        private readonly F16StoresModel stores;
        private readonly F16ElectromechanicalModel electromechanical;

        private F16AircraftState state;
        private FlightControlOutput flightControlOutput;
        private bool hasHadWeightOnWheels;
        private double takeoffConfirmationTimeS;

        private F16AircraftSimulation(F16AircraftDefinition definition, StartupPreset preset)
        {
            this.definition = definition;
            aerodynamics = new AerodynamicsModel(definition);
            groundContact = new GroundContactModel();
            flightControlLaw = new F16FlightControlLaw(definition);
            engine = new F16EngineModel(definition);
            fuel = new F16FuelSystem(definition);
            stores = new F16StoresModel();
            electromechanical = new F16ElectromechanicalModel();

            double initialFuelKg = definition.InternalFuelCapacityKg * InitialFuelFraction;
            fuel.Reset(initialFuelKg, 0.0, 450.0, 900.0);
            if (preset == StartupPreset.ColdAndDark)
            {
                fuel.SetFuelPumpEnabled(false);
            }
            engine.Reset(preset == StartupPreset.ColdAndDark ? EngineMode.Off : EngineMode.Military);
            electromechanical.Reset(preset);
            InitializeState(preset);
        }

        private F16AircraftSimulation(
            F16AircraftDefinition definition,
            in AircraftInitialCondition initialCondition)
        {
            this.definition = definition;
            aerodynamics = new AerodynamicsModel(definition);
            groundContact = new GroundContactModel();
            flightControlLaw = new F16FlightControlLaw(definition);
            engine = new F16EngineModel(definition);
            fuel = new F16FuelSystem(definition);
            stores = new F16StoresModel();
            electromechanical = new F16ElectromechanicalModel();

            double fuelFraction = Clamp(initialCondition.InternalFuelFraction, 0.0, 1.0);
            fuel.Reset(
                definition.InternalFuelCapacityKg * fuelFraction,
                Math.Max(0.0, initialCondition.ExternalFuelKg),
                Math.Max(0.0, initialCondition.BingoFuelKg),
                Math.Max(initialCondition.BingoFuelKg, initialCondition.JokerFuelKg));
            if (initialCondition.Preset == StartupPreset.ColdAndDark)
            {
                fuel.SetFuelPumpEnabled(false);
            }

            engine.Reset(initialCondition.Preset == StartupPreset.ColdAndDark
                ? EngineMode.Off
                : EngineMode.Military);
            electromechanical.Reset(initialCondition.Preset);
            stores.Configure(in initialCondition.Loadout);
            InitializeState(in initialCondition);
        }

        public F16AircraftState State => state;
        public F16AircraftDefinition Definition => definition;
        public FlightControlOutput FlightControls => flightControlOutput;
        public PropulsionState Propulsion => engine.State;
        public FuelState Fuel => fuel.State;
        public ElectricalState Electrical => electromechanical.Electrical;
        public HydraulicState Hydraulics => electromechanical.Hydraulics;
        public LandingGearState LandingGear => electromechanical.LandingGear;
        public WarningState Warnings => electromechanical.Warnings;
        public StoresState Stores => stores.State;
        public double StoresDragCoefficient => stores.TotalDragCoefficient;
        public AnalyticRunway Runway => groundContact.Runway;

        public static F16AircraftSimulation CreateKtex(StartupPreset preset)
        {
            return new F16AircraftSimulation(F16AircraftDefinition.CreateDefault(), preset);
        }

        public static F16AircraftSimulation CreateKtex(in AircraftInitialCondition initialCondition)
        {
            return new F16AircraftSimulation(F16AircraftDefinition.CreateDefault(), in initialCondition);
        }

        public void ConfigureLoadout(in StoreLoadoutCollection loadout)
        {
            stores.Configure(in loadout);
            stores.UpdateReadiness(electromechanical.Electrical.AvionicsBusPowered, true);
            state.TotalMassKg = definition.EmptyMassKg + fuel.State.TotalFuelKg + stores.TotalMassKg;
        }

        public bool TryReleaseStore(
            int stationIndex,
            int quantity,
            bool emergencyJettison,
            out StoreReleaseResult result)
        {
            bool released = stores.TryRelease(stationIndex, quantity, emergencyJettison, out result);
            if (released)
            {
                state.TotalMassKg = definition.EmptyMassKg + fuel.State.TotalFuelKg + stores.TotalMassKg;
            }

            return released;
        }

        public void InjectFailure(FailureType failure, double severityNormalized, double durationS)
        {
            electromechanical.InjectFailure(failure, severityNormalized, durationS);
            if (failure == FailureType.Engine && severityNormalized > 0.0)
            {
                engine.Fail();
            }
            else if (failure == FailureType.Fuel && severityNormalized > 0.0)
            {
                fuel.SetFuelPumpEnabled(false);
            }
        }

        public void SetSystemSwitch(AircraftSystemSwitch systemSwitch, bool enabled)
        {
            if (systemSwitch == AircraftSystemSwitch.FuelPump)
            {
                fuel.SetFuelPumpEnabled(enabled);
                return;
            }

            electromechanical.SetSwitch(systemSwitch, enabled);
        }

        public void SetLandingGearDown(bool down)
        {
            electromechanical.SetLandingGearCommand(down);
        }

        public void OffsetEcefPosition(in DVector3 offsetEcefM)
        {
            state.EcefPositionM += offsetEcefM;
            UpdateDerivedState(0.0);
        }

        public void Step(in PilotControlInput input, double deltaTimeS)
        {
            if (!(deltaTimeS > 0.0) || double.IsNaN(deltaTimeS) || double.IsInfinity(deltaTimeS))
            {
                throw new ArgumentOutOfRangeException(nameof(deltaTimeS));
            }

            GeoMath.EcefToLla(
                state.EcefPositionM,
                out double longitudeRad,
                out double latitudeRad,
                out double ellipsoidHeightM);
            AtmosphereSample atmosphere = IsaAtmosphere.Sample(ellipsoidHeightM);
            AirDataState preStepAirData = AerodynamicsModel.ComputeAirData(
                in state.BodyVelocityMps,
                in atmosphere);
            electromechanical.SetWeightOnWheels(state.WeightOnWheels);
            electromechanical.SetBrakeCommand(input.WheelBrakeNormalized);
            electromechanical.SetSpeedBrakeCommand(input.SpeedBrakeNormalized);
            electromechanical.Update(engine.AccessoryDriveAvailable, deltaTimeS);
            stores.UpdateReadiness(electromechanical.Electrical.AvionicsBusPowered, true);
            FlightControlSensors sensors = new FlightControlSensors
            {
                AngleOfAttackRad = preStepAirData.AngleOfAttackRad,
                SideslipRad = preStepAirData.SideslipRad,
                NormalLoadFactorG = state.NormalLoadFactorG,
                RollRateRadps = state.BodyAngularVelocityRadps.X,
                PitchRateRadps = state.BodyAngularVelocityRadps.Y,
                YawRateRadps = state.BodyAngularVelocityRadps.Z,
                CalibratedAirspeedMps = preStepAirData.CalibratedAirspeedMps,
                WeightOnWheels = state.WeightOnWheels
            };
            flightControlOutput = flightControlLaw.Update(
                in input,
                in sensors,
                electromechanical.ActuatorAuthorityNormalized,
                deltaTimeS);

            bool fuelAvailable = fuel.AvailableFuelKg > 0.0;
            engine.Update(
                input.ThrottleNormalized,
                input.EngineStartCommand,
                fuelAvailable,
                electromechanical.Electrical.EssentialBusPowered,
                Math.Max(0.0, ellipsoidHeightM),
                preStepAirData.Mach,
                deltaTimeS);
            fuel.Update(engine.State.FuelFlowKgps, deltaTimeS);
            double totalMassKg = Math.Max(
                1.0,
                definition.EmptyMassKg + fuel.State.TotalFuelKg + stores.TotalMassKg);
            double additionalDragCoefficient = stores.TotalDragCoefficient +
                                               0.12 * Clamp(input.SpeedBrakeNormalized, 0.0, 1.0);

            AerodynamicLoads aerodynamicLoads = aerodynamics.Evaluate(
                in state.BodyVelocityMps,
                in state.BodyAngularVelocityRadps,
                flightControlOutput.ElevatorDeflectionRad,
                flightControlOutput.AileronDeflectionRad,
                flightControlOutput.RudderDeflectionRad,
                additionalDragCoefficient,
                in atmosphere);
            GroundContactLoads groundLoads = groundContact.Evaluate(
                in state.EcefPositionM,
                in state.EcefVelocityMps,
                in state.BodyToEcefOrientation,
                in state.BodyAngularVelocityRadps,
                electromechanical.LandingGear.NoseGearPositionNormalized,
                electromechanical.LandingGear.LeftMainGearPositionNormalized,
                electromechanical.LandingGear.RightMainGearPositionNormalized,
                electromechanical.LandingGear.LeftBrakePressurePa / NominalBrakePressurePa,
                electromechanical.LandingGear.RightBrakePressurePa / NominalBrakePressurePa);

            DVector3 thrustBodyN = DVector3.UnitX * engine.State.ThrustN;
            DVector3 appliedForceBodyN = aerodynamicLoads.ForceBodyN +
                                         groundLoads.ForceBodyN +
                                         thrustBodyN;
            DVector3 appliedMomentBodyNm = aerodynamicLoads.MomentBodyNm +
                                           groundLoads.MomentBodyNm;
            double gravityMps2 = ComputeWgs84Gravity(latitudeRad, ellipsoidHeightM);
            EnuBasis localEnu = GeoMath.CreateEnuBasis(longitudeRad, latitudeRad);
            DVector3 gravityEcefMps2 = -localEnu.Up * gravityMps2;
            DVector3 appliedForceEcefN = state.BodyToEcefOrientation.Rotate(appliedForceBodyN);
            DVector3 accelerationEcefMps2 = appliedForceEcefN / totalMassKg + gravityEcefMps2;

            DVector3 oldBodyRatesRadps = state.BodyAngularVelocityRadps;
            DVector3 angularAccelerationRadps2 = ComputeAngularAcceleration(
                in oldBodyRatesRadps,
                in appliedMomentBodyNm);
            DVector3 newBodyRatesRadps = oldBodyRatesRadps + angularAccelerationRadps2 * deltaTimeS;
            DVector3 midpointBodyRatesRadps = (oldBodyRatesRadps + newBodyRatesRadps) * 0.5;
            DQuaternion newOrientation = state.BodyToEcefOrientation
                .IntegrateBodyAngularVelocity(midpointBodyRatesRadps, deltaTimeS)
                .Normalized;
            DVector3 newEcefVelocityMps = state.EcefVelocityMps + accelerationEcefMps2 * deltaTimeS;
            DVector3 newEcefPositionM = state.EcefPositionM + newEcefVelocityMps * deltaTimeS;
            DVector3 newBodyVelocityMps = newOrientation.RotateInverse(newEcefVelocityMps);

            state.Tick++;
            state.SimulationTimeS += deltaTimeS;
            state.EcefPositionM = newEcefPositionM;
            state.EcefVelocityMps = newEcefVelocityMps;
            state.BodyVelocityMps = newBodyVelocityMps;
            state.BodyAngularVelocityRadps = newBodyRatesRadps;
            state.BodyToEcefOrientation = newOrientation;
            state.TotalMassKg = totalMassKg;
            state.NormalLoadFactorG = -appliedForceBodyN.Z / (totalMassKg * ReferenceGravityMps2);

            UpdateDerivedState(deltaTimeS);
        }

        private void InitializeState(StartupPreset preset)
        {
            AnalyticRunway runway = groundContact.Runway;
            DQuaternion levelOrientation = CreateBodyToEcefOrientation(
                runway.ForwardEcef,
                runway.RightEcef,
                -runway.UpEcef);
            state = default(F16AircraftState);
            state.BodyToEcefOrientation = levelOrientation;
            state.TotalMassKg = definition.EmptyMassKg + fuel.State.TotalFuelKg;
            state.NormalLoadFactorG = 1.0;
            takeoffConfirmationTimeS = 0.0;

            if (preset == StartupPreset.RunwayReady || preset == StartupPreset.ColdAndDark)
            {
                state.EcefPositionM = runway.ThresholdEcefM +
                                      runway.ForwardEcef * RunwaySpawnDistanceM +
                                      runway.UpEcef * RunwayCenterOfMassHeightM;
                state.EcefVelocityMps = DVector3.Zero;
                state.BodyVelocityMps = DVector3.Zero;
                state.BodyAngularVelocityRadps = DVector3.Zero;
                state.HasTakenOff = false;
                hasHadWeightOnWheels = true;
            }
            else
            {
                state.BodyToEcefOrientation = (levelOrientation *
                    DQuaternion.FromAxisAngle(DVector3.UnitY, AirborneSpawnPitchRad)).Normalized;
                state.EcefPositionM = runway.CenterEcefM +
                                      runway.ForwardEcef * 300.0 +
                                      runway.UpEcef * AirborneSpawnHeightAboveKtexM;
                state.BodyVelocityMps = DVector3.UnitX * AirborneSpawnTrueAirspeedMps;
                state.EcefVelocityMps = state.BodyToEcefOrientation.Rotate(state.BodyVelocityMps);
                state.BodyAngularVelocityRadps = DVector3.Zero;
                state.HasTakenOff = true;
                hasHadWeightOnWheels = false;
            }

            UpdateDerivedState(0.0);
        }

        private void InitializeState(in AircraftInitialCondition initialCondition)
        {
            if (initialCondition.SpawnMode == AircraftSpawnMode.KtexRunway27)
            {
                InitializeState(initialCondition.Preset);
                electromechanical.ConfigureInitialLandingGear(
                    initialCondition.LandingGearDown,
                    initialCondition.Preset != StartupPreset.Airborne);
                state.TotalMassKg = definition.EmptyMassKg + fuel.State.TotalFuelKg + stores.TotalMassKg;
                UpdateDerivedState(0.0);
                return;
            }

            EnuBasis enu = GeoMath.CreateEnuBasis(
                initialCondition.LongitudeRad,
                initialCondition.LatitudeRad);
            double sinHeading = Math.Sin(initialCondition.HeadingRad);
            double cosHeading = Math.Cos(initialCondition.HeadingRad);
            DVector3 forwardEcef = enu.North * cosHeading + enu.East * sinHeading;
            DVector3 rightEcef = enu.East * cosHeading - enu.North * sinHeading;
            DQuaternion levelOrientation = CreateBodyToEcefOrientation(
                forwardEcef,
                rightEcef,
                -enu.Up);
            DQuaternion pitch = DQuaternion.FromAxisAngle(DVector3.UnitY, initialCondition.PitchRad);
            DQuaternion roll = DQuaternion.FromAxisAngle(DVector3.UnitX, initialCondition.RollRad);

            state = default(F16AircraftState);
            state.EcefPositionM = GeoMath.LlaToEcef(
                initialCondition.LongitudeRad,
                initialCondition.LatitudeRad,
                initialCondition.EllipsoidHeightM);
            state.BodyToEcefOrientation = (levelOrientation * pitch * roll).Normalized;
            state.BodyVelocityMps = DVector3.UnitX * Math.Max(0.0, initialCondition.TrueAirspeedMps);
            state.EcefVelocityMps = state.BodyToEcefOrientation.Rotate(state.BodyVelocityMps);
            state.BodyAngularVelocityRadps = new DVector3(
                initialCondition.BodyAngularVelocityXRadps,
                initialCondition.BodyAngularVelocityYRadps,
                initialCondition.BodyAngularVelocityZRadps);
            state.TotalMassKg = definition.EmptyMassKg + fuel.State.TotalFuelKg + stores.TotalMassKg;
            state.NormalLoadFactorG = 1.0;
            state.HasTakenOff = true;
            hasHadWeightOnWheels = false;
            takeoffConfirmationTimeS = 0.0;
            electromechanical.ConfigureInitialLandingGear(initialCondition.LandingGearDown, false);
            UpdateDerivedState(0.0);
        }

        private void UpdateDerivedState(double deltaTimeS)
        {
            GeoMath.EcefToLla(
                state.EcefPositionM,
                out state.LongitudeRad,
                out state.LatitudeRad,
                out state.EllipsoidHeightM);
            EnuBasis enu = GeoMath.CreateEnuBasis(state.LongitudeRad, state.LatitudeRad);
            AtmosphereSample atmosphere = IsaAtmosphere.Sample(state.EllipsoidHeightM);
            AirDataState airData = AerodynamicsModel.ComputeAirData(in state.BodyVelocityMps, in atmosphere);
            GroundContactLoads groundLoads = groundContact.Evaluate(
                in state.EcefPositionM,
                in state.EcefVelocityMps,
                in state.BodyToEcefOrientation,
                in state.BodyAngularVelocityRadps,
                electromechanical.LandingGear.NoseGearPositionNormalized,
                electromechanical.LandingGear.LeftMainGearPositionNormalized,
                electromechanical.LandingGear.RightMainGearPositionNormalized,
                0.0,
                0.0);
            FrdBasis body = GeoMath.CreateFrdBasis(state.BodyToEcefOrientation);

            double forwardEast = DVector3.Dot(body.Forward, enu.East);
            double forwardNorth = DVector3.Dot(body.Forward, enu.North);
            state.HeadingRad = NormalizeAnglePositive(Math.Atan2(forwardEast, forwardNorth));
            state.PitchRad = Math.Asin(Clamp(DVector3.Dot(body.Forward, enu.Up), -1.0, 1.0));
            state.RollRad = Math.Atan2(
                -DVector3.Dot(body.Right, enu.Up),
                -DVector3.Dot(body.Down, enu.Up));
            state.TrueAirspeedMps = airData.TrueAirspeedMps;
            state.CalibratedAirspeedMps = airData.CalibratedAirspeedMps;
            state.Mach = airData.Mach;
            state.AngleOfAttackRad = airData.AngleOfAttackRad;
            state.SideslipRad = airData.SideslipRad;
            state.DynamicPressurePa = airData.DynamicPressurePa;
            state.ClimbRateMps = DVector3.Dot(state.EcefVelocityMps, enu.Up);
            DVector3 horizontalVelocityMps = state.EcefVelocityMps - enu.Up * state.ClimbRateMps;
            state.GroundSpeedMps = horizontalVelocityMps.Length;
            state.MeanSeaLevelAltitudeM = state.EllipsoidHeightM;
            state.AboveGroundLevelAltitudeM = groundContact.Runway.HeightAbovePlaneM(in state.EcefPositionM);
            state.DistanceAlongRunwayM = groundLoads.DistanceAlongRunwayM;
            state.DistanceRightOfRunwayCenterlineM = groundLoads.DistanceRightOfCenterlineM;
            state.WeightOnWheels = groundLoads.WeightOnWheels;

            if (groundLoads.WeightOnWheels)
            {
                hasHadWeightOnWheels = true;
                takeoffConfirmationTimeS = 0.0;
            }
            else if (hasHadWeightOnWheels &&
                     state.CalibratedAirspeedMps > 55.0 &&
                     state.AboveGroundLevelAltitudeM > RunwayCenterOfMassHeightM + 0.5 &&
                     state.ClimbRateMps > 0.25)
            {
                takeoffConfirmationTimeS += Math.Max(0.0, deltaTimeS);
                if (takeoffConfirmationTimeS >= TakeoffConfirmationTimeS)
                {
                    state.HasTakenOff = true;
                }
            }
            else
            {
                takeoffConfirmationTimeS = 0.0;
            }
        }

        private DVector3 ComputeAngularAcceleration(
            in DVector3 bodyRatesRadps,
            in DVector3 momentBodyNm)
        {
            double ix = definition.RollMomentOfInertiaKgm2;
            double iy = definition.PitchMomentOfInertiaKgm2;
            double iz = definition.YawMomentOfInertiaKgm2;
            double ixz = definition.ProductOfInertiaXzKgm2;
            double p = bodyRatesRadps.X;
            double q = bodyRatesRadps.Y;
            double r = bodyRatesRadps.Z;
            double rollRightHandSide = momentBodyNm.X -
                ((iz - iy) * q * r - ixz * p * q);
            double yawRightHandSide = momentBodyNm.Z -
                ((iy - ix) * p * q + ixz * q * r);
            double determinant = ix * iz - ixz * ixz;
            double rollAcceleration =
                (iz * rollRightHandSide + ixz * yawRightHandSide) / determinant;
            double pitchAcceleration =
                (momentBodyNm.Y - ((ix - iz) * p * r + ixz * (p * p - r * r))) / iy;
            double yawAcceleration =
                (ixz * rollRightHandSide + ix * yawRightHandSide) / determinant;
            return new DVector3(
                Clamp(rollAcceleration, -MaximumAngularAccelerationRadps2, MaximumAngularAccelerationRadps2),
                Clamp(pitchAcceleration, -MaximumAngularAccelerationRadps2, MaximumAngularAccelerationRadps2),
                Clamp(yawAcceleration, -MaximumAngularAccelerationRadps2, MaximumAngularAccelerationRadps2));
        }

        private static double ComputeWgs84Gravity(double latitudeRad, double ellipsoidHeightM)
        {
            double sineLatitude = Math.Sin(latitudeRad);
            double sineSquared = sineLatitude * sineLatitude;
            double surfaceGravityMps2 = Wgs84GravityEquatorMps2 *
                (1.0 + Wgs84GravityFormulaK * sineSquared) /
                Math.Sqrt(1.0 - Wgs84EccentricitySquared * sineSquared);
            double radiusRatio = GeoMath.Wgs84SemiMajorAxisM /
                                 (GeoMath.Wgs84SemiMajorAxisM + Math.Max(-1000.0, ellipsoidHeightM));
            return surfaceGravityMps2 * radiusRatio * radiusRatio;
        }

        private static DQuaternion CreateBodyToEcefOrientation(
            in DVector3 forwardEcef,
            in DVector3 rightEcef,
            in DVector3 downEcef)
        {
            double m00 = forwardEcef.X;
            double m01 = rightEcef.X;
            double m02 = downEcef.X;
            double m10 = forwardEcef.Y;
            double m11 = rightEcef.Y;
            double m12 = downEcef.Y;
            double m20 = forwardEcef.Z;
            double m21 = rightEcef.Z;
            double m22 = downEcef.Z;
            double trace = m00 + m11 + m22;

            if (trace > 0.0)
            {
                double scale = Math.Sqrt(trace + 1.0) * 2.0;
                return new DQuaternion(
                    (m21 - m12) / scale,
                    (m02 - m20) / scale,
                    (m10 - m01) / scale,
                    0.25 * scale).Normalized;
            }

            if (m00 > m11 && m00 > m22)
            {
                double scale = Math.Sqrt(1.0 + m00 - m11 - m22) * 2.0;
                return new DQuaternion(
                    0.25 * scale,
                    (m01 + m10) / scale,
                    (m02 + m20) / scale,
                    (m21 - m12) / scale).Normalized;
            }

            if (m11 > m22)
            {
                double scale = Math.Sqrt(1.0 + m11 - m00 - m22) * 2.0;
                return new DQuaternion(
                    (m01 + m10) / scale,
                    0.25 * scale,
                    (m12 + m21) / scale,
                    (m02 - m20) / scale).Normalized;
            }

            double finalScale = Math.Sqrt(1.0 + m22 - m00 - m11) * 2.0;
            return new DQuaternion(
                (m02 + m20) / finalScale,
                (m12 + m21) / finalScale,
                0.25 * finalScale,
                (m10 - m01) / finalScale).Normalized;
        }

        private static double NormalizeAnglePositive(double angleRad)
        {
            double twoPi = Math.PI * 2.0;
            double normalized = angleRad % twoPi;
            return normalized < 0.0 ? normalized + twoPi : normalized;
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            return value < minimum ? minimum : value > maximum ? maximum : value;
        }
    }
}
