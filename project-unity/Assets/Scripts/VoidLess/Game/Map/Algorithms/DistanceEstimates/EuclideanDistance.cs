using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.DistanceEstimates
{
    public class EuclideanDistanceEstimater: IDistanceEstimater
    {
        public float EstimateDistance(ILayeredMap map, MapCell src, MapCell dest)
        {
            return (dest.Pos - src.Pos).magnitude;
        }
    }
}