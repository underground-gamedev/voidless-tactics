using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.EventSystem.GlobalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;

namespace VoidLess.Game.Setup.SetupSteps.ViewSteps
{
    public class CharacterViewDynamicBindSystem : IEventHandler
    {
        private CharacterViewComponentController viewController;
        public CharacterViewDynamicBindSystem(CharacterViewComponentController viewController)
        {
            this.viewController = viewController;
        }
        
        public HandleStatus Handle(BattleState state, IGlobalEvent globalEvent)
        {
            return globalEvent switch
            {
                CharacterAddedOnMapGameEvent characterAddedEvent => Handle(characterAddedEvent),
                CharacterRemovedFromMapGameEvent characterRemovedEvent => Handle(characterRemovedEvent),
                _ => HandleStatus.Skipped,
            };
        }

        private HandleStatus Handle(CharacterAddedOnMapGameEvent characterAddedEvent)
        {
            return viewController.AddViewToCharacter(
                characterAddedEvent.Character,
                characterAddedEvent.Map,
                characterAddedEvent.Cell);
        }

        private HandleStatus Handle(CharacterRemovedFromMapGameEvent characterRemovedEvent)
        {
            return viewController.RemoveViewFromCharacter(
                characterRemovedEvent.Character);
        }
    }
}