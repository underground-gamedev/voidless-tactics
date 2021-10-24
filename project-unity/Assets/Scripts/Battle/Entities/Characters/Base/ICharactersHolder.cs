using System;
using System.Collections.Generic;

namespace Battle
{
    public interface IEntitysHolder
    {
        event Action<IEntity> OnCharacterAdded;
        event Action<IEntity> OnCharacterRemoved;

        IReadOnlyList<IEntity> Characters { get; }

        void AddCharacter(IEntity character);
        void RemoveCharacter(IEntity character);
    }
}