using Sirenix.OdinInspector;
using UnityEngine;
using VoidLess.Game.Entities.Teams.Components.TeamInfo;

namespace VoidLess.Game.Entities.Teams.Base
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