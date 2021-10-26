namespace VoidLess.Game.EventSystem.Base
{
    public interface IEventHandlerHolder
    {
        void AddHandler(IEventHandler handler);
        void RemoveHandler(IEventHandler handler);
    }
}