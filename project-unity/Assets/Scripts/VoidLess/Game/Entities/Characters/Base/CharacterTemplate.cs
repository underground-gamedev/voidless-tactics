using Sirenix.OdinInspector;
using UnityEngine;
using VoidLess.Core.Entities;
using VoidLess.Game.Entities.Characters.Components.HealthComponent;
using VoidLess.Game.Entities.Characters.Components.InitiativeComponent;
using VoidLess.Game.Entities.Characters.Components.SpawnPositionComponent;
using VoidLess.Game.Entities.Characters.Components.TeamTagComponent;
using VoidLess.Game.Entities.Characters.Components.ViewTagComponent;
using VoidLess.Game.Entities.Teams.Components.TeamInfo;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Entities.Characters.Base
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