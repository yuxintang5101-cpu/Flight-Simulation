namespace FlightSim.Platform.Unity
{
    internal static class FlightSimExecutionOrder
    {
        public const int Simulation = -300;
        public const int AircraftVisual = -200;
        public const int TerrainQuery = -150;
        public const int Camera = -100;
    }
}
