using System;
using System.Collections.Generic;
using VoidLess.Core.Entities;
using VoidLess.Utils.Collections;

namespace VoidLess.Game.TimeLine
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