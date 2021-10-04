using Battle.Map.Interfaces;

namespace Battle
{
    public class CharacterAddedOnMapGameEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly ICharacter Character;
        public readonly MapCell Cell;
        
        public CharacterAddedOnMapGameEvent(ILayeredMap map, ICharacter character, MapCell cell)
        {
            Map = map;
            Character = character;
            Cell = cell;
        }
    }
}