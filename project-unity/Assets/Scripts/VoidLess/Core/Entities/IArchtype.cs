using System;
using System.Collections.Generic;

namespace VoidLess.Core.Entities
{
    public interface IArchtype
    {
        IReadOnlyList<Type> Components { get; }
    }
}