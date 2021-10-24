using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Battle
{
    public interface ITimeLine
    {
        event Action OnOrderChanged;
        event Action<IEntity> OnActiveChanged;

        IReadOnlyList<IEntity> Order { get; }
        [CanBeNull] IEntity Active { get; }

        void Set([NotNull] IEntity character, float priority);
        void Remove([NotNull] IEntity character);
    }
}