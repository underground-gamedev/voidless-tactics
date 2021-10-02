using System;

namespace Battle.Map.Interfaces
{
    public interface ILayeredMapHolder
    {
        event Action OnMapChanged;
        ILayeredMap Map { get; }
        void Set(ILayeredMap map);
        void Reset();
    }
}