using VoidLess.Core.Entities;

namespace VoidLess.Game.EventSystem.GlobalEvents.GameEvents
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