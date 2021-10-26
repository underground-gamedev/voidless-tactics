namespace VoidLess.Core.Entities
{
    public interface IEntityAttachable
    {
        void OnAttached(IEntity entity);
        void OnDeAttached();
    }
}