using System.Collections.Generic;
using Battle.Map.Extensions;

namespace Battle
{
    public class DirectCrossAreaSelector : ForwardAreaSelector, IAreaSelector
    {
        protected override List<MapCell> GetDirections()
        {
            return MapDirections.Direct();
        }
        public override string GetDescription(IEntity caster)
        {
            return $"direct cross: {range}";
        }
    }
}