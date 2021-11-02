using UnityEngine;
using VoidLess.Game.Systems;

namespace VoidLess.Game.Setup.SetupSteps.GameSteps
{
    [CreateAssetMenu(fileName = "MoveSystemSetupStep.asset", menuName = "CUSTOM/Setups/MoveSystemSetupStep", order = (int)SetupOrder.MoveSystem)]
    public class MoveSystemSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            state.EventQueue.AddHandler(new MoveSystem());
        }

        protected override SetupOrder SetupOrder => SetupOrder.MoveSystem;
        
    }
}