using System.Collections.Generic;
using Godot;

public class DiagonalCrossAreaSelector : ForwardAreaSelector, IAreaSelector
{
    protected override List<(int, int)> GetDirections(SpellComponentContext ctx)
    {
        return new List<(int, int)>() {
            (1, 1), (-1, 1), (-1, -1), (1, -1),
        };
    }
    public override string GetDescription(Character caster)
    {
        return $"{TextHelpers.GetIconBBCode("7_89")} diagonal cross: {range}";
    }
}