using Battle.Map.Interfaces;
using Core.Components;

namespace Battle.Components.MapBindingComponent
{
    public class MapBindingComponent : IComponent
    {
        public readonly ILayeredMap Map;
        
        public MapBindingComponent(ILayeredMap map)
        {
            Map = map;
        }
    }
}