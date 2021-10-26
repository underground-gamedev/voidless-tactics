using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.MapCellCheckRules
{
    public class MapCellCheckMeta
    {
        public readonly ILayeredMap Map;
        public readonly MapCell CheckCell;
        
        public MapCellCheckMeta(ILayeredMap map, MapCell checkCell)
        {
            Map = map;
            CheckCell = checkCell;
        }
    }
}