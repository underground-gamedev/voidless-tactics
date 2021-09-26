using System.Collections.Generic;
using Core.Components;

namespace Battle
{
    public interface ITeamMembers: IComponent
    {
        IReadOnlyList<ICharacter> Members { get; }
        
        void Add(ICharacter character);
        void Remove(ICharacter character);
    }
}