using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Layers.SolidMapLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.CullPatternRules
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