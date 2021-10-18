using System;
using Core.Components;

namespace Battle
{
    public interface IBehaviourComponent: IComponent
    {
        void Add(IBehaviour behaviour);
        void Remove(IBehaviour behaviour);
        
        void Handle<T>(T personalEvent) where T : IPersonalEvent;
        void DelayedHandle(IPersonalEvent personalEvent);
        bool RespondTo(Type eventType);
    }
}