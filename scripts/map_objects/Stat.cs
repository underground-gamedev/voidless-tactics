using Godot;
public class Stat: Node
{
    [Export]
    private string statName = "Unknown";
    [Export]
    private int maxValue = 9999;
    [Export]
    private int minValue = 0;
    [Export]
    private int actualValue = 5;

    [Signal]
    public delegate void OnActualValueChanged(Stat stat, int oldValue);
    [Signal]
    public delegate void OnMaxValueChanged(Stat stat, int oldValue);
    public delegate void OnMinValueChanged(Stat stat, int oldValue);

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
            actualValue = Mathf.Clamp(value, minValue, maxValue);
            EmitSignal(nameof(OnActualValueChanged), this, oldValue);
        }
    }

    public Stat()
    {
    }

    public Stat(int minValue, int maxValue, int actualValue)
    {
        this.minValue =  minValue;
        this.maxValue = maxValue;
        this.ActualValue = actualValue;
    }
}
