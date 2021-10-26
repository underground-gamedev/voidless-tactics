using System.Collections.Generic;
using System.Linq;
using VoidLess.Game.EventSystem.GlobalEvents;

namespace VoidLess.Game.EventSystem.Base
{
    public class EventQueue : IEventQueue
    {
        private List<IEventHandler> handlers = new List<IEventHandler>();
        private List<IEventWatcher> watchers = new List<IEventWatcher>();
        private List<IEventTracer> tracers = new List<IEventTracer>();

        private Queue<IGlobalEvent> delayedEvents = new Queue<IGlobalEvent>();
        
        private List<object> blockers = new List<object>();

        private BattleState state;
        public bool IsBlocked => blockers.Any();

        public EventQueue(BattleState state)
        {
            this.state = state;
        }

        public void AddWatcher(IEventWatcher watcher)
        {
            watchers.Add(watcher);
        }

        public void RemoveWatcher(IEventWatcher watcher)
        {
            watchers.Remove(watcher);
        }
        
        public void AddHandler(IEventHandler handler)
        {
            handlers.Add(handler);
        }

        public void RemoveHandler(IEventHandler handler)
        {
            handlers.Remove(handler);
        }

        public void AddTracer(IEventTracer tracer)
        {
            tracers.Add(tracer);
        }

        public void RemoveTracer(IEventTracer tracer)
        {
            tracers.Remove(tracer);
        }
        
        public void Block(object blocker)
        {
            blockers.Add(blocker);
        }

        public void Unblock(object blocker)
        {
            blockers.Remove(blocker);

            if (!IsBlocked && delayedEvents.Count > 0)
            {
                ContinueHandle();
            }
        }

        public void Handle(IGlobalEvent globalEvent)
        {
            delayedEvents.Enqueue(globalEvent);
            
            if (delayedEvents.Count > 1)
            {
                return;
            }

            if (IsBlocked)
            {
                return;
            }

            ContinueHandle();
        }

        private void ContinueHandle()
        {
            while (delayedEvents.Count > 0)
            {
                var currentEvent = delayedEvents.Peek();
                HandleSingle(currentEvent);
                delayedEvents.Dequeue();

                if (IsBlocked) return;
            }
        }

        private void HandleSingle(IGlobalEvent globalEvent)
        {
            var resultStatus = HandleStatus.Skipped;
            
            tracers.ForEach(tracer => tracer.TraceBeforeAllHandlers(globalEvent));

            foreach (var handler in handlers)
            {
                tracers.ForEach(tracer => tracer.TraceBefore(handler, globalEvent));
                var handleStatus = handler.Handle(state, globalEvent);
                tracers.ForEach(tracer => tracer.TraceAfter(handler, globalEvent, handleStatus));

                if (handleStatus == HandleStatus.Skipped) continue;
                resultStatus = handleStatus;
                if (handleStatus == HandleStatus.Consumed) break;
            }
            
            tracers.ForEach(tracer => tracer.TraceAfterAllHandlers(globalEvent));
            
            foreach (var watcher in watchers)
            {
                watcher.Watch(globalEvent, resultStatus);
            }
            
            tracers.ForEach(tracer => tracer.TraceAfterAllWatchers(globalEvent));
        }
    }
}