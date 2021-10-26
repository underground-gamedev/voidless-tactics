using System;
using VoidLess.Game.Magic.Mana;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Layers.ManaInfoMapLayer
{
    public interface IManaInfoMapLayer: IMapLayer
    {
        ManaInfo GetMana(MapCell cell);
        
        event Action<MapCell, ManaInfo> OnManaChanged;
    }
}