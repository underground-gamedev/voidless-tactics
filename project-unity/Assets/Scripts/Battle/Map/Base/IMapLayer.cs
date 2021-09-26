using Core.Components;

namespace Battle.Map.Interfaces
{
    public interface IMapLayer: IComponent
    {
        void OnAttached(ILayeredMap map);
        void OnDeAttached();
    }
}