using Battle.Components;

namespace Battle
{
    public class CheckDeathBehaviour : IBehaviour<TakeHitPersonalEvent>
    {
        private ICharacter character;
        public int HandlePriority => (int)BehaviourPriority.CheckDeath;
        
        public CheckDeathBehaviour(ICharacter parent)
        {
            character = parent;
        }
        
        public void Handle(TakeHitPersonalEvent _)
        {
            var stats = character.GetStatComponent();
            
            if (!(stats.Get(StatType.CurrentHealth)?.ModifiedValue <= 0)) return;
            
            var emitter = character.GetGlobalEmitter();
            emitter?.Emit(new DeathCharacterGameEvent(character));
        }

        
        public void Handle(IPersonalEvent personalEvent)
        {
            if (personalEvent is TakeHitPersonalEvent takeHit) Handle(takeHit);
        }
    }
}