using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Battle.Map.Interfaces;
using UnityEngine;

namespace Battle.Map.Extensions
{
    public static class SizedMapExt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Size(this ISizedMap map)
        {
            return new Vector2Int(map.Width, map.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CellCount(this ISizedMap map)
        {
            return map.Width * map.Height;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MapCell CellBy(this ISizedMap map, int x, int y)
        {
            map.CheckOutOfBounds(x, y);
            return MapCell.FromXY(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MapCell> Positions(this ISizedMap map)
        {
            var width = map.Width;
            var height = map.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    yield return map.CellBy(x, y);
                }
            }
            
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOutOfBounds(this ISizedMap map, int x, int y)
        {
            return x < 0 || x >= map.Width || y < 0 || y >= map.Height;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOutOfBounds(this ISizedMap map, MapCell cell)
        {
            return map.IsOutOfBounds(cell.X, cell.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckOutOfBounds(this ISizedMap map, int x, int y)
        {
            #if DEBUG
            if (map.IsOutOfBounds(x, y))
            {
                throw new InvalidOperationException($"{x},{y} out of bounds. Recheck CellBy execution");
            }
            #endif
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckOutOfBounds(this ISizedMap map, MapCell cell)
        {
            map.CheckOutOfBounds(cell.X, cell.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<MapCell> DirectNeighboursFor(this ISizedMap map, MapCell pos)
        {
            return map.NeighboursByDirections(pos, MapDirections.Direct());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<MapCell> DiagonalNeighboursFor(this ISizedMap map, MapCell pos)
        {
            return map.NeighboursByDirections(pos, MapDirections.Diagonal());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<MapCell> AllNeighboursFor(this ISizedMap map, MapCell pos)
        {
            return map.NeighboursByDirections(pos, MapDirections.All());
        }

        private static List<MapCell> NeighboursByDirections(this ISizedMap map, MapCell cell, List<MapCell> directions)
        {
            map.CheckOutOfBounds(cell.X, cell.Y);
            
            var neigh = new List<MapCell>();
            foreach (var dir in directions)
            {
                var neighX = cell.X + dir.X;
                var neighY = cell.Y + dir.Y;

                if (map.IsOutOfBounds(neighX, neighY)) continue;
                neigh.Add(map.CellBy(neighX, neighY));
            }
            return neigh;
        }
    }
}