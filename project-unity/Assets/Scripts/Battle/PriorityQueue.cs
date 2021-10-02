using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Battle
{
    public class PriorityQueue<T>
    {
        private readonly List<KeyValuePair<float, T>> queue;
        public List<T> Elements => queue.Select(pair => pair.Value).Reverse().ToList();
        [CanBeNull]
        public T Active => queue.Count == 0 ? default : queue.Last().Value;
        
        public event Action OnQueueChanged;
        public event Action OnActiveChanged;

        public PriorityQueue()
        {
            queue = new List<KeyValuePair<float, T>>();
        }

        public void Set([NotNull] T newElem, float priority = 0)
        {
            var existedPair = queue.FirstOrDefault(elem => elem.Value.Equals(newElem));
            if (existedPair.Value != null)
            {
                if (Mathf.Abs(existedPair.Key - priority) < Mathf.Epsilon) return;
                queue.Remove(existedPair);
            }

            var insertIndex = queue
                .TakeWhile(pair => priority > pair.Key && Mathf.Abs(pair.Key - priority) > Mathf.Epsilon)
                .Count();
            queue.Insert(insertIndex, new KeyValuePair<float, T>(priority, newElem));
            
            OnQueueChanged?.Invoke();
            if (insertIndex == queue.Count - 1)
            {
                OnActiveChanged?.Invoke();
            }
        }

        public void Remove([NotNull] T delElem)
        {
            var active = Active;
            if (active == null) return;
            
            var existedPair = queue.FirstOrDefault(elem => elem.Value.Equals(delElem));
            if (existedPair.Value == null) return;
            
            queue.Remove(existedPair);
            OnQueueChanged?.Invoke();
            if (!active.Equals(Active))
            {
                OnActiveChanged?.Invoke();
            }
        }
    }
}