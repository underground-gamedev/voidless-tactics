namespace VoidLess.Game.EventSystem.Base
{
    public interface IEventWatcherHolder
    {
        void AddWatcher(IEventWatcher watcher);
        void RemoveWatcher(IEventWatcher watcher);
    }
}