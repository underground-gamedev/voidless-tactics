using Battle.Components;

namespace Battle
{
    public class SubtractHealthBehaviour : IBehaviour<TakeHitPersonalEvent>
    {
        private ICharacter character;

        public SubtractHealthBehaviour(ICharacter parent)
        {
            character = parent;
        }

        public int HandlePriority => (int)BehaviourPriority.SubtractHealth;
            
        public void Handle(TakeHitPersonalEvent hitEvent)
        {
            var health = character.Stats.Get(StatType.CurrentHealth);
            if (health == null) return;
            character.Stats.Remove(StatType.CurrentHealth);
            character.Stats.Add(StatType.CurrentHealth, health + -hitEvent.Value);
            character.GetGlobalEmitter()?.Emit(new DamagedGameEvent(character, hitEvent.Value));
        }

        public void Handle(IPersonalEvent personalEvent)
        {
            if (personalEvent is TakeHitPersonalEvent hitEvent) Handle(hitEvent);
        }
    }
}