using System;

namespace Battle
{
    public interface IBehaviour
    {
        int HandlePriority { get; }
        void Handle(IPersonalEvent personalEvent);
    }
}