using System.Linq;
using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.SpawnPositionComponent;
using VoidLess.Game.Map.Layers.CharacterMapLayer;

namespace VoidLess.Game.Setup.SetupSteps.BindSteps
{
    [CreateAssetMenu(fileName = "BindMapWithCharactersSetupStep.asset", menuName = "CUSTOM/Setups/BindMapWithCharactersSetupStep", order = (int)SetupOrder.BindMapWithCharacters)]
    public class BindMapWithCharactersSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            var map = state.Map.Map;
            var characters = state.Characters.Characters;

            var characterLayer = map.GetLayer<ICharacterMapLayer>();
            var charactersWithSpawnPosition =
                characters.Where(character => character.HasComponent<SpawnPositionComponent>());
            
            foreach (var character in charactersWithSpawnPosition)
            {
                var spawnPosCom = character.GetComponent<SpawnPositionComponent>();
                characterLayer.AddCharacter(character, spawnPosCom.SpawnPosition);
            }
        }

        protected override SetupOrder SetupOrder => SetupOrder.BindMapWithCharacters;
    }
}