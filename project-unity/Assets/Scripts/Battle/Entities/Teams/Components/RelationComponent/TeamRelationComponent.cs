using System.Linq;

namespace Battle.Components.RelationComponent
{
    public class TeamRelationComponent : IRelationComponent, ITeamAttachable
    {
        private ITeam team;
        
        public EntityRelation RelationTo(IEntity entity)
        {
            if (!(entity is IEntity)) return EntityRelation.Neutral;
            
            var members = team.MemberCollection.Members;
            return members.Contains(entity) ? EntityRelation.Friendly : EntityRelation.Agressive;

        }

        public void OnAttached(ITeam team)
        {
            this.team = team;
        }

        public void OnDeAttached()
        {
            this.team = null;
        }
    }
}