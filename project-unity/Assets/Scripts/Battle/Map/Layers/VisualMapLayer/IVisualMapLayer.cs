namespace Battle.Map.Interfaces
{
    public interface IVisualMapLayer: IMapLayer
    {
        void RedrawAll();
        void RedrawSingle(MapCell pos);
    }
}