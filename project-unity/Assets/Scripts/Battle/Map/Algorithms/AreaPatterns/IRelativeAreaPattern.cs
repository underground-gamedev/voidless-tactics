using System.Collections.Generic;
using Battle.Map.Interfaces;

namespace Battle.Algorithms.AreaPatterns
{
    public interface IRelativeAreaPattern
    {
        IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src, MapCell dest);
    }
}