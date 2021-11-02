using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.CharacterViewComponent;
using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.EventSystem.GlobalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;
using VoidLess.Game.Map.Layers.CoordinateConverterLayer;

namespace VoidLess.Game.Setup.SetupSteps.ViewSteps
{
    public class CharacterViewRelocateSystem : IEventHandler
    {
        public HandleStatus Handle(BattleState state, IGlobalEvent globalEvent)
        {
            return globalEvent switch
            {
                CharacterRelocatedGameEvent relocatedEvent => Handle(relocatedEvent),
                _ => HandleStatus.Skipped,
            };
        }

        private HandleStatus Handle(CharacterRelocatedGameEvent relocateEvent)
        {
            var character = relocateEvent.Character;
            var characterView = character.GetComponent<CharacterViewComponent>();
            if (characterView == null) return HandleStatus.Skipped;
            
            var map = relocateEvent.Map;
            var coordinateConverter = map.GetLayer<ICoordinateConverterLayer>();
            if (coordinateConverter == null) return HandleStatus.Skipped;

            var dest = relocateEvent.Dest;
            var viewDest = coordinateConverter.MapToGlobal(dest);
            characterView.Relocate(viewDest);

            return HandleStatus.Handled;
        }
    }
}