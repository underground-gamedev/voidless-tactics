using UnityEngine;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.EventSystem.GlobalEvents.UtilityEvents
{
    public class HoverOnCellUtilityEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly MapCell Cell;
        public readonly Vector2 Offset;
        
        public HoverOnCellUtilityEvent(ILayeredMap map, MapCell cell, Vector2 offset)
        {
            Map = map;
            Cell = cell;
            Offset = offset;
        }
        
        public override string ToString()
        {
            return $"{nameof(Cell)}: {Cell}, {nameof(Offset)}: {Offset}";
        }
    }
}