using System;
using Battle.Map.Interfaces;
using Core.Components;

namespace Battle
{
    public class EditableMap: Entity, ILayeredMap
    {
        public event Action<Type, IMapLayer> OnLayerAssociated;
        public event Action<Type, IMapLayer> OnLayerUnAssociated;

        public int Width { get; }
        public int Height { get; }

        private ComponentContainer<IMapLayer> layers;
        
        public EditableMap(int width, int height)
        {
            Width = width;
            Height = height;
            
            layers = new ComponentContainer<IMapLayer>(OnLayerAdded, OnLayerRemoved);
        }

        public ILayeredMap AddLayer(Type type, IMapLayer layer)
        {
            layers.Attach(type, layer);
            OnLayerAssociated?.Invoke(type, layer);
            return this;
        }
        
        public EditableMap AddLayer<T>(T layer) where T : class, IMapLayer
        {
            AddLayer(typeof(T), layer);
            return this;
        }

        public EditableMap AddLayer<T1, T2>(T1 layer) 
            where T1 : class, IMapLayer
            where T2 : class, IMapLayer
        {
            if (!(layer is T2))
            {
                throw new InvalidOperationException();
            }
            
            AddLayer<T1>(layer);
            AddLayer<T2>(layer as T2);
            return this;
        }

        public EditableMap AddLayer<T1, T2, T3>(T1 layer)
            where T1 : class, IMapLayer
            where T2 : class, IMapLayer
            where T3 : class, IMapLayer
        {
            if (!(layer is T2) || !(layer is T3))
            {
                throw new InvalidOperationException();
            }
            
            AddLayer<T1>(layer);
            AddLayer<T2>(layer as T2);
            AddLayer<T3>(layer as T3);
            return this;
        }

        public T GetLayer<T>() where T : class, IMapLayer
        {
            return layers.Get<T>();
        }

        void ILayeredMap.AddLayer<T>(T layer)
        {
            AddLayer<T>(layer);
        }

        public void RemoveLayer<T>() where T : class, IMapLayer
        {
            var layer = layers.Get<T>();
            
            if (layer == null) return;
            
            layers.DeAttach<T>();
            OnLayerUnAssociated?.Invoke(typeof(T), layer);
        }

        public void RemoveAll()
        {
            layers.DeAttachAll();
        }

        private void OnLayerAdded(IMapLayer layer)
        {
            layer.OnAttached(this);
        }

        private void OnLayerRemoved(IMapLayer layer)
        {
            layer.OnDeAttached();
        }
    }
}