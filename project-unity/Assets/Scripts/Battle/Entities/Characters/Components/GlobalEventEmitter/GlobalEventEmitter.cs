using Battle.EventSystem;

namespace Battle
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