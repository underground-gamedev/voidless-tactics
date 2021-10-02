namespace Battle.EventSystem
{
    public interface IEventQueue : IEventHandlerHolder, IEventWatcherHolder, IEventTracerHolder, IBlockable, IGlobalEventHandler
    {

    }
}