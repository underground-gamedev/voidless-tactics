namespace Battle
{
    public class WaitTurnGameEvent : IGlobalEvent
    {
        public readonly ICharacter Character;
        public readonly float Initiative;
        
        public WaitTurnGameEvent(ICharacter character, float initiative)
        {
            Character = character;
            Initiative = initiative;
        }
    }
}