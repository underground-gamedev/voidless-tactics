using System.Collections.Generic;

namespace Battle
{
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
            return $"diagonal cross: {range}";
        }
    }
}