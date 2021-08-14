using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class CircleAreaSelector : Node, IAreaSelector
{
    [Export]
    private int range;

    public List<MapCell> GetFullArea(SpellComponentContext ctx)
    {
        var (cx, cy) = ctx.TargetCell.Position;
        return AreaSelectorHelpers.GetCircle(cx, cy, range)
            .Where(pos => !ctx.Map.IsOutOfBounds(pos.Item1, pos.Item2))
            .Select(pos => ctx.Map.CellBy(pos.Item1, pos.Item2))
            .ToList();
    }

    public List<MapCell> GetRealArea(SpellComponentContext ctx)
    {
        return GetFullArea(ctx);
    }

    public string GetDescription(Character caster)
    {
        return $"{TextHelpers.GetIconBBCode("4_04")} circle: {range}";
    }
}