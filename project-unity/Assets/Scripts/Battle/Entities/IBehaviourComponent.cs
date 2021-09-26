using System;
using Core.Components;

namespace Battle
{
    public interface IBehaviourComponent: IComponent
    {
        void Add(IBehaviour behaviour);
        void Del(IBehaviour behaviour);
        
        void Handle(IGameEvent gameEvent);
        bool RespondTo(Type eventType);
    }
}