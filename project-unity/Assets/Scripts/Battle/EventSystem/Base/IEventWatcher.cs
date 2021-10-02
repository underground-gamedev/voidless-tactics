namespace Battle.EventSystem
{
    public interface IEventWatcher
    {
        void Watch(IGlobalEvent globalEvent, HandleStatus handleStatus);
    }
}