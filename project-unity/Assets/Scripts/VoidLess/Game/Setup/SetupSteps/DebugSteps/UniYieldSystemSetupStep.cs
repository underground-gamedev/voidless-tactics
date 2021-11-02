using UnityEngine;

namespace VoidLess.Game.Setup.SetupSteps.DebugSteps
{
    [CreateAssetMenu(fileName = "YieldSetupStep.asset", menuName = "CUSTOM/Setups/YieldSetupStep", order = (int)SetupOrder.Yield)]
    public class UniYieldSystemSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            state.EventQueue.AddHandler(new UniYieldSystem());
        }

        protected override SetupOrder SetupOrder => SetupOrder.Yield;

    }
}