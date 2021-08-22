using System;

namespace Battle
{
    public class ManaCell
    {
        private ManaType manaType;
        private int actualValue;
        private ManaStore manaStore;

        public ManaType ManaType
        {
            get => manaType;
        }

        public int ActualValue
        {
            get => actualValue;
        }

        public ManaCell()
        {
            manaType = ManaType.None;
            actualValue = 0;
        }

        public ManaCell(ManaType manaType, int actualValue)
        {
            Set(manaType, actualValue);
        }

        public int Consume(int needCount)
        {
            var wouldTake = Math.Min(actualValue, needCount);
            actualValue -= wouldTake;
            actualValue = manaStore?.Refill(actualValue) ?? actualValue;

            if (actualValue == 0)
            {
                SetInfinity(ManaType.Wind, 15);
            }

            return wouldTake;
        }

        public void Add(int value)
        {
            var newValue = actualValue + (long)value;
            actualValue = (int)Math.Min(newValue, int.MaxValue);
        }

        public void Set(ManaType type, int val, ManaStore store = null)
        {
            manaType = type;
            actualValue = val;
            manaStore = store;
        }

        public void SetInfinity(ManaType type, int limit)
        {
            manaType = type;
            actualValue = limit;
            manaStore = new ManaStore(int.MaxValue, limit);
        }
    }
}