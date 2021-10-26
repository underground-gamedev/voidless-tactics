using VoidLess.Core.Entities;

namespace VoidLess.Game.EventSystem.GlobalEvents.GameEvents
{
    public class DamagedGameEvent: IGlobalEvent
    {
        public readonly IEntity Sender;
        public readonly int Value;

        public DamagedGameEvent(IEntity sender, int value)
        {
            Sender = sender;
            Value = value;
        }
    }
}