using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VoidLess.Game.Map.Structs
{
    [DebuggerDisplay("{X},{Y}")]
    public readonly struct MapCell : IEquatable<MapCell>
    {
        public int X { get; }
        public int Y { get; }
        public Vector2Int Pos => new Vector2Int(X, Y);
        public (int, int) XY => (X, Y);

        public MapCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MapCell(Vector2Int pos)
        {
            X = pos.x;
            Y = pos.y;
        }

        public override bool Equals(object obj)
        {
            if (obj is MapCell cell)
            {
                return XY == cell.XY;
            }
            return base.Equals(obj);
        }

        public bool Equals(MapCell other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
        
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static bool operator ==(MapCell left, MapCell right)
        {
            return EqualityComparer<MapCell>.Default.Equals(left, right);
        }

        public static bool operator !=(MapCell left, MapCell right)
        {
            return !(left == right);
        }
       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MapCell FromXY(int x, int y)
        {
            return new MapCell(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MapCell FromVector(Vector2Int pos)
        {
            return new MapCell(pos);
        }
    }
}

