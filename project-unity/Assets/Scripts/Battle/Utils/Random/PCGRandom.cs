using System;

namespace Battle.Systems.RandomSystem
{
    public class PCGRandom : IRandomGenerator
    {
        private const ulong StateMultiplier = 6364136223846793005ul;
        private const ulong Increment = 1442695040888963407ul;

        private ulong state;

        public PCGRandom() : this(Environment.TickCount)
        {
        }

        public PCGRandom(int seed)
        {
            Initialize((ulong)seed);
        }

        private void Initialize(ulong seed)
        {
            state = 0ul;
            NextUInt();
            state += seed;
            NextUInt();
        }

        public uint NextUInt()
        {
            ulong oldState = state;
            state = unchecked(oldState * StateMultiplier + Increment);
            uint xorShifted = (uint)(((oldState >> 18) ^ oldState) >> 27);
            int rot = (int)(oldState >> 59);
            uint result = (xorShifted >> rot) | (xorShifted << ((-rot) & 31));
            return result;
        }
    }
}