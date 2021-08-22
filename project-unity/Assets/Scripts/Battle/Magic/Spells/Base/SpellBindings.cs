using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class SpellBindings: ScriptableObject
    {
        [SerializeField]
        private List<ISpell> bindings;

        public IReadOnlyList<ISpell> Bindings => bindings;
    }
}