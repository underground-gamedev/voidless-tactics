using System;
using System.Collections.Generic;
using System.Linq;
using VoidLess.Core.Components;
using VoidLess.Game.Map.Algorithms.AreaPatterns;
using VoidLess.Game.Map.Algorithms.DistanceEstimates;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Layers.SolidMapLayer;
using VoidLess.Game.Map.Structs;
using VoidLess.Game.Rules.MapCellCheckRules;

namespace VoidLess.Game.Map.Layers.PathfindLayer
{
    [Require(typeof(ISolidMapLayer))]
    public class PathfindLayer : IPathfindMapLayer
    {
        private ILayeredMap map;
        private IAreaPattern defaultPatternRule;
        private IDistanceEstimater defaultDistanceRule;
        private IMapCellCheckRule defaultCheckDestRule;
        
        public void OnAttached(ILayeredMap map)
        {
            this.map = map;
            defaultPatternRule = new PathfindNeighboursPattern();
            defaultDistanceRule = new EuclideanDistanceEstimater();
            defaultCheckDestRule = MapCellCheckRule.Inverse(
                new OrSequenceCheck()
                    .AddRule(MapCellCheckRule.OutOfBounds())
                    .AddRule(MapCellCheckRule.IsSolid())
                );
        }

        public void OnDeAttached()
        {
            this.map = null;
            this.defaultDistanceRule = null;
            this.defaultPatternRule = null;
            this.defaultCheckDestRule = null;
        }

        public IEnumerable<MapCell> GetAreaByDistance(MapCell src, float distance, 
            IPathfindMapLayer.SearchNeighbours neighbourPattern = null,
            IPathfindMapLayer.DistanceBetween neighbourDistance = null,
            IPathfindMapLayer.ValidEndpoint validDest = null)
        {
            neighbourPattern ??= defaultPatternRule.GetPattern;
            neighbourDistance ??= defaultDistanceRule.EstimateDistance;
            validDest ??= defaultCheckDestRule.ApplyRule;
            
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
                        if (validDest(map, neigh))
                        {
                            result.Add(neigh);
                        }
                    }
                };

                var neighbours = neighbourPattern(map, curr);
                foreach (var neigh in neighbours.Where(neigh => !closed.Contains(neigh)))
                {
                    var neighDistance = neighbourDistance(map, curr, neigh);
                    addNeighbour(curr, neigh, neighDistance);
                }
            }

            return result.ToList();
        }

        public PathfindResult Pathfind(MapCell src, MapCell dest,
            IPathfindMapLayer.SearchNeighbours neighbourPattern = null,
            IPathfindMapLayer.DistanceBetween neighbourDistance = null,
            IPathfindMapLayer.ValidEndpoint validDest = null)
        {
            neighbourPattern ??= defaultPatternRule.GetPattern;
            neighbourDistance ??= defaultDistanceRule.EstimateDistance;
            validDest ??= defaultCheckDestRule.ApplyRule;

            if (!validDest(map, dest))
            {
                return PathfindResult.Fail();
            }
            
            var closed = new HashSet<MapCell>();
            var open = new HashSet<MapCell>() { src };
            var from = new Dictionary<MapCell, MapCell>();
            var pathCostCalc = new Dictionary<MapCell, float>();
            var pathCostEstimate = new Dictionary<MapCell, float>();

            var hFix = neighbourDistance(map, src, dest) / (src.Pos - dest.Pos).magnitude;
            Func<MapCell, MapCell, float> h = (src, dest) => hFix * (src.Pos - dest.Pos).magnitude;

            pathCostCalc[src] = 0;
            pathCostEstimate[src] = h(src, dest);

            while(open.Count > 0)
            {
                var curr = GetNextCell(closed, pathCostEstimate);

                if (curr == dest)
                {
                    var path = BuildPath(from, src, dest).ToArray();
                    var cost = pathCostCalc[dest];
                    return PathfindResult.Success(path, cost);
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

                var neighbours = neighbourPattern(map, curr);
                foreach (var neigh in neighbours.Where(neigh => !closed.Contains(neigh)))
                {
                    var neighDistance = neighbourDistance(map, curr, neigh);
                    addNeighbour(curr, neigh, neighDistance);
                }
            }

            return PathfindResult.Fail();
        }
        
        private MapCell GetNextCell(HashSet<MapCell> closed, Dictionary<MapCell, float> estimatedCosts)
        {
            float min = float.PositiveInfinity;
            MapCell result = default;

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
    }
}