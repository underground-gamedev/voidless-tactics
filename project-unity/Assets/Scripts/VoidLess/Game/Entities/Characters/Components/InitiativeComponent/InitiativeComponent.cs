using VoidLess.Core.Components;
using VoidLess.Core.Entities;
using VoidLess.Core.Stats;
using VoidLess.Game.Entities.Characters.Behaviours;
using VoidLess.Game.Entities.Characters.Components.GlobalEventEmitter;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;

namespace VoidLess.Game.Entities.Characters.Components.InitiativeComponent
{
    public class InitiativeComponent : IComponent, IEntityAttachable
    {
        private int minInitiative;
        private int maxInitiative;

        private IEntity character;
        private TurnWaitBehaviour turnWaitBehaviour;
        public InitiativeComponent(int min, int max)
        {
            minInitiative = min;
            maxInitiative = max;
        }

        public void OnAttached(IEntity character)
        {
            if (character == null) return;
            
            this.character = character;
            var stats = character.Stats();
            stats.Add(StatType.MinInitiative, new Stat(minInitiative));
            stats.Add(StatType.MaxInitiative, new Stat(maxInitiative));
            turnWaitBehaviour = new TurnWaitBehaviour(minInitiative, maxInitiative);
            turnWaitBehaviour.OnWait += EmitWaitToGlobal;
            character.AddBehaviour(turnWaitBehaviour);
        }

        public void OnDeAttached()
        {
            if (character == null) return;
            
            var stats = character.Stats();
            stats.Add(StatType.MinInitiative, new Stat(minInitiative));
            stats.Remove(StatType.MinInitiative);
            stats.Remove(StatType.MaxInitiative);
            character.RemoveBehaviour(turnWaitBehaviour);
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