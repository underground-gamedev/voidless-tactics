using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ManaMapGenerator: Node
{
    [Export]
    private int seed;
    [Export]
    private int prepareMoveCount = 4;
    private Random rand;

    public override void _Ready()
    {
        rand = (seed == -1) ? new Random() : new Random(seed);
    }

    public void Generate(TacticMap map)
    {
        foreach (var cell in map)
        {
            if (!cell.Solid)
            {
                if ((float)rand.NextDouble() < 0.8f) continue;
                var manaType = rand.Next(1, 4);
                cell.Mana.ManaType = (ManaType)manaType;
                cell.Mana.Density = (float)rand.NextDouble() * 2;

                if (cell.Mana.ManaType == ManaType.None)
                    cell.Mana.Density = 0;
            }
        }

        var manaMover = new ManaMover(map);
        
        var state = GDPrint.active;
		

        
        for (var i = 0; i < prepareMoveCount; i++)
        {
            manaMover.ApplyChangesMap(manaMover.GetChangesMap());
        }
        
    }
}