using System;
using System.Collections.Generic;
using Godot;

public class DirectLineAreaSelector : ForwardAreaSelector, IAreaSelector
{
    protected override List<(int, int)> GetDirections(SpellComponentContext ctx)
    {
        var srcPos = ctx.SourceCell.Position;
        var (srcX, srcY) = srcPos;
        var targetPos = ctx.TargetCell.Position;
        var (targetX, targetY) = targetPos;
        var dirX = Mathf.Clamp(targetX - srcX, -1, 1);
        var dirY = Mathf.Clamp(targetY - srcY, -1, 1);

        if (dirX == 0 && dirY == 0)
        {
            return new List<(int, int)>();
        }

        return new List<(int, int)>() { (dirX, dirY) };
    }

    public override string GetDescription(Character caster)
    {
        return $"{TextHelpers.GetIconBBCode("7_89")} relative line: {range}";
    }
}