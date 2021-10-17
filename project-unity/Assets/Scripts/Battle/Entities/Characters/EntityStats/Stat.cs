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
        public int ModifiedValue => modifiedValue;
        
        [SerializeField]
        private int baseValue = 5;
        private int modifiedValue;
        [OdinSerialize]
        private Dictionary<StatModifierSource, StatModifier> modifiers = new Dictionary<StatModifierSource, StatModifier>();


        public Stat(int actualValue)
        {
            baseValue = actualValue;
            modifiedValue = baseValue;
        }

        private Stat(int actualValue, Dictionary<StatModifierSource, StatModifier> modifiers)
        {
            baseValue = actualValue;
            this.modifiers = modifiers.ToDictionary(pair => pair.Key, pair => pair.Value);
            RecalculateModifiedValue();
        }

        public Stat AddModifier(StatModifierSource source, StatModifier modifier)
        {
            var newStat = new Stat(baseValue, modifiers);
            newStat.MutableAddModifier(source, modifier);
            return newStat;
        }

        private void MutableAddModifier(StatModifierSource source, StatModifier modifier)
        {
            if (modifiers.ContainsKey(source))
            {
                modifiers[source] = modifiers[source].StackWith(modifier);
            }
            else
            {
                modifiers.Add(source, modifier);
            }
            
            RecalculateModifiedValue();
        }

        public Stat RemoveModifier(StatModifierSource source)
        {
            var newStat = new Stat(baseValue, modifiers);
            newStat.MutableRemoveModifier(source);
            return newStat;
        }

        private void MutableRemoveModifier(StatModifierSource source)
        {
            modifiers.Remove(source);
            RecalculateModifiedValue();
        }

        public T GetModifier<T>(StatModifierSource source) where T : StatModifier
        {
            return modifiers.ContainsKey(source) ? (T)modifiers[source] : null;
        }

        private void RecalculateModifiedValue()
        {
             modifiedValue = modifiers.Values
                .OrderBy(mod => mod.ModifyPriority)
                .Aggregate(baseValue, (curr, mod) => mod.ModifyValue(baseValue, curr));
        }
        
        public static Stat operator +(Stat stat, int value)
        {
            return new Stat(stat.baseValue + value, stat.modifiers);
        }
        
        public static Stat operator -(Stat stat, int value)
        {
            return stat + -value;
        }
    }
}