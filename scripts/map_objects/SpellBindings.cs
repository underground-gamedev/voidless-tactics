using System.Collections.Generic;
using Godot;

public class SpellBindings
{
    private Dictionary<ManaType, string[]> bindings;
    public SpellBindings(Dictionary<ManaType, string[]> bindings)
    {
        this.bindings = bindings;
    }

    public Dictionary<ManaType, List<ModularSpell>> BindSpells(SpellComponent parent)
    {
        var result = new Dictionary<ManaType, List<ModularSpell>>();
        foreach (var item in bindings)
        {
            var manaType = item.Key;
            var spellPathes = item.Value;
            var resultSpells = new List<ModularSpell>();
            foreach (var path in spellPathes)
            {
                var packedSpell = (PackedScene)ResourceLoader.Load(path);
                var spell = (ModularSpell)packedSpell.Instance();
                parent.AddChild(spell);
                resultSpells.Add(spell);
            }
            result.Add(manaType, resultSpells);
        }
        return result;
    }
}