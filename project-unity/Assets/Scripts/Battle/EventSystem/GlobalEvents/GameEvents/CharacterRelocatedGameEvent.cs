using Battle.Map.Interfaces;

namespace Battle
{
    public class CharacterRelocatedGameEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly ICharacter Character;
        public readonly MapCell Src;
        public readonly MapCell Dest;
        
        public CharacterRelocatedGameEvent(ILayeredMap map, ICharacter character, MapCell src, MapCell dest)
        {
            Map = map;
            Character = character;
            Src = src;
            Dest = dest;
        }
    }
}