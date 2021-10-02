namespace Battle
{
    public class DamagedGlobalEvent: IGlobalEvent
    {
        public IEntity Sender { get; }
        public int Value { get; }

        public DamagedGlobalEvent(IEntity sender, int value)
        {
            Sender = sender;
            Value = value;
        }
    }
}