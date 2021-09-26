using Core.Components;

namespace Battle
{
    public interface IRelationComponent: IComponent
    {
        EntityRelation RelationTo(IEntity entity);
    }
}