using Battle.EventSystem;
using Battle.Map.Interfaces;

namespace Battle
{
    public class BattleState
    {
        public IEventQueue EventQueue { get; }
        public ILayeredMapHolder Map { get; }
        public IEntitysHolder Characters { get; }
        public ITeamsHolder Teams { get; }
        public ITimeLine TimeLine { get; }

        public BattleState()
        {
            EventQueue = new EventQueue(this);
            Map = new MapHolder();
            Characters = new CharactersHolder();
            Teams = new TeamsHolder();
            TimeLine = new TimeLine();
        }
    }
}