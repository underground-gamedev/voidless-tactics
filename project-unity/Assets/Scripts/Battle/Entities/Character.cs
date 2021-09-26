using System;
using System.Collections.Generic;
using Core.Components;
using JetBrains.Annotations;

namespace Battle
{
    public class Character: Entity, ICharacter
    {
        private readonly List<ITurnWatcher> turnWatchers = new List<ITurnWatcher>();
        private readonly List<IRoundWatcher> roundWatchers = new List<IRoundWatcher>();

        public IStatComponent Stats { get; }
        public IActiveSkillComponent Skills { get; }
        public IBehaviourComponent Behaviours { get; }

        public Character()
        {
            OnNewComponentAttached( 
                TryAddToTurnWatcherCache,
                TryAddToRoundWatcherCache,
                TryCallAttachedToCharacter
                );
            
            OnComponentCompleteDeAttached(
                TryRemoveFromTurnWatcherCache,
                TryRemoveFromRoundWatcherCache,
                TryCallDeAttachedFromCharacter
                );
        }
        
        public void OnTurnStart()
        {
            foreach (var turnWatcher in turnWatchers)
            {
                turnWatcher.OnTurnStart();
            }
        }

        public void OnTurnEnd()
        {
            foreach (var turnWatcher in turnWatchers)
            {
                turnWatcher.OnTurnEnd();
            }
        }

        public void OnRoundStart()
        {
            foreach (var roundWatcher in roundWatchers)
            {
                roundWatcher.OnRoundStart();
            }
        }

        public void OnRoundEnd()
        {
            foreach (var roundWatcher in roundWatchers)
            {
                roundWatcher.OnRoundEnd();
            }
        }

        private void TryAddToTurnWatcherCache(IComponent com)
        {
            if (com is ITurnWatcher tw)
            {
                turnWatchers.Add(tw);
            }
        }

        private void TryAddToRoundWatcherCache(IComponent com)
        {
            if (com is IRoundWatcher rw)
            {
                roundWatchers.Add(rw);
            }
        }

        private void TryCallAttachedToCharacter(IComponent com)
        {
            if (com is ICharacterAttachable ca)
            {
                ca.OnAttached(this);
            }
        }

        private void TryRemoveFromTurnWatcherCache(IComponent com)
        {
            if (com is ITurnWatcher tw)
            {
                turnWatchers.Remove(tw);
            }
        }

        private void TryRemoveFromRoundWatcherCache(IComponent com)
        {
            if (com is IRoundWatcher rw)
            {
                roundWatchers.Remove(rw);
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