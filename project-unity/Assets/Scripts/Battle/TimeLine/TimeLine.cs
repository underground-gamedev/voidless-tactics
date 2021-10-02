using System;
using System.Collections.Generic;

namespace Battle
{
    public class TimeLine: ITimeLine
    {
        private PriorityQueue<ICharacter> queue;
        public event Action OnOrderChanged;
        public event Action<ICharacter> OnActiveChanged;
        public IReadOnlyList<ICharacter> Order => queue.Elements;
        public ICharacter Active => queue.Active;

        public TimeLine()
        {
            queue = new PriorityQueue<ICharacter>();
            queue.OnQueueChanged += OnQueueChangedTrigger;
            queue.OnActiveChanged += OnActiveChangedTrigger;
        }

        public void Set(ICharacter character, float initiative)
        {
            queue.Set(character, initiative);
        }

        public void Remove(ICharacter character)
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