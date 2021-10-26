using System.Linq;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Teams.Base;

namespace VoidLess.Game.Entities.Teams.Components.RelationComponent
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