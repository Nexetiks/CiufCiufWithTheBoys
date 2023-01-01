using System;
using System.Collections.Generic;

namespace Entities
{
    public class Stat
    {
        public Action<float> OnModifiersChanged;
        
        private float baseValue;
        private HashSet<StatModifier> modifiers;

        public Stat(float baseValue)
        {
            this.baseValue = baseValue;
            this.modifiers = new HashSet <StatModifier>();
        }
        
        public float Value
        {
            get
            {
                // Calculate the total value of the stat by applying all the modifiers
                float finalValue = baseValue;
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