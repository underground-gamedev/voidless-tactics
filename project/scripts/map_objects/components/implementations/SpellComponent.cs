using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class SpellComponent : Node, ISpellComponent
{
    private Character character;
    private Dictionary<ManaType, List<ModularSpell>> spells;
    private List<ModularSpell> activeSpells {
        get {
            var manaType = character.GetManaContainerComponent()?.ManaType;
            if (manaType == null || !spells.ContainsKey(manaType.Value)) return new List<ModularSpell>();
            return spells[manaType.Value];
        }
    }

    public bool CastSpellAvailable()
    {
        return activeSpells.Any(spell => spell.CastAvailable());
    }

    public List<string> GetAvailableSpellNames()
    {
        return activeSpells.Where(spell => spell.CastAvailable())
                     .Select(spell => spell.SpellName)
                     .ToList();
    }

    public ISpell GetSpellByName(string name)
    {
        return activeSpells.FirstOrDefault(spell => spell.SpellName == name);
    }

    public override void _Ready()
    {
        character = this.FindParent<Character>();
        spells = ConfiguredSpellBindings.DefaultSpellBindings.BindSpells(this);
    }
}