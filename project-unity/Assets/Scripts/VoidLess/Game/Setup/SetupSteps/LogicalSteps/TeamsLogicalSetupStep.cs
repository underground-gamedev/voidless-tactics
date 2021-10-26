using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VoidLess.Game.Entities.Teams.Base;

namespace VoidLess.Game.Setup.SetupSteps.LogicalSteps
{
    [CreateAssetMenu(fileName = "TeamsLogicalSetupStep.asset", menuName = "CUSTOM/Setups/TeamsLogicalSetupStep", order = (int)SetupOrder.TeamsLogical)]
    public class TeamsLogicalSetupStep : SerializableSetupStep
    {
        [OdinSerialize, Required]
        private List<TeamTemplate> templates = new List<TeamTemplate>();

        public override void Setup(BattleState state)
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

        protected override SetupOrder SetupOrder => SetupOrder.TeamsLogical;
    }
}