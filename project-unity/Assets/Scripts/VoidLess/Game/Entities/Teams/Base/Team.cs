using VoidLess.Core.Components;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Teams.Components.RelationComponent;
using VoidLess.Game.Entities.Teams.Components.TeamInfo;
using VoidLess.Game.Entities.Teams.Components.TeamMemberCollection;

namespace VoidLess.Game.Entities.Teams.Base
{
    public class Team : Entity, ITeam
    {
        public ITeamInfo Info => this.GetComponent<ITeamInfo>();
        public ITeamMemberCollection MemberCollection => this.GetComponent<ITeamMemberCollection>();
        public IRelationComponent Relations => this.GetComponent<IRelationComponent>();

        public Team(string name, TeamTag tag)
        {
            OnNewComponentAttached(TryCallAttachedToTeam);
            OnComponentCompleteDeAttached(TryCallDeAttachedFromTeam);
            
            this.AddWithAssociation<ITeamInfo>(new TeamInfo(name, tag));
            this.AddWithAssociation<ITeamMemberCollection>(new TeamMemberCollection());
            this.AddWithAssociation<IRelationComponent>(new TeamRelationComponent());
        }
        
        private void TryCallAttachedToTeam(IComponent com)
        {
            if (com is ITeamAttachable ta)
            {
                ta.OnAttached(this);
            }
        }

        private void TryCallDeAttachedFromTeam(IComponent com)
        {
            if (com is ITeamAttachable ta)
            {
                ta.OnDeAttached();
            }
        }
    }
}