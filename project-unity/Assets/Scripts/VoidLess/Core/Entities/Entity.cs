using System;
using System.Collections.Generic;
using System.Linq;
using VoidLess.Core.Components;
using VoidLess.Core.Components.BehaviourComponent;
using VoidLess.Core.Components.StatComponent;

namespace VoidLess.Core.Entities
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
            coms.Attach<IStatComponent>(new StatComponent());
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
            TryCallOnAttachedToEntity(com);
        }
        
        public void RemoveComponent(IComponent com)
        {
            coms.DeAttach(com.GetType());
            TryCallOnDeAttachedFromEntity(com);
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

        public bool Correspond(IArchtype archtype)
        {
            return archtype.Components.All(comType => GetComponent(comType) != null);
        }

        public IComponent GetComponent(Type associatedType)
        {
            if (associations.TryGetValue(associatedType, out var comType))
            {
                return coms.Get(comType);
            }
            return coms.Get(associatedType);
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