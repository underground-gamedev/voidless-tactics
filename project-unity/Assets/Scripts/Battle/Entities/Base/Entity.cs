using System;
using Core.Components;

namespace Battle
{
    public class Entity: IEntity
    {
        private readonly ComponentContainer<IComponent> coms;
        private readonly IBehaviourComponent behaviourComponent;
        private Action<IComponent> onComponentAttachedHandler;
        private Action<IComponent> onComponentDeAttachedHandler;

        public Entity()
        {
            coms = new ComponentContainer<IComponent>(OnComponentAttached, OnComponentDeAttached);
            behaviourComponent = new BehaviourComponent();
            
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

        public void AddComponent(Type associatedType, IComponent com)
        {
            coms.Attach(associatedType, com);
            OnComponentAssociated(associatedType, com);
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

        public void AddBehaviour<T>(IBehaviour<T> behaviour) where T : IPersonalEvent
        {
            behaviourComponent.Add(behaviour);
        }

        public void HandleEvent<T>(T personalEvent) where T : IPersonalEvent {
            behaviourComponent.Handle(personalEvent);
        }

        public IComponent GetComponent(Type associatedType)
        {
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