using JetBrains.Annotations;

namespace Battle
{
    public interface ICharacter : IEntity
    {
        IStatComponent Stats { get; }
        IActiveSkillComponent Skills { get; }
    }
}