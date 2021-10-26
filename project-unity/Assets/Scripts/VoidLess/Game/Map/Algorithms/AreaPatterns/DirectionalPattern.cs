using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Layers.CharacterMapLayer;
using VoidLess.Game.Map.Layers.SolidMapLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.AreaPatterns
{
    public class DirectionalPattern: IAreaPattern
    {
        private List<MapCell> directions;
        private int range;
        
        private bool interruptOnCharacter;
        private bool interruptOnSolid;
        private bool excludeBasePosition;

        public DirectionalPattern([NotNull] List<MapCell> directions, int range)
        {
            this.directions = directions;
            this.range = range;
        }

        public DirectionalPattern InterruptOnCharacter(bool enabled)
        {
            interruptOnCharacter = enabled;
            return this;
        }

        public DirectionalPattern InterruptOnSolid(bool enabled)
        {
            interruptOnSolid = enabled;
            return this;
        }

        public DirectionalPattern ExcludeBasePositions(bool enabled)
        {
            excludeBasePosition = enabled;
            return this;
        }

        public IEnumerable<MapCell> GetPattern(ILayeredMap map, MapCell src)
        {
            var area = new List<MapCell>();

            var charLayer = map.GetLayer<ICharacterMapLayer>();
            var solidLayer = map.GetLayer<ISolidMapLayer>();

            bool IsInterrupt(MapCell cell) => 
                interruptOnCharacter && charLayer.GetCharacter(cell) != null 
                || interruptOnSolid && solidLayer.IsSolid(cell);

            if (!excludeBasePosition && !map.IsOutOfBounds(src))
            {
                area.Add(src);
                if (IsInterrupt(src)) { return area; }
            }

            var (baseX, baseY) = src.XY;

            var growthDirections = directions.ToList();

            for (var r = 0; r < range; r++)
            {
                var needRemove = new List<MapCell>();
                foreach (var dir in growthDirections)
                {
                    var (offsetX, offsetY) = dir.XY;
                    var targetX = baseX + offsetX * range;
                    var targetY = baseY + offsetY * range;
                    if (map.IsOutOfBounds(targetX, targetY)) continue;
                    var cell = map.CellBy(targetX, targetY);
                    area.Add(cell);

                    if (IsInterrupt(cell))
                    {
                        needRemove.Add(dir);
                    }
                }

                growthDirections.RemoveAll(dir => needRemove.Contains(dir));
            }

            return area;
        }
    }
}