using System;
using System.Collections.Generic;

namespace Entities
{
    [Serializable]
    public class Stat
    {
        public Action<float> OnModifiersChanged;

        private HashSet<StatModifier> modifiers;

        public float BaseValue { get; private set; }

        public Stat(float baseValue)
        {
            this.BaseValue = baseValue;
            this.modifiers = new HashSet<StatModifier>();
        }

        public float Value
        {
            get
            {
                // Calculate the total value of the stat by applying all the modifiers
                float finalValue = BaseValue;

                foreach (StatModifier modifier in modifiers)
                {
                    finalValue += modifier.Value;
                }

                return finalValue;
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            // Add the modifier to the collection using the source as the key
            modifiers.Add(modifier);
            OnModifiersChanged?.Invoke(Value);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            // Remove the modifier from the collection using the source as the key
            modifiers.Remove(modifier);
            OnModifiersChanged?.Invoke(Value);
        }
    }
}