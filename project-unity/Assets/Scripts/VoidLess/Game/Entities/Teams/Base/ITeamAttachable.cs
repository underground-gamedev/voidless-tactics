namespace VoidLess.Game.Entities.Teams.Base
{
    public interface ITeamAttachable
    {
        void OnAttached(ITeam team);
        void OnDeAttached();
    }
}