using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "BattleSetup.asset", menuName = "CUSTOM/Setups/BattleSetup", order = 0)]
    public class BattleSetup : SerializedScriptableObject, IBattleStateSetup
    {
        [OdinSerialize, Required]
        private List<IBattleStateSetupStep> steps = new List<IBattleStateSetupStep>();

        public BattleState Setup()
        {
            var state = new BattleState();
            foreach (var step in steps)
            {
                step.Setup(state);
            }

            return state;
        }
    }
}