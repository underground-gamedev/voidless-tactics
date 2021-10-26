using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.VisualMapLayer
{
    public interface IVisualMapLayer: IMapLayer
    {
        void RedrawAll();
        void RedrawSingle(MapCell pos);
    }
}