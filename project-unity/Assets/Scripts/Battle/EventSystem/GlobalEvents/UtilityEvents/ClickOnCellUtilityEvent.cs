using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle
{
    public class ClickOnCellUtilityEvent : IGlobalEvent
    {
        public readonly ILayeredMap Map;
        public readonly MapCell Cell;
        public readonly Vector2 Offset;
        
        public ClickOnCellUtilityEvent(ILayeredMap map, MapCell cell, Vector2 offset)
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