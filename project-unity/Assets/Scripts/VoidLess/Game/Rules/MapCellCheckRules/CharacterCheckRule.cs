using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Layers.CharacterMapLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Rules.MapCellCheckRules
{
    public class CharacterCheckRule: IMapCellCheckRule
    {
        public bool ApplyRule(ILayeredMap map, MapCell checkedCell)
        {
            var character = map.GetLayer<ICharacterMapLayer>().GetCharacter(checkedCell);
            return character != null;
        }
    }
}