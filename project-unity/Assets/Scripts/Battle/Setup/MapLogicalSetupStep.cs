using Battle.Map.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "MapLogicalSetupStep.asset", menuName = "CUSTOM/Setups/MapLogicalSetupStep", order = 1)]
    public class MapLogicalSetupStep : SerializableSetupStep
    {
        [SerializeField] private Vector2Int size;

        public override void Setup(BattleState state)
        {
            var map = new EditableMap(size.x, size.y)
                .AddLayer<ISolidMapLayer>(new NonSolidMapLayer())
                .AddLayer<ICharacterMapLayer>(new CharacterMapLayer())
                .AddLayer<IManaEditorMapLayer>(new ManaEditorMapLayer())
                .AddLayer<IManaMapLayer, IManaInfoMapLayer>(new ManaMapLayer())
                .AddLayer<IPathfindMapLayer>(new PathfindLayer());
            
            state.Map.Set(map);
        }

        protected override SetupOrder SetupOrder => SetupOrder.MapLogical;
    }
}