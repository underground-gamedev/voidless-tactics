namespace Battle
{
    public interface ITurnWatcher
    {
        void OnTurnStart();
        void OnTurnEnd();
    }
}