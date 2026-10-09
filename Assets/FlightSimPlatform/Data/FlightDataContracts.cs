using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Data
{
    public enum FlightDataDomain
    {
        None,
        Simulation,
        Mission,
        Engagement
    }

    [Serializable]
    public struct FlightDataHealth
    {
        public int AircraftCapacity;
        public int RejectedAircraftCount;
        public int EventCapacity;
        public int EventCount;
        public ulong OldestEventSequence;
        public ulong NewestEventSequence;
        public bool HasMission;
    }

    [Serializable]
    public struct AircraftDataSnapshot
    {
        public AircraftIdentityState Identity;
        public AircraftFastState Fast;
        public AircraftSystemsState Systems;
        public TacticalPictureState Tactical;
        public AircraftCombatState Combat;
        public bool HasFast;
        public bool HasSystems;
        public bool HasTactical;
        public bool HasCombat;
        public bool IsValid;
    }

    [Serializable]
    public struct PlatformDataEvent
    {
        public ulong Sequence;
        public FlightDataDomain Domain;
        public SimulationEvent Simulation;
        public MissionEvent Mission;
        public WeaponEngagementEvent Engagement;
    }

    public interface IFlightDataHub
    {
        ushort ContractVersion { get; }
        ushort MissionSchemaVersion { get; }
        int AircraftCount { get; }

        bool TryGetAircraftId(int index, out AircraftId id);
        bool TryGetAircraft(AircraftId id, out AircraftDataSnapshot snapshot);
        bool TryGetMission(out MissionSnapshot snapshot);
        bool TryGetCombat(AircraftId id, out AircraftCombatState state);
        FlightDataHealth GetHealth();
        bool TryReadEvent(ulong afterSequence, out PlatformDataEvent dataEvent);
    }
}
