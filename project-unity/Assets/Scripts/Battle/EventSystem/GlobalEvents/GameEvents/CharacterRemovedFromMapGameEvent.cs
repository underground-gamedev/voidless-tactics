using Battle.Map.Interfaces;

namespace Battle
{
    public class CharacterRemovedFromMapGameEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly ICharacter Character;
        
        public CharacterRemovedFromMapGameEvent(ILayeredMap map, ICharacter character)
        {
            Map = map;
            Character = character;
        }
    }
}