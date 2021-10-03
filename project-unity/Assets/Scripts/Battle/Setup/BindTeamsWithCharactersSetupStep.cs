using System.Linq;
using Battle.Components.TeamTagComponent;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "BindTeamsWithCharactersSetupStep.asset", menuName = "CUSTOM/Setups/BindTeamsWithCharactersSetupStep", order = (int)SetupOrder.BindTeamsWithCharacters)]
    public class BindTeamsWithCharactersSetupStep : SerializableSetupStep
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
                    Debug.LogWarning($"{nameof(BindTeamsWithCharactersSetupStep)}:: Unexpected character without TeamTagComponent");
                    continue;
                }

                if (teamByTag.TryGetValue(teamTagCom.TeamTag, out var team))
                {
                    team.MemberCollection.Add(character);
                    continue;
                }
                
                Debug.LogError($"{nameof(BindTeamsWithCharactersSetupStep)}:: Team with tag {teamTagCom.TeamTag} not found. Recheck configuration");
            }
        }

        protected override SetupOrder SetupOrder => SetupOrder.BindTeamsWithCharacters;
    }
}