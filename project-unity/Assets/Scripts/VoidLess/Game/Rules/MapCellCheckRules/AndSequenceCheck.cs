using System.Collections.Generic;
using System.Linq;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.MapCellCheckRules
{
    public class AndSequenceCheck: IMapCellCheckRule
    {
        private List<IMapCellCheckRule> rules = new List<IMapCellCheckRule>();

        public AndSequenceCheck AddRule(IMapCellCheckRule rule)
        {
            rules.Add(rule);
            return this;
        }
        
        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            return rules.All(rule => rule.ApplyRule(map, checkedCell));
        }
    }
}