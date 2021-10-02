namespace Battle.Components
{
    public static class CharacterComponentsExt
    {
        public static IGlobalEventEmitter GetGlobalEmitter(this ICharacter character)
        {
            return character.GetComponent<IGlobalEventEmitter>();
        }
    }
}