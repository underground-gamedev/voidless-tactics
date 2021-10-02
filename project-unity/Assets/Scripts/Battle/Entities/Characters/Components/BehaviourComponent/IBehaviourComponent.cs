using System;
using Core.Components;

namespace Battle
{
    public interface IBehaviourComponent: IComponent
    {
        void Add(IBehaviour behaviour);
        void Remove(IBehaviour behaviour);
        
        void Handle(IPersonalEvent personalEvent);
        void DelayedHandle(IPersonalEvent personalEvent);
        bool RespondTo(Type eventType);
    }
}