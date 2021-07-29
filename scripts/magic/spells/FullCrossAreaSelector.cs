using System.Collections.Generic;
using Godot;

public class FullCrossAreaSelector : ForwardAreaSelector, IAreaSelector
{
    protected override List<(int, int)> GetDirections(SpellComponentContext ctx)
    {
        return new List<(int, int)>() {
            (0, 1), (1, 0), (0, -1), (-1, 0),
            (1, 1), (-1, 1), (-1, -1), (1, -1),
        };
    }
    public override string GetDescription(Character caster)
    {
        return $"full cross: {range}";
    }
}