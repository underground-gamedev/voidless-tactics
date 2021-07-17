using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ManaMapGenerator: Node
{
    [Export]
    private int seed;
    private Random rand;

    public override void _Ready()
    {
        rand = seed == -1 ? new Random() : new Random(seed);
    }

    public void Generate(TacticMap map)
    {
        foreach (var cell in map)
        {
            if (!cell.Solid)
            {
                var manaType = rand.Next(1, 4);
                cell.Mana.ManaType = (ManaType)manaType;
                cell.Mana.Density = rand.NextDouble() / 2;
				
                if (manaType == (int)ManaType.None)
					cell.Mana.Density = 0;
            }
        }

    }
}