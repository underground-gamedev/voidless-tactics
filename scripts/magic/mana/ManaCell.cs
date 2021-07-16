using System;
using Godot;

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
        set {
            if (value > 1) value = 1;
            if (value < 0) value = 0;
            density = value;
            if (value <= 0.01) manaType = ManaType.None;
        } 
    }

    public ManaCell(ManaType type, double density)
    {
        this.ManaType = type;
        this.Density = density;
    }
	public static int ComparisonByDensityToLowest(ManaCell x, ManaCell y)
	{
		//if (x == null && y == null)
		//	return 0;
		
		if (x.density > y.density)
			return 1;
		return -1;
	}

    public ManaCell()
    {
        this.ManaType = ManaType.None;
        this.Density = 0;
    }
}