public class ManaCell
{
    private ManaType manaType;
    private double density;

    public ManaType ManaType
    {
        get => manaType;
        set => manaType = value;
    }
    public double Density
    {
        get => density;
        set => density = value;
    }

    public ManaCell(ManaType type, double density)
    {
        this.ManaType = type;
        this.Density = density;
    }

    public ManaCell()
    {
        this.ManaType = ManaType.None;
        this.Density = 0;
    }
}