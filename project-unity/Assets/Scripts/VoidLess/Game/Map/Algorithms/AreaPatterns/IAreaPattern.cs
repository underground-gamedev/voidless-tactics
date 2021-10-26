using System.Collections.Generic;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.AreaPatterns
{
    public interface IAreaPattern
    {
        IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src);
    }
}