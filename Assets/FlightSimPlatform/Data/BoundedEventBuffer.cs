using System;

namespace FlightSim.Platform.Data
{
    internal sealed class BoundedEventBuffer
    {
        private readonly PlatformDataEvent[] events;
        private int head;
        private int count;

        public BoundedEventBuffer(int capacity)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            events = new PlatformDataEvent[capacity];
        }

        public int Capacity => events.Length;
        public int Count => count;
        public ulong OldestSequence => count == 0 ? 0UL : events[head].Sequence;
        public ulong NewestSequence => count == 0 ? 0UL : events[(head + count - 1) % events.Length].Sequence;

        public void Append(in PlatformDataEvent dataEvent)
        {
            int index = (head + count) % events.Length;
            if (count == events.Length)
            {
                index = head;
                head = (head + 1) % events.Length;
            }
            else
            {
                count++;
            }

            events[index] = dataEvent;
        }

        public bool TryRead(ulong afterSequence, out PlatformDataEvent dataEvent)
        {
            for (int offset = 0; offset < count; offset++)
            {
                PlatformDataEvent candidate = events[(head + offset) % events.Length];
                if (candidate.Sequence > afterSequence)
                {
                    dataEvent = candidate;
                    return true;
                }
            }

            dataEvent = default(PlatformDataEvent);
            return false;
        }

        public void Clear()
        {
            Array.Clear(events, 0, events.Length);
            head = 0;
            count = 0;
        }
    }
}
