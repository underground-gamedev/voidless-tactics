namespace Battle
{
    public class WaitTurnGameEvent : IGlobalEvent
    {
        public readonly IEntity Character;
        public readonly float Initiative;
        
        public WaitTurnGameEvent(IEntity character, float initiative)
        {
            Character = character;
            Initiative = initiative;
        }
    }
}