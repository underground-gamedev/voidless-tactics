using System.Collections.Generic;
using UnityEngine;

namespace VoidLess.Game.Magic.Spells.Base
{
    public class SpellBindings: ScriptableObject
    {
        [SerializeField]
        private List<ISpell> bindings;

        public IReadOnlyList<ISpell> Bindings => bindings;
    }
}