using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Data
{
    public sealed class FlightDataHub : IFlightDataHub, IFlightTelemetrySink, IMissionTelemetrySink
    {
        public const int MaximumAircraft = 8;
        public const int DefaultEventCapacity = 128;
        private const int MaximumObjectives = 64;

        private readonly object sync = new object();
        private readonly AircraftSlot[] aircraft = new AircraftSlot[MaximumAircraft];
        private readonly MissionObjectiveState[] objectives = new MissionObjectiveState[MaximumObjectives];
        private readonly BoundedEventBuffer events;

        private MissionState mission;
        private AutomationRunState automation;
        private int aircraftCount;
        private int objectiveCount;
        private int rejectedAircraftCount;
        private ulong nextEventSequence = 1UL;
        private bool hasMission;
        private bool hasAutomation;

        public FlightDataHub()
            : this(DefaultEventCapacity)
        {
        }

        public FlightDataHub(int eventCapacity)
        {
            events = new BoundedEventBuffer(eventCapacity);
        }

        public ushort ContractVersion => FlightSimulationContract.ContractVersion;
        public ushort MissionSchemaVersion => MissionDirectorSchemaVersion;
        public int AircraftCount
        {
            get
            {
                lock (sync) return aircraftCount;
            }
        }

        private const ushort MissionDirectorSchemaVersion = 1;

        public bool TryGetAircraftId(int index, out AircraftId id)
        {
            lock (sync)
            {
                if (index >= 0 && index < aircraftCount)
                {
                    id = aircraft[index].Id;
                    return true;
                }

                id = default(AircraftId);
                return false;
            }
        }

        public bool TryGetAircraft(AircraftId id, out AircraftDataSnapshot snapshot)
        {
            lock (sync)
            {
                int index = FindAircraft(id);
                if (index >= 0)
                {
                    snapshot = aircraft[index].Snapshot;
                    return true;
                }

                snapshot = default(AircraftDataSnapshot);
                return false;
            }
        }

        public bool TryGetMission(out MissionSnapshot snapshot)
        {
            var actorBuffer = new MissionActorState[MaximumAircraft];
            var objectiveBuffer = new MissionObjectiveState[MaximumObjectives];
            MissionState missionCopy;
            AutomationRunState automationCopy;
            int actorCount;
            int copiedObjectiveCount;
            lock (sync)
            {
                if (!hasMission)
                {
                    snapshot = default(MissionSnapshot);
                    return false;
                }
                actorCount = 0;
                for (int index = 0; index < aircraftCount; index++)
                {
                    if (!aircraft[index].HasActor) continue;
                    actorBuffer[actorCount++] = aircraft[index].Actor;
                }
                copiedObjectiveCount = objectiveCount;
                Array.Copy(objectives, objectiveBuffer, copiedObjectiveCount);
                missionCopy = mission;
                automationCopy = hasAutomation ? automation : default(AutomationRunState);
            }

            var actors = new MissionActorState[actorCount];
            var missionObjectives = new MissionObjectiveState[copiedObjectiveCount];
            Array.Copy(actorBuffer, actors, actorCount);
            Array.Copy(objectiveBuffer, missionObjectives, copiedObjectiveCount);
            snapshot = new MissionSnapshot
            {
                Mission = missionCopy,
                Actors = actors,
                Objectives = missionObjectives,
                Automation = automationCopy
            };
            return true;
        }

        internal void Reset()
        {
            lock (sync)
            {
                Array.Clear(aircraft, 0, aircraft.Length);
                Array.Clear(objectives, 0, objectives.Length);
                aircraftCount = 0;
                objectiveCount = 0;
                rejectedAircraftCount = 0;
                mission = default(MissionState);
                automation = default(AutomationRunState);
                hasMission = false;
                hasAutomation = false;
                events.Clear();
            }
        }

        public bool TryGetCombat(AircraftId id, out AircraftCombatState state)
        {
            lock (sync)
            {
                int index = FindAircraft(id);
                if (index >= 0 && aircraft[index].Snapshot.HasCombat)
                {
                    state = aircraft[index].Snapshot.Combat;
                    return true;
                }

                state = default(AircraftCombatState);
                return false;
            }
        }

        public FlightDataHealth GetHealth()
        {
            lock (sync)
            {
                return new FlightDataHealth
                {
                    AircraftCapacity = MaximumAircraft,
                    RejectedAircraftCount = rejectedAircraftCount,
                    EventCapacity = events.Capacity,
                    EventCount = events.Count,
                    OldestEventSequence = events.OldestSequence,
                    NewestEventSequence = events.NewestSequence,
                    HasMission = hasMission
                };
            }
        }

        public bool TryReadEvent(ulong afterSequence, out PlatformDataEvent dataEvent)
        {
            lock (sync) return events.TryRead(afterSequence, out dataEvent);
        }

        public void OnFastState(in AircraftFastState state)
        {
            lock (sync)
            {
                if (!TryGetOrCreateAircraft(state.Aircraft, out int index)) return;
                AircraftDataSnapshot snapshot = aircraft[index].Snapshot;
                snapshot.Fast = state;
                snapshot.HasFast = true;
                snapshot.IsValid = snapshot.HasFast && snapshot.HasSystems && snapshot.HasTactical;
                aircraft[index].Snapshot = snapshot;
            }
        }

        public void OnSystemsState(in AircraftSystemsState state)
        {
            lock (sync)
            {
                if (!TryGetOrCreateAircraft(state.Aircraft, out int index)) return;
                AircraftDataSnapshot snapshot = aircraft[index].Snapshot;
                snapshot.Systems = state;
                snapshot.HasSystems = true;
                snapshot.IsValid = snapshot.HasFast && snapshot.HasSystems && snapshot.HasTactical;
                aircraft[index].Snapshot = snapshot;
            }
        }

        public void OnTacticalPictureState(in TacticalPictureState state)
        {
            lock (sync)
            {
                if (!TryGetOrCreateAircraft(state.Aircraft, out int index)) return;
                AircraftDataSnapshot snapshot = aircraft[index].Snapshot;
                snapshot.Tactical = state;
                snapshot.HasTactical = true;
                snapshot.IsValid = snapshot.HasFast && snapshot.HasSystems && snapshot.HasTactical;
                aircraft[index].Snapshot = snapshot;
            }
        }

        public void OnSimulationEvent(in SimulationEvent simulationEvent)
        {
            lock (sync)
            {
                PlatformDataEvent dataEvent = new PlatformDataEvent
                {
                    Sequence = TakeNextEventSequence(),
                    Domain = FlightDataDomain.Simulation,
                    Simulation = simulationEvent
                };
                events.Append(in dataEvent);
            }
        }

        public void OnMissionState(in MissionState state)
        {
            lock (sync)
            {
                mission = state;
                hasMission = true;
            }
        }

        public void OnMissionActorState(in MissionActorState state)
        {
            lock (sync)
            {
                if (!TryGetOrCreateAircraft(state.Aircraft, out int index)) return;
                aircraft[index].Actor = state;
                aircraft[index].HasActor = true;
                AircraftDataSnapshot snapshot = aircraft[index].Snapshot;
                AircraftIdentityState identity = snapshot.Identity;
                identity.Aircraft = state.Aircraft;
                identity.Callsign = state.Callsign ?? string.Empty;
                identity.Side = state.Side;
                identity.Role = state.Role;
                snapshot.Identity = identity;
                aircraft[index].Snapshot = snapshot;
            }
        }

        public void OnMissionObjectiveState(in MissionObjectiveState state)
        {
            lock (sync)
            {
                int index = FindObjective(state.ObjectiveId);
                if (index < 0)
                {
                    if (objectiveCount >= objectives.Length) return;
                    index = objectiveCount++;
                }
                objectives[index] = state;
            }
        }

        public void OnAircraftCombatState(in AircraftCombatState state)
        {
            lock (sync)
            {
                if (!TryGetOrCreateAircraft(state.Aircraft, out int index)) return;
                AircraftDataSnapshot snapshot = aircraft[index].Snapshot;
                snapshot.Combat = state;
                snapshot.HasCombat = true;
                aircraft[index].Snapshot = snapshot;
            }
        }

        public void OnAutomationRunState(in AutomationRunState state)
        {
            lock (sync)
            {
                automation = state;
                hasAutomation = true;
            }
        }

        public void OnMissionEvent(in MissionEvent missionEvent)
        {
            lock (sync)
            {
                PlatformDataEvent dataEvent = new PlatformDataEvent
                {
                    Sequence = TakeNextEventSequence(),
                    Domain = FlightDataDomain.Mission,
                    Mission = missionEvent
                };
                events.Append(in dataEvent);
            }
        }

        public void OnWeaponEngagementEvent(in WeaponEngagementEvent engagementEvent)
        {
            lock (sync)
            {
                PlatformDataEvent dataEvent = new PlatformDataEvent
                {
                    Sequence = TakeNextEventSequence(),
                    Domain = FlightDataDomain.Engagement,
                    Engagement = engagementEvent
                };
                events.Append(in dataEvent);
            }
        }

        private bool TryGetOrCreateAircraft(AircraftId id, out int index)
        {
            index = FindAircraft(id);
            if (index >= 0) return true;
            if (string.IsNullOrEmpty(id.Value) || aircraftCount >= MaximumAircraft)
            {
                rejectedAircraftCount++;
                return false;
            }

            index = aircraftCount++;
            aircraft[index] = new AircraftSlot
            {
                Id = id,
                Snapshot = new AircraftDataSnapshot
                {
                    Identity = AircraftIdentityState.CreateDefault(id, false)
                }
            };
            return true;
        }

        private int FindAircraft(AircraftId id)
        {
            for (int index = 0; index < aircraftCount; index++)
            {
                if (aircraft[index].Id == id) return index;
            }
            return -1;
        }

        private int FindObjective(string objectiveId)
        {
            for (int index = 0; index < objectiveCount; index++)
            {
                if (string.Equals(objectives[index].ObjectiveId, objectiveId, StringComparison.Ordinal)) return index;
            }
            return -1;
        }

        private int CountMissionActors()
        {
            int count = 0;
            for (int index = 0; index < aircraftCount; index++) if (aircraft[index].HasActor) count++;
            return count;
        }

        private ulong TakeNextEventSequence()
        {
            if (nextEventSequence == ulong.MaxValue)
                throw new InvalidOperationException("Flight data event sequence is exhausted.");
            return nextEventSequence++;
        }

        private struct AircraftSlot
        {
            public AircraftId Id;
            public AircraftDataSnapshot Snapshot;
            public MissionActorState Actor;
            public bool HasActor;
        }
    }
}
