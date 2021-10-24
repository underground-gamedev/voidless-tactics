using System;
using System.Collections.Generic;

namespace Battle
{
    public class TimeLine: ITimeLine
    {
        private PriorityQueue<IEntity> queue;
        public event Action OnOrderChanged;
        public event Action<IEntity> OnActiveChanged;
        public IReadOnlyList<IEntity> Order => queue.Elements;
        public IEntity Active => queue.Active;

        public TimeLine()
        {
            queue = new PriorityQueue<IEntity>();
            queue.OnQueueChanged += OnQueueChangedTrigger;
            queue.OnActiveChanged += OnActiveChangedTrigger;
        }

        public void Set(IEntity character, float initiative)
        {
            queue.Set(character, initiative);
        }

        public void Remove(IEntity character)
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