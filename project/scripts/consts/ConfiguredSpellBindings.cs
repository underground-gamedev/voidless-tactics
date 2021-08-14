using System.Collections.Generic;
using System.Linq;

public static class ConfiguredSpellBindings
{
    public const string PrefabPathPattern = "res://prefabs/spells/{0}.tscn";

    private static string[] ApplyPathPattern(params string[] pathArr)
    {
        return pathArr.Select(path => string.Format(PrefabPathPattern, path)).ToArray();
    }

    public static SpellBindings DefaultSpellBindings = new SpellBindings(new Dictionary<ManaType, string[]> {
        [ManaType.Nature] = ApplyPathPattern(
            "Heal"
        ),
        [ManaType.Magma] = ApplyPathPattern(
            "Bolt"
        ),
    });
}