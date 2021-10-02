namespace Battle.EventSystem
{
    public interface IEventTracer
    {
        void TraceBefore(IEventHandler handler, IGlobalEvent globalEvent);
        void TraceAfter(IEventHandler handler, IGlobalEvent globalEvent, HandleStatus status);
    }
}