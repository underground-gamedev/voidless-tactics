using System;
using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "MapLogicalSetupStep.asset", menuName = "CUSTOM/Setups/MapLogicalSetupStep", order = (int)SetupOrder.MapLogical)]
    public class MapLogicalSetupStep : SerializableSetupStep
    {
        [SerializeField] private Vector2Int size;

        public override void Setup(BattleState state)
        {
            var map = new EditableMap(size.x, size.y);
            state.Map.Set(map);

            map.AddComponent<IGlobalEventEmitter>(new GlobalEventEmitter(state.EventQueue));
            map.AddLayer<MapEventsEmitter>(new MapEventsEmitter());
                
            map.AddLayer<ISolidMapLayer>(new NonSolidMapLayer())
               .AddLayer<ICharacterMapLayer>(new CharacterMapLayer())
               .AddLayer<IManaEditorMapLayer>(new ManaEditorMapLayer())
               .AddLayer<IManaMapLayer, IManaInfoMapLayer>(new ManaMapLayer())
               .AddLayer<IPathfindMapLayer>(new PathfindLayer());
        }

        protected override SetupOrder SetupOrder => SetupOrder.MapLogical;

        private class MapEventsEmitter : IMapLayer
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
}