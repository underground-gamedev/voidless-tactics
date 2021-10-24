namespace Battle
{
    public class EndTurnGameEvent : IGlobalEvent
    {
        public readonly IEntity Character;

        public EndTurnGameEvent(IEntity character)
        {
            Character = character;
        }
    }
}