using System;
using System.Collections.Generic;

namespace Battle
{
    public class TeamMemberCollection : ITeamMemberCollection
    {
        public event Action<ICharacter> OnCharacterAdded;
        public event Action<ICharacter> OnCharacterRemoved;
        
        private List<ICharacter> members = new List<ICharacter>();
        
        public IReadOnlyList<ICharacter> Members { get; }
        
        public void Add(ICharacter character)
        {
            members.Add(character);
            OnCharacterAdded?.Invoke(character);
        }

        public void Remove(ICharacter character)
        {
            if (members.Remove(character))
            {
                OnCharacterRemoved?.Invoke(character);
            }
        }
    }
}