using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle
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

        public void Handle(IPersonalEvent personalEvent)
        {
            asyncBlockers.Push(personalEvent);
            delayedEvents.Add(personalEvent, new List<IPersonalEvent>());
            
            foreach (var behaviour in behaviours)
            {
                behaviour.Handle(personalEvent);
            }

            asyncBlockers.Pop();

            var delayed = delayedEvents[personalEvent].ToList();
            foreach (var delayedEvent in delayed)
            {
                Handle(delayedEvent);
                delayedEvents.Remove(delayedEvent);
            }
            
            delayedEvents.Remove(personalEvent);
        }

        public void DelayedHandle(IPersonalEvent personalEvent)
        {
            if (asyncBlockers.Count > 0)
            {
                var blocker = asyncBlockers.Peek();
                delayedEvents[blocker].Add(personalEvent);
            }
            else
            {
                Handle(personalEvent);
            }
        }

        public bool RespondTo(Type eventType)
        {
            return true;
        }
    }
}