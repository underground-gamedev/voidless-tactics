using System.Collections.Generic;
using System.Linq;

namespace VoidLess.Core.Stats
{
    public class Stat
    {
        public readonly int BaseValue;
        public readonly int ModifiedValue;
        private readonly Dictionary<StatModifierSource, StatModifier> modifiers;

        public Stat(int baseValue) : this(baseValue, default)
        {
        }

        private Stat(int baseValue, Dictionary<StatModifierSource, StatModifier> statModifiers = null)
        {
            BaseValue = baseValue;
            modifiers = statModifiers ?? new Dictionary<StatModifierSource, StatModifier>();
            ModifiedValue = modifiers.Values
                .OrderBy(mod => mod.ModifyPriority)
                .Aggregate(BaseValue, (curr, mod) => mod.ModifyValue(BaseValue, curr));
        }

        public Stat AddModifier(StatModifierSource source, StatModifier modifier)
        {
            var copyModifiers = modifiers.ToDictionary(pair => pair.Key, pair => pair.Value);
            copyModifiers[source] = copyModifiers.ContainsKey(source) ? copyModifiers[source].StackWith(modifier) : modifier;
            return new Stat(BaseValue, copyModifiers);
        }

        public Stat RemoveModifier(StatModifierSource source)
        {
            var copyModifiers = modifiers.ToDictionary(pair => pair.Key, pair => pair.Value);
            copyModifiers.Remove(source);
            return new Stat(BaseValue, copyModifiers);
        }

        public T GetModifier<T>(StatModifierSource source) where T : StatModifier
        {
            return modifiers.ContainsKey(source) ? (T)modifiers[source] : null;
        }

        public bool Equals(Stat other)
        {
            return BaseValue == other.BaseValue && ModifiedValue == other.ModifiedValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Stat) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (BaseValue * 397) ^ ModifiedValue;
            }
        }
        
        public static Stat operator +(Stat stat, int value)
        {
            return new Stat(stat.BaseValue + value, stat.modifiers);
        }
        
        public static Stat operator -(Stat stat, int value)
        {
            return stat + -value;
        }
    }
}