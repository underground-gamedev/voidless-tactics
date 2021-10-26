using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VoidLess.Game.Map.MapGenerators;

namespace VoidLess.Game.Setup.SetupSteps.LogicalSteps
{
    [CreateAssetMenu(fileName = "MapGeneratorSetup.asset", menuName = "CUSTOM/Setups/MapGeneratorSetupStep", order = (int)SetupOrder.MapGenerator)]
    public class MapGeneratorSetupStep: SerializableSetupStep
    {
        [OdinSerialize, Required]
        private List<IMapGeneratorStep> steps = new List<IMapGeneratorStep>();

        public override void Setup(BattleState state)
        {
            var map = state.Map.Map;
            
            foreach (var step in steps)
            {
                step.Generate(map);
            }
        }

        protected override SetupOrder SetupOrder => SetupOrder.MapGenerator;
    }
}