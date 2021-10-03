﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class EntityStat: ISerializationCallbackReceiver
    {
        [SerializeField]
        private int baseValue = 5;
        private Dictionary<StatModifierSource, EntityStatModifier> modifiers = new Dictionary<StatModifierSource, EntityStatModifier>();
        public int BaseValue { get => baseValue; set => baseValue = value; }
        public int Value => GetModified(BaseValue, (mod, curr) => mod.ModifyValue(baseValue, curr));

        public EntityStat(int actualValue)
        {
            BaseValue = actualValue;
        }

        public void AddModifier(StatModifierSource source, EntityStatModifier modifier)
        {
            if (modifiers.ContainsKey(source))
            {
                modifiers[source] = modifiers[source].StackWith(modifier);
                return;
            }

            modifiers.Add(source, modifier);
        }

        public void RemoveModifier(StatModifierSource source)
        {
            modifiers.Remove(source);
        }

        public T GetModifier<T>(StatModifierSource source) where T : EntityStatModifier
        {
            return modifiers.ContainsKey(source) ? (T)modifiers[source] : null;
        }
        
        private int GetModified(int baseVal, Func<EntityStatModifier, int, int> applyModifier)
        {
            var currentValue = baseVal;
            foreach (var modifier in modifiers.Values.OrderBy(mod => mod.ModifyPriority))
            {
                currentValue = applyModifier(modifier, currentValue);
            }
            return currentValue;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            modifiers = new Dictionary<StatModifierSource, EntityStatModifier>();
        }
    }
}