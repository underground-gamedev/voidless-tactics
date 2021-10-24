using Battle.EventSystem;
using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "TurnSystemSetupStep.asset", menuName = "CUSTOM/Setups/TurnSystemSetupStep", order = (int)SetupOrder.TurnSystem)]
    public class TurnSystemSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            state.EventQueue.AddHandler(new TurnSystem());
        }

        protected override SetupOrder SetupOrder => SetupOrder.TurnSystem;

        private class TurnSystem : IEventHandler
        {
            public HandleStatus Handle(BattleState state, IGlobalEvent globalEvent)
            {
                return globalEvent switch
                {
                    StartGameUtilityEvent startGameEvent => Handle(state, startGameEvent),
                    StartRoundGameEvent startRoundEvent => Handle(state, startRoundEvent),
                    WaitTurnGameEvent waitTurnEvent => Handle(state, waitTurnEvent),
                    SelectNextActiveGameEvent selectNextEvent => Handle(state, selectNextEvent),
                    EndTurnGameEvent endTurnEvent => Handle(state, endTurnEvent),
                    EndRoundGameEvent endRoundEvent => Handle(state, endRoundEvent),
                    _ => HandleStatus.Skipped,
                };
            }

            private HandleStatus Handle(BattleState state, StartGameUtilityEvent startGameEvent)
            {
                state.EventQueue.Handle(new StartRoundGameEvent());
                
                return HandleStatus.Handled;
            }

            private HandleStatus Handle(BattleState state, StartRoundGameEvent startRoundEvent)
            {
                var charLayer = state.Map.Map.GetLayer<ICharacterMapLayer>();
                foreach (var character in charLayer?.GetAllCharacters() ?? new ICharacter[]{})
                {
                    character.HandleEvent(new StartRoundGameEvent());
                }
                
                state.EventQueue.Handle(new SelectNextActiveGameEvent());
                
                return HandleStatus.Handled;
            }

            private HandleStatus Handle(BattleState state, SelectNextActiveGameEvent selectNextEvent)
            {
                var nextActive = state.TimeLine.Active;
                if (nextActive != null)
                {
                    nextActive.HandleEvent(new TakeTurnPersonalEvent());
                }
                else
                {
                    state.EventQueue.Handle(new EndRoundGameEvent());
                }

                return HandleStatus.Handled;
            }

            private HandleStatus Handle(BattleState state, WaitTurnGameEvent waitTurnEvent)
            {
                state.TimeLine.Set(waitTurnEvent.Character, waitTurnEvent.Initiative);

                return HandleStatus.Handled;
            }

            private HandleStatus Handle(BattleState state, EndTurnGameEvent endTurnEvent)
            {
                state.TimeLine.Remove(endTurnEvent.Character);
                state.EventQueue.Handle(new SelectNextActiveGameEvent());
                
                return HandleStatus.Handled;
            }

            private HandleStatus Handle(BattleState state, EndRoundGameEvent endRoundEvent)
            {
                foreach (var character in state.Characters.Characters)
                {
                    character.HandleEvent(new EndRoundGameEvent());
                }
                
                state.EventQueue.Handle(new YieldUtilityEvent());
                state.EventQueue.Handle(new StartRoundGameEvent());
                
                return HandleStatus.Handled;
            }
        }
    }
}