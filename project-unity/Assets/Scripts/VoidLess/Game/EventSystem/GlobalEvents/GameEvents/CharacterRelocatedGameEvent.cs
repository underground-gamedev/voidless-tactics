using VoidLess.Core.Entities;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.EventSystem.GlobalEvents.GameEvents
{
    public class CharacterRelocatedGameEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly IEntity Character;
        public readonly MapCell Src;
        public readonly MapCell Dest;
        
        public CharacterRelocatedGameEvent(ILayeredMap map, IEntity character, MapCell src, MapCell dest)
        {
            Map = map;
            Character = character;
            Src = src;
            Dest = dest;
        }
    }
}