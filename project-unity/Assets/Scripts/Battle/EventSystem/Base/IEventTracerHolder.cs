namespace Battle.EventSystem
{
    public interface IEventTracerHolder
    {
        void AddTracer(IEventTracer tracer);
        void RemoveTracer(IEventTracer tracer);
    }
}