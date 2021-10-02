namespace Battle
{
    public interface ITeam: IEntity
    {
        ITeamInfo Info { get; }
        ITeamMemberCollection MemberCollection { get; }
        IRelationComponent Relations { get; }
    }
}