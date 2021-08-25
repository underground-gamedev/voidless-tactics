using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    [RequireComponent(typeof(TacticMap))]
    public class PathfindLayer : MonoBehaviour
    {
        [SerializeField]
        private float DirectCost = 1;
        [SerializeField]
        private float DiagonalCost = 1.4142f;

        private TacticMap map;

        private void Start()
        {
            map = GetComponent<TacticMap>();
        }

        public List<MapCell> GetAreaByDistance(MapCell src, float distance)
        {
            var result = new HashSet<MapCell>();
            var closed = new HashSet<MapCell>();
            var open = new HashSet<MapCell>() { src };
            var pathCostCalc = new Dictionary<MapCell, float>();

            pathCostCalc[src] = 0;

            while(open.Count > 0)
            {
                var curr = open.First();

                open.Remove(curr);
                closed.Add(curr);

                Action<MapCell, MapCell, float> addNeighbour = (curr, neigh, cost) =>
                {
                    var tempG = pathCostCalc[curr] + cost;
                    if (!open.Contains(curr) && tempG < distance)
                    {
                        pathCostCalc[neigh] = tempG;
                        if (!closed.Contains(neigh)) open.Add(neigh);
                        result.Add(neigh);
                    }
                };

                foreach(var neigh in map.DirectNeighboursFor(curr.X, curr.Y))
                {
                    if (!neigh.Solid)
                    {
                        addNeighbour(curr, neigh, DirectCost);
                    }
                }

                foreach(var neigh in map.DiagonalNeighboursFor(curr.X, curr.Y))
                {
                    if (!neigh.Solid)
                    {
                        addNeighbour(curr, neigh, DiagonalCost);
                    }
                }
            }

            return result.ToList();
        }

        public PathfindResult Pathfind(MapCell src, MapCell dest)
        {
            var closed = new HashSet<MapCell>();
            var open = new HashSet<MapCell>() { src };
            var from = new Dictionary<MapCell, MapCell>();
            var pathCostCalc = new Dictionary<MapCell, float>();
            var pathCostEstimate = new Dictionary<MapCell, float>();

            Func<MapCell, MapCell, float> h = (src, dest) => (src.Position - dest.Position).magnitude;

            pathCostCalc[src] = 0;
            pathCostEstimate[src] = h(src, dest);

            while(open.Count > 0)
            {
                var curr = GetNextCell(closed, pathCostEstimate);

                if (curr == dest)
                {
                    var path = BuildPath(from, src, dest);
                    var cost = pathCostCalc[dest];
                    return SuccessPathfind(path, cost);
                }

                open.Remove(curr);
                closed.Add(curr);

                Action<MapCell, MapCell, float> addNeighbour = (curr, neigh, cost) =>
                {
                    var tempG = pathCostCalc[curr] + cost;
                    if (!open.Contains(curr) || tempG < pathCostCalc[neigh])
                    {
                        from[neigh] = curr;
                        pathCostCalc[neigh] = tempG;
                        pathCostEstimate[neigh] = tempG + h(neigh, dest);
                    }

                    open.Add(neigh);
                };

                foreach(var neigh in map.DirectNeighboursFor(curr.X, curr.Y).Where(neigh => !closed.Contains(neigh)))
                {
                    if (!neigh.Solid)
                    {
                        addNeighbour(curr, neigh, DirectCost);
                    }
                }

                foreach(var neigh in map.DiagonalNeighboursFor(curr.X, curr.Y).Where(neigh => !closed.Contains(neigh)))
                {
                    if (!neigh.Solid)
                    {
                        addNeighbour(curr, neigh, DiagonalCost);
                    }
                }
            }

            return FailurePathfind();
        }
        private MapCell GetNextCell(HashSet<MapCell> closed, Dictionary<MapCell, float> estimatedCosts)
        {
            float min = float.PositiveInfinity;
            MapCell result = null;

            foreach (var est in estimatedCosts)
            {
                if (closed.Contains(est.Key)) continue;
                if (est.Value < min)
                {
                    min = est.Value;
                    result = est.Key;
                }
            }

            return result;
        }

        private List<MapCell> BuildPath(Dictionary<MapCell, MapCell> from, MapCell src, MapCell dest)
        {
            var path = new List<MapCell>();
            var curr = dest;
            while(curr != src)
            {
                path.Add(curr);
                curr = from[curr];
            }
            path.Add(src);
            path.Reverse();
            return path;
        }

        public struct PathfindResult
        {
            public bool IsSuccess;
            public List<MapCell> Path;
            public float Cost;
        }
        private static PathfindResult FailurePathfind()
        {
            return new PathfindResult()
            {
                IsSuccess = false,
            };
        }

        private static PathfindResult SuccessPathfind(List<MapCell> path, float cost)
        {
            return new PathfindResult() { 
                IsSuccess = true,
                Path = path,
                Cost = cost,
            };
        }

    }
}