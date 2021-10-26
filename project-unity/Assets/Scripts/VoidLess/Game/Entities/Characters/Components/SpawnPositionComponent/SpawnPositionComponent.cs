using VoidLess.Core.Components;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Entities.Characters.Components.SpawnPositionComponent
{
    public class SpawnPositionComponent : IComponent
    {
        public MapCell SpawnPosition { get; }

        public SpawnPositionComponent(MapCell spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }
    }
}