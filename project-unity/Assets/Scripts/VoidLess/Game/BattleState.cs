using VoidLess.Game.Entities.Characters.Base;
using VoidLess.Game.Entities.Teams.Base;
using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.Map;
using VoidLess.Game.Map.Base;
using VoidLess.Game.TimeLine;

namespace VoidLess.Game
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
            EventQueue = new EventQueue(this);
            Map = new MapHolder();
            Characters = new CharactersHolder();
            Teams = new TeamsHolder();
            TimeLine = new TimeLine.TimeLine();
        }
    }
}