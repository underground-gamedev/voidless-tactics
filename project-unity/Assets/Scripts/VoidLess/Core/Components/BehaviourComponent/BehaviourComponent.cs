using System.Collections.Generic;
using System.Linq;

namespace VoidLess.Core.Components.BehaviourComponent
{
    public class BehaviourComponent: IBehaviourComponent
    {
        private List<IBehaviour> behaviours = new List<IBehaviour>();
        
        private Stack<IPersonalEvent> asyncBlockers = new Stack<IPersonalEvent>();
        private Dictionary<IPersonalEvent, List<IPersonalEvent>> delayedEvents =
            new Dictionary<IPersonalEvent, List<IPersonalEvent>>();

        public void Add(IBehaviour behaviour)
        {
            behaviours.Add(behaviour);
            behaviours = behaviours.OrderByDescending(beh => beh.HandlePriority).ToList();
        }

        public void Remove(IBehaviour behaviour)
        {
            behaviours.Remove(behaviour);
        }

        public void Handle<T>(T personalEvent) where T : IPersonalEvent
        {
            if (asyncBlockers.Count > 0)
            {
                var blocker = asyncBlockers.Peek();
                delayedEvents[blocker].Add(personalEvent);
            }
            else
            {
                HandleNow(personalEvent);
            }
        }

        public void HandleNow<T>(T personalEvent) where T : IPersonalEvent
        {
            asyncBlockers.Push(personalEvent);
            delayedEvents.Add(personalEvent, new List<IPersonalEvent>());
            
            foreach (var behaviour in behaviours)
            {
                if (behaviour is IBehaviour<T> specificBeh)
                {
                    specificBeh.Handle(personalEvent);
                }
            }

            asyncBlockers.Pop();

            var delayed = delayedEvents[personalEvent].ToList();
            foreach (var delayedEvent in delayed)
            {
                HandleNow(delayedEvent);
                delayedEvents.Remove(delayedEvent);
            }
            
            delayedEvents.Remove(personalEvent);
        }
    }
}