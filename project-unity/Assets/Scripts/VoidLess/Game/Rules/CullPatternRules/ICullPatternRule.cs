using System.Collections.Generic;
using Sirenix.Utilities;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.CullPatternRules
{
    public interface ICullPatternRule
    {
        IEnumerable<MapCell> ApplyRule(ILayeredMap map, ImmutableHashSet<MapCell> source);
    }
}