using System.Collections.Generic;

namespace Battle.Map.Interfaces
{
    public interface IPathfindMapLayer: IMapLayer
    {
        public delegate IEnumerable<MapCell> SearchNeighbours(ILayeredMap map, MapCell from);
        public delegate float DistanceBetween(ILayeredMap map, MapCell from, MapCell to);
        public delegate bool ValidEndpoint(ILayeredMap map, MapCell endpoint);
        
        IEnumerable<MapCell> GetAreaByDistance(MapCell src, float distance, 
            SearchNeighbours neighbourPattern = null,
            DistanceBetween neighbourDistance = null,
            ValidEndpoint validDest = null);
        
        PathfindResult Pathfind(MapCell src, MapCell dest,
            SearchNeighbours neighbourPattern = null,
            DistanceBetween neighbourDistance = null,
            ValidEndpoint validDest = null);
    }
}