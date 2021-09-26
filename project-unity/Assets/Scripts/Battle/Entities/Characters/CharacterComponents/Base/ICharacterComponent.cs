namespace Battle
{
    public interface ICharacterComponent
    {
        void OnAttached(ICharacter character);
        void OnDeattached();
    }
}