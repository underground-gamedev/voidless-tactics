namespace Battle.Components
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