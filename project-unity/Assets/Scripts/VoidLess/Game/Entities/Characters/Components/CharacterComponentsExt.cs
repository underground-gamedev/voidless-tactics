using VoidLess.Core.Components.StatComponent;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter;

namespace VoidLess.Game.Entities.Characters.Components
{
    public static class CharacterComponentsExt
    {
        public static IGlobalEventEmitter Emitter(this IEntity entity)
        {
            return entity.GetComponent<IGlobalEventEmitter>();
        }

        public static IStatComponent Stats(this IEntity entity)
        {
            return entity.GetComponent<IStatComponent>();
        }
    }
}