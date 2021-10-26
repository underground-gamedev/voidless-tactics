using VoidLess.Core.Entities;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.EventSystem.GlobalEvents.GameEvents
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