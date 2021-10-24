using Battle.EventSystem;
using Battle.Map.Extensions;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "DebugControlSetupStep.asset", menuName = "CUSTOM/Setups/DebugControlSetupStep", order = (int)SetupOrder.DebugControl)]
    public class DebugControlSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            state.EventQueue.AddHandler(new DebugControl());
        }

        protected override SetupOrder SetupOrder => SetupOrder.DebugControl;
        
        private class DebugControl : IEventHandler
        {
            public HandleStatus Handle(BattleState state, IGlobalEvent globalEvent)
            {
                return globalEvent switch
                {
                    ClickOnCellUtilityEvent clickEvent => Handle(state, clickEvent),
                    _ => HandleStatus.Skipped,
                };
            }

            private HandleStatus Handle(BattleState state, ClickOnCellUtilityEvent clickEvent)
            {
                var clickCell = clickEvent.Cell;
                var map = clickEvent.Map;
                
                if (map.IsOutOfBounds(clickCell)) return HandleStatus.Skipped;

                var active = state.TimeLine.Active;
                if (active == null) return HandleStatus.Skipped;
                
                state.EventQueue.Handle(new MoveToGameEvent(active, clickCell));
                
                return HandleStatus.Handled;
            }
        }
    }
}