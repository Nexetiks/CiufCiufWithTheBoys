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
        public event Action OnTriggerAbilityPerformed;

        /// <summary>
        /// Remember to initialize args in the ability constructor, gather necessary references and so on
        /// </summary>
        protected T args;

        protected Entity abilityOwner;
        private EffectTrigger<T> entityAbilityEffectTrigger;
        private ContinuousEffectsUpdater<T> abilityEffectsInUpdate;
        private ContinuousEffectsUpdater<T> abilityEffectsInFixedUpdate;
        
        protected abstract TriggeredEffect<T> DefaultTriggeredEffect { get; }
        protected abstract ContinuousEffect<T> DefaultEffectInUpdate { get; }
        protected abstract ContinuousEffect<T> DefaultEffectInFixedUpdate { get; }

        // The name of the Ability
        public string Name { get; protected set; }

        // Constructor that sets the name of the Ability
        public Ability(string name)
        {
            Name = name;
            entityAbilityEffectTrigger = new EffectTrigger<T>();
            abilityEffectsInUpdate = new ContinuousEffectsUpdater<T>();
            abilityEffectsInFixedUpdate = new ContinuousEffectsUpdater<T>();
        }

        /// <summary>
        /// You must override this to provide ability with required references in args
        /// </summary>
        /// <param name="abilityOwner"></param>
        public virtual void Initialize(Entity abilityOwner)
        {
            this.abilityOwner = abilityOwner;
            if (DefaultTriggeredEffect != null)
            {
                entityAbilityEffectTrigger.Add(DefaultTriggeredEffect);
            }

            if (DefaultEffectInUpdate != null)
            {
                abilityEffectsInUpdate.AddEffect(DefaultEffectInUpdate);
            }

            if (DefaultEffectInFixedUpdate != null)
            {
                abilityEffectsInFixedUpdate.AddEffect(DefaultEffectInFixedUpdate);
            }
        }

        public void AddTriggeredEffect(TriggeredEffect<T> triggeredEffect)
        {
            entityAbilityEffectTrigger.Add(triggeredEffect);
        }

        public void RemoveTriggeredEffect(TriggeredEffect<T> triggeredEffect)
        {
            entityAbilityEffectTrigger.Remove(triggeredEffect);
        }
        
        public void TriggerPerform(EffectArgs args)
        {
            this.args = args as T;
            entityAbilityEffectTrigger.TriggerEffects(args as T);
            OnTriggerAbilityPerformed?.Invoke();
            Debug.Log($"Performed {Name}");
        }

        public void UpdateContinuous()
        {
            abilityEffectsInUpdate.UpdateEffects(args);
        }

        public void FixedUpdateContinuous()
        {
            abilityEffectsInFixedUpdate.UpdateEffects(args);
        }
    }

    public interface IAmAbility
    {
        public void TriggerPerform(EffectArgs args);
        public void UpdateContinuous();
        public void FixedUpdateContinuous();
        public void Initialize(Entity abilityOwner);
    }
}