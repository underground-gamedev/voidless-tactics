namespace VoidLess.Game.EventSystem.Base
{
    public interface IEventTracerHolder
    {
        void AddTracer(IEventTracer tracer);
        void RemoveTracer(IEventTracer tracer);
    }
}