using System.Collections.Generic;

public interface ISpellComponent
{
    List<string> GetAvailableSpellNames();
    ISpell GetSpellByName(string name);
    bool CastSpellAvailable();
}