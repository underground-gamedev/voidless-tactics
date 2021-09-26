using System.Collections.Generic;
using Battle.Map.Interfaces;

namespace Battle
{
    public class BattleState
    {
        public ILayeredMap Map { get; }
        public IList<ICharacter> Characters { get; }
        public IList<ITeam> Teams { get; }
        public TimeLine TimeLine { get; }

        public BattleState(ILayeredMap map)
        {
            Map = map;
            Characters = new List<ICharacter>();
            Teams = new List<ITeam>();
            TimeLine = new TimeLine();
        }
    }
}