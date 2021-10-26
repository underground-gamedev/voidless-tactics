using VoidLess.Core.Entities;
using VoidLess.Game.Map.Base;

namespace VoidLess.Game.EventSystem.GlobalEvents.GameEvents
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