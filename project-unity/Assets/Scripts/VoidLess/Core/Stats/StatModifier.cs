namespace VoidLess.Core.Stats
{
    public abstract class StatModifier
    {
        public abstract int ModifyValue(int baseValue, int modifiedCurrent);
        public virtual int ModifyPriority => 0;
        public virtual StatModifier StackWith(StatModifier statModifier) { return statModifier; }
    }
}