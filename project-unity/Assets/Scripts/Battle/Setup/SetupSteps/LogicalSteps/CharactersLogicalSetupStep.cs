using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "CharactersLogicalSetupStep.asset", menuName = "CUSTOM/Setups/CharactersLogicalSetupStep", order = (int)SetupOrder.CharactersLogical)]
    public class CharactersLogicalSetupStep : SerializableSetupStep
    {
        [OdinSerialize, Required]
        private List<CharacterTemplate> templates = new List<CharacterTemplate>();

        public override void Setup(BattleState state)
        {
            foreach (var template in templates)
            {
                var character = template.Generate();
                character.AddWithAssociation<IGlobalEventEmitter>(new GlobalEventEmitter(state.EventQueue));
                state.Characters.AddCharacter(character);
            }
        }

        protected override SetupOrder SetupOrder => SetupOrder.CharactersLogical;
    }
}