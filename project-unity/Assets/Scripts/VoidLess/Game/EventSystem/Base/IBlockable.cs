namespace VoidLess.Game.EventSystem.Base
{
    public interface IBlockable
    {
        bool IsBlocked { get; }
        
        void Block(object blocker);
        void Unblock(object blocker);
    }
}