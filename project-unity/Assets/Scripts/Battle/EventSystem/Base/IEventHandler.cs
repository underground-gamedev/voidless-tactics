namespace Battle.EventSystem
{
    public interface IEventHandler
    {
        HandleStatus Handle(IGlobalEvent globalEvent);
    }
}