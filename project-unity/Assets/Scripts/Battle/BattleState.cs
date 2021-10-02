using Battle.EventSystem;
using Battle.Map.Interfaces;

namespace Battle
{
    public class BattleState
    {
        public IEventQueue EventQueue { get; }
        public ILayeredMapHolder Map { get; }
        public ICharactersHolder Characters { get; }
        public ITeamsHolder Teams { get; }
        public ITimeLine TimeLine { get; }

        public BattleState()
        {
            EventQueue = new EventSystem.EventQueue();
            Map = new MapHolder();
            Characters = new CharactersHolder();
            Teams = new TeamsHolder();
            TimeLine = new TimeLine();
        }
    }
}