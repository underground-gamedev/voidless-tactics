namespace Battle
{
    public class DeathCharacterGlobalEvent: IGlobalEvent
    {
        public IEntity Sender { get; }

        public DeathCharacterGlobalEvent(IEntity sender)
        {
            Sender = sender;
        }
    }
}