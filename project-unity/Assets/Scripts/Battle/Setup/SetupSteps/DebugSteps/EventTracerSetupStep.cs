using Battle.EventSystem;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "EventTracerSetupStep.asset", menuName = "CUSTOM/Setups/EventTracerSetupStep", order = (int)SetupOrder.EventTracer)]
    public class EventTracerSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            state.EventQueue.AddTracer(new DebugLogEventTracer());
        }

        protected override SetupOrder SetupOrder => SetupOrder.EventTracer;
        
        private class DebugLogEventTracer : IEventTracer
        {
            public void TraceBefore(IEventHandler handler, IGlobalEvent globalEvent)
            {
            }

            public void TraceAfter(IEventHandler handler, IGlobalEvent globalEvent, HandleStatus status)
            {
            }

            public void TraceBefore(IEventWatcher watcher, IGlobalEvent globalEvent)
            {
            }

            public void TraceAfter(IEventWatcher watcher, IGlobalEvent globalEvent)
            {
            }

            public void TraceBeforeAllHandlers(IGlobalEvent globalEvent)
            {
                Debug.Log(
                    $"EventTrace::{nameof(TraceBeforeAllHandlers)} >> handle event {globalEvent.GetType().Name}\n" +
                           $"To string {globalEvent}");
            }

            public void TraceAfterAllHandlers(IGlobalEvent globalEvent)
            {
            }

            public void TraceAfterAllWatchers(IGlobalEvent globalEvent)
            {
            }
        }
    }
}