using System;

namespace FlightSim.Platform.Contracts
{
    [Serializable]
    public struct AircraftIdentityState
    {
        public AircraftId Aircraft;
        public string Callsign;
        public AircraftSide Side;
        public AircraftRole Role;
        public string SimulationModelId;
        public string VisualModelId;

        public static AircraftIdentityState CreateDefault(AircraftId aircraft, bool isLocalPilot)
        {
            return new AircraftIdentityState
            {
                Aircraft = aircraft,
                Callsign = aircraft.Value,
                Side = AircraftSide.Friendly,
                Role = isLocalPilot ? AircraftRole.Player : AircraftRole.Wingman,
                SimulationModelId = "F16_SEMI_REAL_V1",
                VisualModelId = "F35_VISUAL_V1"
            };
        }
    }

    [Serializable]
    public struct AircraftSnapshot
    {
        public AircraftId Aircraft;
        public AircraftFastState Fast;
        public AircraftSystemsState Systems;
        public TacticalPictureState Tactical;

        public static AircraftSnapshot CreateDefault(AircraftId aircraft)
        {
            return new AircraftSnapshot
            {
                Aircraft = aircraft,
                Fast = AircraftFastState.CreateDefault(aircraft),
                Systems = AircraftSystemsState.CreateDefault(aircraft),
                Tactical = TacticalPictureState.CreateDefault(aircraft)
            };
        }
    }

    [Serializable]
    public struct AircraftFastState
    {
        public AircraftId Aircraft;
        public ulong Tick;
        public double SimulationTimeS;
        public double EcefPositionXM;
        public double EcefPositionYM;
        public double EcefPositionZM;
        public double EcefVelocityXMps;
        public double EcefVelocityYMps;
        public double EcefVelocityZMps;
        public double BodyVelocityXMps;
        public double BodyVelocityYMps;
        public double BodyVelocityZMps;
        public double BodyAngularVelocityXRadps;
        public double BodyAngularVelocityYRadps;
        public double BodyAngularVelocityZRadps;
        public double BodyToEcefQuaternionX;
        public double BodyToEcefQuaternionY;
        public double BodyToEcefQuaternionZ;
        public double BodyToEcefQuaternionW;
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
        public double NormalLoadFactorG;
        public double MeanSeaLevelAltitudeM;
        public double AboveGroundLevelAltitudeM;
        public double ClimbRateMps;
        public bool TerrainSampleValid;
        public double TerrainSampleAgeS;

        public static AircraftFastState CreateDefault(AircraftId aircraft)
        {
            return new AircraftFastState
            {
                Aircraft = aircraft,
                BodyToEcefQuaternionW = 1.0
            };
        }
    }

    [Serializable]
    public struct AircraftSystemsState
    {
        public AircraftId Aircraft;
        public double SimulationTimeS;
        public FlightControlState FlightControls;
        public PropulsionState Propulsion;
        public FuelState Fuel;
        public ElectricalState Electrical;
        public HydraulicState Hydraulics;
        public LandingGearState LandingGear;
        public StoresState Stores;
        public AvionicsState Avionics;
        public WarningState Warnings;

        public static AircraftSystemsState CreateDefault()
        {
            return CreateDefault(default(AircraftId));
        }

        public static AircraftSystemsState CreateDefault(AircraftId aircraft)
        {
            return new AircraftSystemsState
            {
                Aircraft = aircraft,
                Stores = StoresState.CreateDefault()
            };
        }
    }

    [Serializable]
    public struct FlightControlState
    {
        public ControlAuthority ActiveControlAuthority;
        public double PitchCommandNormalized;
        public double RollCommandNormalized;
        public double YawCommandNormalized;
        public double AileronDeflectionRad;
        public double ElevatorDeflectionRad;
        public double RudderDeflectionRad;
        public double LeadingEdgeFlapDeflectionRad;
        public double TrailingEdgeFlapDeflectionRad;
        public bool AngleOfAttackLimiterActive;
        public bool GForceLimiterActive;
        public bool RollRateLimiterActive;
        public bool FlightControlComputerEnabled;
        public bool AutopilotEngaged;
    }

    [Serializable]
    public struct PropulsionState
    {
        public EngineMode Mode;
        public bool EngineRunning;
        public bool AfterburnerActive;
        public double N1Percent;
        public double N2Percent;
        public double ExhaustGasTemperatureC;
        public double FuelFlowKgps;
        public double NozzlePositionNormalized;
        public double ThrustN;
    }

