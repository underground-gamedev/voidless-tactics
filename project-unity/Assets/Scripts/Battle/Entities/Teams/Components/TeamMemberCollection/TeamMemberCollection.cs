using System;
using System.Collections.Generic;

namespace Battle
{
    public class TeamMemberCollection : ITeamMemberCollection
    {
        public event Action<IEntity> OnCharacterAdded;
        public event Action<IEntity> OnCharacterRemoved;
        
        private List<IEntity> members = new List<IEntity>();
        
        public IReadOnlyList<IEntity> Members { get; }
        
        public void Add(IEntity character)
        {
            members.Add(character);
            OnCharacterAdded?.Invoke(character);
        }

        public void Remove(IEntity character)
        {
            if (members.Remove(character))
            {
                OnCharacterRemoved?.Invoke(character);
            }
        }
    }
}