using System;

namespace FlightSim.Platform.Contracts
{
    [Serializable]
    public struct StoreStationCollection
    {
        public StoreStationState Station0;
        public StoreStationState Station1;
        public StoreStationState Station2;
        public StoreStationState Station3;
        public StoreStationState Station4;
        public StoreStationState Station5;
        public StoreStationState Station6;
        public StoreStationState Station7;
        public StoreStationState Station8;

        public int Length => StoresState.StationCapacity;

        public StoreStationState this[int index]
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

    [Serializable]
    public struct TacticalTrackCollection
    {
        public TacticalTrack Track0;
        public TacticalTrack Track1;
        public TacticalTrack Track2;
        public TacticalTrack Track3;
        public TacticalTrack Track4;
        public TacticalTrack Track5;
        public TacticalTrack Track6;
        public TacticalTrack Track7;
        public TacticalTrack Track8;
        public TacticalTrack Track9;
        public TacticalTrack Track10;
        public TacticalTrack Track11;
        public TacticalTrack Track12;
        public TacticalTrack Track13;
        public TacticalTrack Track14;
        public TacticalTrack Track15;
        public TacticalTrack Track16;
        public TacticalTrack Track17;
        public TacticalTrack Track18;
        public TacticalTrack Track19;
        public TacticalTrack Track20;
        public TacticalTrack Track21;
        public TacticalTrack Track22;
        public TacticalTrack Track23;
        public TacticalTrack Track24;
        public TacticalTrack Track25;
        public TacticalTrack Track26;
        public TacticalTrack Track27;
        public TacticalTrack Track28;
        public TacticalTrack Track29;
        public TacticalTrack Track30;
        public TacticalTrack Track31;

        public int Length => TacticalPictureState.TrackCapacity;

        public TacticalTrack this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Track0;
                    case 1: return Track1;
                    case 2: return Track2;
                    case 3: return Track3;
                    case 4: return Track4;
                    case 5: return Track5;
                    case 6: return Track6;
                    case 7: return Track7;
                    case 8: return Track8;
                    case 9: return Track9;
                    case 10: return Track10;
                    case 11: return Track11;
                    case 12: return Track12;
                    case 13: return Track13;
                    case 14: return Track14;
                    case 15: return Track15;
                    case 16: return Track16;
                    case 17: return Track17;
                    case 18: return Track18;
                    case 19: return Track19;
                    case 20: return Track20;
                    case 21: return Track21;
                    case 22: return Track22;
                    case 23: return Track23;
                    case 24: return Track24;
                    case 25: return Track25;
                    case 26: return Track26;
                    case 27: return Track27;
                    case 28: return Track28;
                    case 29: return Track29;
                    case 30: return Track30;
                    case 31: return Track31;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: Track0 = value; break;
                    case 1: Track1 = value; break;
                    case 2: Track2 = value; break;
                    case 3: Track3 = value; break;
                    case 4: Track4 = value; break;
                    case 5: Track5 = value; break;
                    case 6: Track6 = value; break;
                    case 7: Track7 = value; break;
                    case 8: Track8 = value; break;
                    case 9: Track9 = value; break;
                    case 10: Track10 = value; break;
                    case 11: Track11 = value; break;
                    case 12: Track12 = value; break;
                    case 13: Track13 = value; break;
                    case 14: Track14 = value; break;
                    case 15: Track15 = value; break;
                    case 16: Track16 = value; break;
                    case 17: Track17 = value; break;
                    case 18: Track18 = value; break;
                    case 19: Track19 = value; break;
                    case 20: Track20 = value; break;
                    case 21: Track21 = value; break;
                    case 22: Track22 = value; break;
                    case 23: Track23 = value; break;
                    case 24: Track24 = value; break;
                    case 25: Track25 = value; break;
                    case 26: Track26 = value; break;
                    case 27: Track27 = value; break;
                    case 28: Track28 = value; break;
                    case 29: Track29 = value; break;
                    case 30: Track30 = value; break;
                    case 31: Track31 = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }
    }

    [Serializable]
    public struct WaypointCollection
    {
        public WaypointState Waypoint0;
        public WaypointState Waypoint1;
        public WaypointState Waypoint2;
        public WaypointState Waypoint3;
        public WaypointState Waypoint4;
        public WaypointState Waypoint5;
        public WaypointState Waypoint6;
        public WaypointState Waypoint7;
        public WaypointState Waypoint8;
        public WaypointState Waypoint9;
        public WaypointState Waypoint10;
        public WaypointState Waypoint11;
        public WaypointState Waypoint12;
        public WaypointState Waypoint13;
        public WaypointState Waypoint14;
        public WaypointState Waypoint15;

        public int Length => TacticalPictureState.WaypointCapacity;

        public WaypointState this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Waypoint0;
                    case 1: return Waypoint1;
                    case 2: return Waypoint2;
                    case 3: return Waypoint3;
                    case 4: return Waypoint4;
                    case 5: return Waypoint5;
                    case 6: return Waypoint6;
                    case 7: return Waypoint7;
                    case 8: return Waypoint8;
                    case 9: return Waypoint9;
                    case 10: return Waypoint10;
                    case 11: return Waypoint11;
                    case 12: return Waypoint12;
                    case 13: return Waypoint13;
                    case 14: return Waypoint14;
                    case 15: return Waypoint15;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: Waypoint0 = value; break;
                    case 1: Waypoint1 = value; break;
                    case 2: Waypoint2 = value; break;
                    case 3: Waypoint3 = value; break;
                    case 4: Waypoint4 = value; break;
                    case 5: Waypoint5 = value; break;
                    case 6: Waypoint6 = value; break;
                    case 7: Waypoint7 = value; break;
                    case 8: Waypoint8 = value; break;
                    case 9: Waypoint9 = value; break;
                    case 10: Waypoint10 = value; break;
                    case 11: Waypoint11 = value; break;
                    case 12: Waypoint12 = value; break;
                    case 13: Waypoint13 = value; break;
                    case 14: Waypoint14 = value; break;
                    case 15: Waypoint15 = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }
    }
}
