namespace Battle
{
    public class NeedActionGameEvent : IGlobalEvent
    {
        public readonly ICharacter Character;

        public NeedActionGameEvent(ICharacter character)
        {
            Character = character;
        }
    }
}