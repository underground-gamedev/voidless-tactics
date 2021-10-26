using System.Collections.Generic;
using VoidLess.Core.Entities;
using VoidLess.Game.Magic.Spells.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Magic.Spells.Implementations.AreaSelectors
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