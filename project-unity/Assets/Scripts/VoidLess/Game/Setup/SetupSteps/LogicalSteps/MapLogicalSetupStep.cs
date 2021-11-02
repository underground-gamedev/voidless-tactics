using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter;
using VoidLess.Game.Map;
using VoidLess.Game.Map.Layers.CharacterMapLayer;
using VoidLess.Game.Map.Layers.ManaEditorMapLayer;
using VoidLess.Game.Map.Layers.ManaInfoMapLayer;
using VoidLess.Game.Map.Layers.ManaMapLayer;
using VoidLess.Game.Map.Layers.PathfindLayer;
using VoidLess.Game.Map.Layers.SolidMapLayer;

namespace VoidLess.Game.Setup.SetupSteps.LogicalSteps
{
    [CreateAssetMenu(fileName = "MapLogicalSetupStep.asset", menuName = "CUSTOM/Setups/MapLogicalSetupStep", order = (int)SetupOrder.MapLogical)]
    public class MapLogicalSetupStep : SerializableSetupStep
    {
        [SerializeField] private Vector2Int size;

        public override void Setup(BattleState state)
        {
            var map = new EditableMap(size.x, size.y);
            state.Map.Set(map);

            map.AddWithAssociation<IGlobalEventEmitter>(new GlobalEventEmitter(state.EventQueue));
            map.AddLayer<MapEventEmitter>(new MapEventEmitter());
                
            map.AddLayer<ISolidMapLayer>(new NonSolidMapLayer())
               .AddLayer<ICharacterMapLayer>(new CharacterMapLayer())
               .AddLayer<IManaEditorMapLayer>(new ManaEditorMapLayer())
               .AddLayer<IManaMapLayer, IManaInfoMapLayer>(new ManaMapLayer())
               .AddLayer<IPathfindMapLayer>(new PathfindLayer());
        }

        protected override SetupOrder SetupOrder => SetupOrder.MapLogical;
    }
}