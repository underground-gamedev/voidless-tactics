namespace Battle
{
    public class NeedActionGameEvent : IGlobalEvent
    {
        public readonly IEntity Character;

        public NeedActionGameEvent(IEntity character)
        {
            Character = character;
        }
    }
}