    [Serializable]
    public struct FuelState
    {
        public double InternalFuelKg;
        public double ExternalFuelKg;
        public double LeftFuelKg;
        public double RightFuelKg;
        public double TotalFuelKg;
        public FuelTransferMode TransferMode;
        public double FuelImbalanceKg;
        public double BingoFuelKg;
        public double JokerFuelKg;
        public double CenterOfGravityPercentMac;
        public double FuelFlowKgps;
        public bool LowFuelWarning;
        public bool FuelPumpEnabled;
    }

    [Serializable]
    public struct ElectricalState
    {
        public double MainBusVoltageV;
        public double EssentialBusVoltageV;
        public double AvionicsBusVoltageV;
        public double BatteryVoltageV;
        public double GeneratorCurrentA;
        public bool MainBusPowered;
        public bool EssentialBusPowered;
        public bool AvionicsBusPowered;
        public bool GeneratorOnline;
        public bool BatteryOnline;
    }

    [Serializable]
    public struct HydraulicState
    {
        public double SystemAPressurePa;
        public double SystemBPressurePa;
        public double BrakePressurePa;
        public bool SystemAOnline;
        public bool SystemBOnline;
    }

    [Serializable]
    public struct LandingGearState
    {
        public double NoseGearPositionNormalized;
        public double LeftMainGearPositionNormalized;
        public double RightMainGearPositionNormalized;
        public double LeftBrakePressurePa;
        public double RightBrakePressurePa;
        public double SpeedBrakePositionNormalized;
        public double CanopyPositionNormalized;
        public bool NoseWheelSteeringEnabled;
        public bool WeightOnWheels;
    }

    [Serializable]
    public struct StoresState
    {
        public const int StationCapacity = 9;

        public StoreStationCollection Stations;

        public static StoresState CreateDefault()
        {
            return new StoresState { Stations = new StoreStationCollection() };
        }
    }

    [Serializable]
    public struct StoreStationState
    {
        public int StationIndex;
        public string StoreType;
        public int Quantity;
        public double StoreMassKg;
        public double DragCoefficient;
        public double LongitudinalCgM;
        public double LateralCgM;
        public bool IsArmed;
        public bool IsSelected;
        public bool IsReady;
        public bool IsReleased;
    }

    [Serializable]
    public struct AvionicsState
    {
        public InertialNavigationState InsState;
        public bool HudEnabled;
        public HudMode HudMode;
        public AircraftStartupState StartupState;
        public StartupPreset ActiveStartupPreset;
        public bool MasterModeAirToAir;
        public bool RadarEnabled;
        public bool RadarLocked;
        public double RadarRangeM;
        public bool NavigationComputerEnabled;
        public bool DataLinkEnabled;
        public HudState Hud;
    }

    [Serializable]
    public struct HudState
    {
        public bool Enabled;
        public bool IsValid;
        public HudMode Mode;
        public HudScaleMode ScaleMode;
        public HudVelocitySource VelocitySource;
        public HudAltitudeSource AltitudeSource;
        public double BrightnessNormalized;
        public bool LandingDeclutter;
        public bool DriftCutout;
        public bool ShowFlightPathMarker;
        public double CalibratedAirspeedMps;
        public double TrueAirspeedMps;
        public double GroundSpeedMps;
        public double BarometricAltitudeM;
        public double RadarAltitudeM;
        public bool RadarAltitudeValid;
        public double HeadingRad;
        public double PitchRad;
        public double RollRad;
        public double FlightPathAzimuthRad;
        public double FlightPathElevationRad;
        public double AngleOfAttackRad;
        public double NormalLoadFactorG;
        public double MaximumRecordedLoadFactorG;
        public double Mach;
        public double VerticalSpeedMps;
        public double AltitudeLowWarningM;
        public bool SteerpointValid;
        public int SelectedSteerpointIndex;
        public double SteerpointBearingRad;
        public double SteerpointElevationRad;
        public double SteerpointDistanceM;
        public double SteerpointSlantRangeM;
        public double SteerpointTimeToGoS;
        public double GreatCircleSteeringErrorRad;
        public bool IlsEnabled;
        public bool LocalizerValid;
        public double LocalizerDeviationNormalized;
        public bool GlideslopeValid;
        public double GlideslopeDeviationNormalized;
        public double CommandSteeringLateralNormalized;
        public double CommandSteeringVerticalNormalized;
        public bool MasterCaution;
        public bool MasterWarning;
        public bool WeightOnWheels;
        public bool LandingGearDown;
        public MasterArmState MasterArm;
        public bool MasterArmEnabled;
        public string SelectedStoreType;
        public int SelectedStoreQuantity;
    }

