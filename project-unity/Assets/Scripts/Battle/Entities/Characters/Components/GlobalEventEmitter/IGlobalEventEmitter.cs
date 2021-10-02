using Core.Components;

namespace Battle
{
    public interface IGlobalEventEmitter: IComponent
    {
        public void Emit(IGlobalEvent globalEvent);
    }
}