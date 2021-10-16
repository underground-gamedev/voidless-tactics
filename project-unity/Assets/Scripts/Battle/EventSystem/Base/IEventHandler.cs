namespace Battle.EventSystem
{
    public interface IEventHandler
    {
        HandleStatus Handle(BattleState state, IGlobalEvent globalEvent);
    }
}