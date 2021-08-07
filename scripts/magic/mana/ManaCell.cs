using System;
using Godot;

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
        this.manaType = ManaType.None;
        this.actualValue = 0;
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
            Set(ManaType.Wind, int.MaxValue);
        }

        return wouldTake;
    }

    public void Add(int value)
    {
        var newValue = actualValue + (long)value;
        actualValue = (int)Math.Min(newValue, int.MaxValue);
    }

    public void Set(ManaType type, int actualValue, ManaStore store = null)
    {
        this.manaType = type;
        this.actualValue = actualValue;
        this.manaStore = store;
    }
}