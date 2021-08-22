using System.Collections.Generic;

namespace Battle
{
    public interface ISpellComponent
    {
        List<string> GetAvailableSpellNames();
        ISpell GetSpellByName(string name);
        bool CastSpellAvailable();
    }
}