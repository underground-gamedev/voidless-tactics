using VoidLess.Core.Entities;

namespace VoidLess.Game.EventSystem.GlobalEvents.GameEvents
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