using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Battle
{
    [RequireComponent(typeof(MapObject), typeof(BasicCharacterStats))]
    public class Character : MonoBehaviour, ITurnWatcher, IRoundWatcher
    {
        public UnityEvent DeathTrigger;

        [HideInInspector]
        public Team Team;

        private MapObject mapObject;
        public MapObject MapObject => mapObject ??= GetComponent<MapObject>();

        private BasicCharacterStats stats;
        public BasicCharacterStats BasicStats => stats ??= GetComponent<BasicCharacterStats>();

        private IAttackComponent attackComponent;
        public IAttackComponent AttackComponent => attackComponent ??= GetComponent<IAttackComponent>();

        private ITargetComponent targetComponent;
        public ITargetComponent TargetComponent => targetComponent ??= GetComponent<ITargetComponent>();

        private IWaitComponent waitComponent;
        public IWaitComponent WaitComponent => waitComponent ??= GetComponent<IWaitComponent>();

        private IMoveComponent moveComponent;
        public IMoveComponent MoveComponent => moveComponent ??= GetComponent<IMoveComponent>();

        private ISpellComponent spellComponent;
        public ISpellComponent SpellComponent => spellComponent ??= GetComponent<ISpellComponent>();

        private IManaContainerComponent manaContainerComponent;
        public IManaContainerComponent ManaContainerComponent => manaContainerComponent ??= GetComponent<IManaContainerComponent>();

        private IManaGiveComponent manaGiveComponent;
        public IManaGiveComponent ManaGiveComponent => manaGiveComponent ??= GetComponent<IManaGiveComponent>();

        private AIComponent aiComponent;
        public AIComponent AIComponent => aiComponent ??= GetComponent<AIComponent>();

        private List<ITurnWatcher> turnWatchers;
        public IReadOnlyList<ITurnWatcher> TurnWatchers => turnWatchers ??= GetComponents<ITurnWatcher>().Where(watcher => !watcher.Equals(this)).ToList();

        private List<IRoundWatcher> roundWatchers;
        public IReadOnlyList<IRoundWatcher> RoundWatchers => roundWatchers ??= GetComponents<IRoundWatcher>().Where(watcher => !watcher.Equals(this)).ToList();

        public void Death()
        {
            DeathTrigger?.Invoke();
            DeathTrigger?.RemoveAllListeners();
            Destroy(gameObject);
        }

        public void OnTurnStart()
        {
            foreach(var watcher in TurnWatchers)
            {
                watcher.OnTurnStart();
            }
        }

        public void OnTurnEnd()
        {
            foreach(var watcher in TurnWatchers)
            {
                watcher.OnTurnEnd();
            }
        }

        public void OnRoundStart()
        {
            foreach(var watcher in RoundWatchers)
            {
                watcher.OnRoundStart();
            }
        }

        public void OnRoundEnd()
        {
            foreach(var watcher in RoundWatchers)
            {
                watcher.OnRoundEnd();
            }
        }
    }
}