using System;

namespace Battle.Map.Interfaces
{
    public interface IManaEditorMapLayer: IManaInfoMapLayer
    {
        void SetMana(MapCell cell, ManaInfo mana);
    }
}