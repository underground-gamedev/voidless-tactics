using Battle.Components.RelationComponent;
using Core.Components;

namespace Battle
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
            
            this.AddComponent<ITeamInfo>(new TeamInfo(name, tag));
            this.AddComponent<ITeamMemberCollection>(new TeamMemberCollection());
            this.AddComponent<IRelationComponent>(new TeamRelationComponent());
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