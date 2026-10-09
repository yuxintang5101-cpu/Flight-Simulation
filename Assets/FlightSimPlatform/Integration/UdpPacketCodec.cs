using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Integration
{
    public enum UdpMessageType : ushort
    {
        Unspecified = 0,
        FastState = 1,
        SystemsState = 2,
        TacticalPictureState = 3,
        SimulationEvent = 4,
        Reserved5 = 5,
        Reserved6 = 6,
        MissionState = 10,
        MissionActorState = 11,
        MissionObjectiveState = 12,
        AircraftCombatState = 13,
        AutomationRunState = 14,
        MissionEvent = 15,
        WeaponEngagementEvent = 16
    }

    public readonly struct UdpPacketHeader
    {
        // Written little-endian, this value appears on the wire as the ASCII bytes "FSIM".
        public const uint FsimMagic = 0x4D495346u;

        public readonly uint Magic;
        public readonly ushort ContractVersion;
        public readonly UdpMessageType MessageType;
        public readonly uint Sequence;
        public readonly ulong Tick;
        public readonly AircraftId Aircraft;
        public readonly ushort PayloadLength;
        public readonly uint Checksum;

        internal UdpPacketHeader(
            uint magic,
            ushort contractVersion,
            UdpMessageType messageType,
            uint sequence,
            ulong tick,
            AircraftId aircraft,
            ushort payloadLength,
            uint checksum)
        {
            Magic = magic;
            ContractVersion = contractVersion;
            MessageType = messageType;
            Sequence = sequence;
            Tick = tick;
            Aircraft = aircraft;
            PayloadLength = payloadLength;
            Checksum = checksum;
        }
    }

    public static class UdpPacketCodec
    {
        public const int MaximumPacketBytes = 1200;
        public const int AircraftIdFieldBytes = 32;
        public const int HeaderBytes = 58;

        private const int AircraftIdDataBytes = AircraftIdFieldBytes - 1;
        private const int ChecksumOffset = 54;
        private const int FastStatePayloadBytes = (34 * sizeof(double)) + sizeof(byte);
        private const ushort ContractVersion = FlightSimulationContract.ContractVersion;

        public static bool TryEncodeFastState(
            in AircraftFastState state,
            uint sequence,
            Span<byte> destination,
            out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer))
            {
                return false;
            }

            if (!TryWriteFastStatePayload(ref writer, in state) || writer.Position != FastStatePayloadBytes)
            {
                return false;
            }

            return TryFinalizePacket(
                state.Aircraft,
                UdpMessageType.FastState,
                sequence,
                state.Tick,
                writer.Position,
                destination,
                out bytesWritten);
        }

        public static bool TryDecodeFastState(
            ReadOnlySpan<byte> packet,
            out UdpPacketHeader header,
            out AircraftFastState state)
        {
            state = default;
            if (!TryDecodeHeader(packet, out header) ||
                header.MessageType != UdpMessageType.FastState ||
                header.PayloadLength != FastStatePayloadBytes)
            {
                return false;
            }

            PacketReader reader = new PacketReader(packet.Slice(HeaderBytes, header.PayloadLength));
            state.Aircraft = header.Aircraft;
            state.Tick = header.Tick;

            return reader.TryReadDouble(out state.SimulationTimeS) &&
                   reader.TryReadDouble(out state.EcefPositionXM) &&
                   reader.TryReadDouble(out state.EcefPositionYM) &&
                   reader.TryReadDouble(out state.EcefPositionZM) &&
                   reader.TryReadDouble(out state.EcefVelocityXMps) &&
                   reader.TryReadDouble(out state.EcefVelocityYMps) &&
                   reader.TryReadDouble(out state.EcefVelocityZMps) &&
                   reader.TryReadDouble(out state.BodyVelocityXMps) &&
                   reader.TryReadDouble(out state.BodyVelocityYMps) &&
                   reader.TryReadDouble(out state.BodyVelocityZMps) &&
                   reader.TryReadDouble(out state.BodyAngularVelocityXRadps) &&
                   reader.TryReadDouble(out state.BodyAngularVelocityYRadps) &&
                   reader.TryReadDouble(out state.BodyAngularVelocityZRadps) &&
                   reader.TryReadDouble(out state.BodyToEcefQuaternionX) &&
                   reader.TryReadDouble(out state.BodyToEcefQuaternionY) &&
                   reader.TryReadDouble(out state.BodyToEcefQuaternionZ) &&
                   reader.TryReadDouble(out state.BodyToEcefQuaternionW) &&
                   reader.TryReadDouble(out state.LongitudeRad) &&
                   reader.TryReadDouble(out state.LatitudeRad) &&
                   reader.TryReadDouble(out state.EllipsoidHeightM) &&
                   reader.TryReadDouble(out state.HeadingRad) &&
                   reader.TryReadDouble(out state.PitchRad) &&
                   reader.TryReadDouble(out state.RollRad) &&
                   reader.TryReadDouble(out state.TrueAirspeedMps) &&
                   reader.TryReadDouble(out state.CalibratedAirspeedMps) &&
                   reader.TryReadDouble(out state.GroundSpeedMps) &&
                   reader.TryReadDouble(out state.Mach) &&
                   reader.TryReadDouble(out state.AngleOfAttackRad) &&
                   reader.TryReadDouble(out state.SideslipRad) &&
                   reader.TryReadDouble(out state.NormalLoadFactorG) &&
                   reader.TryReadDouble(out state.MeanSeaLevelAltitudeM) &&
                   reader.TryReadDouble(out state.AboveGroundLevelAltitudeM) &&
                   reader.TryReadDouble(out state.ClimbRateMps) &&
                   reader.TryReadBoolean(out state.TerrainSampleValid) &&
                   reader.TryReadDouble(out state.TerrainSampleAgeS) &&
                   reader.Remaining == 0;
        }

        public static bool TryEncodeSystemsState(
            in AircraftSystemsState state,
            ulong tick,
            uint sequence,
            Span<byte> destination,
            out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) ||
                !TryWriteSystemsStatePayload(ref writer, in state))
            {
                return false;
            }

            return TryFinalizePacket(
                state.Aircraft,
                UdpMessageType.SystemsState,
                sequence,
                tick,
                writer.Position,
                destination,
                out bytesWritten);
        }

        public static bool TryEncodeTacticalPictureState(
            in TacticalPictureState state,
            ulong tick,
            uint sequence,
            Span<byte> destination,
            out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) ||
                !TryWriteTacticalPicturePayload(ref writer, in state))
            {
                return false;
            }

            return TryFinalizePacket(
                state.Aircraft,
                UdpMessageType.TacticalPictureState,
                sequence,
                tick,
                writer.Position,
                destination,
                out bytesWritten);
        }

        public static bool TryEncodeSimulationEvent(
            in SimulationEvent simulationEvent,
            ulong tick,
            uint sequence,
            Span<byte> destination,
            out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) ||
                !writer.TryWriteDouble(simulationEvent.SimulationTimeS) ||
                !writer.TryWriteInt32((int)simulationEvent.Type) ||
                !writer.TryWriteInt32(simulationEvent.Code) ||
                !writer.TryWriteString(simulationEvent.Message))
            {
                return false;
            }

            return TryFinalizePacket(
                simulationEvent.Aircraft,
                UdpMessageType.SimulationEvent,
                sequence,
                tick,
                writer.Position,
                destination,
                out bytesWritten);
        }

        public static bool TryEncodeMissionState(
            in MissionState state,
            uint sequence,
            Span<byte> destination,
            out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) ||
                !writer.TryWriteString(state.MissionId) || !writer.TryWriteString(state.RunId) ||
                !writer.TryWriteInt32(state.SchemaVersion) || !writer.TryWriteInt32(state.Seed) ||
                !writer.TryWriteInt32((int)state.Phase) || !writer.TryWriteInt32((int)state.Result) ||
                !writer.TryWriteDouble(state.SimulationTimeS) || !writer.TryWriteDouble(state.ElapsedTimeS) ||
                !writer.TryWriteDouble(state.RemainingTimeS) || !writer.TryWriteInt32(state.ActiveObjectiveCount) ||
                !writer.TryWriteInt32(state.SucceededObjectiveCount) || !writer.TryWriteInt32(state.FailedObjectiveCount) ||
                !writer.TryWriteBoolean(state.IsValid) || !writer.TryWriteString(state.CompletionReason)) return false;
            return TryFinalizePacket(default(AircraftId), UdpMessageType.MissionState, sequence, state.Tick, writer.Position, destination, out bytesWritten);
        }

        public static bool TryDecodeMissionState(ReadOnlySpan<byte> packet, out UdpPacketHeader header, out MissionState state)
        {
            state = default;
            if (!TryDecodeHeader(packet, out header) || header.MessageType != UdpMessageType.MissionState) return false;
            PacketReader reader = new PacketReader(packet.Slice(HeaderBytes, header.PayloadLength));
            state.Tick = header.Tick;
            return reader.TryReadString(out state.MissionId) && reader.TryReadString(out state.RunId) &&
                   reader.TryReadInt32(out state.SchemaVersion) && reader.TryReadInt32(out state.Seed) &&
                   reader.TryReadInt32(out int phase) && Assign(ref state.Phase, phase) &&
                   reader.TryReadInt32(out int result) && Assign(ref state.Result, result) &&
                   reader.TryReadDouble(out state.SimulationTimeS) && reader.TryReadDouble(out state.ElapsedTimeS) &&
                   reader.TryReadDouble(out state.RemainingTimeS) && reader.TryReadInt32(out state.ActiveObjectiveCount) &&
                   reader.TryReadInt32(out state.SucceededObjectiveCount) && reader.TryReadInt32(out state.FailedObjectiveCount) &&
                   reader.TryReadBoolean(out state.IsValid) && reader.TryReadString(out state.CompletionReason) && reader.Remaining == 0;
        }

        public static bool TryEncodeMissionActorState(in MissionActorState state, ulong tick, uint sequence, Span<byte> destination, out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) || !writer.TryWriteString(state.Callsign) ||
                !writer.TryWriteInt32((int)state.Side) || !writer.TryWriteInt32((int)state.Role) ||
                !writer.TryWriteInt32((int)state.Status) || !writer.TryWriteInt32((int)state.AiMode) ||
                !writer.TryWriteInt32((int)state.ControlAuthority) || !writer.TryWriteFixedAircraftId(state.LeaderAircraft) ||
                !writer.TryWriteInt32(state.FormationSlot) || !writer.TryWriteDouble(state.AiSkillNormalized) ||
                !writer.TryWriteInt32(state.CurrentWaypointIndex) ||
                !writer.TryWriteFixedAircraftId(state.SelectedTarget) || !writer.TryWriteBoolean(state.IsDetected) ||
                !writer.TryWriteBoolean(state.IsEngaged) || !writer.TryWriteBoolean(state.IsValid)) return false;
            return TryFinalizePacket(state.Aircraft, UdpMessageType.MissionActorState, sequence, tick, writer.Position, destination, out bytesWritten);
        }

        public static bool TryDecodeMissionActorState(
            ReadOnlySpan<byte> packet,
            out UdpPacketHeader header,
            out MissionActorState state)
        {
            state = default;
            if (!TryDecodeHeader(packet, out header) || header.MessageType != UdpMessageType.MissionActorState) return false;
            PacketReader reader = new PacketReader(packet.Slice(HeaderBytes, header.PayloadLength));
            state.Aircraft = header.Aircraft;
            return reader.TryReadString(out state.Callsign) &&
                   reader.TryReadInt32(out int side) && Assign(ref state.Side, side) &&
                   reader.TryReadInt32(out int role) && Assign(ref state.Role, role) &&
                   reader.TryReadInt32(out int status) && Assign(ref state.Status, status) &&
                   reader.TryReadInt32(out int aiMode) && Assign(ref state.AiMode, aiMode) &&
                   reader.TryReadInt32(out int authority) && Assign(ref state.ControlAuthority, authority) &&
                   reader.TryReadFixedAircraftId(out state.LeaderAircraft) &&
                   reader.TryReadInt32(out state.FormationSlot) && reader.TryReadDouble(out state.AiSkillNormalized) &&
                   reader.TryReadInt32(out state.CurrentWaypointIndex) && reader.TryReadFixedAircraftId(out state.SelectedTarget) &&
                   reader.TryReadBoolean(out state.IsDetected) && reader.TryReadBoolean(out state.IsEngaged) &&
                   reader.TryReadBoolean(out state.IsValid) && reader.Remaining == 0;
        }

        public static bool TryEncodeMissionObjectiveState(in MissionObjectiveState state, ulong tick, uint sequence, Span<byte> destination, out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) || !writer.TryWriteString(state.ObjectiveId) ||
                !writer.TryWriteString(state.DisplayName) || !writer.TryWriteInt32((int)state.Type) ||
                !writer.TryWriteInt32((int)state.Status) || !writer.TryWriteDouble(state.ProgressNormalized) ||
                !writer.TryWriteInt32(state.CurrentCount) || !writer.TryWriteInt32(state.RequiredCount) ||
                !writer.TryWriteDouble(state.DeadlineS) || !writer.TryWriteString(state.FailureReason)) return false;
            return TryFinalizePacket(default(AircraftId), UdpMessageType.MissionObjectiveState, sequence, tick, writer.Position, destination, out bytesWritten);
        }

        public static bool TryDecodeMissionObjectiveState(
            ReadOnlySpan<byte> packet,
            out UdpPacketHeader header,
            out MissionObjectiveState state)
        {
            state = default;
            if (!TryDecodeHeader(packet, out header) || header.MessageType != UdpMessageType.MissionObjectiveState) return false;
            PacketReader reader = new PacketReader(packet.Slice(HeaderBytes, header.PayloadLength));
            return reader.TryReadString(out state.ObjectiveId) && reader.TryReadString(out state.DisplayName) &&
                   reader.TryReadInt32(out int type) && Assign(ref state.Type, type) &&
                   reader.TryReadInt32(out int status) && Assign(ref state.Status, status) &&
                   reader.TryReadDouble(out state.ProgressNormalized) && reader.TryReadInt32(out state.CurrentCount) &&
                   reader.TryReadInt32(out state.RequiredCount) && reader.TryReadDouble(out state.DeadlineS) &&
                   reader.TryReadString(out state.FailureReason) && reader.Remaining == 0;
        }

        public static bool TryEncodeAircraftCombatState(in AircraftCombatState state, ulong tick, uint sequence, Span<byte> destination, out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) ||
                !writer.TryWriteDouble(state.SimulationTimeS) || !writer.TryWriteInt32((int)state.MasterArm) ||
                !writer.TryWriteInt32(state.SelectedStationIndex) || !writer.TryWriteString(state.SelectedStoreType) ||
                !writer.TryWriteInt32(state.SelectedStoreQuantity) || !writer.TryWriteInt32(state.SelectedTrackId) ||
                !writer.TryWriteFixedAircraftId(state.SelectedTarget) || !writer.TryWriteInt32((int)state.RadarTrackState) ||
                !writer.TryWriteDouble(state.TargetRangeM) || !writer.TryWriteDouble(state.ClosureRateMps) ||
                !writer.TryWriteDouble(state.TargetBearingRad) || !writer.TryWriteDouble(state.TargetElevationRad) ||
                !writer.TryWriteDouble(state.TargetAspectAngleRad) || !writer.TryWriteDouble(state.LockQualityNormalized) ||
                !writer.TryWriteBoolean(state.InLaunchZone) || !writer.TryWriteBoolean(state.ShootCue) ||
                !writer.TryWriteBoolean(state.MissileLaunchWarning) || !writer.TryWriteBoolean(state.IsValid)) return false;
            return TryFinalizePacket(state.Aircraft, UdpMessageType.AircraftCombatState, sequence, tick, writer.Position, destination, out bytesWritten);
        }

        public static bool TryDecodeAircraftCombatState(ReadOnlySpan<byte> packet, out UdpPacketHeader header, out AircraftCombatState state)
        {
            state = default;
            if (!TryDecodeHeader(packet, out header) || header.MessageType != UdpMessageType.AircraftCombatState) return false;
            PacketReader reader = new PacketReader(packet.Slice(HeaderBytes, header.PayloadLength));
            state.Aircraft = header.Aircraft;
            return reader.TryReadDouble(out state.SimulationTimeS) && reader.TryReadInt32(out int arm) && Assign(ref state.MasterArm, arm) &&
                   reader.TryReadInt32(out state.SelectedStationIndex) && reader.TryReadString(out state.SelectedStoreType) &&
                   reader.TryReadInt32(out state.SelectedStoreQuantity) && reader.TryReadInt32(out state.SelectedTrackId) &&
                   reader.TryReadFixedAircraftId(out state.SelectedTarget) && reader.TryReadInt32(out int track) && Assign(ref state.RadarTrackState, track) &&
                   reader.TryReadDouble(out state.TargetRangeM) && reader.TryReadDouble(out state.ClosureRateMps) &&
                   reader.TryReadDouble(out state.TargetBearingRad) && reader.TryReadDouble(out state.TargetElevationRad) &&
                   reader.TryReadDouble(out state.TargetAspectAngleRad) && reader.TryReadDouble(out state.LockQualityNormalized) &&
                   reader.TryReadBoolean(out state.InLaunchZone) && reader.TryReadBoolean(out state.ShootCue) &&
                   reader.TryReadBoolean(out state.MissileLaunchWarning) && reader.TryReadBoolean(out state.IsValid) && reader.Remaining == 0;
        }

        public static bool TryEncodeAutomationRunState(in AutomationRunState state, ulong tick, uint sequence, Span<byte> destination, out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) || !writer.TryWriteString(state.BatchId) ||
                !writer.TryWriteString(state.RunId) || !writer.TryWriteString(state.MissionId) || !writer.TryWriteInt32(state.Seed) ||
                !writer.TryWriteInt32((int)state.Mode) || !writer.TryWriteInt32((int)state.Status) ||
                !writer.TryWriteInt32((int)state.PlayerControlAuthority) || !writer.TryWriteDouble(state.SimulationTimeS) ||
                !writer.TryWriteDouble(state.WallClockTimeS) || !writer.TryWriteDouble(state.SimulationRate) ||
                !writer.TryWriteInt32((int)state.TerrainSource) || !writer.TryWriteBoolean(state.TerrainDataValid) ||
                !writer.TryWriteString(state.CompletionReason) || !writer.TryWriteString(state.ReportPath)) return false;
            return TryFinalizePacket(default(AircraftId), UdpMessageType.AutomationRunState, sequence, tick, writer.Position, destination, out bytesWritten);
        }

        public static bool TryDecodeAutomationRunState(
            ReadOnlySpan<byte> packet,
            out UdpPacketHeader header,
            out AutomationRunState state)
        {
            state = default;
            if (!TryDecodeHeader(packet, out header) || header.MessageType != UdpMessageType.AutomationRunState) return false;
            PacketReader reader = new PacketReader(packet.Slice(HeaderBytes, header.PayloadLength));
            return reader.TryReadString(out state.BatchId) && reader.TryReadString(out state.RunId) &&
                   reader.TryReadString(out state.MissionId) && reader.TryReadInt32(out state.Seed) &&
                   reader.TryReadInt32(out int mode) && Assign(ref state.Mode, mode) &&
                   reader.TryReadInt32(out int status) && Assign(ref state.Status, status) &&
                   reader.TryReadInt32(out int authority) && Assign(ref state.PlayerControlAuthority, authority) &&
                   reader.TryReadDouble(out state.SimulationTimeS) && reader.TryReadDouble(out state.WallClockTimeS) &&
                   reader.TryReadDouble(out state.SimulationRate) &&
                   reader.TryReadInt32(out int terrain) && Assign(ref state.TerrainSource, terrain) &&
                   reader.TryReadBoolean(out state.TerrainDataValid) && reader.TryReadString(out state.CompletionReason) &&
                   reader.TryReadString(out state.ReportPath) && reader.Remaining == 0;
        }

        public static bool TryEncodeMissionEvent(in MissionEvent missionEvent, ulong tick, uint sequence, Span<byte> destination, out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) || !writer.TryWriteString(missionEvent.RunId) ||
                !writer.TryWriteString(missionEvent.MissionId) || !writer.TryWriteDouble(missionEvent.SimulationTimeS) ||
                !writer.TryWriteInt32((int)missionEvent.Type) || !writer.TryWriteFixedAircraftId(missionEvent.Target) ||
                !writer.TryWriteInt32(missionEvent.Code) || !writer.TryWriteString(missionEvent.Message)) return false;
            return TryFinalizePacket(missionEvent.Source, UdpMessageType.MissionEvent, sequence, tick, writer.Position, destination, out bytesWritten);
        }

        public static bool TryDecodeMissionEvent(
            ReadOnlySpan<byte> packet,
            out UdpPacketHeader header,
            out MissionEvent missionEvent)
        {
            missionEvent = default;
            if (!TryDecodeHeader(packet, out header) || header.MessageType != UdpMessageType.MissionEvent) return false;
            PacketReader reader = new PacketReader(packet.Slice(HeaderBytes, header.PayloadLength));
            missionEvent.Source = header.Aircraft;
            return reader.TryReadString(out missionEvent.RunId) && reader.TryReadString(out missionEvent.MissionId) &&
                   reader.TryReadDouble(out missionEvent.SimulationTimeS) &&
                   reader.TryReadInt32(out int type) && Assign(ref missionEvent.Type, type) &&
                   reader.TryReadFixedAircraftId(out missionEvent.Target) && reader.TryReadInt32(out missionEvent.Code) &&
                   reader.TryReadString(out missionEvent.Message) && reader.Remaining == 0;
        }

        public static bool TryEncodeWeaponEngagementEvent(in WeaponEngagementEvent state, ulong tick, uint sequence, Span<byte> destination, out int bytesWritten)
        {
            bytesWritten = 0;
            if (!TryCreatePayloadWriter(destination, out PacketWriter writer) || !writer.TryWriteString(state.RunId) ||
                !writer.TryWriteInt32(state.EngagementSequence) || !writer.TryWriteDouble(state.SimulationTimeS) ||
                !writer.TryWriteFixedAircraftId(state.Target) || !writer.TryWriteString(state.WeaponType) ||
                !writer.TryWriteInt32(state.StationIndex) || !writer.TryWriteDouble(state.LaunchRangeM) ||
                !writer.TryWriteDouble(state.ClosureRateMps) || !writer.TryWriteDouble(state.LockQualityNormalized) ||
                !writer.TryWriteDouble(state.TimeToImpactS) || !writer.TryWriteInt32((int)state.Outcome) ||
                !writer.TryWriteString(state.Reason)) return false;
            return TryFinalizePacket(state.Shooter, UdpMessageType.WeaponEngagementEvent, sequence, tick, writer.Position, destination, out bytesWritten);
        }

        public static bool TryDecodeWeaponEngagementEvent(
            ReadOnlySpan<byte> packet,
            out UdpPacketHeader header,
            out WeaponEngagementEvent state)
        {
            state = default;
            if (!TryDecodeHeader(packet, out header) || header.MessageType != UdpMessageType.WeaponEngagementEvent) return false;
            PacketReader reader = new PacketReader(packet.Slice(HeaderBytes, header.PayloadLength));
            state.Shooter = header.Aircraft;
            return reader.TryReadString(out state.RunId) && reader.TryReadInt32(out state.EngagementSequence) &&
                   reader.TryReadDouble(out state.SimulationTimeS) && reader.TryReadFixedAircraftId(out state.Target) &&
                   reader.TryReadString(out state.WeaponType) && reader.TryReadInt32(out state.StationIndex) &&
                   reader.TryReadDouble(out state.LaunchRangeM) && reader.TryReadDouble(out state.ClosureRateMps) &&
                   reader.TryReadDouble(out state.LockQualityNormalized) && reader.TryReadDouble(out state.TimeToImpactS) &&
                   reader.TryReadInt32(out int outcome) && Assign(ref state.Outcome, outcome) &&
                   reader.TryReadString(out state.Reason) && reader.Remaining == 0;
        }

        public static bool TryDecodeHeader(ReadOnlySpan<byte> packet, out UdpPacketHeader header)
        {
            header = default;
            if (packet.Length < HeaderBytes || packet.Length > MaximumPacketBytes)
            {
                return false;
            }

            PacketReader reader = new PacketReader(packet.Slice(0, HeaderBytes));
            if (!reader.TryReadUInt32(out uint magic) ||
                !reader.TryReadUInt16(out ushort version) ||
                !reader.TryReadUInt16(out ushort messageTypeValue) ||
                !reader.TryReadUInt32(out uint sequence) ||
                !reader.TryReadUInt64(out ulong tick) ||
                !reader.TryReadFixedAircraftId(out AircraftId aircraft) ||
                !reader.TryReadUInt16(out ushort payloadLength) ||
                !reader.TryReadUInt32(out uint checksum) ||
                reader.Remaining != 0)
            {
                return false;
            }

            UdpMessageType messageType = (UdpMessageType)messageTypeValue;
            if (magic != UdpPacketHeader.FsimMagic ||
                version != ContractVersion ||
                !IsKnownMessageType(messageType) ||
                payloadLength != packet.Length - HeaderBytes ||
                checksum != ComputeChecksum(packet))
            {
                return false;
            }

            header = new UdpPacketHeader(
                magic,
                version,
                messageType,
                sequence,
                tick,
                aircraft,
                payloadLength,
                checksum);
            return true;
        }

        private static bool TryCreatePayloadWriter(Span<byte> destination, out PacketWriter writer)
        {
            int packetCapacity = Math.Min(destination.Length, MaximumPacketBytes);
            if (packetCapacity < HeaderBytes)
            {
                writer = default;
                return false;
            }

            writer = new PacketWriter(destination.Slice(HeaderBytes, packetCapacity - HeaderBytes));
            return true;
        }

        private static bool TryFinalizePacket(
            AircraftId aircraft,
            UdpMessageType messageType,
            uint sequence,
            ulong tick,
            int payloadLength,
            Span<byte> destination,
            out int bytesWritten)
        {
            bytesWritten = 0;
            int packetLength = HeaderBytes + payloadLength;
            if (payloadLength < 0 ||
                payloadLength > ushort.MaxValue ||
                packetLength > MaximumPacketBytes ||
                destination.Length < packetLength)
            {
                return false;
            }

            PacketWriter writer = new PacketWriter(destination.Slice(0, HeaderBytes));
            if (!writer.TryWriteUInt32(UdpPacketHeader.FsimMagic) ||
                !writer.TryWriteUInt16(ContractVersion) ||
                !writer.TryWriteUInt16((ushort)messageType) ||
                !writer.TryWriteUInt32(sequence) ||
                !writer.TryWriteUInt64(tick) ||
                !writer.TryWriteFixedAircraftId(aircraft) ||
                !writer.TryWriteUInt16((ushort)payloadLength) ||
                !writer.TryWriteUInt32(0u) ||
                writer.Position != HeaderBytes)
            {
                return false;
            }

            uint checksum = ComputeChecksum(destination.Slice(0, packetLength));
            WriteUInt32(destination, ChecksumOffset, checksum);
            bytesWritten = packetLength;
            return true;
        }

        private static bool TryWriteFastStatePayload(ref PacketWriter writer, in AircraftFastState state)
        {
            return writer.TryWriteDouble(state.SimulationTimeS) &&
                   writer.TryWriteDouble(state.EcefPositionXM) &&
                   writer.TryWriteDouble(state.EcefPositionYM) &&
                   writer.TryWriteDouble(state.EcefPositionZM) &&
                   writer.TryWriteDouble(state.EcefVelocityXMps) &&
                   writer.TryWriteDouble(state.EcefVelocityYMps) &&
                   writer.TryWriteDouble(state.EcefVelocityZMps) &&
                   writer.TryWriteDouble(state.BodyVelocityXMps) &&
                   writer.TryWriteDouble(state.BodyVelocityYMps) &&
                   writer.TryWriteDouble(state.BodyVelocityZMps) &&
                   writer.TryWriteDouble(state.BodyAngularVelocityXRadps) &&
                   writer.TryWriteDouble(state.BodyAngularVelocityYRadps) &&
                   writer.TryWriteDouble(state.BodyAngularVelocityZRadps) &&
                   writer.TryWriteDouble(state.BodyToEcefQuaternionX) &&
                   writer.TryWriteDouble(state.BodyToEcefQuaternionY) &&
                   writer.TryWriteDouble(state.BodyToEcefQuaternionZ) &&
                   writer.TryWriteDouble(state.BodyToEcefQuaternionW) &&
                   writer.TryWriteDouble(state.LongitudeRad) &&
                   writer.TryWriteDouble(state.LatitudeRad) &&
                   writer.TryWriteDouble(state.EllipsoidHeightM) &&
                   writer.TryWriteDouble(state.HeadingRad) &&
                   writer.TryWriteDouble(state.PitchRad) &&
                   writer.TryWriteDouble(state.RollRad) &&
                   writer.TryWriteDouble(state.TrueAirspeedMps) &&
                   writer.TryWriteDouble(state.CalibratedAirspeedMps) &&
                   writer.TryWriteDouble(state.GroundSpeedMps) &&
                   writer.TryWriteDouble(state.Mach) &&
                   writer.TryWriteDouble(state.AngleOfAttackRad) &&
                   writer.TryWriteDouble(state.SideslipRad) &&
                   writer.TryWriteDouble(state.NormalLoadFactorG) &&
                   writer.TryWriteDouble(state.MeanSeaLevelAltitudeM) &&
                   writer.TryWriteDouble(state.AboveGroundLevelAltitudeM) &&
                   writer.TryWriteDouble(state.ClimbRateMps) &&
                   writer.TryWriteBoolean(state.TerrainSampleValid) &&
                   writer.TryWriteDouble(state.TerrainSampleAgeS);
        }

        private static bool TryWriteSystemsStatePayload(ref PacketWriter writer, in AircraftSystemsState state)
        {
            FlightControlState flightControls = state.FlightControls;
            PropulsionState propulsion = state.Propulsion;
            FuelState fuel = state.Fuel;
            ElectricalState electrical = state.Electrical;
            HydraulicState hydraulics = state.Hydraulics;
            LandingGearState landingGear = state.LandingGear;
            AvionicsState avionics = state.Avionics;
            HudState hud = avionics.Hud;
            WarningState warnings = state.Warnings;

            if (!writer.TryWriteDouble(state.SimulationTimeS) ||
                !writer.TryWriteInt32((int)flightControls.ActiveControlAuthority) ||
                !writer.TryWriteDouble(flightControls.PitchCommandNormalized) ||
                !writer.TryWriteDouble(flightControls.RollCommandNormalized) ||
                !writer.TryWriteDouble(flightControls.YawCommandNormalized) ||
                !writer.TryWriteDouble(flightControls.AileronDeflectionRad) ||
                !writer.TryWriteDouble(flightControls.ElevatorDeflectionRad) ||
                !writer.TryWriteDouble(flightControls.RudderDeflectionRad) ||
                !writer.TryWriteDouble(flightControls.LeadingEdgeFlapDeflectionRad) ||
                !writer.TryWriteDouble(flightControls.TrailingEdgeFlapDeflectionRad) ||
                !writer.TryWriteBoolean(flightControls.AngleOfAttackLimiterActive) ||
                !writer.TryWriteBoolean(flightControls.GForceLimiterActive) ||
                !writer.TryWriteBoolean(flightControls.RollRateLimiterActive) ||
                !writer.TryWriteBoolean(flightControls.FlightControlComputerEnabled) ||
                !writer.TryWriteBoolean(flightControls.AutopilotEngaged) ||
                !writer.TryWriteInt32((int)propulsion.Mode) ||
                !writer.TryWriteBoolean(propulsion.EngineRunning) ||
                !writer.TryWriteBoolean(propulsion.AfterburnerActive) ||
                !writer.TryWriteDouble(propulsion.N1Percent) ||
                !writer.TryWriteDouble(propulsion.N2Percent) ||
                !writer.TryWriteDouble(propulsion.ExhaustGasTemperatureC) ||
                !writer.TryWriteDouble(propulsion.FuelFlowKgps) ||
                !writer.TryWriteDouble(propulsion.NozzlePositionNormalized) ||
                !writer.TryWriteDouble(propulsion.ThrustN) ||
                !writer.TryWriteDouble(fuel.InternalFuelKg) ||
                !writer.TryWriteDouble(fuel.ExternalFuelKg) ||
                !writer.TryWriteDouble(fuel.LeftFuelKg) ||
                !writer.TryWriteDouble(fuel.RightFuelKg) ||
                !writer.TryWriteDouble(fuel.TotalFuelKg) ||
                !writer.TryWriteInt32((int)fuel.TransferMode) ||
                !writer.TryWriteDouble(fuel.FuelImbalanceKg) ||
                !writer.TryWriteDouble(fuel.BingoFuelKg) ||
                !writer.TryWriteDouble(fuel.JokerFuelKg) ||
                !writer.TryWriteDouble(fuel.CenterOfGravityPercentMac) ||
                !writer.TryWriteDouble(fuel.FuelFlowKgps) ||
                !writer.TryWriteBoolean(fuel.LowFuelWarning) ||
                !writer.TryWriteBoolean(fuel.FuelPumpEnabled) ||
                !writer.TryWriteDouble(electrical.MainBusVoltageV) ||
                !writer.TryWriteDouble(electrical.EssentialBusVoltageV) ||
                !writer.TryWriteDouble(electrical.AvionicsBusVoltageV) ||
                !writer.TryWriteDouble(electrical.BatteryVoltageV) ||
                !writer.TryWriteDouble(electrical.GeneratorCurrentA) ||
                !writer.TryWriteBoolean(electrical.MainBusPowered) ||
                !writer.TryWriteBoolean(electrical.EssentialBusPowered) ||
                !writer.TryWriteBoolean(electrical.AvionicsBusPowered) ||
                !writer.TryWriteBoolean(electrical.GeneratorOnline) ||
                !writer.TryWriteBoolean(electrical.BatteryOnline) ||
                !writer.TryWriteDouble(hydraulics.SystemAPressurePa) ||
                !writer.TryWriteDouble(hydraulics.SystemBPressurePa) ||
                !writer.TryWriteDouble(hydraulics.BrakePressurePa) ||
                !writer.TryWriteBoolean(hydraulics.SystemAOnline) ||
                !writer.TryWriteBoolean(hydraulics.SystemBOnline) ||
                !writer.TryWriteDouble(landingGear.NoseGearPositionNormalized) ||
                !writer.TryWriteDouble(landingGear.LeftMainGearPositionNormalized) ||
                !writer.TryWriteDouble(landingGear.RightMainGearPositionNormalized) ||
                !writer.TryWriteDouble(landingGear.LeftBrakePressurePa) ||
                !writer.TryWriteDouble(landingGear.RightBrakePressurePa) ||
                !writer.TryWriteDouble(landingGear.SpeedBrakePositionNormalized) ||
                !writer.TryWriteDouble(landingGear.CanopyPositionNormalized) ||
                !writer.TryWriteBoolean(landingGear.NoseWheelSteeringEnabled) ||
                !writer.TryWriteBoolean(landingGear.WeightOnWheels) ||
                !writer.TryWriteByte(StoresState.StationCapacity))
            {
                return false;
            }

            for (int i = 0; i < StoresState.StationCapacity; i++)
            {
                StoreStationState station = state.Stores.Stations[i];
                if (!writer.TryWriteInt32(station.StationIndex) ||
                    !writer.TryWriteString(station.StoreType) ||
                    !writer.TryWriteInt32(station.Quantity) ||
                    !writer.TryWriteDouble(station.StoreMassKg) ||
                    !writer.TryWriteDouble(station.DragCoefficient) ||
                    !writer.TryWriteDouble(station.LongitudinalCgM) ||
                    !writer.TryWriteDouble(station.LateralCgM) ||
                    !writer.TryWriteBoolean(station.IsArmed) ||
                    !writer.TryWriteBoolean(station.IsSelected) ||
                    !writer.TryWriteBoolean(station.IsReady) ||
                    !writer.TryWriteBoolean(station.IsReleased))
                {
                    return false;
                }
            }

            return writer.TryWriteInt32((int)avionics.InsState) &&
                   writer.TryWriteBoolean(avionics.HudEnabled) &&
                   writer.TryWriteInt32((int)avionics.HudMode) &&
                   writer.TryWriteInt32((int)avionics.StartupState) &&
                   writer.TryWriteInt32((int)avionics.ActiveStartupPreset) &&
                   writer.TryWriteBoolean(avionics.MasterModeAirToAir) &&
                   writer.TryWriteBoolean(avionics.RadarEnabled) &&
                   writer.TryWriteBoolean(avionics.RadarLocked) &&
                   writer.TryWriteDouble(avionics.RadarRangeM) &&
                   writer.TryWriteBoolean(avionics.NavigationComputerEnabled) &&
                   writer.TryWriteBoolean(avionics.DataLinkEnabled) &&
                   writer.TryWriteBoolean(hud.IsValid) &&
                   writer.TryWriteInt32((int)hud.Mode) &&
                   writer.TryWriteDouble(hud.CalibratedAirspeedMps) &&
                   writer.TryWriteDouble(hud.TrueAirspeedMps) &&
                   writer.TryWriteDouble(hud.BarometricAltitudeM) &&
                   writer.TryWriteDouble(hud.RadarAltitudeM) &&
                   writer.TryWriteBoolean(hud.RadarAltitudeValid) &&
                   writer.TryWriteDouble(hud.HeadingRad) &&
                   writer.TryWriteDouble(hud.PitchRad) &&
                   writer.TryWriteDouble(hud.RollRad) &&
                   writer.TryWriteDouble(hud.FlightPathAzimuthRad) &&
                   writer.TryWriteDouble(hud.FlightPathElevationRad) &&
                   writer.TryWriteDouble(hud.AngleOfAttackRad) &&
                   writer.TryWriteDouble(hud.NormalLoadFactorG) &&
                   writer.TryWriteDouble(hud.Mach) &&
                   writer.TryWriteDouble(hud.VerticalSpeedMps) &&
                   writer.TryWriteInt32(hud.SelectedSteerpointIndex) &&
                   writer.TryWriteDouble(hud.SteerpointBearingRad) &&
                   writer.TryWriteDouble(hud.SteerpointElevationRad) &&
                   writer.TryWriteDouble(hud.SteerpointDistanceM) &&
                   writer.TryWriteBoolean(hud.MasterCaution) &&
                   writer.TryWriteBoolean(hud.MasterWarning) &&
                   writer.TryWriteBoolean(hud.WeightOnWheels) &&
                   writer.TryWriteBoolean(hud.LandingGearDown) &&
                   writer.TryWriteBoolean(hud.MasterArmEnabled) &&
                   writer.TryWriteString(hud.SelectedStoreType) &&
                   writer.TryWriteInt32(hud.SelectedStoreQuantity) &&
                   writer.TryWriteBoolean(warnings.MasterCaution) &&
                   writer.TryWriteBoolean(warnings.MasterWarning) &&
                   writer.TryWriteBoolean(warnings.FireWarning) &&
                   writer.TryWriteBoolean(warnings.HydraulicWarning) &&
                   writer.TryWriteBoolean(warnings.ElectricalWarning) &&
                   writer.TryWriteBoolean(warnings.FuelWarning) &&
                   writer.TryWriteBoolean(warnings.LowAltitudeWarning) &&
                   writer.TryWriteBoolean(warnings.OverspeedWarning) &&
                   writer.TryWriteBoolean(warnings.StallWarning) &&
                   writer.TryWriteBoolean(warnings.LandingGearWarning) &&
                   writer.TryWriteBoolean(warnings.CanopyWarning);
        }

        private static bool TryWriteTacticalPicturePayload(ref PacketWriter writer, in TacticalPictureState state)
        {
            FormationState formation = state.Formation;
            int validTrackCount = 0;
            int validWaypointCount = 0;
            for (int i = 0; i < TacticalPictureState.TrackCapacity; i++)
            {
                if (state.Tracks[i].IsValid)
                {
                    validTrackCount++;
                }
            }

            for (int i = 0; i < TacticalPictureState.WaypointCapacity; i++)
            {
                if (state.Waypoints[i].IsValid)
                {
                    validWaypointCount++;
                }
            }

            if (!writer.TryWriteDouble(state.SimulationTimeS) ||
                !writer.TryWriteFixedAircraftId(formation.LeaderAircraft) ||
                !writer.TryWriteInt32(formation.FormationSlot) ||
                !writer.TryWriteDouble(formation.DesiredForwardOffsetM) ||
                !writer.TryWriteDouble(formation.DesiredRightOffsetM) ||
                !writer.TryWriteDouble(formation.DesiredUpOffsetM) ||
                !writer.TryWriteBoolean(formation.IsFormationActive) ||
                !writer.TryWriteInt32((int)formation.Mode) ||
                !writer.TryWriteByte((byte)validTrackCount))
            {
                return false;
            }

            for (int i = 0; i < TacticalPictureState.TrackCapacity; i++)
            {
                TacticalTrack track = state.Tracks[i];
                if (!track.IsValid)
                {
                    continue;
                }

                if (!writer.TryWriteInt32(track.TrackId) ||
                    !writer.TryWriteInt32((int)track.Classification) ||
                    !writer.TryWriteInt32((int)track.Affiliation) ||
                    !writer.TryWriteDouble(track.EcefPositionXM) ||
                    !writer.TryWriteDouble(track.EcefPositionYM) ||
                    !writer.TryWriteDouble(track.EcefPositionZM) ||
                    !writer.TryWriteDouble(track.EcefVelocityXMps) ||
                    !writer.TryWriteDouble(track.EcefVelocityYMps) ||
                    !writer.TryWriteDouble(track.EcefVelocityZMps) ||
                    !writer.TryWriteDouble(track.RangeM) ||
                    !writer.TryWriteDouble(track.BearingRad) ||
                    !writer.TryWriteDouble(track.ElevationRad) ||
                    !writer.TryWriteDouble(track.ConfidenceNormalized) ||
                    !writer.TryWriteBoolean(track.IsValid))
                {
                    return false;
                }
            }

            if (!writer.TryWriteByte((byte)validWaypointCount))
            {
                return false;
            }

            for (int i = 0; i < TacticalPictureState.WaypointCapacity; i++)
            {
                WaypointState waypoint = state.Waypoints[i];
                if (!waypoint.IsValid)
                {
                    continue;
                }

                if (!writer.TryWriteInt32(waypoint.Index) ||
                    !writer.TryWriteDouble(waypoint.LongitudeRad) ||
                    !writer.TryWriteDouble(waypoint.LatitudeRad) ||
                    !writer.TryWriteDouble(waypoint.EllipsoidHeightM) ||
                    !writer.TryWriteDouble(waypoint.EcefPositionXM) ||
                    !writer.TryWriteDouble(waypoint.EcefPositionYM) ||
                    !writer.TryWriteDouble(waypoint.EcefPositionZM) ||
                    !writer.TryWriteString(waypoint.Name) ||
                    !writer.TryWriteBoolean(waypoint.IsActive) ||
                    !writer.TryWriteBoolean(waypoint.IsValid))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsKnownMessageType(UdpMessageType messageType)
        {
            return messageType == UdpMessageType.FastState ||
                   messageType == UdpMessageType.SystemsState ||
                   messageType == UdpMessageType.TacticalPictureState ||
                   messageType == UdpMessageType.SimulationEvent ||
                   messageType == UdpMessageType.MissionState ||
                   messageType == UdpMessageType.MissionActorState ||
                   messageType == UdpMessageType.MissionObjectiveState ||
                   messageType == UdpMessageType.AircraftCombatState ||
                   messageType == UdpMessageType.AutomationRunState ||
                   messageType == UdpMessageType.MissionEvent ||
                   messageType == UdpMessageType.WeaponEngagementEvent;
        }

        private static bool Assign<TEnum>(ref TEnum destination, int value) where TEnum : struct
        {
            destination = (TEnum)Enum.ToObject(typeof(TEnum), value);
            return true;
        }

        private static uint ComputeChecksum(ReadOnlySpan<byte> packet)
        {
            uint crc = 0xFFFFFFFFu;
            crc = UpdateCrc32(crc, packet.Slice(0, ChecksumOffset));
            if (packet.Length > HeaderBytes)
            {
                crc = UpdateCrc32(crc, packet.Slice(HeaderBytes));
            }

            return ~crc;
        }

        private static uint UpdateCrc32(uint crc, ReadOnlySpan<byte> bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                crc ^= bytes[i];
                for (int bit = 0; bit < 8; bit++)
                {
                    uint mask = (uint)-(int)(crc & 1u);
                    crc = (crc >> 1) ^ (0xEDB88320u & mask);
                }
            }

            return crc;
        }

        private static void WriteUInt32(Span<byte> destination, int offset, uint value)
        {
            destination[offset] = (byte)value;
            destination[offset + 1] = (byte)(value >> 8);
            destination[offset + 2] = (byte)(value >> 16);
            destination[offset + 3] = (byte)(value >> 24);
        }

        private static bool TryGetUtf8ByteCount(string value, out int byteCount)
        {
            value = value ?? string.Empty;
            byteCount = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char character = value[i];
                if (character <= 0x7F)
                {
                    byteCount++;
                }
                else if (character <= 0x7FF)
                {
                    byteCount += 2;
                }
                else if (char.IsHighSurrogate(character))
                {
                    if (i + 1 >= value.Length || !char.IsLowSurrogate(value[i + 1]))
                    {
                        byteCount = 0;
                        return false;
                    }

                    byteCount += 4;
                    i++;
                }
                else if (char.IsLowSurrogate(character))
                {
                    byteCount = 0;
                    return false;
                }
                else
                {
                    byteCount += 3;
                }
            }

            return true;
        }

        private static bool TryWriteUtf8(string value, Span<byte> destination, out int bytesWritten)
        {
            value = value ?? string.Empty;
            bytesWritten = 0;
            if (!TryGetUtf8ByteCount(value, out int requiredBytes) || destination.Length < requiredBytes)
            {
                return false;
            }

            int position = 0;
            for (int i = 0; i < value.Length; i++)
            {
                int codePoint = value[i];
                if (codePoint <= 0x7F)
                {
                    destination[position++] = (byte)codePoint;
                }
                else if (codePoint <= 0x7FF)
                {
                    destination[position++] = (byte)(0xC0 | (codePoint >> 6));
                    destination[position++] = (byte)(0x80 | (codePoint & 0x3F));
                }
                else if (char.IsHighSurrogate((char)codePoint))
                {
                    int lowSurrogate = value[++i];
                    codePoint = 0x10000 + (((codePoint - 0xD800) << 10) | (lowSurrogate - 0xDC00));
                    destination[position++] = (byte)(0xF0 | (codePoint >> 18));
                    destination[position++] = (byte)(0x80 | ((codePoint >> 12) & 0x3F));
                    destination[position++] = (byte)(0x80 | ((codePoint >> 6) & 0x3F));
                    destination[position++] = (byte)(0x80 | (codePoint & 0x3F));
                }
                else
                {
                    destination[position++] = (byte)(0xE0 | (codePoint >> 12));
                    destination[position++] = (byte)(0x80 | ((codePoint >> 6) & 0x3F));
                    destination[position++] = (byte)(0x80 | (codePoint & 0x3F));
                }
            }

            bytesWritten = position;
            return true;
        }

        private static bool TryReadUtf8(ReadOnlySpan<byte> source, out string value)
        {
            value = string.Empty;
            int characterCount = 0;
            int position = 0;
            while (position < source.Length)
            {
                if (!TryReadCodePoint(source, ref position, out int codePoint))
                {
                    return false;
                }

                characterCount += codePoint <= 0xFFFF ? 1 : 2;
            }

            if (characterCount == 0)
            {
                return true;
            }

            char[] characters = new char[characterCount];
            int characterPosition = 0;
            position = 0;
            while (position < source.Length)
            {
                TryReadCodePoint(source, ref position, out int codePoint);
                if (codePoint <= 0xFFFF)
                {
                    characters[characterPosition++] = (char)codePoint;
                }
                else
                {
                    codePoint -= 0x10000;
                    characters[characterPosition++] = (char)(0xD800 + (codePoint >> 10));
                    characters[characterPosition++] = (char)(0xDC00 + (codePoint & 0x3FF));
                }
            }

            value = new string(characters);
            return true;
        }

        private static bool TryReadCodePoint(ReadOnlySpan<byte> source, ref int position, out int codePoint)
        {
            codePoint = 0;
            byte first = source[position++];
            if (first <= 0x7F)
            {
                codePoint = first;
                return true;
            }

            if (first >= 0xC2 && first <= 0xDF)
            {
                if (!TryReadContinuation(source, ref position, out int second))
                {
                    return false;
                }

                codePoint = ((first & 0x1F) << 6) | second;
                return true;
            }

            if (first >= 0xE0 && first <= 0xEF)
            {
                if (position + 1 >= source.Length)
                {
                    return false;
                }

                byte secondByte = source[position++];
                byte thirdByte = source[position++];
                if ((secondByte & 0xC0) != 0x80 ||
                    (thirdByte & 0xC0) != 0x80 ||
                    (first == 0xE0 && secondByte < 0xA0) ||
                    (first == 0xED && secondByte >= 0xA0))
                {
                    return false;
                }

                codePoint = ((first & 0x0F) << 12) |
                            ((secondByte & 0x3F) << 6) |
                            (thirdByte & 0x3F);
                return true;
            }

            if (first >= 0xF0 && first <= 0xF4)
            {
                if (position + 2 >= source.Length)
                {
                    return false;
                }

                byte secondByte = source[position++];
                byte thirdByte = source[position++];
                byte fourthByte = source[position++];
                if ((secondByte & 0xC0) != 0x80 ||
                    (thirdByte & 0xC0) != 0x80 ||
                    (fourthByte & 0xC0) != 0x80 ||
                    (first == 0xF0 && secondByte < 0x90) ||
                    (first == 0xF4 && secondByte >= 0x90))
                {
                    return false;
                }

                codePoint = ((first & 0x07) << 18) |
                            ((secondByte & 0x3F) << 12) |
                            ((thirdByte & 0x3F) << 6) |
                            (fourthByte & 0x3F);
                return true;
            }

            return false;
        }

        private static bool TryReadContinuation(ReadOnlySpan<byte> source, ref int position, out int value)
        {
            value = 0;
            if (position >= source.Length)
            {
                return false;
            }

            byte next = source[position++];
            if ((next & 0xC0) != 0x80)
            {
                return false;
            }

            value = next & 0x3F;
            return true;
        }

        private ref struct PacketWriter
        {
            private Span<byte> destination;
            private int position;

            public PacketWriter(Span<byte> destination)
            {
                this.destination = destination;
                position = 0;
            }

            public int Position => position;

            public int Remaining => destination.Length - position;

            public bool TryWriteByte(byte value)
            {
                if (Remaining < sizeof(byte))
                {
                    return false;
                }

                destination[position++] = value;
                return true;
            }

            public bool TryWriteBoolean(bool value)
            {
                return TryWriteByte(value ? (byte)1 : (byte)0);
            }

            public bool TryWriteUInt16(ushort value)
            {
                if (Remaining < sizeof(ushort))
                {
                    return false;
                }

                destination[position++] = (byte)value;
                destination[position++] = (byte)(value >> 8);
                return true;
            }

            public bool TryWriteInt32(int value)
            {
                return TryWriteUInt32(unchecked((uint)value));
            }

            public bool TryWriteUInt32(uint value)
            {
                if (Remaining < sizeof(uint))
                {
                    return false;
                }

                destination[position++] = (byte)value;
                destination[position++] = (byte)(value >> 8);
                destination[position++] = (byte)(value >> 16);
                destination[position++] = (byte)(value >> 24);
                return true;
            }

            public bool TryWriteUInt64(ulong value)
            {
                if (Remaining < sizeof(ulong))
                {
                    return false;
                }

                destination[position++] = (byte)value;
                destination[position++] = (byte)(value >> 8);
                destination[position++] = (byte)(value >> 16);
                destination[position++] = (byte)(value >> 24);
                destination[position++] = (byte)(value >> 32);
                destination[position++] = (byte)(value >> 40);
                destination[position++] = (byte)(value >> 48);
                destination[position++] = (byte)(value >> 56);
                return true;
            }

            public bool TryWriteDouble(double value)
            {
                return TryWriteUInt64(unchecked((ulong)BitConverter.DoubleToInt64Bits(value)));
            }

            public bool TryWriteFixedAircraftId(AircraftId aircraft)
            {
                if (Remaining < AircraftIdFieldBytes ||
                    !TryGetUtf8ByteCount(aircraft.Value, out int byteCount) ||
                    byteCount > AircraftIdDataBytes)
                {
                    return false;
                }

                Span<byte> field = destination.Slice(position, AircraftIdFieldBytes);
                field.Clear();
                field[0] = (byte)byteCount;
                if (!TryWriteUtf8(aircraft.Value, field.Slice(1, byteCount), out int bytesWritten) ||
                    bytesWritten != byteCount)
                {
                    return false;
                }

                position += AircraftIdFieldBytes;
                return true;
            }

            public bool TryWriteString(string value)
            {
                if (!TryGetUtf8ByteCount(value, out int byteCount) ||
                    byteCount > ushort.MaxValue ||
                    Remaining < sizeof(ushort) + byteCount)
                {
                    return false;
                }

                TryWriteUInt16((ushort)byteCount);
                if (!TryWriteUtf8(value, destination.Slice(position, byteCount), out int bytesWritten) ||
                    bytesWritten != byteCount)
                {
                    return false;
                }

                position += byteCount;
                return true;
            }
        }

        private ref struct PacketReader
        {
            private readonly ReadOnlySpan<byte> source;
            private int position;

            public PacketReader(ReadOnlySpan<byte> source)
            {
                this.source = source;
                position = 0;
            }

            public int Remaining => source.Length - position;

            public bool TryReadByte(out byte value)
            {
                value = 0;
                if (Remaining < sizeof(byte))
                {
                    return false;
                }

                value = source[position++];
                return true;
            }

            public bool TryReadBoolean(out bool value)
            {
                value = false;
                if (!TryReadByte(out byte encoded) || encoded > 1)
                {
                    return false;
                }

                value = encoded == 1;
                return true;
            }

            public bool TryReadUInt16(out ushort value)
            {
                value = 0;
                if (Remaining < sizeof(ushort))
                {
                    return false;
                }

                value = (ushort)(source[position] | (source[position + 1] << 8));
                position += sizeof(ushort);
                return true;
            }

            public bool TryReadUInt32(out uint value)
            {
                value = 0;
                if (Remaining < sizeof(uint))
                {
                    return false;
                }

                value = source[position] |
                        ((uint)source[position + 1] << 8) |
                        ((uint)source[position + 2] << 16) |
                        ((uint)source[position + 3] << 24);
                position += sizeof(uint);
                return true;
            }

            public bool TryReadInt32(out int value)
            {
                value = 0;
                if (!TryReadUInt32(out uint encoded))
                {
                    return false;
                }

                value = unchecked((int)encoded);
                return true;
            }

            public bool TryReadUInt64(out ulong value)
            {
                value = 0;
                if (Remaining < sizeof(ulong))
                {
                    return false;
                }

                value = source[position] |
                        ((ulong)source[position + 1] << 8) |
                        ((ulong)source[position + 2] << 16) |
                        ((ulong)source[position + 3] << 24) |
                        ((ulong)source[position + 4] << 32) |
                        ((ulong)source[position + 5] << 40) |
                        ((ulong)source[position + 6] << 48) |
                        ((ulong)source[position + 7] << 56);
                position += sizeof(ulong);
                return true;
            }

            public bool TryReadDouble(out double value)
            {
                value = 0.0;
                if (!TryReadUInt64(out ulong bits))
                {
                    return false;
                }

                value = BitConverter.Int64BitsToDouble(unchecked((long)bits));
                return true;
            }

            public bool TryReadFixedAircraftId(out AircraftId aircraft)
            {
                aircraft = default;
                if (Remaining < AircraftIdFieldBytes)
                {
                    return false;
                }

                ReadOnlySpan<byte> field = source.Slice(position, AircraftIdFieldBytes);
                int byteCount = field[0];
                if (byteCount > AircraftIdDataBytes)
                {
                    return false;
                }

                for (int i = byteCount + 1; i < field.Length; i++)
                {
                    if (field[i] != 0)
                    {
                        return false;
                    }
                }

                if (!TryReadUtf8(field.Slice(1, byteCount), out string value))
                {
                    return false;
                }

                aircraft = new AircraftId(value);
                position += AircraftIdFieldBytes;
                return true;
            }

            public bool TryReadString(out string value)
            {
                value = string.Empty;
                if (!TryReadUInt16(out ushort byteCount) || Remaining < byteCount)
                {
                    return false;
                }

                if (!TryReadUtf8(source.Slice(position, byteCount), out value))
                {
                    return false;
                }

                position += byteCount;
                return true;
            }
        }
    }
}
