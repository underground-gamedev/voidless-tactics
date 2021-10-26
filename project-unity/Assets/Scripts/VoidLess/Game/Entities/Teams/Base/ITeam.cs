using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Teams.Components.RelationComponent;
using VoidLess.Game.Entities.Teams.Components.TeamInfo;
using VoidLess.Game.Entities.Teams.Components.TeamMemberCollection;

namespace VoidLess.Game.Entities.Teams.Base
{
    public interface ITeam: IEntity
    {
        ITeamInfo Info { get; }
        ITeamMemberCollection MemberCollection { get; }
        IRelationComponent Relations { get; }
    }
}