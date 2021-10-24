using Core.Components;

namespace Battle
{
    public class Character: Entity, ICharacter
    {
        public IStatComponent Stats => this.GetComponent<IStatComponent>();
        public IActiveSkillComponent Skills => this.GetComponent<IActiveSkillComponent>();
        public IBehaviourComponent Behaviours => this.GetComponent<IBehaviourComponent>();

        public Character()
        {
            OnNewComponentAttached(TryCallAttachedToCharacter);
            OnComponentCompleteDeAttached(TryCallDeAttachedFromCharacter);

            AddComponent(typeof(IStatComponent), new StatComponent());
            AddComponent(typeof(IActiveSkillComponent), new ActiveSkillComponent());
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