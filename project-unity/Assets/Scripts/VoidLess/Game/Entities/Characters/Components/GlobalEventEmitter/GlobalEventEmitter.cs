using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.EventSystem.GlobalEvents;

namespace VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter
{
    public class GlobalEventEmitter: IGlobalEventEmitter
    {
        private IGlobalEventHandler handler;
        
        public GlobalEventEmitter(IGlobalEventHandler handler)
        {
            this.handler = handler;
        }
        
        public void Emit(IGlobalEvent globalEvent)
        {
            handler?.Handle(globalEvent);
        }
    }
}