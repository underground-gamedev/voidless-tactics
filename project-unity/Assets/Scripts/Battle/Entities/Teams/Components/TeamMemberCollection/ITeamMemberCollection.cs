using System;
using System.Collections.Generic;
using Core.Components;

namespace Battle
{
    public interface ITeamMemberCollection: IComponent
    {
        event Action<ICharacter> OnCharacterAdded;
        event Action<ICharacter> OnCharacterRemoved;
        
        IReadOnlyList<ICharacter> Members { get; }
        
        void Add(ICharacter character);
        void Remove(ICharacter character);
    }
}