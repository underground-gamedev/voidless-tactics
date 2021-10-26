using VoidLess.Core.Components;

namespace VoidLess.Game.Map.Base
{
    public interface IMapLayer: IComponent
    {
        void OnAttached(ILayeredMap map);
        void OnDeAttached();
    }
}