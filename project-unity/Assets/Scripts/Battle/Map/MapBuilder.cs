using System;
using System.Collections.Generic;
using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle
{
    public class MapBuilder : MonoBehaviour
    {
        [SerializeField]
        private int width = 30;
        [SerializeField]
        private int height = 20;

        private EditableMap map;

        public void Awake()
        {
            map = new EditableMap(width, height);
        }

        public MapBuilder AddLayer(Type type, IMapLayer layer)
        {
            map.AddLayer(type, layer);
            return this;
        }

        public MapBuilder AddLayer<T>(T layer) where T : class, IMapLayer
        {
            return AddLayer(typeof(T), layer);
        }

        public MapBuilder AddLayer<T1, T2>(T1 layer)
            where T1 : class, IMapLayer
            where T2 : class, IMapLayer
        {
            AddLayer(typeof(T1), layer);
            AddLayer(typeof(T2), layer);
            return this;
        }

        public MapBuilder AddLayer<T1, T2, T3>(T1 layer)
            where T1 : class, IMapLayer
            where T2 : class, IMapLayer
            where T3 : class, IMapLayer
        {
            AddLayer(typeof(T1), layer);
            AddLayer(typeof(T2), layer);
            AddLayer(typeof(T3), layer);
            return this;
        }

        public T GetLayer<T>() where T : class, IMapLayer
        {
            return map.GetLayer<T>();
        }

        public ILayeredMap Build()
        {
            var result = map;
            return map;
            
        }
    }
}
