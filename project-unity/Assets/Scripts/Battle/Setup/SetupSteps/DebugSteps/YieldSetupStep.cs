using Battle.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "YieldSetupStep.asset", menuName = "CUSTOM/Setups/YieldSetupStep", order = (int)SetupOrder.Yield)]
    public class YieldSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            state.EventQueue.AddHandler(new YieldHandler());
        }

        protected override SetupOrder SetupOrder => SetupOrder.Yield;

        private class YieldHandler : IEventHandler
        {
            public HandleStatus Handle(BattleState state, IGlobalEvent globalEvent)
            {
                return globalEvent switch
                {
                    YieldUtilityEvent yieldEvent => Handle(state, yieldEvent),
                    _ => HandleStatus.Skipped,
                };
            }

            private HandleStatus Handle(BattleState state, YieldUtilityEvent yieldEvent)
            {
                state.EventQueue.Block(this);
                DOTween.Sequence()
                    .AppendInterval(0.01f)
                    .AppendCallback(() =>
                    {
                        state.EventQueue.Unblock(this);
                    });
                return HandleStatus.Handled;
            }
        }
    }
}