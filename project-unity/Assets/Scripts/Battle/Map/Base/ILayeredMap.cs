namespace Battle.Map.Interfaces
{
    public interface ILayeredMap: ISizedMap
    {
        T GetLayer<T>() where T : class, IMapLayer;
    }
}