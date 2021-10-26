using System;
using System.Collections.Generic;
using System.Linq;

namespace VoidLess.Core.Stats
{
    public sealed class Stat
    {
        public readonly int BaseValue;
        public readonly int ModifiedValue;
        private readonly Dictionary<string, StatModifier> modifiers;

        public Stat(int baseValue) : this(baseValue, default)
        {
        }

        private Stat(int baseValue, Dictionary<string, StatModifier> statModifiers = null)
        {
            BaseValue = baseValue;
            modifiers = statModifiers ?? new Dictionary<string, StatModifier>();
            ModifiedValue = modifiers.Values
                .OrderBy(mod => mod.ModifyPriority)
                .Aggregate(BaseValue, (curr, mod) => mod.ModifyValue(BaseValue, curr));
        }

        public Stat AddModifier<TEnum>(TEnum source, StatModifier modifier) where TEnum : Enum
        {
            var copyModifiers = modifiers.ToDictionary(pair => pair.Key, pair => pair.Value);
            var sourceName = EnumToString(source);
            copyModifiers[sourceName] = copyModifiers.ContainsKey(sourceName) ? copyModifiers[sourceName].StackWith(modifier) : modifier;
            return new Stat(BaseValue, copyModifiers);
        }

        public Stat RemoveModifier<TEnum>(TEnum source) where TEnum : Enum
        {
            var copyModifiers = modifiers.ToDictionary(pair => pair.Key, pair => pair.Value);
            copyModifiers.Remove(EnumToString(source));
            return new Stat(BaseValue, copyModifiers);
        }

        public T GetModifier<T>(string source) where T : StatModifier
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
        
        private static string EnumToString<TEnum>(TEnum source) where TEnum : Enum
        {
            var typeHash = typeof(TEnum).GUID;
            var valueOffset = Convert.ToInt32(source);
            return $"{typeHash}+{valueOffset}";
        }

    }
}