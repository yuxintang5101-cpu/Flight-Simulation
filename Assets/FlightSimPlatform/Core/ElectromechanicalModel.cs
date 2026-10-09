using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Core
{
    public sealed class F16ElectromechanicalModel
    {
        private const double NominalBusVoltageV = 28.0;
        private const double NominalHydraulicPressurePa = 20_684_271.9;
        private const double HydraulicRiseRatePaps = 12_000_000.0;
        private const double HydraulicDecayRatePaps = 18_000_000.0;

        private ElectricalState electrical;
        private HydraulicState hydraulics;
        private LandingGearState landingGear;
        private WarningState warnings;
        private WarningState latchedWarnings;

        private bool batterySwitchEnabled;
        private bool generatorSwitchEnabled;
        private bool hydraulicASwitchEnabled;
        private bool hydraulicBSwitchEnabled;
        private bool flightControlComputerSwitchEnabled;
        private bool gearDownCommand;
        private bool noseWheelSteeringCommand;
        private bool weightOnWheels;
        private double leftBrakeCommand;
        private double rightBrakeCommand;
        private double speedBrakeCommand;
        private double canopyCommand;
        private double batteryChargeNormalized;
        private double actuatorAuthorityNormalized;

        private TimedFailure engineFailure;
        private TimedFailure electricalFailure;
        private TimedFailure hydraulicFailure;
        private TimedFailure flightControlFailure;
        private TimedFailure fuelFailure;
        private TimedFailure avionicsFailure;

        public F16ElectromechanicalModel()
        {
            Reset(StartupPreset.ColdAndDark);
        }

        public ElectricalState Electrical => electrical;

        public HydraulicState Hydraulics => hydraulics;

        public LandingGearState LandingGear => landingGear;

        public WarningState Warnings => warnings;

        public double ActuatorAuthorityNormalized => actuatorAuthorityNormalized;

        public double BatteryChargeNormalized => batteryChargeNormalized;

        public void Reset(StartupPreset preset)
        {
            ClearAllFailures();
            warnings = default(WarningState);
            latchedWarnings = default(WarningState);
            electrical = default(ElectricalState);
            hydraulics = default(HydraulicState);
            landingGear = default(LandingGearState);
            batteryChargeNormalized = 1.0;
            leftBrakeCommand = 0.0;
            rightBrakeCommand = 0.0;
            speedBrakeCommand = 0.0;
            noseWheelSteeringCommand = false;

            switch (preset)
            {
                case StartupPreset.RunwayReady:
                    batterySwitchEnabled = true;
                    generatorSwitchEnabled = true;
                    hydraulicASwitchEnabled = true;
                    hydraulicBSwitchEnabled = true;
                    flightControlComputerSwitchEnabled = true;
                    gearDownCommand = true;
                    canopyCommand = 0.0;
                    weightOnWheels = true;
                    electrical = CreatePoweredElectricalState();
                    hydraulics = CreatePressurizedHydraulicState();
                    landingGear.NoseGearPositionNormalized = 1.0;
                    landingGear.LeftMainGearPositionNormalized = 1.0;
                    landingGear.RightMainGearPositionNormalized = 1.0;
                    landingGear.WeightOnWheels = true;
                    actuatorAuthorityNormalized = 1.0;
                    break;
                case StartupPreset.Airborne:
                    batterySwitchEnabled = true;
                    generatorSwitchEnabled = true;
                    hydraulicASwitchEnabled = true;
                    hydraulicBSwitchEnabled = true;
                    flightControlComputerSwitchEnabled = true;
                    gearDownCommand = false;
                    canopyCommand = 0.0;
                    weightOnWheels = false;
                    electrical = CreatePoweredElectricalState();
                    hydraulics = CreatePressurizedHydraulicState();
                    actuatorAuthorityNormalized = 1.0;
                    break;
                case StartupPreset.ColdAndDark:
                default:
                    batterySwitchEnabled = false;
                    generatorSwitchEnabled = false;
                    hydraulicASwitchEnabled = false;
                    hydraulicBSwitchEnabled = false;
                    flightControlComputerSwitchEnabled = false;
                    gearDownCommand = true;
                    canopyCommand = 1.0;
                    weightOnWheels = true;
                    landingGear.NoseGearPositionNormalized = 1.0;
                    landingGear.LeftMainGearPositionNormalized = 1.0;
                    landingGear.RightMainGearPositionNormalized = 1.0;
                    landingGear.CanopyPositionNormalized = 1.0;
                    landingGear.WeightOnWheels = true;
                    actuatorAuthorityNormalized = 0.0;
                    break;
            }
        }

        public void Update(bool engineRunning, double deltaTimeS)
        {
            double dt = Max(0.0, deltaTimeS);
            double electricalSeverity = electricalFailure.Severity;
            bool generatorAvailable = generatorSwitchEnabled && engineRunning && electricalSeverity < 0.999;

            UpdateBattery(generatorAvailable, dt);
            UpdateElectrical(generatorAvailable, electricalSeverity);
            UpdateHydraulics(engineRunning, dt);
            UpdateMechanisms(dt);
            UpdateWarnings(engineRunning);
            AdvanceFailures(dt);
        }

        public void InjectFailure(FailureType failure, double severityNormalized, double durationS)
        {
            TimedFailure injected = TimedFailure.Create(severityNormalized, durationS);
            switch (failure)
            {
                case FailureType.Engine:
                    engineFailure = injected;
                    latchedWarnings.FireWarning = injected.Severity > 0.0;
                    latchedWarnings.MasterWarning = injected.Severity >= 0.8;
                    break;
                case FailureType.Electrical:
                    electricalFailure = injected;
                    latchedWarnings.ElectricalWarning = injected.Severity > 0.0;
                    latchedWarnings.MasterCaution = injected.Severity > 0.0;
                    break;
                case FailureType.Hydraulic:
                    hydraulicFailure = injected;
                    latchedWarnings.HydraulicWarning = injected.Severity > 0.0;
                    latchedWarnings.MasterCaution = injected.Severity > 0.0;
                    break;
                case FailureType.FlightControl:
                    flightControlFailure = injected;
                    latchedWarnings.MasterCaution = injected.Severity > 0.0;
                    break;
                case FailureType.Fuel:
                    fuelFailure = injected;
                    latchedWarnings.FuelWarning = injected.Severity > 0.0;
                    latchedWarnings.MasterCaution = injected.Severity > 0.0;
                    break;
                case FailureType.Avionics:
                    avionicsFailure = injected;
                    latchedWarnings.ElectricalWarning = injected.Severity > 0.0;
                    latchedWarnings.MasterCaution = injected.Severity > 0.0;
                    break;
            }

            warnings = MergeWarnings(warnings, latchedWarnings);
        }

        public void ClearFailure(FailureType failure)
        {
            switch (failure)
            {
                case FailureType.Engine:
                    engineFailure = default(TimedFailure);
                    break;
                case FailureType.Electrical:
                    electricalFailure = default(TimedFailure);
                    break;
                case FailureType.Hydraulic:
                    hydraulicFailure = default(TimedFailure);
                    break;
                case FailureType.FlightControl:
                    flightControlFailure = default(TimedFailure);
                    break;
                case FailureType.Fuel:
                    fuelFailure = default(TimedFailure);
                    break;
                case FailureType.Avionics:
                    avionicsFailure = default(TimedFailure);
                    break;
            }
        }

        public void ClearAllFailures()
        {
            engineFailure = default(TimedFailure);
            electricalFailure = default(TimedFailure);
            hydraulicFailure = default(TimedFailure);
            flightControlFailure = default(TimedFailure);
            fuelFailure = default(TimedFailure);
            avionicsFailure = default(TimedFailure);
        }

        public bool HasActiveFailure(FailureType failure)
        {
            switch (failure)
            {
                case FailureType.Engine: return engineFailure.IsActive;
                case FailureType.Electrical: return electricalFailure.IsActive;
                case FailureType.Hydraulic: return hydraulicFailure.IsActive;
                case FailureType.FlightControl: return flightControlFailure.IsActive;
                case FailureType.Fuel: return fuelFailure.IsActive;
                case FailureType.Avionics: return avionicsFailure.IsActive;
                default: return false;
            }
        }

        public void ClearLatchedWarnings()
        {
            latchedWarnings = default(WarningState);
            warnings = default(WarningState);
        }

        public void SetSwitch(AircraftSystemSwitch systemSwitch, bool enabled)
        {
            switch (systemSwitch)
            {
                case AircraftSystemSwitch.Battery:
                    batterySwitchEnabled = enabled;
                    break;
                case AircraftSystemSwitch.Generator:
                    generatorSwitchEnabled = enabled;
                    break;
                case AircraftSystemSwitch.HydraulicSystemA:
                    hydraulicASwitchEnabled = enabled;
                    break;
                case AircraftSystemSwitch.HydraulicSystemB:
                    hydraulicBSwitchEnabled = enabled;
                    break;
                case AircraftSystemSwitch.FlightControlComputer:
                    flightControlComputerSwitchEnabled = enabled;
                    break;
            }
        }

        public void SetLandingGearCommand(bool down)
        {
            gearDownCommand = down;
        }

        public void ConfigureInitialLandingGear(bool down, bool isWeightOnWheels)
        {
            gearDownCommand = down;
            double position = down ? 1.0 : 0.0;
            landingGear.NoseGearPositionNormalized = position;
            landingGear.LeftMainGearPositionNormalized = position;
            landingGear.RightMainGearPositionNormalized = position;
            weightOnWheels = isWeightOnWheels;
            landingGear.WeightOnWheels = isWeightOnWheels;
        }

        public void SetGearDown(bool down)
        {
            SetLandingGearCommand(down);
        }

        public void SetBrakeCommand(double normalized)
        {
            SetWheelBrakeCommands(normalized, normalized);
        }

        public void SetWheelBrakeCommands(double leftNormalized, double rightNormalized)
        {
            leftBrakeCommand = Clamp01(leftNormalized);
            rightBrakeCommand = Clamp01(rightNormalized);
        }

        public void SetSpeedBrakeCommand(double normalized)
        {
            speedBrakeCommand = Clamp01(normalized);
        }

        public void SetCanopyCommand(double normalized)
        {
            canopyCommand = Clamp01(normalized);
        }

        public void SetWeightOnWheels(bool isWeightOnWheels)
        {
            weightOnWheels = isWeightOnWheels;
            landingGear.WeightOnWheels = isWeightOnWheels;
        }

        public void SetNoseWheelSteeringEnabled(bool enabled)
        {
            noseWheelSteeringCommand = enabled;
        }

        private void UpdateBattery(bool generatorAvailable, double dt)
        {
            if (generatorAvailable)
            {
                batteryChargeNormalized = MoveTowards(batteryChargeNormalized, 1.0, 0.015 * dt);
            }
            else if (batterySwitchEnabled)
            {
                batteryChargeNormalized = MoveTowards(batteryChargeNormalized, 0.0, 0.0025 * dt);
            }
        }

        private void UpdateElectrical(bool generatorAvailable, double electricalSeverity)
        {
            electrical.GeneratorOnline = generatorAvailable;
            electrical.BatteryOnline = batterySwitchEnabled &&
                batteryChargeNormalized > 0.01 &&
                electricalSeverity < 0.999;
            electrical.BatteryVoltageV = 20.0 + (8.0 * batteryChargeNormalized);
            electrical.GeneratorCurrentA = generatorAvailable
                ? 55.0 * (1.0 - electricalSeverity)
                : 0.0;

            double sourceVoltageV = generatorAvailable
                ? NominalBusVoltageV
                : electrical.BatteryOnline ? electrical.BatteryVoltageV : 0.0;
            sourceVoltageV *= 1.0 - electricalSeverity;
            electrical.MainBusVoltageV = sourceVoltageV;
            electrical.EssentialBusVoltageV = sourceVoltageV > 0.0 ? Max(0.0, sourceVoltageV - 0.3) : 0.0;
            electrical.AvionicsBusVoltageV = sourceVoltageV > 0.0 ? Max(0.0, sourceVoltageV - 0.5) : 0.0;
            electrical.MainBusPowered = electrical.MainBusVoltageV >= 20.0;
            electrical.EssentialBusPowered = electrical.EssentialBusVoltageV >= 18.0;
            electrical.AvionicsBusPowered = electrical.AvionicsBusVoltageV >= 20.0 && avionicsFailure.Severity < 0.999;
        }

        private void UpdateHydraulics(bool engineRunning, double dt)
        {
            double hydraulicSeverity = hydraulicFailure.Severity;
            bool totalHydraulicFailure = hydraulicSeverity >= 0.999;
            double systemATargetPressurePa = NominalHydraulicPressurePa * (1.0 - hydraulicSeverity);
            bool systemAAvailable = engineRunning && hydraulicASwitchEnabled && hydraulicSeverity < 0.999;
            bool systemBAvailable = engineRunning && hydraulicBSwitchEnabled && !totalHydraulicFailure;

            hydraulics.SystemAPressurePa = UpdateHydraulicPressure(
                hydraulics.SystemAPressurePa,
                systemAAvailable ? systemATargetPressurePa : 0.0,
                hydraulicSeverity,
                dt);
            hydraulics.SystemBPressurePa = UpdateHydraulicPressure(
                hydraulics.SystemBPressurePa,
                systemBAvailable ? NominalHydraulicPressurePa : 0.0,
                totalHydraulicFailure ? 1.0 : 0.0,
                dt);
            hydraulics.SystemAOnline = systemAAvailable && hydraulics.SystemAPressurePa >= NominalHydraulicPressurePa * 0.55;
            hydraulics.SystemBOnline = systemBAvailable && hydraulics.SystemBPressurePa >= NominalHydraulicPressurePa * 0.55;

            double hydraulicAuthority = Max(
                hydraulics.SystemAPressurePa / NominalHydraulicPressurePa,
                hydraulics.SystemBPressurePa / NominalHydraulicPressurePa);
            actuatorAuthorityNormalized = electrical.EssentialBusPowered && flightControlComputerSwitchEnabled
                ? Clamp01(hydraulicAuthority) * (1.0 - flightControlFailure.Severity)
                : 0.0;
        }

        private void UpdateMechanisms(double dt)
        {
            double hydraulicAuthority = Clamp01(Max(
                hydraulics.SystemAPressurePa / NominalHydraulicPressurePa,
                hydraulics.SystemBPressurePa / NominalHydraulicPressurePa));
            double gearTarget = gearDownCommand ? 1.0 : 0.0;
            double gearRate = 0.45 * hydraulicAuthority;
            if (gearDownCommand && gearRate < 0.08)
            {
                gearRate = 0.08;
            }

            landingGear.NoseGearPositionNormalized = MoveTowards(
                landingGear.NoseGearPositionNormalized,
                gearTarget,
                gearRate * dt);
            landingGear.LeftMainGearPositionNormalized = MoveTowards(
                landingGear.LeftMainGearPositionNormalized,
                gearTarget,
                gearRate * dt);
            landingGear.RightMainGearPositionNormalized = MoveTowards(
                landingGear.RightMainGearPositionNormalized,
                gearTarget,
                gearRate * dt);

            double brakeSupplyPa = hydraulics.SystemAOnline
                ? hydraulics.SystemAPressurePa
                : hydraulics.SystemBOnline ? hydraulics.SystemBPressurePa * 0.5 : 0.0;
            landingGear.LeftBrakePressurePa = brakeSupplyPa * leftBrakeCommand;
            landingGear.RightBrakePressurePa = brakeSupplyPa * rightBrakeCommand;
            hydraulics.BrakePressurePa = 0.5 *
                (landingGear.LeftBrakePressurePa + landingGear.RightBrakePressurePa);

            double speedBrakeRate = 0.8 * hydraulicAuthority;
            landingGear.SpeedBrakePositionNormalized = MoveTowards(
                landingGear.SpeedBrakePositionNormalized,
                speedBrakeCommand,
                speedBrakeRate * dt);

            if (electrical.EssentialBusPowered)
            {
                landingGear.CanopyPositionNormalized = MoveTowards(
                    landingGear.CanopyPositionNormalized,
                    canopyCommand,
                    0.35 * dt);
            }

            landingGear.WeightOnWheels = weightOnWheels;
            landingGear.NoseWheelSteeringEnabled = noseWheelSteeringCommand &&
                weightOnWheels &&
                landingGear.NoseGearPositionNormalized >= 0.98 &&
                hydraulics.SystemAOnline;
        }

        private void UpdateWarnings(bool engineRunning)
        {
            WarningState active = default(WarningState);
            active.FireWarning = engineFailure.Severity >= 0.8;
            active.HydraulicWarning = hydraulicFailure.Severity > 0.0 ||
                ((hydraulicASwitchEnabled || hydraulicBSwitchEnabled) &&
                 (!hydraulics.SystemAOnline || !hydraulics.SystemBOnline));
            active.ElectricalWarning = electricalFailure.Severity > 0.0 ||
                ((batterySwitchEnabled || generatorSwitchEnabled) && !electrical.EssentialBusPowered) ||
                avionicsFailure.Severity > 0.0;
            active.FuelWarning = fuelFailure.Severity > 0.0;
            active.LandingGearWarning = weightOnWheels &&
                (landingGear.NoseGearPositionNormalized < 0.98 ||
                 landingGear.LeftMainGearPositionNormalized < 0.98 ||
                 landingGear.RightMainGearPositionNormalized < 0.98);
            active.CanopyWarning = engineRunning && landingGear.CanopyPositionNormalized > 0.02;
            active.MasterWarning = active.FireWarning;
            active.MasterCaution = active.HydraulicWarning ||
                active.ElectricalWarning ||
                active.FuelWarning ||
                active.LandingGearWarning ||
                active.CanopyWarning ||
                flightControlFailure.Severity > 0.0;

            latchedWarnings = MergeWarnings(latchedWarnings, active);
            warnings = latchedWarnings;
        }

        private void AdvanceFailures(double dt)
        {
            engineFailure.Advance(dt);
            electricalFailure.Advance(dt);
            hydraulicFailure.Advance(dt);
            flightControlFailure.Advance(dt);
            fuelFailure.Advance(dt);
            avionicsFailure.Advance(dt);
        }

        private static double UpdateHydraulicPressure(
            double currentPressurePa,
            double targetPressurePa,
            double severity,
            double dt)
        {
            if (severity >= 0.999)
            {
                return 0.0;
            }

            double ratePaps = targetPressurePa > currentPressurePa
                ? HydraulicRiseRatePaps
                : HydraulicDecayRatePaps;
            return MoveTowards(currentPressurePa, targetPressurePa, ratePaps * dt);
        }

        private static ElectricalState CreatePoweredElectricalState()
        {
            return new ElectricalState
            {
                MainBusVoltageV = NominalBusVoltageV,
                EssentialBusVoltageV = NominalBusVoltageV - 0.3,
                AvionicsBusVoltageV = NominalBusVoltageV - 0.5,
                BatteryVoltageV = NominalBusVoltageV,
                GeneratorCurrentA = 55.0,
                MainBusPowered = true,
                EssentialBusPowered = true,
                AvionicsBusPowered = true,
                GeneratorOnline = true,
                BatteryOnline = true
            };
        }

        private static HydraulicState CreatePressurizedHydraulicState()
        {
            return new HydraulicState
            {
                SystemAPressurePa = NominalHydraulicPressurePa,
                SystemBPressurePa = NominalHydraulicPressurePa,
                SystemAOnline = true,
                SystemBOnline = true
            };
        }

        private static WarningState MergeWarnings(WarningState left, WarningState right)
        {
            return new WarningState
            {
                MasterCaution = left.MasterCaution || right.MasterCaution,
                MasterWarning = left.MasterWarning || right.MasterWarning,
                FireWarning = left.FireWarning || right.FireWarning,
                HydraulicWarning = left.HydraulicWarning || right.HydraulicWarning,
                ElectricalWarning = left.ElectricalWarning || right.ElectricalWarning,
                FuelWarning = left.FuelWarning || right.FuelWarning,
                LowAltitudeWarning = left.LowAltitudeWarning || right.LowAltitudeWarning,
                OverspeedWarning = left.OverspeedWarning || right.OverspeedWarning,
                StallWarning = left.StallWarning || right.StallWarning,
                LandingGearWarning = left.LandingGearWarning || right.LandingGearWarning,
                CanopyWarning = left.CanopyWarning || right.CanopyWarning
            };
        }

        private static double MoveTowards(double current, double target, double maximumDelta)
        {
            if (current < target)
            {
                return Min(current + maximumDelta, target);
            }

            return Max(current - maximumDelta, target);
        }

        private static double Clamp01(double value)
        {
            return value < 0.0 ? 0.0 : value > 1.0 ? 1.0 : value;
        }

        private static double Min(double left, double right)
        {
            return left < right ? left : right;
        }

        private static double Max(double left, double right)
        {
            return left > right ? left : right;
        }

        private struct TimedFailure
        {
            public double Severity;
            public double RemainingS;
            public bool Persistent;

            public bool IsActive => Severity > 0.0;

            public static TimedFailure Create(double severityNormalized, double durationS)
            {
                return new TimedFailure
                {
                    Severity = Clamp01(severityNormalized),
                    RemainingS = Max(0.0, durationS),
                    Persistent = durationS <= 0.0
                };
            }

            public void Advance(double dt)
            {
                if (Persistent || Severity <= 0.0 || dt <= 0.0)
                {
                    return;
                }

                RemainingS -= dt;
                if (RemainingS <= 0.0)
                {
                    this = default(TimedFailure);
                }
            }
        }
    }
}
