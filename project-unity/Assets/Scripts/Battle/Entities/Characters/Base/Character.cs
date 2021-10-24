using Core.Components;

namespace Battle
{
    public class Character: Entity, ICharacter
    {
        public IStatComponent Stats => this.GetComponent<IStatComponent>();
        public IActiveSkillComponent Skills => this.GetComponent<IActiveSkillComponent>();

        public Character()
        {
            OnNewComponentAttached(TryCallAttachedToCharacter);
            OnComponentCompleteDeAttached(TryCallDeAttachedFromCharacter);

            AddComponent(new StatComponent());
            AssociateComponent(typeof(IStatComponent), typeof(StatComponent));
            AddComponent(new ActiveSkillComponent());
            AssociateComponent(typeof(IActiveSkillComponent), typeof(ActiveSkillComponent));
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