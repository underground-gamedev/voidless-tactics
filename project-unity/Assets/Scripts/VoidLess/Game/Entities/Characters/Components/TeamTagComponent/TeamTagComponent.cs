using VoidLess.Core.Components;
using VoidLess.Game.Entities.Teams.Components.TeamInfo;

namespace VoidLess.Game.Entities.Characters.Components.TeamTagComponent
{
    public class TeamTagComponent : IComponent
    {
        public TeamTag TeamTag { get; }

        public TeamTagComponent(TeamTag teamTag)
        {
            TeamTag = teamTag;
        }
    }
}