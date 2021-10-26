using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Base;
using VoidLess.Game.Entities.Characters.Components.CharacterViewComponent;
using VoidLess.Game.Entities.Characters.Components.ViewTagComponent;
using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.EventSystem.GlobalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Layers.CharacterMapLayer;
using VoidLess.Game.Map.Layers.CoordinateConverterLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Setup.SetupSteps.ViewSteps
{
    [CreateAssetMenu(fileName = "CharactersViewSetupStep.asset", menuName = "CUSTOM/Setups/CharactersViewSetupStep", order = (int)SetupOrder.CharactersView)]
    public class CharactersViewSetupStep : SerializableSetupStep
    {
        [OdinSerialize, Required]
        private ViewBindings bindings;
        
        public override void Setup(BattleState state)
        {
            var viewController = new CharacterViewComponentController(bindings);
            
            state.EventQueue.AddHandler(new CharacterViewComponentHandler(viewController));
            state.EventQueue.AddHandler(new RelocateViewAfterLogicalRelocation());

            var characters = state.Characters.Characters;
            var map = state.Map.Map;
            var characterLayer = map.GetLayer<ICharacterMapLayer>();
            
            if (characterLayer == null) return;
            foreach (var character in characters)
            {
                if (character.HasComponent<CharacterViewComponent>()) continue;
                var pos = characterLayer.GetPosition(character);
                if (!pos.HasValue) continue;
                viewController.AddViewToCharacter(character, map, pos.Value);
            }
        }

        protected override SetupOrder SetupOrder => SetupOrder.CharactersView;
        
        private class RelocateViewAfterLogicalRelocation : IEventHandler
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

        private class CharacterViewComponentHandler : IEventHandler
        {
            private CharacterViewComponentController viewController;
            public CharacterViewComponentHandler(CharacterViewComponentController viewController)
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

        private class CharacterViewComponentController
        {
            private ViewBindings viewBindings;
            public CharacterViewComponentController(ViewBindings viewBindings)
            {
                this.viewBindings = viewBindings;
            }

            public HandleStatus AddViewToCharacter(IEntity character, ILayeredMap map, MapCell position)
            {
                var coordinateConverter = map.GetLayer<ICoordinateConverterLayer>();
                if (coordinateConverter == null)
                {
                    Debug.LogError($"{nameof(CharacterViewComponentHandler)}:: Invalid configuration. Expected coordinate converter layer on map.");
                    return HandleStatus.Skipped;
                }
                
                var viewTagCom = character.GetComponent<ViewTagComponent>();
                if (viewTagCom == null) return HandleStatus.Skipped;
                
                var viewTag = viewTagCom.ViewTag;
                var view = viewBindings.CreateView(viewTag);
                if (view == null) return HandleStatus.Skipped;

                character.AddComponent(view);
                var viewDest = coordinateConverter.MapToGlobal(position);
                view.Relocate(viewDest);
                
                return HandleStatus.Handled;
            }

            public HandleStatus RemoveViewFromCharacter(IEntity character)
            {
                var view = character.GetComponent<CharacterViewComponent>();
                if (view == null) return HandleStatus.Skipped;
                character.RemoveComponent<CharacterViewComponent>();
                viewBindings.RemoveView(view);
                return HandleStatus.Handled;
            }
        }
        
        private class ViewBindings
        {
            [OdinSerialize, Required]
            private Dictionary<CharacterViewTag, CharacterViewComponent> viewBindings;
            public ViewBindings(Dictionary<CharacterViewTag, CharacterViewComponent> viewBindings)
            {
                this.viewBindings = viewBindings;
            }

            public CharacterViewComponent CreateView(CharacterViewTag viewTag)
            {
                if (!viewBindings.TryGetValue(viewTag, out var viewAsset))
                {
                    Debug.LogError($"{nameof(ViewBindings)}:: Invalid configuration. Not found view component for {viewTag}");
                    return null;
                }

                return Instantiate(viewAsset);
            }

            public void RemoveView(CharacterViewComponent view)
            {
                Destroy(view);
            }
        }
    }
}