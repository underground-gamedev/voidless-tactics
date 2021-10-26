using VoidLess.Core.Components;
using VoidLess.Game.Map.Base;

namespace VoidLess.Game.Entities.Characters.Components.MapBindingComponent
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