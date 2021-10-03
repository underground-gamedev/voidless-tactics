namespace Battle
{
    public class DeathCharacterGameEvent: IGlobalEvent
    {
        public readonly IEntity Sender;

        public DeathCharacterGameEvent(IEntity sender)
        {
            Sender = sender;
        }
    }
}