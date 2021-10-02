using System.Linq;
using Battle.Components.TeamTagComponent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "TeamsBindCharactersStep.asset", menuName = "CUSTOM/Setups/TeamsBindCharacterStep", order = 6)]
    public class TeamsBindCharactersStep : SerializedScriptableObject, IBattleStateSetupStep
    {
        public void Setup(BattleState state)
        {
            var teams = state.Teams.Teams;
            var characters = state.Characters.Characters;

            var teamByTag = teams.ToDictionary(team => team.Info.TeamTag);
            foreach (var character in characters)
            {
                var teamTagCom = character.GetComponent<TeamTagComponent>();
                if (teamTagCom == null)
                {
                    Debug.LogWarning($"{nameof(TeamsBindCharactersStep)}:: Unexpected character without TeamTagComponent");
                    continue;
                }

                if (teamByTag.TryGetValue(teamTagCom.TeamTag, out var team))
                {
                    team.MemberCollection.Add(character);
                    continue;
                }
                
                Debug.LogError($"{nameof(TeamsBindCharactersStep)}:: Team with tag {teamTagCom.TeamTag} not found. Recheck configuration");
            }
        }
    }
}