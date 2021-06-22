using System.Collections.Generic;
using System.Linq;
using Godot;

public class SpellComponent : Node, ISpellComponent
{
    private Character character;

    private List<ISpell> Spells => this.GetChilds<ISpell>(".");

    public bool CastSpellAvailable()
    {
        return Spells.Any(spell => spell.CastAvailable());
    }

    public List<string> GetAvailableSpellNames()
    {
        return Spells.Where(spell => spell.CastAvailable())
                     .Select(spell => (spell as Node).Name)
                     .ToList();
    }

    public ISpell GetSpellByName(string name)
    {
        return Spells.FirstOrDefault(spell => (spell as Node).Name == name);
    }

    public override void _Ready()
    {
        character = this.FindParent<Character>();
    }
}