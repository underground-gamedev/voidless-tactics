using DG.Tweening;
using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.EventSystem.GlobalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.UtilityEvents;

namespace VoidLess.Game.Systems
{
    public class UniYieldSystem : IEventHandler
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