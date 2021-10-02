using Battle.Components.SpawnPositionComponent;
using Battle.Components.TeamTagComponent;
using Battle.Components.ViewTagComponent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "TeamTemplate.asset", menuName = "CUSTOM/Templates/TeamTemplate", order = 1)]
    public class TeamTemplate : SerializedScriptableObject
    {
        [SerializeField] private string teamName;
        [SerializeField] private TeamTag teamTag;

        public ITeam Generate()
        {
            var team = new Team(teamName, teamTag);

            return team;
        }
    }
}