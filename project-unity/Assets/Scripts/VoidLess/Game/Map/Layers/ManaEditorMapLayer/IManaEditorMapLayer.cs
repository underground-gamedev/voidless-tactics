using VoidLess.Game.Magic.Mana;
using VoidLess.Game.Map.Layers.ManaInfoMapLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.ManaEditorMapLayer
{
    public interface IManaEditorMapLayer: IManaInfoMapLayer
    {
        void SetMana(MapCell cell, ManaInfo mana);
    }
}