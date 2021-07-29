using System.Collections.Generic;
using System.Linq;
using Godot;

public class SquareAreaSelector: Node, IAreaSelector
{
    [Export]
    private int range;

    public List<MapCell> GetFullArea(SpellComponentContext ctx)
    {
        var (cx, cy) = ctx.TargetCell.Position;
        var result = new List<MapCell>();
        for (int x = cx - range; x <= cx + range; x++)
        {
            for (int y = cy - range; y <= cy + range; y++)
            {
                if (!ctx.Map.IsOutOfBounds(x, y))
                {
                    result.Add(ctx.Map.CellBy(x, y));
                }
            }
        }

        return result;
    }

    public List<MapCell> GetRealArea(SpellComponentContext ctx)
    {
        return GetFullArea(ctx);
    }

    public string GetDescription(Character caster)
    {
        return $"square: {range}";
    }
}