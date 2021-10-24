using System;
using System.Collections.Generic;

namespace Battle
{
    public interface IArchtype
    {
        IReadOnlyList<Type> Components { get; }
    }
}