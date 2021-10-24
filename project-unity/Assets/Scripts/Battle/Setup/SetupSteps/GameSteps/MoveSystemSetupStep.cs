using Battle.EventSystem;
using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "MoveSystemSetupStep.asset", menuName = "CUSTOM/Setups/MoveSystemSetupStep", order = (int)SetupOrder.MoveSystem)]
    public class MoveSystemSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            state.EventQueue.AddHandler(new MoveSystem());
        }

        protected override SetupOrder SetupOrder => SetupOrder.MoveSystem;
        
        private class MoveSystem : IEventHandler
        {
            public HandleStatus Handle(BattleState state, IGlobalEvent globalEvent)
            {
                return globalEvent switch
                {
                    MoveToGameEvent moveTo => Handle(state, moveTo),
                    _ => HandleStatus.Skipped,
                };
            }

            private HandleStatus Handle(BattleState state, MoveToGameEvent moveToEvent)
            {
                var (ent, targetCell) = moveToEvent;
                var map = state.Map.Map;

                var pathfinder = map.GetLayer<IPathfindMapLayer>();
                var entityLayer = map.GetLayer<ICharacterMapLayer>();

                var currCell = entityLayer.GetPosition(ent);
                if (!currCell.HasValue) return HandleStatus.Skipped;

                var pathResult = pathfinder.Pathfind(currCell.Value, targetCell);
                if (!pathResult.IsSuccess) return HandleStatus.Skipped;
                
                entityLayer.RelocateCharacter(ent, targetCell);
                state.EventQueue.Handle(new CharacterRelocatedGameEvent(map, ent, currCell.Value, targetCell));
                state.EventQueue.Handle(new EndTurnGameEvent(ent));
                
                return HandleStatus.Handled;
            }
        }
    }
}