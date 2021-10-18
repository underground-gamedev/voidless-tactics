namespace Battle
{
    public interface IBehaviour
    {
        int HandlePriority { get; }
        void Handle(IPersonalEvent personalEvent);
    }

    public interface IBehaviour<in T>: IBehaviour where T : IPersonalEvent
    {
        void Handle(T personalEvent);
    }
}