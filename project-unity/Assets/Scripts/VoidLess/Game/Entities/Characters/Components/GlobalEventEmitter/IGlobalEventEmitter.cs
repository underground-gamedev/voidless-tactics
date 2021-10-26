using VoidLess.Core.Components;
using VoidLess.Game.EventSystem.GlobalEvents;

namespace VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter
{
    public interface IGlobalEventEmitter: IComponent
    {
        public void Emit(IGlobalEvent globalEvent);
    }
}