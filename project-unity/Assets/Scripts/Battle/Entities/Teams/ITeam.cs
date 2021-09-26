using JetBrains.Annotations;

namespace Battle
{
    public interface ITeam: IEntity
    {
        [NotNull] ITeamInfo Info { get; }
        [NotNull] ITeamMembers Members { get; }
        [NotNull] IRelationComponent Relations { get; }
    }
}