using Battle.Map.Interfaces;

namespace Battle.MapGenerators
{
    public interface IMapGeneratorStep
    {
        void Generate(ILayeredMap map);
    }
}