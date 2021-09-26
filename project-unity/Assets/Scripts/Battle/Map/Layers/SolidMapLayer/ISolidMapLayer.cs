using Battle.Map.Interfaces;

namespace Battle
{
    public interface ISolidMapLayer: IMapLayer
    {
        bool IsSolid(MapCell cell);
    }
}