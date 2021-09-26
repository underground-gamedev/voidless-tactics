using System;

namespace Battle.Map.Interfaces
{
    public interface IManaMapLayer: IManaInfoMapLayer
    {
        ManaInfo Consume(MapCell cell, int count);
    }
}