using System;
using Entities.Effects;
using UnityEngine;

// The Ability class represents a single ability that can be performed and
// affects an Entity by changing its parameters or affecting its Components.
namespace Entities.Abilities
{
    [System.Serializable]
    public abstract class Ability<T> : IAmAbility where T:  DefaultAbilityEffectArgs
    {
        public event Action OnAbilityPerformed;
        private EffectTrigger<T> entityAbilityEffectTrigger;
        protected abstract Effect<T> DefaultEffect { get; }

        // The name of the Ability
        public string Name { get; protected set; }

        // Constructor that sets the name of the Ability
        public Ability(string name)
        {
            Name = name;
            entityAbilityEffectTrigger = new EffectTrigger<T>();
            if (DefaultEffect != null)
            {
                entityAbilityEffectTrigger.Add(DefaultEffect);
            }
        }

        public void AddEffect(Effect<T> effect)
        {
            entityAbilityEffectTrigger.Add(effect);
        }

        public void RemoveEffect(Effect<T> effect)
        {
            entityAbilityEffectTrigger.Remove(effect);
        }
        
        // Abstract method that must be implemented by subclasses to perform the
        // ability and affect the given Entity or another separate Entity.
        public virtual void Perform(EffectArgs args)
        {
            entityAbilityEffectTrigger.TriggerEffects(args as T);
            OnAbilityPerformed?.Invoke();
            Debug.Log($"Performed {Name}");
        }
    }

    public interface IAmAbility
    {
        public void Perform(EffectArgs args);
    }
}