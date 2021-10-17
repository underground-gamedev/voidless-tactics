using System;
using Battle.Components.RandomGeneratorComponent;
using Battle.Systems.RandomSystem;

namespace Battle.Characters.Behaviours
{
    public class TurnWaitBehaviour : IBehaviour
    {
        public event Action<int> OnWait;
        
        public int HandlePriority => 0;
        
        private ICharacter character;

        private int minInitiative;
        private int maxInitiative;
        
        public TurnWaitBehaviour(ICharacter character)
        {
            this.character = character;
        }

        public TurnWaitBehaviour(int minInitiative, int maxInitiative)
        {
            this.minInitiative = minInitiative;
            this.maxInitiative = maxInitiative;
        }

        public void Handle(IPersonalEvent personalEvent)
        {
            if (!RespondTo(personalEvent.GetType())) return;
            
            OnWait?.Invoke(this.minInitiative);
        }

        public bool RespondTo(Type eventType)
        {
            return eventType == typeof(StartRoundGameEvent);
        }

    }
}