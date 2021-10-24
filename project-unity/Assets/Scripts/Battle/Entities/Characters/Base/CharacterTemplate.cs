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

        public IEntity Generate()
        {
            var character = new Entity();
            character.AddComponent(new TeamTagComponent(teamTag));
            character.AddComponent(new ViewTagComponent(viewTag));
            character.AddComponent(new HealthComponent(health));
            character.AddComponent(new SpawnPositionComponent(MapCell.FromVector(spawnPosition)));
            character.AddComponent(new InitiativeComponent(minInitiative, maxInitiative));

            return character;
        }
    }
}