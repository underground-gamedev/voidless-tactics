using Battle.Map.Interfaces;

namespace Battle
{
    public class CharacterRemovedFromMapGameEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly IEntity Character;
        
        public CharacterRemovedFromMapGameEvent(ILayeredMap map, IEntity character)
        {
            Map = map;
            Character = character;
        }
    }
}