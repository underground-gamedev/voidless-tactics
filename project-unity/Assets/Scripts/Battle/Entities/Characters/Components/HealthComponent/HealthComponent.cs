using System;
using Battle.Components;
using Core.Components;
using JetBrains.Annotations;

namespace Battle
{
    public class HealthComponent: IComponent, IEntityAttachable
    {
        private IEntity character;
        private int initialHealth;

        private SubtractHealthBehaviour subtractHealthBehaviour;
        private CheckDeathBehaviour checkDeathBehaviour;
        
        public HealthComponent(int initialHealth)
        {
            this.initialHealth = initialHealth;
        }
        public void OnAttached([NotNull] IEntity character)
        {
            character.Correspond(Archtype.StatHolder);
            
            this.character = character;
            
            var stats = this.character.GetStatComponent();
            stats.Add(StatType.CurrentHealth, new Stat(initialHealth));
            
            subtractHealthBehaviour = new SubtractHealthBehaviour(character);
            checkDeathBehaviour = new CheckDeathBehaviour(character);
            
            character.AddBehaviour(subtractHealthBehaviour);
            character.AddBehaviour(checkDeathBehaviour);
        }

        void IEntityAttachable.OnDeAttached()
        {
            if (character == null) return;
            
            var stats = character.GetStatComponent();
            stats.Remove(StatType.CurrentHealth);
            stats.Remove(StatType.MaxHealth);
            
            character.RemoveBehaviour(subtractHealthBehaviour);
            character.RemoveBehaviour(checkDeathBehaviour);

            character = null;
        }
    }
}