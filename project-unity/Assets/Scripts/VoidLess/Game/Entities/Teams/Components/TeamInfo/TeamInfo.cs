namespace VoidLess.Game.Entities.Teams.Components.TeamInfo
{
    public class TeamInfo : ITeamInfo
    {
        public string Name { get; }
        public TeamTag TeamTag { get; }

        public TeamInfo(string name, TeamTag tag)
        {
            Name = name;
            TeamTag = tag;
        }
    }
}