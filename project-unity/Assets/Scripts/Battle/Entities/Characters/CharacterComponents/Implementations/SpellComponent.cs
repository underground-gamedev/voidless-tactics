using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
namespace Battle
{
    [RequireComponent(typeof(Character))]
    public class SpellComponent : MonoBehaviour, ISpellComponent
    {
        [SerializeField]
        private SpellBindings bindings;
        private Character character;
        private IReadOnlyList<ISpell> activeSpells => bindings.Bindings;

        public void Start()
        {
            character = GetComponent<Character>();
        }

        public bool CastSpellAvailable()
        {
            return activeSpells.Any(spell => spell.CastAvailable(character));
        }

        public List<string> GetAvailableSpellNames()
        {
            return activeSpells.Where(spell => spell is ModularSpell && spell.CastAvailable(character))
                         .Select(spell => (spell as ModularSpell).SpellName)
                         .ToList();
        }

        public ISpell GetSpellByName(string name)
        {
            return activeSpells.FirstOrDefault(spell => (spell as ModularSpell).SpellName == name);
        }
    }

}
*/