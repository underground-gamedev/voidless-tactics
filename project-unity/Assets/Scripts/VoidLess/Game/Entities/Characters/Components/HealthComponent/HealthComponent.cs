using JetBrains.Annotations;
using VoidLess.Core.Components;
using VoidLess.Core.Entities;
using VoidLess.Core.Stats;

namespace VoidLess.Game.Entities.Characters.Components.HealthComponent
{
    public class HealthComponent: IComponent, IEntityAttachable
    {
        private IEntity entity;
        private int initialHealth;

        private SubtractHealthBehaviour subtractHealthBehaviour;
        private CheckDeathBehaviour checkDeathBehaviour;
        
        public HealthComponent(int initialHealth)
        {
            this.initialHealth = initialHealth;
        }
        public void OnAttached([NotNull] IEntity entity)
        {
            entity.ShouldCorrespond(Archtype.StatHolder);
            
            this.entity = entity;
            
            var stats = this.entity.Stats();
            stats.Add(StatType.CurrentHealth, new Stat(initialHealth));
            
            subtractHealthBehaviour = new SubtractHealthBehaviour(entity);
            checkDeathBehaviour = new CheckDeathBehaviour(entity);
            
            entity.AddBehaviour(subtractHealthBehaviour);
            entity.AddBehaviour(checkDeathBehaviour);
        }

        void IEntityAttachable.OnDeAttached()
        {
            if (entity == null) return;
            
            var stats = entity.Stats();
            stats.Remove(StatType.CurrentHealth);
            stats.Remove(StatType.MaxHealth);
            
            entity.RemoveBehaviour(subtractHealthBehaviour);
            entity.RemoveBehaviour(checkDeathBehaviour);

            entity = null;
        }
    }
}