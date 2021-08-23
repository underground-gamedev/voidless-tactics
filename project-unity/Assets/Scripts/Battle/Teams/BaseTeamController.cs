using System.Collections;
using UnityEngine;

namespace Battle
{
    public abstract class BaseTeamController : MonoBehaviour
    {
        public abstract IEnumerator MakeTurn(TacticMap map, Team team, Character character);
    }
}