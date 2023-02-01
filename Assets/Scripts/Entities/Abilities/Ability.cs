using System;
using Entities.Effects;
using UnityEngine;

// The Ability class represents a single ability that can be performed and
// affects an Entity by changing its parameters or affecting its Components.
namespace Entities.Abilities
{
    //TODO: Add initialization args for gathering references and UpdateArgs i.e. when something changes, for TriggerEffects,
    // like a direction in the MoveEngineTriggerEffect
    [Serializable]
    public abstract class Ability<T> : IAmAbility where T : DefaultAbilityArgs
    {
        public event Action OnTriggerAbilityPerformed;

        protected Entity abilityOwner;

        /// <summary>
        /// Remember to initialize args in the ability constructor, gather necessary references and so on
        /// </summary>
        public T Args { get; protected set; }
        
        public virtual bool IsPerforming => true;
        public virtual bool CanPerform => true;

        // The name of the Ability
        public string Name { get; protected set; }

        // Constructor that sets the name of the Ability
        protected Ability(string name)
        {
            Name = name;
        }

        /// <summary>
        /// You must override this to provide ability with required references in args
        /// </summary>
        /// <param name="abilityOwner"></param>
        public virtual void Initialize(Entity abilityOwner)
        {
            this.abilityOwner = abilityOwner;
        }

        public void Perform(DefaultAbilityArgs args)
        {
            if (!CanPerform) return;

            if (args != null)
            {
                Args = args as T;
            }
            
            OnPerform();
            OnTriggerAbilityPerformed?.Invoke();
        }

        public virtual void UpdateAbility()
        {
        }

        public virtual void FixedUpdateAbility()
        {
        }
        
        protected virtual void OnPerform()
        {
        }

        public virtual object Clone()
        {
            Ability<T> copy = (Ability<T>)this.MemberwiseClone();
            return copy;
        }
    }

    public interface IAmAbility : ICloneable
    {
        public void Perform(DefaultAbilityArgs args);
        public void UpdateAbility();
        public void FixedUpdateAbility();
        public void Initialize(Entity abilityOwner);
    }
}