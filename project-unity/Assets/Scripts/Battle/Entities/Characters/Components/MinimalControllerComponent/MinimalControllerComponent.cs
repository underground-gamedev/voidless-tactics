using Core.Components;

namespace Battle.Components.MinimalControllerComponent
{
    public class MinimalControllerComponent : IComponent, IEntityAttachable
    {
        private SkipTurnBehaviour skipTurnBehaviour;
        private IEntity character;
        public void OnAttached(IEntity character)
        {
            skipTurnBehaviour = new SkipTurnBehaviour(character);
            character.AddBehaviour(skipTurnBehaviour);
            this.character = character;
        }

        public void OnDeAttached()
        {
            character.RemoveBehaviour(skipTurnBehaviour);
            character = null;
        }
        
        private class SkipTurnBehaviour : IBehaviour<TakeTurnPersonalEvent>
        {
            private IEntity character;
            public SkipTurnBehaviour(IEntity character)
            {
                this.character = character;
            }

            public void Handle(TakeTurnPersonalEvent _)
            {
                var emitter = character.GetComponent<IGlobalEventEmitter>();
                emitter?.Emit(new EndTurnGameEvent(character));
            }

            public int HandlePriority => 0;
            
            public void Handle(IPersonalEvent personalEvent)
            {
                if (personalEvent is TakeTurnPersonalEvent takeTurn) Handle(takeTurn);
            }
        }
    }
}