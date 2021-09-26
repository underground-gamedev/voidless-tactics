using System.Collections.Generic;
using System.Linq;
using Battle.Map.Extensions;
using Battle.Map.Interfaces;
using Battle.Rules.CullPatternRules;
using Sirenix.Utilities;

namespace Battle.Rules.MapPatternRules
{
    public class CullOutOfBounds: ICullPatternRule
    {
        public IEnumerable<MapCell> ApplyRule(ILayeredMap map, ImmutableHashSet<MapCell> source)
        {
            return source.Where(cell => !map.IsOutOfBounds(cell.X, cell.Y)).ToHashSet();
        }
    }
}