using System.Collections.Generic;
using System.Linq;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Layers.SolidMapLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.AreaPatterns
{
    public class PathfindNeighboursPattern: IAreaPattern
    {
        private static readonly List<MapCell> NeighDirections = MapDirections.All();
        public IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src)
        {
            var solidLayer = map.GetLayer<ISolidMapLayer>();
            return NeighDirections.Select(dir => MapCell.FromVector(src.Pos + dir.Pos))
                .Where(cell => !map.IsOutOfBounds(cell) && !solidLayer.IsSolid(cell))
                .ToList();
        }
    }
}