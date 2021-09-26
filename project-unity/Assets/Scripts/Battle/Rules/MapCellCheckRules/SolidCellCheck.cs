using Battle.Map.Interfaces;

namespace Battle.Rules.MapCellCheckRules
{
    public class SolidCellCheck: IMapCellCheckRule
    {
        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            return map.GetLayer<ISolidMapLayer>().IsSolid(checkedCell);
        }
    }
}