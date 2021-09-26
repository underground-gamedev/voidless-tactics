using System.Collections.Generic;
using System.Linq;
using Battle.Map.Interfaces;

namespace Battle.Rules.MapCellCheckRules
{
    public class OrSequenceCheck: IMapCellCheckRule
    {
        private List<IMapCellCheckRule> rules = new List<IMapCellCheckRule>();

        public OrSequenceCheck AddRule(IMapCellCheckRule rule)
        {
            rules.Add(rule);
            return this;
        }
        
        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            return rules.Any(rule => rule.ApplyRule(map, checkedCell));
        }
    }
}