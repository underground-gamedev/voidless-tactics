namespace Battle
{
    public interface IEntityAttachable
    {
        void OnAttached(IEntity entity);
        void OnDeAttached();
    }
}