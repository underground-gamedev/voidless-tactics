using System;

namespace Battle.Characters.Behaviours
{
    public class TurnWaitBehaviour : IBehaviour
    {
        public event Action<int> OnWait;
        
        public int HandlePriority => 0;
        
        private int minInitiative;
        private int maxInitiative;

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