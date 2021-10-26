using System;
using System.Collections.Generic;
using VoidLess.Core.Entities;

namespace VoidLess.Game.Entities.Characters.Base
{
    public class CharactersHolder: ICharactersHolder
    {
        public event Action<IEntity> OnCharacterAdded;
        public event Action<IEntity> OnCharacterRemoved;

        private List<IEntity> characters = new List<IEntity>();
        public IReadOnlyList<IEntity> Characters => characters;
        
        public void AddCharacter(IEntity character)
        {
            character.ShouldCorrespond(Archtype.Character);
            
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