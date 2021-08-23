using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class Team : MonoBehaviour
    {
        [SerializeField]
        private TacticMap map;
        public TacticMap Map => map;

        [SerializeField]
        private string teamName;
        public string TeamName => teamName;

        [SerializeField]
        private Color teamColor;
        public Color TeamColor => teamColor;

        [SerializeField]
        private List<Character> characters;
        public IReadOnlyList<Character> Characters => characters;

        [HideInInspector]
        public Character ActiveCharacter;

        private BaseTeamController teamController;
        public BaseTeamController TeamController => teamController ??= GetComponent<BaseTeamController>();

        private BaseTeamSetup teamSetup;
        public BaseTeamSetup TeamSetup => teamSetup ??= GetComponent<BaseTeamSetup>();

        private void Start()
        {
            foreach (var character in characters)
            {
                character.DeathTrigger.AddListener(() => {
                    characters.Remove(character);
                    character.Team = null;
                    if (ActiveCharacter == character) ActiveCharacter = null;
                });
                character.Team = this;
            }
        }
        
        public bool IsEnemy(Character character)
        {
            return !Characters.Contains(character);
        }

        public bool IsAlly(Character character)
        {
            return Characters.Contains(character);
        }
    }
}