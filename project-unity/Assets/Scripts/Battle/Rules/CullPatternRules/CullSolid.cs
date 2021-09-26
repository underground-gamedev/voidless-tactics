using System.Collections.Generic;
using System.Linq;
using Battle.Map.Interfaces;
using Battle.Rules.CullPatternRules;
using Sirenix.Utilities;

namespace Battle.Rules.MapPatternRules
{
    public class CullSolid: ICullPatternRule
    {
        public IEnumerable<MapCell> ApplyRule(ILayeredMap map, ImmutableHashSet<MapCell> source)
        {
            var solidLayer = map.GetLayer<ISolidMapLayer>();
            return source.Where(cell => !solidLayer.IsSolid(cell));
        }
    }
}