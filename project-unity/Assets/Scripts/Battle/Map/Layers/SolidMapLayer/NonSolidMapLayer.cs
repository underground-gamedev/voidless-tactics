using System;
using Battle.Map.Extensions;
using Battle.Map.Interfaces;

namespace Battle
{
    public class NonSolidMapLayer: ISolidMapLayer
    {
        private ILayeredMap map;

        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
        }

        public void OnDeAttached()
        {
            this.map = null;}

        public bool IsSolid(MapCell cell)
        {
            return map.IsOutOfBounds(cell);
        }
    }
}