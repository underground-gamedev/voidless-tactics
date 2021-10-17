using Battle.Characters.Behaviours;
using Core.Components;

namespace Battle.Components.InitiativeComponent
{
    public class InitiativeComponent : IComponent, ICharacterAttachable
    {
        private int minInitiative;
        private int maxInitiative;

        private ICharacter character;
        private TurnWaitBehaviour turnWaitBehaviour;
        public InitiativeComponent(int min, int max)
        {
            minInitiative = min;
            maxInitiative = max;
        }

        public void OnAttached(ICharacter character)
        {
            if (character == null) return;
            
            this.character = character;
            character.Stats.Add(StatType.MinInitiative, new EntityStat(minInitiative));
            character.Stats.Add(StatType.MaxInitiative, new EntityStat(maxInitiative));
            turnWaitBehaviour = new TurnWaitBehaviour(character);
            character.Behaviours.Add(turnWaitBehaviour);
        }

        public void OnDeAttached()
        {
            if (character == null) return;
            
            character.Stats.Remove(StatType.MinInitiative);
            character.Stats.Remove(StatType.MaxInitiative);
            character.Behaviours.Remove(turnWaitBehaviour);
            this.character = null;
        }
    }
}