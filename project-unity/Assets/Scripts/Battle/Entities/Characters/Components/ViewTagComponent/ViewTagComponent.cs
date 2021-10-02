using Core.Components;

namespace Battle.Components.ViewTagComponent
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