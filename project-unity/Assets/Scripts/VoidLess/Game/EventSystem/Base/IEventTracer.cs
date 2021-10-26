using VoidLess.Game.EventSystem.GlobalEvents;

namespace VoidLess.Game.EventSystem.Base
{
    public interface IEventTracer
    {
        void TraceBefore(IEventHandler handler, IGlobalEvent globalEvent);
        void TraceAfter(IEventHandler handler, IGlobalEvent globalEvent, HandleStatus status);
        
        void TraceBefore(IEventWatcher watcher, IGlobalEvent globalEvent);
        void TraceAfter(IEventWatcher watcher, IGlobalEvent globalEvent);

        void TraceBeforeAllHandlers(IGlobalEvent globalEvent);
        void TraceAfterAllHandlers(IGlobalEvent globalEvent);
        void TraceAfterAllWatchers(IGlobalEvent globalEvent);
    }
}