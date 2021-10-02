using System;
using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle
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