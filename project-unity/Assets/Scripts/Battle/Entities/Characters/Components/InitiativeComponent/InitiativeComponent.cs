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
            character.Stats.Add(StatType.MinInitiative, new Stat(minInitiative));
            character.Stats.Add(StatType.MaxInitiative, new Stat(maxInitiative));
            turnWaitBehaviour = new TurnWaitBehaviour(minInitiative, maxInitiative);
            turnWaitBehaviour.OnWait += EmitWaitToGlobal;
            character.Behaviours.Add(turnWaitBehaviour);
        }

        public void OnDeAttached()
        {
            if (character == null) return;
            
            character.Stats.Remove(StatType.MinInitiative);
            character.Stats.Remove(StatType.MaxInitiative);
            character.Behaviours.Remove(turnWaitBehaviour);
            turnWaitBehaviour.OnWait -= EmitWaitToGlobal;
            this.character = null;
        }

        private void EmitWaitToGlobal(int initiative)
        {
            var emitter = character?.GetComponent<IGlobalEventEmitter>();
            emitter?.Emit(new WaitTurnGameEvent(character, initiative));
        }
    }
}