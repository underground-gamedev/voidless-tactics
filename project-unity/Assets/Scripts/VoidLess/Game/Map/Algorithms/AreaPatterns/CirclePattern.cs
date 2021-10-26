using System.Collections.Generic;
using System.Linq;
using VoidLess.Game.Magic.Spells.Implementations.AreaSelectors;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.AreaPatterns
{
    public class CirclePattern: IAreaPattern
    {
        private int range;
        
        public CirclePattern(int range)
        {
            this.range = range;
        }
        public IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src)
        {
            var (cx, cy) = src.XY;
            return AreaSelectorHelpers.GetCircle(cx, cy, range)
                .Where(pos => !map.IsOutOfBounds(pos.x, pos.y))
                .Select(pos => map.CellBy(pos.x, pos.y));
        }
    }
}