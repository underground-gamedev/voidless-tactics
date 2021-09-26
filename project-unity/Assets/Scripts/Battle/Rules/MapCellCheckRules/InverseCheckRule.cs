using Battle.Map.Interfaces;

namespace Battle.Rules.MapCellCheckRules
{
    public class InverseCheckRule: IMapCellCheckRule
    {
        private IMapCellCheckRule rule;
        
        public InverseCheckRule(IMapCellCheckRule rule)
        {
            this.rule = rule;
        }

        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            return !rule.ApplyRule(map, checkedCell);
        }
    }
}