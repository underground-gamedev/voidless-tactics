using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.SolidMapLayer
{
    public interface ISolidMapLayer: IMapLayer
    {
        bool IsSolid(MapCell cell);
    }
}