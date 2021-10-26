using System.Collections.Generic;
using VoidLess.Core.Entities;
using VoidLess.Game.Magic.Spells.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Magic.Spells.Implementations.AreaSelectors
{
    public class DiagonalCrossAreaSelector : ForwardAreaSelector, IAreaSelector
    {
        protected override List<MapCell> GetDirections()
        {
            return MapDirections.Diagonal();
        }
        public override string GetDescription(IEntity caster)
        {
            return $"diagonal cross: {range}";
        }
    }
}