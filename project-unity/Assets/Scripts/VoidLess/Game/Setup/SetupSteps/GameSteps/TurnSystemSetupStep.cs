using UnityEngine;

namespace VoidLess.Game.Setup.SetupSteps.GameSteps
{
    [CreateAssetMenu(fileName = "TurnSystemSetupStep.asset", menuName = "CUSTOM/Setups/TurnSystemSetupStep", order = (int)SetupOrder.TurnSystem)]
    public class TurnSystemSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            state.EventQueue.AddHandler(new TurnSystem());
        }

        protected override SetupOrder SetupOrder => SetupOrder.TurnSystem;

    }
}