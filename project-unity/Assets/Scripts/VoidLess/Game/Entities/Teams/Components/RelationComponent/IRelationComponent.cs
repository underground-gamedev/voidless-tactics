using VoidLess.Core.Components;
using VoidLess.Core.Entities;

namespace VoidLess.Game.Entities.Teams.Components.RelationComponent
{
    public interface IRelationComponent: IComponent
    {
        EntityRelation RelationTo(IEntity entity);
    }
}