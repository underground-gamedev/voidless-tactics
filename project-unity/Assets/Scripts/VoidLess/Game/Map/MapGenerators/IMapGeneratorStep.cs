using VoidLess.Game.Map.Base;

namespace VoidLess.Game.Map.MapGenerators
{
    public interface IMapGeneratorStep
    {
        void Generate(ILayeredMap map);
    }
}