    [Serializable]
    public struct WarningState
    {
        public bool MasterCaution;
        public bool MasterWarning;
        public bool FireWarning;
        public bool HydraulicWarning;
        public bool ElectricalWarning;
        public bool FuelWarning;
        public bool LowAltitudeWarning;
        public bool OverspeedWarning;
        public bool StallWarning;
        public bool LandingGearWarning;
        public bool CanopyWarning;
    }

    [Serializable]
    public struct TacticalPictureState
    {
        public const int TrackCapacity = 32;
        public const int WaypointCapacity = 16;

        public AircraftId Aircraft;
        public double SimulationTimeS;
        public FormationState Formation;
        public TacticalTrackCollection Tracks;
        public WaypointCollection Waypoints;

        public static TacticalPictureState CreateDefault()
        {
            return CreateDefault(default(AircraftId));
        }

        public static TacticalPictureState CreateDefault(AircraftId aircraft)
        {
            return new TacticalPictureState
            {
                Aircraft = aircraft,
                Tracks = new TacticalTrackCollection(),
                Waypoints = new WaypointCollection()
            };
        }
    }

    [Serializable]
    public struct FormationState
    {
        public AircraftId LeaderAircraft;
        public int FormationSlot;
        public double DesiredForwardOffsetM;
        public double DesiredRightOffsetM;
        public double DesiredUpOffsetM;
        public bool IsFormationActive;
        public FormationMode Mode;
    }

    [Serializable]
    public struct WaypointState
    {
        public int Index;
        public double LongitudeRad;
        public double LatitudeRad;
        public double EllipsoidHeightM;
        public double EcefPositionXM;
        public double EcefPositionYM;
        public double EcefPositionZM;
        public string Name;
        public bool IsActive;
        public bool IsValid;
    }

    [Serializable]
    public struct TacticalTrack
    {
        public int TrackId;
        public TacticalTrackClassification Classification;
        public TacticalTrackAffiliation Affiliation;
        public double EcefPositionXM;
        public double EcefPositionYM;
        public double EcefPositionZM;
        public double EcefVelocityXMps;
        public double EcefVelocityYMps;
        public double EcefVelocityZMps;
        public double RangeM;
        public double BearingRad;
        public double ElevationRad;
        public double ConfidenceNormalized;
        public bool IsValid;
    }

    [Serializable]
    public struct SimulationEvent
    {
        public AircraftId Aircraft;
        public double SimulationTimeS;
        public SimulationEventType Type;
        public int Code;
        public string Message;
    }

    public enum EngineMode
    {
        Off,
        Starting,
        Idle,
        Military,
        Afterburner,
        Failed
    }

    public enum FuelTransferMode
    {
        Off,
        Automatic,
        Forward,
        Aft,
        ExternalToInternal
    }

    public enum InertialNavigationState
    {
        Off,
        Aligning,
        Navigation,
        Degraded,
        Failed
    }

    public enum HudMode
    {
        Off,
        Nav,
        Landing,
        AirToAir
    }

    public enum HudScaleMode
    {
        VvVah,
        Vah,
        Off
    }

    public enum HudVelocitySource
    {
        Calibrated,
        True,
        Ground
    }

    public enum HudAltitudeSource
    {
        Barometric,
        Radar,
        Automatic
    }

    public enum MasterArmState
    {
        Safe,
        Simulate,
        Arm
    }

    public enum AircraftStartupState
    {
        Off,
        PowerApplied,
        EngineStarting,
        SystemsInitializing,
        Ready
    }

    public enum TacticalTrackClassification
    {
        Unknown,
        Air,
        Surface,
        Ground
    }

    public enum TacticalTrackAffiliation
    {
        Unknown,
        Friendly,
        Neutral,
        Hostile
    }

    public enum AircraftSide
    {
        Neutral,
        Friendly,
        Hostile
    }

    public enum AircraftRole
    {
        Player,
        Wingman,
        Escort,
        Interceptor,
        Patrol,
        Threat
    }

    public enum FormationMode
    {
        None,
        Rejoin,
        Maintain,
        BreakAway,
        ReturnToBase
    }

    public enum SimulationEventType
    {
        Information,
        Warning,
        Failure,
        EngineStarted,
        EngineStopped,
        StoreReleased,
        Touchdown,
        Takeoff,
        Crash,
        FormationModeChanged,
        TerrainDataTimeout,
        TelemetryTimeout,
        ScenarioReset
    }
}
