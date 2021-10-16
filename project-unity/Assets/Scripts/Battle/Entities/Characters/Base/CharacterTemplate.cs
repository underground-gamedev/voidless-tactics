using Battle.Components.InitiativeComponent;
using Battle.Components.SpawnPositionComponent;
using Battle.Components.TeamTagComponent;
using Battle.Components.ViewTagComponent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "CharacterTemplate.asset", menuName = "CUSTOM/Templates/CharacterTemplate", order = 0)]
    public class CharacterTemplate : SerializedScriptableObject
    {
        [SerializeField]
        private TeamTag teamTag;
        [SerializeField]
        private CharacterViewTag viewTag;
        
        [SerializeField]
        private int health;

        [SerializeField]
        private int minInitiative;
        [SerializeField]
        private int maxInitiative;
        
        [SerializeField]
        private Vector2Int spawnPosition;

        public ICharacter Generate()
        {
            var character = new Character();
            character.AddComponent<TeamTagComponent>(new TeamTagComponent(teamTag));
            character.AddComponent<ViewTagComponent>(new ViewTagComponent(viewTag));
            character.AddComponent<HealthComponent>(new HealthComponent(health));
            character.AddComponent<SpawnPositionComponent>(new SpawnPositionComponent(MapCell.FromVector(spawnPosition)));
            character.AddComponent<InitiativeComponent>(new InitiativeComponent(minInitiative, maxInitiative));

            return character;
        }
    }
}