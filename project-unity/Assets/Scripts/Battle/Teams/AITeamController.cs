using System.Collections;
using UnityEngine;

namespace Battle
{
    public class AITeamController : BaseTeamController
    {
        public override IEnumerator MakeTurn(TacticMap map, Team team, Character character)
        {
            yield break;
        }
    }
}