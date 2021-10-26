using VoidLess.Core.Components;
using VoidLess.Game.Entities.Characters.Base;

namespace VoidLess.Game.Entities.Characters.Components.ViewTagComponent
{
    public class ViewTagComponent : IComponent
    {
        public CharacterViewTag ViewTag { get; }
        
        public ViewTagComponent(CharacterViewTag viewTag)
        {
            ViewTag = viewTag;
        }
    }
}