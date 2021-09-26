using Battle.Algorithms.DistanceEstimates;
using Battle.Map.Interfaces;

namespace Battle.Algorithms.DistanceEstimates
{
    public class EuclideanDistanceEstimater: IDistanceEstimater
    {
        public float EstimateDistance(ILayeredMap map, MapCell src, MapCell dest)
        {
            return (dest.Pos - src.Pos).magnitude;
        }
    }
}