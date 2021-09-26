namespace Battle
{
    public interface IRoundWatcher
    {
        void OnRoundStart();
        void OnRoundEnd();
    }
}