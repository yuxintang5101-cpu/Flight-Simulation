using System;
using System.Net;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Data;

namespace FlightSim.Platform.Integration
{
    public struct TelemetryPublisherHealth
    {
        public bool IsRunning;
        public bool UdpAvailable;
        public bool TcpAvailable;
        public int TcpClientCount;
        public long DroppedContinuousFrames;
        public long EventOverflowDisconnects;
        public double LastSimulationTimeS;
    }

    public sealed class ReadOnlyTelemetryPublisher : IDisposable
    {
        private readonly IFlightDataHub hub;
        private readonly int udpBindPort;
        private readonly int udpTargetPort;
        private readonly int tcpPort;
        private UdpIntegrationEndpoint udp;
        private NdjsonTelemetryServer tcp;
        private double lastFastS = double.NegativeInfinity;
        private double lastSystemsS = double.NegativeInfinity;
        private double lastTacticalS = double.NegativeInfinity;
        private double lastMissionS = double.NegativeInfinity;
        private double lastHealthS = double.NegativeInfinity;
        private double lastSimulationTimeS;
        private ulong lastEventSequence;
        private bool running;

        public ReadOnlyTelemetryPublisher(
            IFlightDataHub hub,
            int udpBindPort = UdpIntegrationEndpoint.DefaultBindPort,
            int udpTargetPort = UdpIntegrationEndpoint.DefaultTargetPort,
            int tcpPort = NdjsonTelemetryServer.DefaultPort)
        {
            this.hub = hub ?? throw new ArgumentNullException(nameof(hub));
            this.udpBindPort = udpBindPort;
            this.udpTargetPort = udpTargetPort;
            this.tcpPort = tcpPort;
        }

        public UdpIntegrationEndpoint UdpEndpoint => udp;
        public NdjsonTelemetryServer TcpServer => tcp;

        public void Start()
        {
            if (running) return;
            if (udpTargetPort >= 0)
                udp = new UdpIntegrationEndpoint(udpBindPort, udpTargetPort);
            if (tcpPort >= 0)
            {
                tcp = new NdjsonTelemetryServer(IPAddress.Loopback, tcpPort);
                tcp.Start();
            }
            running = true;
        }

        public void Pump(double simulationTimeS)
        {
            if (!running) return;
            lastSimulationTimeS = simulationTimeS;
            bool fastDue = IsDue(simulationTimeS, ref lastFastS, 0.02);
            bool systemsDue = IsDue(simulationTimeS, ref lastSystemsS, 0.1);
            bool tacticalDue = IsDue(simulationTimeS, ref lastTacticalS, 0.2);
            for (int i = 0; i < hub.AircraftCount; i++)
            {
                if (!hub.TryGetAircraftId(i, out AircraftId id) || !hub.TryGetAircraft(id, out AircraftDataSnapshot snapshot)) continue;
                ulong tick = snapshot.Fast.Tick;
                if (fastDue && snapshot.HasFast)
                {
                    if (udp != null) udp.PublishFastState(in snapshot.Fast);
                    Publish("fastState", tick, snapshot.Fast.SimulationTimeS, id, snapshot.IsValid, snapshot.Fast, false, "fast:" + id.Value);
                }
                if (systemsDue && snapshot.HasSystems)
                {
                    if (udp != null) udp.PublishSystemsState(in snapshot.Systems, tick);
                    Publish("systemsState", tick, snapshot.Systems.SimulationTimeS, id, snapshot.IsValid, snapshot.Systems, false, "systems:" + id.Value);
                }
                if (systemsDue && snapshot.HasCombat)
                {
                    if (udp != null) udp.PublishAircraftCombatState(in snapshot.Combat, tick);
                    Publish("combatState", tick, snapshot.Combat.SimulationTimeS, id, snapshot.Combat.IsValid, snapshot.Combat, false, "combat:" + id.Value);
                }
                if (tacticalDue && snapshot.HasTactical)
                {
                    if (udp != null) udp.PublishTacticalPictureState(in snapshot.Tactical, tick);
                    Publish("tacticalPicture", tick, snapshot.Fast.SimulationTimeS, id, snapshot.IsValid, snapshot.Tactical, false, "tactical:" + id.Value);
                }
                if (tacticalDue)
                    Publish("identity", tick, snapshot.Fast.SimulationTimeS, id, true, snapshot.Identity, false, "identity:" + id.Value);
            }

            if (IsDue(simulationTimeS, ref lastMissionS, 0.2) && hub.TryGetMission(out MissionSnapshot mission))
            {
                ulong tick = mission.Mission.Tick;
                if (udp != null) udp.PublishMissionState(in mission.Mission);
                Publish("missionState", tick, mission.Mission.SimulationTimeS, default(AircraftId), mission.Mission.IsValid, mission.Mission, false, "mission");
                MissionActorState[] actors = mission.Actors ?? Array.Empty<MissionActorState>();
                for (int i = 0; i < actors.Length; i++)
                {
                    if (udp != null) udp.PublishMissionActorState(in actors[i], tick);
                    Publish("missionActor", tick, mission.Mission.SimulationTimeS, actors[i].Aircraft, actors[i].IsValid, actors[i], false, "actor:" + actors[i].Aircraft.Value);
                }
                MissionObjectiveState[] objectives = mission.Objectives ?? Array.Empty<MissionObjectiveState>();
                for (int i = 0; i < objectives.Length; i++)
                {
                    if (udp != null) udp.PublishMissionObjectiveState(in objectives[i], tick);
                    Publish("missionObjective", tick, mission.Mission.SimulationTimeS, default(AircraftId), true, objectives[i], false, "objective:" + objectives[i].ObjectiveId);
                }
                if (udp != null) udp.PublishAutomationRunState(in mission.Automation, tick);
                Publish("automationState", tick, mission.Automation.SimulationTimeS, default(AircraftId), true, mission.Automation, false, "automation");
            }

            PublishEvents();
            if (IsDue(simulationTimeS, ref lastHealthS, 0.2))
                Publish("health", 0, simulationTimeS, default(AircraftId), true, GetHealth(), false, "health");
        }

