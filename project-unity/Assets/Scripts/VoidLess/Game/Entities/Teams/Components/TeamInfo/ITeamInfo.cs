using VoidLess.Core.Components;

namespace VoidLess.Game.Entities.Teams.Components.TeamInfo
{
    public interface ITeamInfo: IComponent
    {
        string Name { get; }
        TeamTag TeamTag { get; }
    }
}