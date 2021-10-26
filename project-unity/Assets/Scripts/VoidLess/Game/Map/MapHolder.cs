using System;
using VoidLess.Game.Map.Base;

namespace VoidLess.Game.Map
{
    public class MapHolder : ILayeredMapHolder
    {
        public event Action OnMapChanged;
        public ILayeredMap Map { get; set; }
        public void Set(ILayeredMap map)
        {
            Map = map;
            OnMapChanged?.Invoke();
        }

        public void Reset()
        {
            Map = null;
            OnMapChanged?.Invoke();
        }
    }
}