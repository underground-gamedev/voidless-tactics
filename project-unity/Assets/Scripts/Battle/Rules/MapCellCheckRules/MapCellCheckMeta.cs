using Battle.Map.Interfaces;

namespace Battle.Rules.MapCellCheckRules
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