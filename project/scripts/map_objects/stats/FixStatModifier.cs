public class FixStatModifier: StatModifier
{
    private int fixValue;
    private int priority;
    public override int ModifyPriority => priority;

    public FixStatModifier(int fixValue, int priority = 0)
    {
        this.fixValue = fixValue;
        this.priority = priority;
    }

    public override int ModifyActualValue(Stat stat, int current)
    {
        return fixValue;
    }
}