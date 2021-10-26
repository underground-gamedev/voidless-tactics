using VoidLess.Game.EventSystem.GlobalEvents;

namespace VoidLess.Game.EventSystem.Base
{
    public interface IGlobalEventHandler
    {
        void Handle(IGlobalEvent globalEvent);
    }
}