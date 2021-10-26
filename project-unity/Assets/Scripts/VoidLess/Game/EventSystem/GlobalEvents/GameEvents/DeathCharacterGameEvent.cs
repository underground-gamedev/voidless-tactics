using VoidLess.Core.Entities;

namespace VoidLess.Game.EventSystem.GlobalEvents.GameEvents
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