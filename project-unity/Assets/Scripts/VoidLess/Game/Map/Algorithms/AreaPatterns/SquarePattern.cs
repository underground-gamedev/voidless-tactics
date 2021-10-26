using System.Collections.Generic;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.AreaPatterns
{
    public class SquarePattern: IAreaPattern
    {
        private int range;
        
        public SquarePattern(int range)
        {
            this.range = range;
        }
        
        public IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src)
        {
            var (cx, cy) = src.XY;
            var result = new List<MapCell>();
            for (int x = cx - range; x <= cx + range; x++)
            {
                for (int y = cy - range; y <= cy + range; y++)
                {
                    if (map.IsOutOfBounds(x, y)) continue;
                    result.Add(map.CellBy(x, y));
                }
            }

            return result;
        }
    }
}