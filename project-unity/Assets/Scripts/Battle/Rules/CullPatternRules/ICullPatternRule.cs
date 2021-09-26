using System.Collections.Generic;
using Battle.Map.Interfaces;
using Sirenix.Utilities;

namespace Battle.Rules.CullPatternRules
{
    public interface ICullPatternRule
    {
        IEnumerable<MapCell> ApplyRule(ILayeredMap map, ImmutableHashSet<MapCell> source);
    }
}