public class AddStatModifier: StatModifier
{
    protected int actualChanger;
    protected int minChanger;
    protected int maxChanger;

    public AddStatModifier(int actual, int min, int max)
    {
        actualChanger = actual;
        minChanger = min;
        maxChanger = max; 
    }

    public override int ModifyActualValue(Stat stat, int current) { return current + actualChanger; }
    public override int ModifyMinValue(Stat stat, int current) { return current + minChanger; }
    public override int ModifyMaxValue(Stat stat, int current) { return current + maxChanger; }
    public override StatModifier StackWith(StatModifier statModifier) { 
        var addStat = (AddStatModifier)statModifier;
        var newActual = actualChanger + addStat.actualChanger;
        var newMin = minChanger + addStat.minChanger;
        var newMax = maxChanger + addStat.maxChanger;
        return new AddStatModifier(newActual, newMin, newMax);
    }
}