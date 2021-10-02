using System;
using System.Collections.Generic;

namespace Battle
{
    public interface ICharactersHolder
    {
        event Action<ICharacter> OnCharacterAdded;
        event Action<ICharacter> OnCharacterRemoved;

        IReadOnlyList<ICharacter> Characters { get; }

        void AddCharacter(ICharacter character);
        void RemoveCharacter(ICharacter character);
    }
}