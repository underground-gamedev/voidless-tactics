using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Layers.SolidMapLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.MapCellCheckRules
{
    public class SolidCellCheck: IMapCellCheckRule
    {
        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            return map.GetLayer<ISolidMapLayer>().IsSolid(checkedCell);
        }
    }
}