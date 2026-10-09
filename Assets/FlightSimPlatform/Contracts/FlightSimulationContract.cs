using System;

namespace FlightSim.Platform.Contracts
{
    public static class FlightSimulationContract
    {
        public const ushort ContractVersion = 2;
    }

    [Serializable]
    public readonly struct AircraftId : IEquatable<AircraftId>
    {
        private readonly string value;

        public AircraftId(string value)
        {
            this.value = value ?? string.Empty;
        }

        public string Value => value ?? string.Empty;

        public bool Equals(AircraftId other)
        {
            return string.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is AircraftId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return StringComparer.Ordinal.GetHashCode(Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static bool operator ==(AircraftId left, AircraftId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AircraftId left, AircraftId right)
        {
            return !left.Equals(right);
        }
    }

    public interface IFlightSimulationService
    {
        ushort ContractVersion { get; }

        bool TryGetLatest(AircraftId aircraftId, out AircraftSnapshot snapshot);

        CommandResult Submit<T>(in T command) where T : struct, ISimulationCommand;

        void RegisterSink(IFlightTelemetrySink sink);

        void UnregisterSink(IFlightTelemetrySink sink);
    }

    public interface IFlightTelemetrySink
    {
        void OnFastState(in AircraftFastState state);

        void OnSystemsState(in AircraftSystemsState state);

        void OnTacticalPictureState(in TacticalPictureState state);

        void OnSimulationEvent(in SimulationEvent simulationEvent);
    }

    public interface ISimulationCommand
    {
    }

    [Serializable]
    public struct CommandResult
    {
        public bool Accepted;
        public int Code;
        public string Message;

        public static CommandResult Success(string message)
        {
            return new CommandResult { Accepted = true, Code = 0, Message = message };
        }

        public static CommandResult Rejected(int code, string message)
        {
            return new CommandResult { Accepted = false, Code = code, Message = message };
        }
    }
}
