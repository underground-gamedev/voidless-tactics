using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
public class Stat: Node
{
    [Export]
    private string statName = "Unknown";
    [Export]
    private int actualValue = 5;
    [Export]
    private int minValue = 0;
    [Export]
    private int maxValue = 9999;

    [Signal]
    public delegate void OnActualValueChanged(Stat stat, int oldValue);
    [Signal]
    public delegate void OnMaxValueChanged(Stat stat, int oldValue);
    [Signal]
    public delegate void OnMinValueChanged(Stat stat, int oldValue);

    Dictionary<string, StatModifier> modifiers = new Dictionary<string, StatModifier>();

    public string StatName {
        get => statName;
        set => statName = value;
    }

    public int MaxValue {
        get => maxValue;
        set {
            var oldValue = maxValue;
            maxValue = value;
            ActualValue = actualValue;
            EmitSignal(nameof(OnMaxValueChanged), this, oldValue);
        }
    }

    public int MinValue {
        get => minValue;
        set {
            var oldValue = minValue;
            minValue = value;
            ActualValue = actualValue;
            EmitSignal(nameof(OnMinValueChanged), this, oldValue);
        } 
    }

    public int ActualValue {
        get => actualValue;
        set {
            var oldValue = actualValue;
            actualValue = Mathf.Clamp(value, MinValue, MaxValue);
            EmitSignal(nameof(OnActualValueChanged), this, oldValue);
        }
    }

    private int GetModified(int baseVal, Func<StatModifier, int, int> applyModifier)
    {
        var currentValue = baseVal;
        foreach (var modifier in modifiers.Values.OrderBy(mod => mod.ModifyPriority))
        {
            currentValue = applyModifier(modifier, currentValue);
        }
        return currentValue;
    }

    public int ModifiedMaxValue => GetModified(MaxValue, (mod, curr) => mod.ModifyMaxValue(this, curr));
    public int ModifiedMinValue => GetModified(MinValue, (mod, curr) => mod.ModifyMinValue(this, curr));
    public int ModifiedActualValue => GetModified(ActualValue, (mod, curr) => mod.ModifyActualValue(this, curr));

    public void AddModifier(string key, StatModifier modifier)
    {
        if (modifiers.ContainsKey(key))
        {
            modifiers[key] = modifiers[key].StackWith(modifier);
            return;
        }

        modifiers.Add(key, modifier);
    }

    public void RemoveModifier(string key)
    {
        modifiers.Remove(key);
    }

    public T GetModifier<T>(string key) where T: StatModifier
    {
        return modifiers.ContainsKey(key) ? (T)modifiers[key] : null;
    }
    public Stat()
    {
    }

    public Stat(int minValue, int maxValue, int actualValue)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.ActualValue = actualValue;
    }
}
