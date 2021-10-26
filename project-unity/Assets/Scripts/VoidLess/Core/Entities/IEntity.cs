using System;
using JetBrains.Annotations;
using VoidLess.Core.Components;
using VoidLess.Core.Components.BehaviourComponent;

namespace VoidLess.Core.Entities
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

        bool Correspond(IArchtype archtype);
    }
}