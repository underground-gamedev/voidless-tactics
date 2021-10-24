namespace Battle
{
    public interface ICharacter : IEntity
    {
        IStatComponent Stats { get; }
    }
}