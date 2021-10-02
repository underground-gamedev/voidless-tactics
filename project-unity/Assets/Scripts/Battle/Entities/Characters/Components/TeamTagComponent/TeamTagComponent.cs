using Core.Components;

namespace Battle.Components.TeamTagComponent
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