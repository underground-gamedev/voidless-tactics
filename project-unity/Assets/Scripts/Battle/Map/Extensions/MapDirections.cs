using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Battle.Map.Extensions
{
    public static class MapDirections
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<MapCell> Direct()
        {
            return new List<MapCell>
            {
                new MapCell(0, -1), new MapCell(-1, 0), new MapCell(1, 0), new MapCell(0, 1),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<MapCell> Diagonal()
        {
            return new List<MapCell>
            {
                new MapCell(-1, -1), new MapCell(-1, 1), new MapCell(1, -1), new MapCell(1, 1),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<MapCell> All()
        {
            return new List<MapCell>
            {
                new MapCell(0, -1), new MapCell(-1, 0), new MapCell(1, 0), new MapCell(0, 1),
                new MapCell(-1, -1), new MapCell(-1, 1), new MapCell(1, -1), new MapCell(1, 1),
            };
        }
    }
}