using VoidLess.Core.Components.BehaviourComponent;
using VoidLess.Core.Entities;
using VoidLess.Core.Stats;
using VoidLess.Game.Entities.Characters.PersonalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;

namespace VoidLess.Game.Entities.Characters.Components.HealthComponent
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