        public TelemetryPublisherHealth GetHealth()
        {
            return new TelemetryPublisherHealth
            {
                IsRunning = running,
                UdpAvailable = udp != null && udp.IsAvailable,
                TcpAvailable = tcp != null && tcp.IsRunning,
                TcpClientCount = tcp?.ClientCount ?? 0,
                DroppedContinuousFrames = tcp?.DroppedContinuousFrames ?? 0,
                EventOverflowDisconnects = tcp?.EventOverflowDisconnects ?? 0,
                LastSimulationTimeS = lastSimulationTimeS
            };
        }

        public void Stop()
        {
            if (!running) return;
            running = false;
            tcp?.Dispose();
            tcp = null;
            udp?.Dispose();
            udp = null;
        }

        public void Dispose() => Stop();

        private void PublishEvents()
        {
            while (hub.TryReadEvent(lastEventSequence, out PlatformDataEvent dataEvent))
            {
                lastEventSequence = dataEvent.Sequence;
                switch (dataEvent.Domain)
                {
                    case FlightDataDomain.Simulation:
                        if (udp != null) udp.PublishSimulationEvent(in dataEvent.Simulation, 0);
                        Publish("simulationEvent", 0, dataEvent.Simulation.SimulationTimeS, dataEvent.Simulation.Aircraft, true, dataEvent.Simulation, true, "event");
                        break;
                    case FlightDataDomain.Mission:
                        if (udp != null) udp.PublishMissionEvent(in dataEvent.Mission, 0);
                        Publish("missionEvent", 0, dataEvent.Mission.SimulationTimeS, dataEvent.Mission.Source, true, dataEvent.Mission, true, "event");
                        break;
                    case FlightDataDomain.Engagement:
                        if (udp != null) udp.PublishWeaponEngagementEvent(in dataEvent.Engagement, 0);
                        Publish("weaponEngagementEvent", 0, dataEvent.Engagement.SimulationTimeS, dataEvent.Engagement.Shooter, true, dataEvent.Engagement, true, "event");
                        break;
                }
            }
        }

        private void Publish<T>(string type, ulong tick, double time, AircraftId id, bool valid, T payload, bool isEvent, string key)
        {
            string line = NdjsonTelemetrySerializer.SerializeEnvelope(
                isEvent ? "event" : "data",
                type,
                hub.ContractVersion,
                hub.MissionSchemaVersion,
                tick,
                time,
                id,
                valid,
                payload);
            tcp?.Publish(line, isEvent, key);
        }

        private static bool IsDue(double now, ref double last, double interval)
        {
            if (now + 1e-9 < last) last = double.NegativeInfinity;
            if (now - last + 1e-9 < interval) return false;
            last = now;
            return true;
        }
    }
}
