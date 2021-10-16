namespace Battle
{
    public class EndTurnGameEvent : IGlobalEvent
    {
        public readonly ICharacter Character;

        public EndTurnGameEvent(ICharacter character)
        {
            Character = character;
        }
    }
}