using System;

namespace VoidLess.Game.Map.Base
{
    public interface ILayeredMapHolder
    {
        event Action OnMapChanged;
        ILayeredMap Map { get; }
        void Set(ILayeredMap map);
        void Reset();
    }
}