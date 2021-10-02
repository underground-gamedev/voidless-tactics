namespace Battle.EventSystem
{
    public interface IEventWatcherHolder
    {
        void AddWatcher(IEventWatcher watcher);
        void RemoveWatcher(IEventWatcher watcher);
    }
}