namespace VoidLess.Core.Components.BehaviourComponent
{
    public interface IBehaviour
    {
        int HandlePriority { get; }
    }

    public interface IBehaviour<in T>: IBehaviour where T : IPersonalEvent
    {
        void Handle(T personalEvent);
    }
}