using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "TeamsLogicalSetupStep.asset", menuName = "CUSTOM/Setups/TeamsLogicalSetupStep", order = 5)]
    public class TeamsLogicalSetupStep : SerializedScriptableObject, IBattleStateSetupStep
    {
        [OdinSerialize, Required]
        private List<TeamTemplate> templates = new List<TeamTemplate>();
        
        public void Setup(BattleState state)
        {
            var teams = templates.Select(template => template.Generate()).ToList();
            if (teams.Select(team => team.Info.TeamTag).Distinct().Count() != teams.Count)
            {
                Debug.LogError($"{nameof(TeamsLogicalSetupStep)}:: Invalid configuration. TeamTag should be unique");
                return;
            }
            
            foreach (var team in teams)
            {
                state.Teams.AddTeam(team);
            }
        }
    }
}