using Battle.Map.Interfaces;

namespace Battle.Rules.MapCellCheckRules
{
    public class CharacterCheckRule: IMapCellCheckRule
    {
        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            var character = map.GetLayer<IEntityMapLayer>().GetCharacter(checkedCell);
            return character != null;
        }
    }
}