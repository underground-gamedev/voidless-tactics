using System.Collections.Generic;
using Battle.Map.Extensions;

namespace Battle
{
    public class DiagonalCrossAreaSelector : ForwardAreaSelector, IAreaSelector
    {
        protected override List<MapCell> GetDirections()
        {
            return MapDirections.Diagonal();
        }
        public override string GetDescription(Character caster)
        {
            return $"diagonal cross: {range}";
        }
    }
}