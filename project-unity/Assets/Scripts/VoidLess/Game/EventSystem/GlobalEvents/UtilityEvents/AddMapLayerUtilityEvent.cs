using System;
using VoidLess.Game.Map.Base;

namespace VoidLess.Game.EventSystem.GlobalEvents.UtilityEvents
{
    public class AddMapLayerUtilityEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly Type TAssociation;
        public readonly IMapLayer Layer;
        
        public AddMapLayerUtilityEvent(ILayeredMap map, Type association, IMapLayer layer)
        {
            Map = map;
            TAssociation = association;
            Layer = layer;
        }
        
        public override string ToString()
        {
            return $"{nameof(TAssociation)}: {TAssociation}, {nameof(Layer)}: {Layer}";
        }
    }
}