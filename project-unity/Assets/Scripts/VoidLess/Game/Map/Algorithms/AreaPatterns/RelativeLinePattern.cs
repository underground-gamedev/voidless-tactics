using System.Collections.Generic;
using UnityEngine;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.AreaPatterns
{
    public class RelativeLinePattern: IRelativeAreaPattern
    {
        private int range;
        
        public RelativeLinePattern(int range)
        {
            this.range = range;
        }
        
        public IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src, MapCell dest)
        {
            var (srcX, srcY) = src.XY;
            var (targetX, targetY) = dest.XY;
            var dirX = Mathf.Clamp(targetX - srcX, -1, 1);
            var dirY = Mathf.Clamp(targetY - srcY, -1, 1);

            if (dirX == 0 && dirY == 0)
            {
                return new List<MapCell>();
            }

            var directions = new List<MapCell>() {new MapCell(dirX, dirY)};
            return new DirectionalPattern(directions, range).GetPattern(map, dest);
        }
    }
}