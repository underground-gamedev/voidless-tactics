using System;
using System.Collections.Generic;

namespace Battle
{
    public class CharactersHolder: ICharactersHolder
    {
        public event Action<IEntity> OnCharacterAdded;
        public event Action<IEntity> OnCharacterRemoved;

        private List<IEntity> characters = new List<IEntity>();
        public IReadOnlyList<IEntity> Characters => characters;
        
        public void AddCharacter(IEntity character)
        {
            character.Correspond(Archtype.Character);
            
            characters.Add(character);
            OnCharacterAdded?.Invoke(character);
        }

        public void RemoveCharacter(IEntity character)
        {
            if (characters.Remove(character))
            {
                OnCharacterRemoved?.Invoke(character);
            }
        }
    }
}