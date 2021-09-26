using UnityEngine;

namespace Battle
{
    public class BasicCharacterStats : MonoBehaviour
    {
        public EntityStat Health;
        public EntityStat MaxHealth;

        public EntityStat Attack;
        public EntityStat Magic;
        public EntityStat Speed;
        public EntityStat ManaControl;

        public EntityStat InitiativeMin;
        public EntityStat InitiativeMax;
        [HideInInspector]
        public EntityStat InitiativeCurrent;
    }
}