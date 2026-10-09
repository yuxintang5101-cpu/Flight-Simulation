using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Core
{
    public struct StoreReleaseResult
    {
        public bool Success;
        public int StationIndex;
        public string StoreType;
        public int RequestedQuantity;
        public int ReleasedQuantity;
        public int RemainingQuantity;
        public double ReleasedMassKg;
        public double RemovedDragCoefficient;
        public double LongitudinalMomentChangeKgM;
        public double LateralMomentChangeKgM;
        public bool EmergencyJettison;
    }

    public sealed class F16StoresModel
    {
        private StoresState state;
        private bool masterArmEnabled;
        private bool electricalPowerAvailable;
        private bool releaseInterlockClear;
        private int selectedStationIndex;
        private double totalMassKg;
        private double totalDragCoefficient;
        private double longitudinalMomentKgM;
        private double lateralMomentKgM;

        public F16StoresModel()
        {
            state = StoresState.CreateDefault();
            masterArmEnabled = true;
            electricalPowerAvailable = true;
            releaseInterlockClear = true;
            selectedStationIndex = -1;
            InitializeEmptyStations();
        }

        public StoresState State => state;

        public bool MasterArmEnabled => masterArmEnabled;

        public int SelectedStationIndex => selectedStationIndex;

        public double TotalMassKg => totalMassKg;

        public double TotalDragCoefficient => totalDragCoefficient;

        public double LongitudinalCenterOfGravityM => totalMassKg > 0.0
            ? longitudinalMomentKgM / totalMassKg
            : 0.0;

        public double LateralCenterOfGravityM => totalMassKg > 0.0
            ? lateralMomentKgM / totalMassKg
            : 0.0;

        public void Configure(in StoreLoadoutCollection loadout)
        {
            state = StoresState.CreateDefault();
            selectedStationIndex = -1;

            StoreStationCollection stations = state.Stations;
            for (int stationIndex = 0; stationIndex < StoresState.StationCapacity; stationIndex++)
            {
                StoreLoadoutConfiguration configuration = loadout[stationIndex];
                int quantity = configuration.Quantity > 0 ? configuration.Quantity : 0;
                string storeType = configuration.StoreType ?? string.Empty;
                ResolveStore(storeType, out StoreCatalogEntry catalogEntry);
                GetStationPosition(stationIndex, out double longitudinalCgM, out double lateralCgM);

                stations[stationIndex] = new StoreStationState
                {
                    StationIndex = stationIndex,
                    StoreType = storeType,
                    Quantity = quantity,
                    StoreMassKg = quantity > 0 ? catalogEntry.MassKg : 0.0,
                    DragCoefficient = quantity > 0 ? catalogEntry.DragCoefficient : 0.0,
                    LongitudinalCgM = longitudinalCgM,
                    LateralCgM = lateralCgM,
                    IsArmed = quantity > 0 && masterArmEnabled,
                    IsSelected = false,
                    IsReady = quantity > 0 && masterArmEnabled && electricalPowerAvailable && releaseInterlockClear,
                    IsReleased = false
                };
            }

            state.Stations = stations;
            RecalculateTotals();
        }

        public void SetMasterArm(bool enabled)
        {
            masterArmEnabled = enabled;
            RefreshReadiness();
        }

        public void UpdateReadiness(bool hasElectricalPower, bool interlockClear)
        {
            electricalPowerAvailable = hasElectricalPower;
            releaseInterlockClear = interlockClear;
            RefreshReadiness();
        }

        public bool SelectStation(int stationIndex)
        {
            if (!IsValidStation(stationIndex))
            {
                return false;
            }

            selectedStationIndex = stationIndex;
            StoreStationCollection stations = state.Stations;
            for (int index = 0; index < StoresState.StationCapacity; index++)
            {
                StoreStationState station = stations[index];
                station.IsSelected = index == stationIndex;
                stations[index] = station;
            }

            state.Stations = stations;
            return true;
        }

        public bool SetStationArmed(int stationIndex, bool armed)
        {
            if (!IsValidStation(stationIndex))
            {
                return false;
            }

            StoreStationCollection stations = state.Stations;
            StoreStationState station = stations[stationIndex];
            station.IsArmed = armed && masterArmEnabled && station.Quantity > 0;
            station.IsReady = station.IsArmed && electricalPowerAvailable && releaseInterlockClear;
            stations[stationIndex] = station;
            state.Stations = stations;
            return true;
        }

        public bool TryRelease(
            int stationIndex,
            int requestedQuantity,
            bool emergencyJettison,
            out StoreReleaseResult result)
        {
            result = default(StoreReleaseResult);
            result.StationIndex = stationIndex;
            result.RequestedQuantity = requestedQuantity;
            result.EmergencyJettison = emergencyJettison;

            if (!IsValidStation(stationIndex))
            {
                return false;
            }

            StoreStationCollection stations = state.Stations;
            StoreStationState station = stations[stationIndex];
            result.StoreType = station.StoreType;
            result.RemainingQuantity = station.Quantity;

            if (station.Quantity <= 0 ||
                (!emergencyJettison && requestedQuantity <= 0) ||
                (!emergencyJettison && (!station.IsArmed || !station.IsReady)))
            {
                return false;
            }

            int releaseQuantity = emergencyJettison && requestedQuantity <= 0
                ? station.Quantity
                : Min(requestedQuantity, station.Quantity);
            if (releaseQuantity <= 0)
            {
                return false;
            }

            result.Success = true;
            result.ReleasedQuantity = releaseQuantity;
            result.RemainingQuantity = station.Quantity - releaseQuantity;
            result.ReleasedMassKg = station.StoreMassKg * releaseQuantity;
            result.RemovedDragCoefficient = station.DragCoefficient * releaseQuantity;
            result.LongitudinalMomentChangeKgM = result.ReleasedMassKg * station.LongitudinalCgM;
            result.LateralMomentChangeKgM = result.ReleasedMassKg * station.LateralCgM;

            station.Quantity = result.RemainingQuantity;
            station.IsReleased = true;
            station.IsReady = station.Quantity > 0 && station.IsArmed && electricalPowerAvailable && releaseInterlockClear;
            if (station.Quantity == 0)
            {
                station.IsArmed = false;
            }

            stations[stationIndex] = station;
            state.Stations = stations;
            RecalculateTotals();
            return true;
        }

        public bool EmergencyJettison(int stationIndex, out StoreReleaseResult result)
        {
            return TryRelease(stationIndex, 0, true, out result);
        }

        private void InitializeEmptyStations()
        {
            StoreStationCollection stations = state.Stations;
            for (int stationIndex = 0; stationIndex < StoresState.StationCapacity; stationIndex++)
            {
                GetStationPosition(stationIndex, out double longitudinalCgM, out double lateralCgM);
                stations[stationIndex] = new StoreStationState
                {
                    StationIndex = stationIndex,
                    StoreType = string.Empty,
                    LongitudinalCgM = longitudinalCgM,
                    LateralCgM = lateralCgM
                };
            }

            state.Stations = stations;
            RecalculateTotals();
        }

        private void RefreshReadiness()
        {
            StoreStationCollection stations = state.Stations;
            for (int index = 0; index < StoresState.StationCapacity; index++)
            {
                StoreStationState station = stations[index];
                station.IsArmed = station.Quantity > 0 && masterArmEnabled;
                station.IsReady = station.IsArmed && electricalPowerAvailable && releaseInterlockClear;
                stations[index] = station;
            }

            state.Stations = stations;
        }

        private void RecalculateTotals()
        {
            totalMassKg = 0.0;
            totalDragCoefficient = 0.0;
            longitudinalMomentKgM = 0.0;
            lateralMomentKgM = 0.0;

            for (int index = 0; index < StoresState.StationCapacity; index++)
            {
                StoreStationState station = state.Stations[index];
                double stationMassKg = station.StoreMassKg * station.Quantity;
                totalMassKg += stationMassKg;
                totalDragCoefficient += station.DragCoefficient * station.Quantity;
                longitudinalMomentKgM += stationMassKg * station.LongitudinalCgM;
                lateralMomentKgM += stationMassKg * station.LateralCgM;
            }
        }

        private static void ResolveStore(string storeType, out StoreCatalogEntry entry)
        {
            switch (storeType)
            {
                case "AIM-9M":
                case "AIM-9X":
                    entry = new StoreCatalogEntry(86.0, 0.010);
                    return;
                case "AIM-120B":
                case "AIM-120C":
                    entry = new StoreCatalogEntry(152.0, 0.014);
                    return;
                case "AGM-65D":
                case "AGM-65G":
                    entry = new StoreCatalogEntry(300.0, 0.030);
                    return;
                case "AGM-88C":
                    entry = new StoreCatalogEntry(361.0, 0.035);
                    return;
                case "GBU-10":
                    entry = new StoreCatalogEntry(934.0, 0.050);
                    return;
                case "GBU-12":
                    entry = new StoreCatalogEntry(230.0, 0.026);
                    return;
                case "GBU-24":
                    entry = new StoreCatalogEntry(1050.0, 0.055);
                    return;
                case "GBU-31":
                    entry = new StoreCatalogEntry(925.0, 0.050);
                    return;
                case "GBU-38":
                    entry = new StoreCatalogEntry(241.0, 0.026);
                    return;
                case "MK-82":
                case "Mk-82":
                    entry = new StoreCatalogEntry(241.0, 0.024);
                    return;
                case "MK-84":
                case "Mk-84":
                    entry = new StoreCatalogEntry(894.0, 0.048);
                    return;
                case "CBU-87":
                case "CBU-97":
                    entry = new StoreCatalogEntry(430.0, 0.036);
                    return;
                case "AN/AAQ-28":
                case "AN/AAQ-33":
                    entry = new StoreCatalogEntry(210.0, 0.032);
                    return;
                case "AN/ALQ-131":
                case "AN/ALQ-184":
                    entry = new StoreCatalogEntry(220.0, 0.038);
                    return;
                case "300GAL":
                case "FUEL-300GAL":
                    entry = new StoreCatalogEntry(1110.0, 0.060);
                    return;
                case "370GAL":
                case "FUEL-370GAL":
                    entry = new StoreCatalogEntry(1360.0, 0.070);
                    return;
                case "600GAL":
                case "FUEL-600GAL":
                    entry = new StoreCatalogEntry(2200.0, 0.090);
                    return;
                case "":
                    entry = default(StoreCatalogEntry);
                    return;
                default:
                    entry = new StoreCatalogEntry(100.0, 0.025);
                    return;
            }
        }

        private static void GetStationPosition(int stationIndex, out double longitudinalCgM, out double lateralCgM)
        {
            switch (stationIndex)
            {
                case 0:
                    longitudinalCgM = 0.65;
                    lateralCgM = -4.95;
                    return;
                case 1:
                    longitudinalCgM = 0.35;
                    lateralCgM = -4.05;
                    return;
                case 2:
                    longitudinalCgM = 0.05;
                    lateralCgM = -3.05;
                    return;
                case 3:
                    longitudinalCgM = -0.20;
                    lateralCgM = -1.80;
                    return;
                case 4:
                    longitudinalCgM = -0.45;
                    lateralCgM = 0.0;
                    return;
                case 5:
                    longitudinalCgM = -0.20;
                    lateralCgM = 1.80;
                    return;
                case 6:
                    longitudinalCgM = 0.05;
                    lateralCgM = 3.05;
                    return;
                case 7:
                    longitudinalCgM = 0.35;
                    lateralCgM = 4.05;
                    return;
                case 8:
                    longitudinalCgM = 0.65;
                    lateralCgM = 4.95;
                    return;
                default:
                    longitudinalCgM = 0.0;
                    lateralCgM = 0.0;
                    return;
            }
        }

        private static bool IsValidStation(int stationIndex)
        {
            return stationIndex >= 0 && stationIndex < StoresState.StationCapacity;
        }

        private static int Min(int left, int right)
        {
            return left < right ? left : right;
        }

        private readonly struct StoreCatalogEntry
        {
            public StoreCatalogEntry(double massKg, double dragCoefficient)
            {
                MassKg = massKg;
                DragCoefficient = dragCoefficient;
            }

            public double MassKg { get; }

            public double DragCoefficient { get; }
        }
    }
}
