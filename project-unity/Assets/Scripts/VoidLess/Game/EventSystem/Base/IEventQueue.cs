namespace VoidLess.Game.EventSystem.Base
{
    public interface IEventQueue : IEventHandlerHolder, IEventWatcherHolder, IEventTracerHolder, IBlockable, IGlobalEventHandler
    {

    }
}