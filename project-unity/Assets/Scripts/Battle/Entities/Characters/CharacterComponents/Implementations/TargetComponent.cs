using System.Collections;
using UnityEngine;

/*
namespace Battle
{
    [RequireComponent(typeof(Character))]
    public class TargetComponent : MonoBehaviour, ITargetComponent
    {
        private MapObject mapObject;
        private BasicCharacterStats basicStats;
        private Character character;

        public void Start()
        {
            character = GetComponent<Character>();
            mapObject = character.MapObject;
            basicStats = character.BasicStats;
        }

        public IEnumerator TakeDamage(int damage)
        {
            var health = basicStats.Health;
            health.BaseValue -= damage;

            if (health.Value <= 0)
            {
                character.Death();
            }
            // await character.Map.PopupText(character.Cell, damage.ToString(), new Color(1f, 0.6f, 0.6f));

            yield break;
        }

        public IEnumerator TakeHeal(int heal)
        {
            var health = basicStats.Health;
            var currentValue = health.Value;
            health.BaseValue += heal;
            var newValue = health.Value;
            var diff = newValue - currentValue;

            if (diff > 0) {
                // await character.Map.PopupText(character.Cell, diff.ToString(), new Color(0.6f, 1.0f, 0.6f));
            }

            yield break;
        }
    }
}
*/