using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Battle
{
    public interface ITimeLine
    {
        event Action OnOrderChanged;
        event Action<ICharacter> OnActiveChanged;

        IReadOnlyList<ICharacter> Order { get; }
        [CanBeNull] ICharacter Active { get; }

        void Set([NotNull] ICharacter character, float priority);
        void Remove([NotNull] ICharacter character);
    }
}