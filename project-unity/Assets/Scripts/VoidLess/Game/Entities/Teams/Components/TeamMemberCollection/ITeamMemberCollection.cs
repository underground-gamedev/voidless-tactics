using System;
using System.Collections.Generic;
using VoidLess.Core.Components;
using VoidLess.Core.Entities;

namespace VoidLess.Game.Entities.Teams.Components.TeamMemberCollection
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