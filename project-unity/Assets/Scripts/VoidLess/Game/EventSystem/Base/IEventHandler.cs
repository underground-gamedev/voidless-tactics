using VoidLess.Game.EventSystem.GlobalEvents;

namespace VoidLess.Game.EventSystem.Base
{
    public interface IEventHandler
    {
        HandleStatus Handle(BattleState state, IGlobalEvent globalEvent);
    }
}