using System.Collections.Generic;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.AreaPatterns
{
    public class SourceBasedRelativeWrapper: IAreaPattern, IRelativeAreaPattern
    {
        private IAreaPattern areaPattern;
        
        public SourceBasedRelativeWrapper(IAreaPattern areaPattern)
        {
            this.areaPattern = areaPattern;
        }
        
        public IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src)
        {
            return areaPattern.GetPattern(map, src);
        }

        public IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src, MapCell dest)
        {
            return areaPattern.GetPattern(map, src);
        }
    }
}