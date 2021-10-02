namespace Battle.EventSystem
{
    public interface IGlobalEventHandler
    {
        void Handle(IGlobalEvent globalEvent);
    }
}