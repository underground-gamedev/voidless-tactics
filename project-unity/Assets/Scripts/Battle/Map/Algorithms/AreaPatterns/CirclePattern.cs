using System.Collections.Generic;
using System.Linq;
using Battle.Map.Extensions;
using Battle.Map.Interfaces;

namespace Battle.Algorithms.AreaPatterns
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