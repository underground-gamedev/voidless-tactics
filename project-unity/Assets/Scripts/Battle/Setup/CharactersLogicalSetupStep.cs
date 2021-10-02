using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "CharactersLogicalSetupStep.asset", menuName = "CUSTOM/Setups/CharactersLogicalSetupStep", order = 4)]
    public class CharactersLogicalSetupStep : SerializedScriptableObject, IBattleStateSetupStep
    {
        [OdinSerialize, Required]
        private List<CharacterTemplate> templates = new List<CharacterTemplate>();
        public void Setup(BattleState state)
        {
            foreach (var template in templates)
            {
                state.Characters.AddCharacter(template.Generate());
            }
        }
    }
}