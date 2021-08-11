using System;
using System.Collections.Generic;
using Godot;

public abstract class ForwardAreaSelector : Node, IAreaSelector
{
    [Export]
    protected int range;
    [Export]
    protected bool interruptOnCharacter;
    [Export]
    protected bool excludeBasePosition;

    protected abstract List<(int, int)> GetDirections(SpellComponentContext ctx);

    private List<MapCell> GetArea(SpellComponentContext ctx, Func<MapCell, bool> dropDirectionPredicate)
    {
        var map = ctx.Map;
        var area = new List<MapCell>();

        if (!excludeBasePosition)
        {
            area.Add(ctx.TargetCell);
            if (interruptOnCharacter && dropDirectionPredicate(ctx.TargetCell)) { return area; }
        }

        var directions = GetDirections(ctx);

        var (baseX, baseY) = ctx.TargetCell.Position;

        for (var range = 1; range < this.range; range++)
        {
            var needRemove = new List<(int, int)>();
            foreach (var dir in directions)
            {
                var (offsetX, offsetY) = dir;
                var targetX = baseX + offsetX * range;
                var targetY = baseY + offsetY * range;
                if (map.IsOutOfBounds(targetX, targetY)) continue;
                var cell = map.CellBy(targetX, targetY);
                area.Add(cell);

                if (interruptOnCharacter && dropDirectionPredicate(cell))
                {
                    needRemove.Add(dir);
                }
            }

            needRemove.ForEach(dir => directions.Remove(dir));
        }

        return area;
    }

    public List<MapCell> GetFullArea(SpellComponentContext ctx)
    {
        return GetArea(ctx, (cell) => false);
    }

    public List<MapCell> GetRealArea(SpellComponentContext ctx)
    {
        return GetArea(ctx, (cell) => cell.MapObject is Character);
    }

    public abstract string GetDescription(Character caster);
}