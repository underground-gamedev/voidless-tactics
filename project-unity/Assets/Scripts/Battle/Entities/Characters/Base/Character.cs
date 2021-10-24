using Core.Components;

namespace Battle
{
    public class Character: Entity, ICharacter
    {
        public Character()
        {
            OnNewComponentAttached(TryCallAttachedToCharacter);
            OnComponentCompleteDeAttached(TryCallDeAttachedFromCharacter);

            AddComponent(new StatComponent());
            AssociateComponent(typeof(IStatComponent), typeof(StatComponent));
        }
        
        private void TryCallAttachedToCharacter(IComponent com)
        {
            if (com is ICharacterAttachable ca)
            {
                ca.OnAttached(this);
            }
        }

        private void TryCallDeAttachedFromCharacter(IComponent com)
        {
            if (com is ICharacterAttachable ca)
            {
                ca.OnDeAttached();
            }
        }
    }
}