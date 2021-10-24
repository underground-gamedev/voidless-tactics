using Battle.Components;

namespace Battle
{
    public class CheckDeathBehaviour : IBehaviour<TakeHitPersonalEvent>
    {
        private IEntity character;
        public int HandlePriority => (int)BehaviourPriority.CheckDeath;
        
        public CheckDeathBehaviour(IEntity parent)
        {
            character = parent;
        }
        
        public void Handle(TakeHitPersonalEvent _)
        {
            var stats = character.Stats();
            
            if (!(stats.Get(StatType.CurrentHealth)?.ModifiedValue <= 0)) return;
            
            var emitter = character.Emitter();
            emitter?.Emit(new DeathCharacterGameEvent(character));
        }

        
        public void Handle(IPersonalEvent personalEvent)
        {
            if (personalEvent is TakeHitPersonalEvent takeHit) Handle(takeHit);
        }
    }
}