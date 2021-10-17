using System;
using Core.Components;
using JetBrains.Annotations;

namespace Battle
{
    public class HealthComponent: IComponent, ICharacterAttachable, IEntityAttachable
    {
        private ICharacter character;
        private int initialHealth;

        private SubtractHealthBehaviour subtractHealthBehaviour;
        private CheckDeathBehaviour checkDeathBehaviour;
        
        public HealthComponent(int initialHealth)
        {
            this.initialHealth = initialHealth;
        }
        public void OnAttached([NotNull] ICharacter character)
        {
            this.character = character;
            
            var stats = this.character.Stats;
            stats.Add(StatType.CurrentHealth, new Stat(initialHealth));
            
            var behaviours = character.Behaviours;

            subtractHealthBehaviour = new SubtractHealthBehaviour(character);
            checkDeathBehaviour = new CheckDeathBehaviour(character);
            
            behaviours.Add(subtractHealthBehaviour);
            behaviours.Add(checkDeathBehaviour);
        }

        public void OnAttached(IEntity entity)
        {
            if (entity is ICharacter) return;
            throw new InvalidOperationException("Health component not support custom entity. Expected ICharacter");
        }

        void ICharacterAttachable.OnDeAttached()
        {
            if (character == null) return;
            
            var stats = character.Stats;
            stats.Remove(StatType.CurrentHealth);
            stats.Remove(StatType.MaxHealth);
            
            var behaviours = character.Behaviours;
            
            behaviours.Remove(subtractHealthBehaviour);
            behaviours.Remove(checkDeathBehaviour);

            character = null;
        }

        void IEntityAttachable.OnDeAttached()
        {
        }
    }
}