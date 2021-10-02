namespace Battle.EventSystem
{
    public interface IEventHandlerHolder
    {
        void AddHandler(IEventHandler handler);
        void RemoveHandler(IEventHandler handler);
    }
}