using System;
using Battle.Components;

namespace Battle
{
    public class SubtractHealthBehaviour : IBehaviour
    {
        private ICharacter character;

        public SubtractHealthBehaviour(ICharacter parent)
        {
            character = parent;
        }
            
        public int HandlePriority => (int)BehaviourPriority.SubtractHealth;
            
        public void Handle(IPersonalEvent personalEvent)
        {
            if (!(personalEvent is TakeHitPersonalEvent hitEvent)) return;
            var health = character.Stats.Get(StatType.CurrentHealth);
            if (health != null)
            {
                character.Stats.Remove(StatType.CurrentHealth);
                character.Stats.Add(StatType.CurrentHealth, health + -hitEvent.Value);
                character.GetGlobalEmitter()?.Emit(new DamagedGameEvent(character, hitEvent.Value));
            }
        }

        public bool RespondTo(Type eventType)
        {
            return eventType == typeof(TakeHitPersonalEvent);
        }
    }
}