using VoidLess.Game.Magic.Mana;
using VoidLess.Game.Map.Layers.ManaInfoMapLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.ManaMapLayer
{
    public interface IManaMapLayer: IManaInfoMapLayer
    {
        ManaInfo Consume(MapCell cell, int count);
    }
}