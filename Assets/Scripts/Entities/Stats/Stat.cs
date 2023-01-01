using System;
using System.Collections.Generic;

namespace Entities
{
    public class Stat
    {
        public Action<float> OnModifiersChanged;
        
        private float baseValue;
        private Dictionary<string, StatModifier> modifiers;

        public Stat(float baseValue)
        {
            this.baseValue = baseValue;
            this.modifiers = new Dictionary<string, StatModifier>();
        }
        
        public float Value
        {
            get
            {
                // Calculate the total value of the stat by applying all the modifiers
                float finalValue = baseValue;
                foreach (StatModifier modifier in modifiers.Values)
                {
                    finalValue += modifier.Value;
                }

                return finalValue;
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            // Add the modifier to the collection using the source as the key
            modifiers[modifier.Source] = modifier;
            OnModifiersChanged?.Invoke(Value);
        }

        public void RemoveModifier(string source)
        {
            // Remove the modifier from the collection using the source as the key
            modifiers.Remove(source);
            OnModifiersChanged?.Invoke(Value);
        }
    }
}