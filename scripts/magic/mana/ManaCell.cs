using System;
using Godot;

public class ManaCell
{
    private ManaType manaType;
    private int density;

    public ManaType ManaType
    {
        get => manaType;
        set => manaType = value;
    }
    public int Density
    {
        get => density;
        set => density = value;
    }

    public ManaCell(ManaType type, int density)
    {
        this.ManaType = type;
        this.Density = density;
    }
	public static int ComparisonByDensityToLowest(ManaCell x, ManaCell y)
	{
        return x.density.CompareTo(y.density);
	}

    public ManaCell()
    {
        this.ManaType = ManaType.None;
        this.Density = 0;
    }
}