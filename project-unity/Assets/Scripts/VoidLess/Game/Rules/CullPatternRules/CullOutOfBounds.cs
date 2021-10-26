using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.CullPatternRules
{
    public class CullOutOfBounds: ICullPatternRule
    {
        public IEnumerable<MapCell> ApplyRule(ILayeredMap map, ImmutableHashSet<MapCell> source)
        {
            return source.Where(cell => !map.IsOutOfBounds(cell.X, cell.Y)).ToHashSet();
        }
    }
}