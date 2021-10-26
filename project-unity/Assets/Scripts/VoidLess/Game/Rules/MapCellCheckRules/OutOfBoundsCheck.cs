using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Extensions;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.MapCellCheckRules
{
    public class OutOfBoundsCheck: IMapCellCheckRule
    {
        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            return map.IsOutOfBounds(checkedCell);
        }
    }
}