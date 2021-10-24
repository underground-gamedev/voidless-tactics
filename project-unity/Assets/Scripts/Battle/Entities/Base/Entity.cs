using System;
using System.Collections.Generic;
using Core.Components;

namespace Battle
{
    public class Entity: IEntity
    {
        private readonly ComponentContainer<IComponent> coms;
        private readonly Dictionary<Type, Type> associations;
        
        private Action<IComponent> onComponentAttachedHandler;
        private Action<IComponent> onComponentDeAttachedHandler;

        public Entity()
        {
            coms = new ComponentContainer<IComponent>(OnComponentAttached, OnComponentDeAttached);
            associations = new Dictionary<Type, Type>();
            coms.Attach<IBehaviourComponent>(new BehaviourComponent());
            
            OnNewComponentAttached(TryCallOnAttachedToEntity);
            OnComponentCompleteDeAttached(TryCallOnDeAttachedFromEntity);
        }

        private void OnComponentAttached(IComponent com)
        {
            onComponentAttachedHandler?.Invoke(com);
        }
        
        private void OnComponentDeAttached(IComponent com)
        {
            onComponentDeAttachedHandler?.Invoke(com);
        }

        public void AddComponent(IComponent com)
        {
            coms.Attach(com.GetType(), com);
            OnComponentAssociated(com.GetType(), com);
        }

        public void RemoveComponent(Type associatedType)
        {
            var com = coms.Get(associatedType);
            if (com != null)
            {
                coms.DeAttach(associatedType);
                OnComponentUnAssociated(associatedType, com);
            }
        }
        
        public void RemoveComponent(IComponent com)
        {
            coms.DeAttach(com.GetType());
        }

        public void AssociateComponent(Type associatedType, Type comType)
        {
            associations.Add(associatedType, comType);
        }

        public void RemoveAssociation(Type associatedType)
        {
            associations.Remove(associatedType);
        }

        public void AddBehaviour(IBehaviour behaviour)
        {
            var behaviourComponent = coms.Get<IBehaviourComponent>();
            behaviourComponent.Add(behaviour);
        }

        public void RemoveBehaviour(IBehaviour behaviour)
        {
            var behaviourComponent = coms.Get<IBehaviourComponent>();
            behaviourComponent.Remove(behaviour);
        }

        public void HandleEvent<T>(T personalEvent) where T : IPersonalEvent {
            var behaviourComponent = coms.Get<IBehaviourComponent>();
            behaviourComponent.Handle(personalEvent);
        }

        public IComponent GetComponent(Type associatedType)
        {
            if (associations.TryGetValue(associatedType, out var comType))
            {
                return coms.Get(comType);
            }
            return coms.Get(associatedType);
        }

        protected virtual void OnComponentUnAssociated(Type associatedType, IComponent com)
        {
        }

        protected virtual void OnComponentAssociated(Type associatedType, IComponent com)
        {
        }

        protected void OnNewComponentAttached(params Action<IComponent>[] handlers)
        {
            foreach (var handler in handlers)
            {
                onComponentAttachedHandler += handler;
            }
        }

        protected void OnComponentCompleteDeAttached(params Action<IComponent>[] handlers)
        {
            foreach (var handler in handlers)
            {
                onComponentDeAttachedHandler += handler;
            }
        }

        private void TryCallOnAttachedToEntity(IComponent com)
        {
            if (com is IEntityAttachable ea)
            {
                ea.OnAttached(this);
            }
        }

        private void TryCallOnDeAttachedFromEntity(IComponent com)
        {
            if (com is IEntityAttachable ea)
            {
                ea.OnDeAttached();
            }
        }
    }
}