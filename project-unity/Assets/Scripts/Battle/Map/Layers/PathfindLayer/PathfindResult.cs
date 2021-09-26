using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public readonly struct PathfindResult
    {
        public readonly bool IsSuccess;
        public readonly MapCell[] Path;
        public readonly float Cost;

        private PathfindResult(bool success, MapCell[] path, float cost)
        {
            IsSuccess = success;
            Path = path;
            Cost = cost;
        }
        
        public bool Equals(PathfindResult other)
        {
            return IsSuccess.Equals(other.IsSuccess)
                   && Mathf.Abs(Cost - other.Cost) > Mathf.Epsilon
                   && Path.Equals(other.Path);
        }

        public override bool Equals(object obj)
        {
            return obj is PathfindResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsSuccess.GetHashCode();
                hashCode = (hashCode * 397) ^ (Path != null ? Path.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Cost.GetHashCode();
                return hashCode;
            }
        }

        public static PathfindResult Fail()
        {
            return new PathfindResult(false, default, default);
        }

        public static PathfindResult Success(MapCell[] path, float cost)
        {
            return new PathfindResult(true, path, cost);
        }
    }
}