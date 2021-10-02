namespace Battle.EventSystem
{
    public interface IBlockable
    {
        bool IsBlocked { get; }
        
        void Block(object blocker);
        void Unblock(object blocker);
    }
}