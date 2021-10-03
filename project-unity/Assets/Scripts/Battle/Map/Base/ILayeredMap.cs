using System;

namespace Battle.Map.Interfaces
{
    public interface ILayeredMap: ISizedMap, IEntity
    {
        event Action<Type, IMapLayer> OnLayerAssociated;
        event Action<Type, IMapLayer> OnLayerUnAssociated;
        
        T GetLayer<T>() where T : class, IMapLayer;
        void AddLayer<T>(T layer) where T : class, IMapLayer;
        void RemoveLayer<T>() where T : class, IMapLayer;
        void RemoveAll();
    }
}