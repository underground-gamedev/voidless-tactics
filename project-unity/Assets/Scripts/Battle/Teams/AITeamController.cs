using System.Collections;
using UnityEngine;

namespace Battle
{
    public class AITeamController : BaseTeamController
    {
        public override IEnumerator MakeTurn(TacticMap map, Team team, Character character)
        {
            var aiComponent = character.AIComponent;
            if (aiComponent != null)
            {
                yield return aiComponent.MakeTurn();
            }
        }
    }
}