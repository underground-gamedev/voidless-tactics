using System.Collections.Generic;
using Godot;

public class DirectLineAreaSelector : Node, IAreaSelector
{
    [Export]
    private int range;

    public List<MapCell> GetFullArea(SpellComponentContext ctx)
    {
        var map = ctx.Map;
        var area = new List<MapCell>();
        area.Add(ctx.BaseCell);

        var directions = new List<(int, int)>() {
            (0, -1), (-1, 0), (1, 0), (0, 1),
        };

        var (baseX, baseY) = ctx.BaseCell.Position;

        for (var range = 1; range <= this.range; range++)
        {
            foreach (var dir in directions)
            {
                var (offsetX, offsetY) = dir;
                var targetX = baseX + offsetX * range;
                var targetY = baseY + offsetY * range;
                if (map.IsOutOfBounds(targetX, targetY)) continue;
                area.Add(map.CellBy(targetX, targetY));
            }
        }

        return area;
    }

    public List<MapCell> GetRealArea(SpellComponentContext ctx)
    {
        return GetFullArea(ctx);
    }

    public string GetDescription(Character caster)
    {
        return $"direct line: {range}";
    }
}