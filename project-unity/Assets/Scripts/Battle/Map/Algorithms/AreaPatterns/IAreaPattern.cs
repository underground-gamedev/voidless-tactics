using System.Collections.Generic;
using Battle.Map.Interfaces;

namespace Battle.Algorithms.AreaPatterns
{
    public interface IAreaPattern
    {
        IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src);
    }
}