using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.SolidMapLayer
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