namespace Battle
{
    public interface ICharacterAttachable
    {
        void OnAttached(ICharacter character);
        void OnDeAttached();
    }
}