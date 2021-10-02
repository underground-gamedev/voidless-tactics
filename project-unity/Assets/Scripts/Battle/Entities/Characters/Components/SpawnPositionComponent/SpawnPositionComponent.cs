using Core.Components;

namespace Battle.Components.SpawnPositionComponent
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