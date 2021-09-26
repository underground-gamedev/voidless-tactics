using System;

namespace Battle.Map.Interfaces
{
    public interface IManaInfoMapLayer: IMapLayer
    {
        ManaInfo GetMana(MapCell cell);
        
        event Action<MapCell, ManaInfo> OnManaChanged;
    }
}