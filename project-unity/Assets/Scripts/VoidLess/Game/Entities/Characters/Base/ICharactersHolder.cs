using System;
using System.Collections.Generic;
using VoidLess.Core.Entities;

namespace VoidLess.Game.Entities.Characters.Base
{
    public interface ICharactersHolder
    {
        event Action<IEntity> OnCharacterAdded;
        event Action<IEntity> OnCharacterRemoved;

        IReadOnlyList<IEntity> Characters { get; }

        void AddCharacter(IEntity character);
        void RemoveCharacter(IEntity character);
    }
}