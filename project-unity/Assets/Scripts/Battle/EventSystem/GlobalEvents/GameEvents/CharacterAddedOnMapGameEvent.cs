using Battle.Map.Interfaces;

namespace Battle
{
    public class CharacterAddedOnMapGameEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly IEntity Character;
        public readonly MapCell Cell;
        
        public CharacterAddedOnMapGameEvent(ILayeredMap map, IEntity character, MapCell cell)
        {
            Map = map;
            Character = character;
            Cell = cell;
        }
    }
}