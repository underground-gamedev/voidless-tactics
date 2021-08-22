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

    [Export] private Texture rasterPatternMap;
    [Export] private int magmaPower = 100;
    [Export] private int naturePower = 100;
    [Export] private int waterPower = 100;
    [Export] private int windPower = 100;


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
                if ((float)rand.NextDouble() < 0.8f) {
                    cell.Mana.SetInfinity(ManaType.Wind, 15);
                    continue;
                }
                var manaType = (ManaType)rand.Next(1, 4);
                cell.Mana.Set(manaType, rand.Next(50, 100));
            }
        }

        var manaMover = new ManaMover(map);
        
        var state = GDPrint.active;
		

        
        for (var i = 0; i < prepareMoveCount; i++)
        {
            manaMover.ApplyChangesMap(manaMover.GetChangesMap());
        }
        
    }

    public void GenerateFromTexture (TacticMap map)
    {
        if (rasterPatternMap == null) Generate(map);

        Image rasterImage = rasterPatternMap.GetData();

        int xOffset = rand.Next(0, rasterImage.GetWidth());
        int yOffset = rand.Next(0, rasterImage.GetHeight());

        rasterImage.Lock();

        //GD.Print("Width Size " + rasterImage.GetWidth() + " Height Size " + rasterImage.GetWidth());

        foreach (MapCell cell in map)
        {
            Color color = rasterImage.GetPixel(CoordToTexSpace(cell.X + xOffset, rasterImage.GetWidth()),
                                               CoordToTexSpace(cell.Y + yOffset, rasterImage.GetHeight()));

            //GD.Print(cell.Position + " " + color);

            if(color.r > 0.8f && color.g < 0.8f && color.b < 0.8f)
            {
                cell.Mana.SetInfinity(ManaType.Magma, magmaPower);
                continue;
            }
            if (color.g > 0.8f && color.b < 0.8f && color.r < 0.8f)
            {
                cell.Mana.SetInfinity(ManaType.Nature, naturePower); //For now
                continue;
            }
            if (color.b > 0.8f && color.r < 0.8f && color.g < 0.8f)
            {
                cell.Mana.SetInfinity(ManaType.Water, waterPower); //For now
                continue;
            }

            cell.Mana.SetInfinity(ManaType.Wind, windPower);
        }

        rasterImage.Unlock();

        var manaMover = new ManaMover(map);
        var state = GDPrint.active;
    }

    int CoordToTexSpace(int coord, int axisMax)
    {
        int sign = Math.Abs(coord) / coord;

        coord = Math.Abs(coord);

        if (coord >= axisMax)
        {
            coord = coord % axisMax;
        }

        if (sign < 0)
            coord = axisMax - coord - 1;
           
        return coord;
    }

}