using System;
using System.Collections.Generic;

namespace Battle
{
    public interface ITeamsHolder
    {
        event Action<ITeam> OnTeamAdded;
        event Action<ITeam> OnTeamRemoved;

        IReadOnlyList<ITeam> Teams { get; }

        void AddTeam(ITeam team);
        void RemoveTeam(ITeam team);
    }
}