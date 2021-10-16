using System;
using Battle.Components.RandomGeneratorComponent;
using Battle.Systems.RandomSystem;
using Core.Components;

namespace Battle.Components.InitiativeComponent
{
    public class InitiativeComponent : IComponent, ICharacterAttachable
    {
        private int minInitiative;
        private int maxInitiative;

        private ICharacter character;
        
        public InitiativeComponent(int min, int max)
        {
            minInitiative = min;
            maxInitiative = max;
        }

        public void OnAttached(ICharacter character)
        {
            if (character == null) return;
            
            this.character = character;
            character.Stats.Add(StatType.MinInitiative, new EntityStat(minInitiative));
            character.Stats.Add(StatType.MaxInitiative, new EntityStat(maxInitiative));
        }

        public void OnDeAttached()
        {
            if (character == null) return;
            
            character.Stats.Remove(StatType.MinInitiative);
            character.Stats.Remove(StatType.MaxInitiative);
            this.character = null;
        }

        private class TurnWaitBehaviour : IBehaviour
        {
            public int HandlePriority => 0;
            
            private ICharacter character;
            
            public TurnWaitBehaviour(ICharacter character)
            {
                this.character = character;
            }
            
            public void Handle(IPersonalEvent personalEvent)
            {
                if (!RespondTo(personalEvent.GetType())) return;

                var minInitiative = character.Stats.Get(StatType.MinInitiative);
                var maxInitiative = character.Stats.Get(StatType.MaxInitiative);

                var random = character.GetComponent<IRandomGeneratorComponent>();
                var currentInitiative = random?.NextFloat(minInitiative.Value, maxInitiative.Value) ?? (maxInitiative.Value + minInitiative.Value) / 2;

                var emitter = character.GetComponent<IGlobalEventEmitter>();
                
                emitter?.Emit(new WaitTurnGameEvent(character, currentInitiative));
            }

            public bool RespondTo(Type eventType)
            {
                return eventType == typeof(TakeTurnPersonalEvent);
            }
        }
    }
}