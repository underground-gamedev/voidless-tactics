namespace Battle
{
    public interface IEntityComponent
    {
        void OnAttached(IEntity character);
        void OnDeattached();
    }
}