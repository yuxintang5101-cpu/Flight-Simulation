using System;

namespace FlightSim.Platform.Contracts
{
    [Serializable]
    public struct AircraftInitialCondition
    {
        public StartupPreset Preset;
        public AircraftSpawnMode SpawnMode;
        public double LongitudeRad;
        public double LatitudeRad;
        public double EllipsoidHeightM;
        public double HeadingRad;
        public double PitchRad;
        public double RollRad;
        public double TrueAirspeedMps;
        public double BodyAngularVelocityXRadps;
        public double BodyAngularVelocityYRadps;
        public double BodyAngularVelocityZRadps;
        public double InternalFuelFraction;
        public double ExternalFuelKg;
        public double BingoFuelKg;
        public double JokerFuelKg;
        public bool LandingGearDown;
        public StoreLoadoutCollection Loadout;

        public static AircraftInitialCondition CreateForPreset(StartupPreset preset)
        {
            const double degreesToRadians = Math.PI / 180.0;
            bool airborne = preset == StartupPreset.Airborne;
            return new AircraftInitialCondition
            {
                Preset = preset,
                SpawnMode = airborne ? AircraftSpawnMode.Geodetic : AircraftSpawnMode.KtexRunway27,
                LongitudeRad = -107.9087 * degreesToRadians,
                LatitudeRad = 37.9538 * degreesToRadians,
                EllipsoidHeightM = airborne ? 4265.0 : 2765.0,
                HeadingRad = 285.0 * degreesToRadians,
                PitchRad = airborne ? 2.5 * degreesToRadians : 0.0,
                TrueAirspeedMps = airborne ? 180.0 : 0.0,
                InternalFuelFraction = 0.70,
                ExternalFuelKg = 0.0,
                BingoFuelKg = 450.0,
                JokerFuelKg = 900.0,
                LandingGearDown = !airborne
            };
        }

        public static AircraftInitialCondition CreateAirborneDefault()
        {
            return CreateForPreset(StartupPreset.Airborne);
        }
    }

    [Serializable]
    public struct AircraftRegistration
    {
        public AircraftId Aircraft;
        public bool IsLocalPilot;
        public AircraftIdentityState Identity;
        public AircraftInitialCondition InitialCondition;

        public static AircraftRegistration Create(
            AircraftId aircraft,
            bool isLocalPilot,
            in AircraftInitialCondition initialCondition)
        {
            return new AircraftRegistration
            {
                Aircraft = aircraft,
                IsLocalPilot = isLocalPilot,
                Identity = AircraftIdentityState.CreateDefault(aircraft, isLocalPilot),
                InitialCondition = initialCondition
            };
        }
    }

    [Serializable]
    public struct ResetScenarioCommand : ISimulationCommand
    {
        public AircraftId Aircraft;
        public string ScenarioId;
    }

    [Serializable]
    public struct StartupPresetCommand : ISimulationCommand
    {
        public AircraftId Aircraft;
        public StartupPreset Preset;
    }

    [Serializable]
    public struct SystemSwitchCommand : ISimulationCommand
    {
        public AircraftId Aircraft;
        public AircraftSystemSwitch Switch;
        public bool IsEnabled;
    }

    [Serializable]
    public struct LandingGearCommand : ISimulationCommand
    {
        public AircraftId Aircraft;
        public bool IsDown;
    }

    [Serializable]
    public struct LoadoutConfigurationCommand : ISimulationCommand
    {
        public AircraftId Aircraft;
        public StoreLoadoutCollection Stations;

        public static LoadoutConfigurationCommand Create(AircraftId aircraft, in StoreLoadoutCollection stations)
        {
            return new LoadoutConfigurationCommand { Aircraft = aircraft, Stations = stations };
        }
    }

    [Serializable]
    public struct FormationCommand : ISimulationCommand
    {
        public AircraftId Aircraft;
        public FormationCommandType Type;
        public AircraftId LeaderAircraft;
        public double ForwardOffsetM;
        public double RightOffsetM;
        public double UpOffsetM;
    }

    [Serializable]
    public struct ReleaseStoreCommand : ISimulationCommand
    {
        public AircraftId Aircraft;
        public int StationIndex;
        public int Quantity;
        public bool EmergencyJettison;
    }

    [Serializable]
    public struct InjectFailureCommand : ISimulationCommand
    {
        public AircraftId Aircraft;
        public FailureType Failure;
        public double SeverityNormalized;
        public double DurationS;
    }

    [Serializable]
    public struct StoreLoadoutConfiguration
    {
        public int StationIndex;
        public string StoreType;
        public int Quantity;
    }

    [Serializable]
    public struct StoreLoadoutCollection
    {
        public StoreLoadoutConfiguration Station0;
        public StoreLoadoutConfiguration Station1;
        public StoreLoadoutConfiguration Station2;
        public StoreLoadoutConfiguration Station3;
        public StoreLoadoutConfiguration Station4;
        public StoreLoadoutConfiguration Station5;
        public StoreLoadoutConfiguration Station6;
        public StoreLoadoutConfiguration Station7;
        public StoreLoadoutConfiguration Station8;

        public int Length => StoresState.StationCapacity;

        public StoreLoadoutConfiguration this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Station0;
                    case 1: return Station1;
                    case 2: return Station2;
                    case 3: return Station3;
                    case 4: return Station4;
                    case 5: return Station5;
                    case 6: return Station6;
                    case 7: return Station7;
                    case 8: return Station8;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: Station0 = value; break;
                    case 1: Station1 = value; break;
                    case 2: Station2 = value; break;
                    case 3: Station3 = value; break;
                    case 4: Station4 = value; break;
                    case 5: Station5 = value; break;
                    case 6: Station6 = value; break;
                    case 7: Station7 = value; break;
                    case 8: Station8 = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }
    }

    public enum StartupPreset
    {
        ColdAndDark,
        RunwayReady,
        Airborne
    }

    public enum AircraftSpawnMode
    {
        KtexRunway27,
        Geodetic
    }

    public enum AircraftSystemSwitch
    {
        Battery,
        Generator,
        FuelPump,
        HydraulicSystemA,
        HydraulicSystemB,
        FlightControlComputer,
        Radar,
        DataLink
    }

    public enum FormationCommandType
    {
        Join,
        Leave,
        SetLeader,
        SetOffset,
        Rejoin,
        Maintain,
        BreakAway,
        ReturnToBase
    }

    public enum FailureType
    {
        Engine,
        Electrical,
        Hydraulic,
        FlightControl,
        Fuel,
        Avionics
    }
}
