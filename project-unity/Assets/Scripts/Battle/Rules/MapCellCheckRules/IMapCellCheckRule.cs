using Battle.Map.Interfaces;

namespace Battle.Rules.MapCellCheckRules
{
    public interface IMapCellCheckRule
    {
        bool ApplyRule(ILayeredMap map, MapCell checkedCell);
    }
}