namespace Battle
{
    public interface ITeamAttachable
    {
        void OnAttached(ITeam team);
        void OnDeAttached();
    }
}