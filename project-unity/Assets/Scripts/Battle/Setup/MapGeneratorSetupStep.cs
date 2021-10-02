using System.Collections.Generic;
using Battle.MapGenerators;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "MapGeneratorSetup.asset", menuName = "CUSTOM/Setups/MapGeneratorSetupStep", order = 2)]
    public class MapGeneratorSetupStep: SerializedScriptableObject, IBattleStateSetupStep
    {
        [OdinSerialize, Required]
        private List<IMapGeneratorStep> steps = new List<IMapGeneratorStep>();
        
        public void Setup(BattleState state)
        {
            var map = state.Map.Map;
            
            foreach (var step in steps)
            {
                step.Generate(map);
            }
        }
    }
}