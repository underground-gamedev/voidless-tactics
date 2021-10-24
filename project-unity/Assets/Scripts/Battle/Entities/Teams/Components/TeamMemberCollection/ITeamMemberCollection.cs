using System;
using System.Collections.Generic;
using Core.Components;

namespace Battle
{
    public interface ITeamMemberCollection: IComponent
    {
        event Action<IEntity> OnCharacterAdded;
        event Action<IEntity> OnCharacterRemoved;
        
        IReadOnlyList<IEntity> Members { get; }
        
        void Add(IEntity character);
        void Remove(IEntity character);
    }
}