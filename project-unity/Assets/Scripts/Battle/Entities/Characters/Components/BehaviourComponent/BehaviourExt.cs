namespace Battle
{
    public static class BehaviourExt
    {
        public static bool RespondTo<T>(this IBehaviour behaviour) where T : IPersonalEvent
        {
            return behaviour.RespondTo(typeof(T));
        }

        public static bool RespondTo<T>(this IBehaviourComponent behaviourCom) where T : IPersonalEvent
        {
            return behaviourCom.RespondTo(typeof(T));
        }
    }
}