using System;

namespace Battle
{
    public interface IBehaviour
    {
        int HandlePriority { get; }
        void Handle(IGameEvent gameEvent);
        bool RespondTo(Type eventType);
    }
}