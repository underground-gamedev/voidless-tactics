using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    [RequireComponent(typeof(Character))]
    public abstract class AIComponent : MonoBehaviour
    {
        protected Character character;

        protected MapObject mapObj;
        protected TacticMap map;

        protected List<Character> characters;
        protected List<Character> enemyCharacters;
        protected List<Character> allyCharacters;

        public IEnumerator MakeTurn()
        {
            if (character == null)
            {
                character = GetComponent<Character>();
                mapObj = character.MapObject;
                map = mapObj.Map;
                characters = map.MapObjects.Select(obj => obj.AsCharacter).Where(ch => ch != null).ToList();
                enemyCharacters = characters.Where(ch => character.Team.IsEnemy(ch)).ToList();
                allyCharacters = characters.Where(ch => character.Team.IsAlly(ch)).ToList();
                foreach (var ch in characters)
                {
                    ch.DeathTrigger.AddListener(() =>
                    {
                        characters.Remove(ch);
                        enemyCharacters.Remove(ch);
                        allyCharacters.Remove(ch);
                    });
                }
            }

            return ChooseAction();
        }

        protected abstract IEnumerator ChooseAction();
    }
}