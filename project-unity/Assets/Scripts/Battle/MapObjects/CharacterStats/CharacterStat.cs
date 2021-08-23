using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class CharacterStat: ISerializationCallbackReceiver
    {
        [SerializeField]
        private int baseValue = 5;
        private Dictionary<string, CharacterStatModifier> modifiers = new Dictionary<string, CharacterStatModifier>();
        public int BaseValue { get => baseValue; set => baseValue = value; }
        public int Value => GetModified(BaseValue, (mod, curr) => mod.ModifyValue(baseValue, curr));

        private int GetModified(int baseVal, Func<CharacterStatModifier, int, int> applyModifier)
        {
            var currentValue = baseVal;
            foreach (var modifier in modifiers.Values.OrderBy(mod => mod.ModifyPriority))
            {
                currentValue = applyModifier(modifier, currentValue);
            }
            return currentValue;
        }

        public void AddModifier(string key, CharacterStatModifier modifier)
        {
            if (modifiers.ContainsKey(key))
            {
                modifiers[key] = modifiers[key].StackWith(modifier);
                return;
            }

            modifiers.Add(key, modifier);
        }

        public void RemoveModifier(string key)
        {
            modifiers.Remove(key);
        }

        public T GetModifier<T>(string key) where T : CharacterStatModifier
        {
            return modifiers.ContainsKey(key) ? (T)modifiers[key] : null;
        }

        public CharacterStat(int actualValue)
        {
            BaseValue = actualValue;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            modifiers = new Dictionary<string, CharacterStatModifier>();
        }
    }
}