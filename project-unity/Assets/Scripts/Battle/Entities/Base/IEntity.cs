using System;
using Core.Components;
using JetBrains.Annotations;

namespace Battle
{
    public interface IEntity
    {
        void AddComponent(Type associatedType, IComponent com);
        void RemoveComponent(Type associatedType);
        [CanBeNull] IComponent GetComponent(Type associatedType);
    }
}