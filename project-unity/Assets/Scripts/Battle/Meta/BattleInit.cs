using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleInit : MonoBehaviour
    {
        [SerializeField]
        private TurnManager turnManager;

        [SerializeField]
        private List<Team> teams;

        public void Start()
        {
            StartCoroutine(InitBattle());
        }

        private IEnumerator InitBattle()
        {
            yield return new WaitForEndOfFrame();

            foreach (var team in teams)
            {
                yield return team.TeamSetup.SetupTeam();
            }

            yield return turnManager.StartTurnLoop();
        }
    }
}