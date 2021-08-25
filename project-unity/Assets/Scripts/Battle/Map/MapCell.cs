using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Battle
{
    [DebuggerDisplay("{X},{Y}")]
    public class MapCell : IEquatable<MapCell>
    {
        public int X { get; }
        public int Y { get; }
        public Vector2Int Position => new Vector2Int(X, Y);
        public (int, int) XY => (X, Y);

        private bool solid;
        public bool Solid { get => solid; set => solid = value; }

        private MapObject mapObject;
        public MapObject MapObject { get => mapObject; set => mapObject = value; }

        private ManaCell manaCell = new ManaCell();
        public ManaCell Mana => manaCell;

        public MapCell(int x, int y)
        {
            X = x;
            Y = y;
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

        public static bool operator ==(MapCell left, MapCell right)
        {
            return EqualityComparer<MapCell>.Default.Equals(left, right);
        }

        public static bool operator !=(MapCell left, MapCell right)
        {
            return !(left == right);
        }
    }
}

