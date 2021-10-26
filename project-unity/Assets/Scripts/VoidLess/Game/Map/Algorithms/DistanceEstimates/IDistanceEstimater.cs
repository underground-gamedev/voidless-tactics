using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Map.Algorithms.DistanceEstimates
{
    public interface IDistanceEstimater
    {
        float EstimateDistance(ILayeredMap map, MapCell src, MapCell dest);
    }
}