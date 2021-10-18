using System;

namespace Battle.Characters.Behaviours
{
    public class TurnWaitBehaviour : IBehaviour<StartRoundGameEvent>
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

        public void Handle(StartRoundGameEvent _)
        {
            OnWait?.Invoke(minInitiative);
        }

        public void Handle(IPersonalEvent personalEvent)
        {
            if (personalEvent is StartRoundGameEvent startRound) Handle(startRound);
        }
    }
}