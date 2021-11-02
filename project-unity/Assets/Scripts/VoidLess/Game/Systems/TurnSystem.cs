using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.PersonalEvents;
using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.EventSystem.GlobalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;
using VoidLess.Game.EventSystem.GlobalEvents.UtilityEvents;
using VoidLess.Game.Map.Layers.CharacterMapLayer;

namespace VoidLess.Game.Setup.SetupSteps.GameSteps
{
    public class TurnSystem : IEventHandler
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
            foreach (var character in charLayer?.GetAllCharacters() ?? new IEntity[]{})
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