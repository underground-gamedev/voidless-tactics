using System;
using Battle.Components;

namespace Battle
{
    public class CheckDeathBehaviour : IBehaviour
    {
        private ICharacter character;
        public int HandlePriority => (int)BehaviourPriority.CheckDeath;
        
        public CheckDeathBehaviour(ICharacter parent)
        {
            character = parent;
        }
        
        public void Handle(IPersonalEvent personalEvent)
        {
            var stats = character.Stats;
            if (stats.Get(StatType.CurrentHealth)?.Value <= 0)
            {
                var emitter = character.GetGlobalEmitter();
                emitter?.Emit(new DeathCharacterGameEvent(character));
            }
        }

        public bool RespondTo(Type eventType)
        {
            return true;
        }
    }
}