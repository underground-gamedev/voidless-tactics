using System;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter;
using VoidLess.Game.EventSystem.GlobalEvents.UtilityEvents;
using VoidLess.Game.Map.Base;

namespace VoidLess.Game.Map
{
    public class MapEventEmitter : IMapLayer
    {
        private ILayeredMap map;

        private void EmitAddLayer(Type association, IMapLayer layer)
        {
            var emitter = map.GetComponent<IGlobalEventEmitter>();
            emitter?.Emit(new AddMapLayerUtilityEvent(map, association, layer));
        }

        private void EmitRemoveLayer(Type association, IMapLayer layer)
        {
            var emitter = map.GetComponent<IGlobalEventEmitter>();
            emitter?.Emit(new RemoveMapLayerUtilityEvent(map, association, layer));
        }

        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
            map.OnLayerAssociated += EmitAddLayer;
            map.OnLayerUnAssociated += EmitRemoveLayer;
        }

        public void OnDeAttached()
        {
            map.OnLayerAssociated -= EmitAddLayer;
            map.OnLayerUnAssociated -= EmitRemoveLayer;
            this.map = null;
        }
    }
}