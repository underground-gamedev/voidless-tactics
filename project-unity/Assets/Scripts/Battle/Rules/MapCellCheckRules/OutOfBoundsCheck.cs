using Battle.Map.Extensions;
using Battle.Map.Interfaces;

namespace Battle.Rules.MapCellCheckRules
{
    public class OutOfBoundsCheck: IMapCellCheckRule
    {
        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            return map.IsOutOfBounds(checkedCell);
        }
    }
}