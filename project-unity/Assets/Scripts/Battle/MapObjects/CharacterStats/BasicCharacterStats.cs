using UnityEngine;

namespace Battle
{
    public class BasicCharacterStats : MonoBehaviour
    {
        public CharacterStat Health;
        public CharacterStat MaxHealth;

        public CharacterStat Attack;
        public CharacterStat Magic;
        public CharacterStat Speed;
        public CharacterStat ManaControl;

        public CharacterStat InitiativeMin;
        public CharacterStat InitiativeMax;
        [HideInInspector]
        public CharacterStat InitiativeCurrent;
    }
}