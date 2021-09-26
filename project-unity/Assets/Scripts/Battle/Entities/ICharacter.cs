using JetBrains.Annotations;

namespace Battle
{
    public interface ICharacter : IEntity, ITurnWatcher, IRoundWatcher
    {
        [NotNull] IStatComponent Stats { get; }
        [NotNull] IActiveSkillComponent Skills { get; }
        [NotNull] IBehaviourComponent Behaviours { get; }
    }
}