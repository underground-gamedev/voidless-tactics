using System;
using Core.Components;
using JetBrains.Annotations;

namespace Battle
{
    public interface IEntity
    {

        public void AddComponent(IComponent com);
        void RemoveComponent(IComponent com);
        [CanBeNull] IComponent GetComponent(Type associatedType);

        void AddBehaviour(IBehaviour behaviour);
        void RemoveBehaviour(IBehaviour behaviour);
        void HandleEvent<T>(T personalEvent) where T : IPersonalEvent;

        void AssociateComponent(Type associatedType, Type comType);
        void RemoveAssociation(Type associatedType);
    }
}