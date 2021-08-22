using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Battle
{
    [RequireComponent(typeof(MapObject), typeof(BasicCharacterStats))]
    public class AttackComponent : MonoBehaviour, IAttackComponent
    {
        private MapObject mapObject;
        private BasicCharacterStats basicStats;

        public void Start()
        {
            mapObject = GetComponent<MapObject>();
            basicStats = GetComponent<BasicCharacterStats>();
        }

        public IEnumerator Attack(ITargetComponent target)
        {
            yield return target.TakeDamage(basicStats.Attack.Value);
        }

        public bool AttackAvailable()
        {
            return true;
        }

        public List<MapCell> GetAttackArea(MapCell from)
        {
            var (x, y) = from.XY;
            var attackArea = mapObject.Map.AllNeighboursFor(x, y);
            return attackArea;
        }
    }
}