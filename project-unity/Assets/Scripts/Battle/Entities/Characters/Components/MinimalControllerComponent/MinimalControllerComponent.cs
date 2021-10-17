using System;
using Core.Components;

namespace Battle.Components.MinimalControllerComponent
{
    public class MinimalControllerComponent : IComponent, ICharacterAttachable
    {
        private SkipTurnBehaviour skipTurnBehaviour;
        private ICharacter character;
        public void OnAttached(ICharacter character)
        {
            skipTurnBehaviour = new SkipTurnBehaviour(character);
            character.Behaviours.Add(skipTurnBehaviour);
            this.character = character;
        }

        public void OnDeAttached()
        {
            character.Behaviours.Remove(skipTurnBehaviour);
            character = null;
        }
        
        private class SkipTurnBehaviour : IBehaviour
        {
            private ICharacter character;
            public SkipTurnBehaviour(ICharacter character)
            {
                this.character = character;
            }
            
            public int HandlePriority => 0;
            
            public void Handle(IPersonalEvent personalEvent)
            {
                if (!RespondTo(personalEvent.GetType())) return;

                var emitter = character.GetComponent<IGlobalEventEmitter>();
                emitter?.Emit(new EndTurnGameEvent(character));
            }

            public bool RespondTo(Type eventType)
            {
                return eventType == typeof(TakeTurnPersonalEvent);
            }
        }
    }
}