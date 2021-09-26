namespace Battle
{
    public static class BehaviourExt
    {
        public static bool RespondTo<T>(this IBehaviour behaviour) where T : IGameEvent
        {
            return behaviour.RespondTo(typeof(T));
        }

        public static bool RespondTo<T>(this IBehaviourComponent behaviourCom) where T : IGameEvent
        {
            return behaviourCom.RespondTo(typeof(T));
        }
    }
}