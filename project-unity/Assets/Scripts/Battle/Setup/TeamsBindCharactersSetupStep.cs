using System.Linq;
using Battle.Components.TeamTagComponent;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "TeamsBindCharactersSetupStep.asset", menuName = "CUSTOM/Setups/TeamsBindCharactersSetupStep", order = (int)SetupOrder.TeamsBindCharacters)]
    public class TeamsBindCharactersSetupStep : SerializableSetupStep
    {
        public override void Setup(BattleState state)
        {
            var teams = state.Teams.Teams;
            var characters = state.Characters.Characters;

            var teamByTag = teams.ToDictionary(team => team.Info.TeamTag);
            foreach (var character in characters)
            {
                var teamTagCom = character.GetComponent<TeamTagComponent>();
                if (teamTagCom == null)
                {
                    Debug.LogWarning($"{nameof(TeamsBindCharactersSetupStep)}:: Unexpected character without TeamTagComponent");
                    continue;
                }

                if (teamByTag.TryGetValue(teamTagCom.TeamTag, out var team))
                {
                    team.MemberCollection.Add(character);
                    continue;
                }
                
                Debug.LogError($"{nameof(TeamsBindCharactersSetupStep)}:: Team with tag {teamTagCom.TeamTag} not found. Recheck configuration");
            }
        }

        protected override SetupOrder SetupOrder => SetupOrder.TeamsBindCharacters;
    }
}