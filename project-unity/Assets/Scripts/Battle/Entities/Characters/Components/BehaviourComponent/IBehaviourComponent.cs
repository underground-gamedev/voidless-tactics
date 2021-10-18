using System;
using Core.Components;

namespace Battle
{
    public interface IBehaviourComponent: IComponent
    {
        void Add(IBehaviour behaviour);
        void Remove(IBehaviour behaviour);
        
        void Handle<T>(T personalEvent) where T : IPersonalEvent;
        void HandleNow<T>(T personalEvent) where T : IPersonalEvent;
    }
}