using System.Collections.Generic;
using Battle.Map.Extensions;

namespace Battle
{
    public class FullCrossAreaSelector : ForwardAreaSelector, IAreaSelector
    {
        protected override List<MapCell> GetDirections()
        {
            return MapDirections.All();
        }
        public override string GetDescription(Character caster)
        {
            return $"full cross: {range}";
        }
    }
}