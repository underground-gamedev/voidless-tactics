using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "BattleSetup.asset", menuName = "CUSTOM/Setups/BattleSetup", order = 0)]
    public class BattleSetup : SerializedScriptableObject, IBattleStateSetup
    {
        [DetailedInfoBox("Basic Info", 
            "This object describes how to initialize the battle scene. To do this, separate objects of \"steps\" are used that make small changes in the game state.\n\n" +
            "Step objects are automatically sorted by internal priority. There is no need to do the ordering manually.\n" +
            "The order is defined as follows: \n" +
            "* Logical objects are initialized without connections between each other\n" +
            "* Objects are connected to each other\n" +
            "* Game objects are visualized and input sources are created \n" +
            "* Initialized UI \n" +
            "* Controllers (AI, Human control logic, etc.) are added and associated with individual objects and input sources \n\n" +
            "Individual steps can be disabled without removing them from the general list. This is done for the convenience of debugging.")]
        
        [OdinSerialize, Required, InlineEditor,
         OnValueChanged(nameof(OrderSteps), includeChildren: true), ListDrawerSettings(DraggableItems = false)]
        private SerializableSetupStep[] steps = Array.Empty<SerializableSetupStep>();

        public BattleState Setup()
        {
            var state = new BattleState();
            OrderSteps();
            foreach (var step in steps.Where(step => step != null && step.Enabled))
            {
                step.Setup(state);
            }

            return state;
        }

        private void OrderSteps()
        {
            steps = steps.OrderBy(step => step?.Order ?? -1).ToArray();
        }
    }
}