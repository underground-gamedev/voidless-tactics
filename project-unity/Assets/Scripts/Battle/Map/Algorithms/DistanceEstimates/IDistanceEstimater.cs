using Battle.Map.Interfaces;

namespace Battle.Algorithms.DistanceEstimates
{
    public interface IDistanceEstimater
    {
        float EstimateDistance(ILayeredMap map, MapCell src, MapCell dest);
    }
}