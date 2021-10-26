using System;
using VoidLess.Core.Components.BehaviourComponent;

namespace VoidLess.Game.Entities.Characters.PersonalEvents
{
    public class TakeHitPersonalEvent: IPersonalEvent
    {
        private int value;
        public int Value
        {
            get => value;
            set => this.value = Math.Max(0, value);
        }

        public TakeHitPersonalEvent(int value)
        {
            Value = value;
        }
    }
}