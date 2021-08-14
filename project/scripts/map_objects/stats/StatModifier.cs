public abstract class StatModifier
{
    public virtual int ModifyPriority => 0;
    public virtual int ModifyMaxValue(Stat stat, int current) { return current; }
    public virtual int ModifyMinValue(Stat stat, int current) { return current; }
    public virtual int ModifyActualValue(Stat stat, int current) { return current; }
    public virtual StatModifier StackWith(StatModifier statModifier) { return statModifier; }
}