using System.Collections.Generic;
using Godot;

public class DirectCrossAreaSelector : ForwardAreaSelector, IAreaSelector
{
    protected override List<(int, int)> GetDirections(SpellComponentContext ctx)
    {
        return new List<(int, int)>() {
            (0, 1), (1, 0), (0, -1), (-1, 0),
        };
    }
    public override string GetDescription(Character caster)
    {
        return $"direct cross: {range}";
    }
}