using System.Collections.Generic;
using Battle.Components.ViewComponent;
using Battle.Components.ViewTagComponent;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "CharactersViewSetupStep.asset", menuName = "CUSTOM/Setups/CharactersViewSetupStep", order = (int)SetupOrder.CharactersView)]
    public class CharactersViewSetupStep : SerializableSetupStep
    {
        [OdinSerialize, Required]
        private Dictionary<CharacterViewTag, CharacterViewComponent> bindings;
        
        public override void Setup(BattleState state)
        {
            var characters = state.Characters.Characters;
            
            foreach (var character in characters)
            {
                var viewTagCom = character.GetComponent<ViewTagComponent>();
                if (viewTagCom == null) continue;
                var viewTag = viewTagCom.ViewTag;

                if (!bindings.TryGetValue(viewTag, out var viewAsset))
                {
                    Debug.LogError($"{nameof(CharactersViewSetupStep)}:: Invalid configuration. Not found view component for {viewTag}");
                    continue;
                }

                var view = Instantiate(viewAsset);
                character.AddComponent<CharacterViewComponent>(view);
            }
        }

        protected override SetupOrder SetupOrder => SetupOrder.CharactersView;
    }
}