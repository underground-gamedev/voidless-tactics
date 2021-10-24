using System;
using Battle.Components;
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
            
            var stats = this.character.GetStatComponent();
            stats.Add(StatType.CurrentHealth, new Stat(initialHealth));
            
            subtractHealthBehaviour = new SubtractHealthBehaviour(character);
            checkDeathBehaviour = new CheckDeathBehaviour(character);
            
            character.AddBehaviour(subtractHealthBehaviour);
            character.AddBehaviour(checkDeathBehaviour);
        }

        public void OnAttached(IEntity entity)
        {
            if (entity is ICharacter) return;
            throw new InvalidOperationException("Health component not support custom entity. Expected ICharacter");
        }

        void ICharacterAttachable.OnDeAttached()
        {
            if (character == null) return;
            
            var stats = character.GetStatComponent();
            stats.Remove(StatType.CurrentHealth);
            stats.Remove(StatType.MaxHealth);
            
            character.RemoveBehaviour(subtractHealthBehaviour);
            character.RemoveBehaviour(checkDeathBehaviour);

            character = null;
        }

        void IEntityAttachable.OnDeAttached()
        {
        }
    }
}