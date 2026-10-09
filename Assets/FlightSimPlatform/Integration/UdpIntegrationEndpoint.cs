using System;
using System.Net;
using System.Net.Sockets;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Integration
{
    public sealed class UdpIntegrationEndpoint : IDisposable
    {
        public const int DefaultBindPort = 49000;
        public const int DefaultTargetPort = 49001;

        private readonly IPEndPoint configuredBindEndPoint;
        private readonly IPEndPoint targetEndPoint;
        private readonly byte[] sendBuffer = new byte[UdpPacketCodec.MaximumPacketBytes];
        private Socket socket;
        private uint outgoingSequence;
        private bool disposed;

        public UdpIntegrationEndpoint(int bindPort = DefaultBindPort, int targetPort = DefaultTargetPort)
            : this(
                new IPEndPoint(IPAddress.Loopback, bindPort),
                new IPEndPoint(IPAddress.Loopback, targetPort))
        {
        }

        public UdpIntegrationEndpoint(IPEndPoint bindEndPoint, IPEndPoint targetEndPoint)
        {
            configuredBindEndPoint = RequireLoopback(bindEndPoint, nameof(bindEndPoint));
            this.targetEndPoint = RequireLoopback(targetEndPoint, nameof(targetEndPoint));
            TryOpenSocket();
        }

        public IPEndPoint BindEndPoint => configuredBindEndPoint;
        public IPEndPoint LocalEndPoint => socket?.LocalEndPoint as IPEndPoint;
        public IPEndPoint TargetEndPoint => targetEndPoint;
        public bool IsAvailable => !disposed && socket != null;

        public void PublishFastState(in AircraftFastState state)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeFastState(in state, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishSystemsState(in AircraftSystemsState state, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeSystemsState(in state, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishTacticalPictureState(in TacticalPictureState state, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeTacticalPictureState(in state, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishSimulationEvent(in SimulationEvent simulationEvent, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeSimulationEvent(in simulationEvent, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishMissionState(in MissionState state)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeMissionState(in state, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishMissionActorState(in MissionActorState state, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeMissionActorState(in state, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishMissionObjectiveState(in MissionObjectiveState state, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeMissionObjectiveState(in state, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishAircraftCombatState(in AircraftCombatState state, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeAircraftCombatState(in state, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishAutomationRunState(in AutomationRunState state, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeAutomationRunState(in state, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishMissionEvent(in MissionEvent missionEvent, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeMissionEvent(in missionEvent, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void PublishWeaponEngagementEvent(in WeaponEngagementEvent engagementEvent, ulong tick)
        {
            uint sequence = NextSequence();
            if (UdpPacketCodec.TryEncodeWeaponEngagementEvent(in engagementEvent, tick, sequence, sendBuffer, out int bytes)) TrySend(bytes);
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            Socket active = socket;
            socket = null;
            try { active?.Dispose(); }
            catch (SocketException) { }
        }

        private void TryOpenSocket()
        {
            Socket candidate = null;
            try
            {
                candidate = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                candidate.Blocking = false;
                candidate.Bind(configuredBindEndPoint);
                socket = candidate;
            }
            catch (SocketException)
            {
                candidate?.Dispose();
                socket = null;
            }
            catch (NotSupportedException)
            {
                candidate?.Dispose();
                socket = null;
            }
        }

        private uint NextSequence()
        {
            outgoingSequence = unchecked(outgoingSequence + 1u);
            return outgoingSequence;
        }

        private void TrySend(int bytesWritten)
        {
            Socket active = socket;
            if (disposed || active == null || bytesWritten <= 0) return;
            try { active.SendTo(sendBuffer, 0, bytesWritten, SocketFlags.None, targetEndPoint); }
            catch (SocketException) { }
            catch (ObjectDisposedException) { }
        }

        private static IPEndPoint RequireLoopback(IPEndPoint endPoint, string parameterName)
        {
            if (endPoint == null) throw new ArgumentNullException(parameterName);
            if (!IPAddress.IsLoopback(endPoint.Address))
                throw new ArgumentException("Read-only telemetry binds to loopback by default.", parameterName);
            return endPoint;
        }
    }
}
