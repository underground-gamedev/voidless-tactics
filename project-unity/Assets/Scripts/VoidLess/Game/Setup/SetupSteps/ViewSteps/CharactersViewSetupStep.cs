using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.CharacterViewComponent;
using VoidLess.Game.Map.Layers.CharacterMapLayer;
using VoidLess.Game.Systems;
using VoidLess.Game.Systems.CharacterViewDynamicBindSystem;

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
            
            state.EventQueue.AddHandler(new CharacterViewDynamicBindSystem(viewController));
            state.EventQueue.AddHandler(new CharacterViewRelocateSystem());

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
        
    }
}

