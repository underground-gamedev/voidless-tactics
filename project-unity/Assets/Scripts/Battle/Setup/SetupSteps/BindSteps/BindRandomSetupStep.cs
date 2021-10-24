using Battle.Components.RandomGeneratorComponent;
using Battle.Systems.RandomSystem;
using UnityEngine;

namespace Battle
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