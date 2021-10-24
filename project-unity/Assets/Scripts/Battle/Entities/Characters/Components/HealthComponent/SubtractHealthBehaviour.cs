using Battle.Components;

namespace Battle
{
    public class SubtractHealthBehaviour : IBehaviour<TakeHitPersonalEvent>
    {
        private IEntity character;

        public SubtractHealthBehaviour(IEntity parent)
        {
            character = parent;
        }

        public int HandlePriority => (int)BehaviourPriority.SubtractHealth;
            
        public void Handle(TakeHitPersonalEvent hitEvent)
        {
            var stats = character.GetStatComponent();
            var health = stats.Get(StatType.CurrentHealth);
            if (health == null) return;
            stats.Remove(StatType.CurrentHealth);
            stats.Add(StatType.CurrentHealth, health - hitEvent.Value);
            character.GetGlobalEmitter()?.Emit(new DamagedGameEvent(character, hitEvent.Value));
        }

        public void Handle(IPersonalEvent personalEvent)
        {
            if (personalEvent is TakeHitPersonalEvent hitEvent) Handle(hitEvent);
        }
    }
}