using System;
using System.Collections.Generic;

namespace VoidLess.Game.Entities.Teams.Base
{
    public class TeamsHolder : ITeamsHolder
    {
        public event Action<ITeam> OnTeamAdded;
        public event Action<ITeam> OnTeamRemoved;

        private List<ITeam> teams = new List<ITeam>();
        public IReadOnlyList<ITeam> Teams => teams;
        
        public void AddTeam(ITeam team)
        {
            teams.Add(team);
            OnTeamAdded?.Invoke(team);
        }

        public void RemoveTeam(ITeam team)
        {
            teams.Remove(team);
            OnTeamRemoved?.Invoke(team);
        }
    }
}