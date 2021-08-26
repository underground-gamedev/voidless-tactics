using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField]
        private TacticMap map;

        [SerializeField]
        private List<Team> teams;

        private List<Character> allCharacters;
        private List<Character> plannedQueue;

        public IEnumerator StartTurnLoop()
        {
            allCharacters = teams
                .SelectMany(c => c.Characters)
                .Where(ch => ch.MapObject.Map != null)
                .ToList();

            foreach (var character in allCharacters)
            {
                character.DeathTrigger.AddListener(() =>
                {
                    allCharacters.Remove(character);
                    plannedQueue.Remove(character);
                });
            }

            var roundNumber = 0;

            while (true)
            {
                roundNumber++;
                allCharacters.ForEach(ch => ch.OnRoundStart());
                plannedQueue = PlanQueue(allCharacters);
                // allCharacters.ForEach(ch => ch.OnTurnPlanned(plannedQueue));
                while (plannedQueue.Count > 0)
                {
                    var activeCharacter = plannedQueue.First();
                    plannedQueue.Remove(activeCharacter);

                    var activeController = activeCharacter.Team.TeamController;
                    var team = activeCharacter.Team;
                    team.ActiveCharacter = activeCharacter;
                    activeCharacter.OnTurnStart();
                    yield return activeController.MakeTurn(map, team, activeCharacter);
                    activeCharacter.OnTurnEnd();
                    team.ActiveCharacter = null;

                    plannedQueue = SortByInitiative(plannedQueue);
                }

                allCharacters.ForEach(ch => ch.OnRoundEnd());
            }
        }

        private List<Character> SortByInitiative(List<Character> plannedQueue)
        {
            return plannedQueue.OrderByDescending(ch => ch.BasicStats.InitiativeCurrent.Value).ToList();
        }

        private List<Character> PlanQueue(List<Character> allCharacters)
        {
            var initiativeGroups = new Dictionary<int, List<Character>>();

            foreach (var ch in allCharacters)
            {
                var stats = ch.BasicStats;
                var initiative = stats.InitiativeCurrent;
                var min = stats.InitiativeMin.Value;
                var max = stats.InitiativeMax.Value;
                initiative.BaseValue = Random.Range(min, max + 1);

                var modifiedInitiative = initiative.Value;
                if (!initiativeGroups.ContainsKey(modifiedInitiative))
                {
                    initiativeGroups.Add(modifiedInitiative, new List<Character>());
                }

                initiativeGroups[modifiedInitiative].Add(ch);
            };

            var plannedQueue = new List<Character>();
            foreach (var group in initiativeGroups.OrderByDescending(x => x.Key).Select(x => x.Value))
            {
                group.Shuffle();
                plannedQueue.AddRange(group);
            }

            return plannedQueue;
        }
    }
}