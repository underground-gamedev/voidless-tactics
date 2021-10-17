using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class Stat
    {
        public int BaseValue => baseValue;
        public int Value => cachedModifiedValue ??= GetModified(BaseValue, (mod, curr) => mod.ModifyValue(baseValue, curr));
        
        [SerializeField]
        private int baseValue = 5;
        [OdinSerialize]
        private Dictionary<StatModifierSource, StatModifier> modifiers = new Dictionary<StatModifierSource, StatModifier>();

        private int? cachedModifiedValue;

        public Stat(int actualValue)
        {
            baseValue = actualValue;
        }

        private Stat(int actualValue, Dictionary<StatModifierSource, StatModifier> modifiers)
        {
            baseValue = actualValue;
            this.modifiers = modifiers.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public Stat AddModifier(StatModifierSource source, StatModifier modifier)
        {
            var newStat = new Stat(baseValue, modifiers);
            
            if (newStat.modifiers.ContainsKey(source))
            {
                newStat.modifiers[source] = modifiers[source].StackWith(modifier);
            }
            else
            {
                newStat.modifiers.Add(source, modifier);
            }
            
            return newStat;
        }

        public Stat RemoveModifier(StatModifierSource source)
        {
            var newStat = new Stat(baseValue, modifiers);
            newStat.modifiers.Remove(source);
            return newStat;
        }

        public T GetModifier<T>(StatModifierSource source) where T : StatModifier
        {
            return modifiers.ContainsKey(source) ? (T)modifiers[source] : null;
        }
        
        private int GetModified(int baseVal, Func<StatModifier, int, int> applyModifier)
        {
            return modifiers.Values
                .OrderBy(mod => mod.ModifyPriority)
                .Aggregate(baseVal, (current, modifier) => applyModifier(modifier, current));
        }

        public static Stat operator +(Stat stat, int addition)
        {
            return new Stat(stat.baseValue + addition, stat.modifiers);
        }
    }
}