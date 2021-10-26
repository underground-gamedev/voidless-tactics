using VoidLess.Game.EventSystem.GlobalEvents;

namespace VoidLess.Game.EventSystem.Base
{
    public interface IEventWatcher
    {
        void Watch(IGlobalEvent globalEvent, HandleStatus handleStatus);
    }
}