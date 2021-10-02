using System;
using System.Collections.Generic;

namespace Battle
{
    public class CharactersHolder: ICharactersHolder
    {
        public event Action<ICharacter> OnCharacterAdded;
        public event Action<ICharacter> OnCharacterRemoved;

        private List<ICharacter> characters = new List<ICharacter>();
        public IReadOnlyList<ICharacter> Characters => characters;
        
        public void AddCharacter(ICharacter character)
        {
            characters.Add(character);
            OnCharacterAdded?.Invoke(character);
        }

        public void RemoveCharacter(ICharacter character)
        {
            if (characters.Remove(character))
            {
                OnCharacterRemoved?.Invoke(character);
            }
        }
    }
}