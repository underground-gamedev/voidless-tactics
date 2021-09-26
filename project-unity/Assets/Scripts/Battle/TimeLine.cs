using System;
using JetBrains.Annotations;

namespace Battle
{
    public class TimeLine
    {
        private PriorityQueue<ICharacter> queue;

        public event Action OnOrderChanged;
        public event Action<ICharacter> OnActiveChanged;
        [CanBeNull] public ICharacter Active => queue.Active;

        public TimeLine()
        {
            queue = new PriorityQueue<ICharacter>();
            queue.OnQueueChanged += OnQueueChangedTrigger;
            queue.OnActiveChanged += OnActiveChangedTrigger;
        }

        public void Add([NotNull] ICharacter character, float initiative)
        {
            queue.Set(character, initiative);
        }

        public void Remove([NotNull] ICharacter character)
        {
            queue.Remove(character);
        }

        private void OnQueueChangedTrigger()
        {
            OnOrderChanged?.Invoke();
        }

        private void OnActiveChangedTrigger()
        {
            OnActiveChanged?.Invoke(Active);
        }
    }
}