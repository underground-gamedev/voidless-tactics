using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.MapCellCheckRules
{
    public interface IMapCellCheckRule
    {
        bool ApplyRule(ILayeredMap map, MapCell checkedCell);
    }
}