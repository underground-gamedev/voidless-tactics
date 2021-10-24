namespace Battle.Components
{
    public static class CharacterComponentsExt
    {
        public static IGlobalEventEmitter GetGlobalEmitter(this IEntity entity)
        {
            return entity.GetComponent<IGlobalEventEmitter>();
        }

        public static IStatComponent GetStatComponent(this IEntity entity)
        {
            return entity.GetComponent<IStatComponent>();
        }
    }
}