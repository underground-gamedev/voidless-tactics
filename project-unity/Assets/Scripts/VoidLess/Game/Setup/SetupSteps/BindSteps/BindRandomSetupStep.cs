using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.RandomGeneratorComponent;
using VoidLess.Utils.Random;

namespace VoidLess.Game.Setup.SetupSteps.BindSteps
{
    [CreateAssetMenu(fileName = "BindRandomSetupStep.asset", menuName = "CUSTOM/Setups/BindRandomSetupStep", order = (int)SetupOrder.BindRandom)]
    public class BindRandomSetupStep : SerializableSetupStep
    {
        [SerializeField]
        private int seed = -1;
        
        public override void Setup(BattleState state)
        {
            var rand = seed == -1 ? new PCGRandom() : new PCGRandom(seed);
            
            state.Map.Map.AddWithAssociation<IRandomGeneratorComponent>(new ExternalRandomGenerator(rand));
            
            foreach (var character in state.Characters.Characters)
            {
                character.AddWithAssociation<IRandomGeneratorComponent>(new ExternalRandomGenerator(rand));
            }
            
            foreach (var team in state.Teams.Teams)
            {
                team.AddWithAssociation<IRandomGeneratorComponent>(new ExternalRandomGenerator(rand));
            }
        }

        protected override SetupOrder SetupOrder => SetupOrder.BindRandom;
    }
}