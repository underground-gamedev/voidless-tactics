using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Battle
{
    [RequireComponent(typeof(Team))]
    public abstract class BaseTeamSetup : SerializedMonoBehaviour
    {
        private Team team;
        protected Team Team => team ??= GetComponent<Team>();
        public abstract IEnumerator SetupTeam();
    }
}