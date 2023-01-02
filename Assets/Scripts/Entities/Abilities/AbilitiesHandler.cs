using System;
using System.Collections.Generic;
using Entities.Effects;

namespace Entities.Abilities
{
// The AbilitiesHandler class manages a collection of Ability classes and allows
// for performing abilities that affect Entities.
    public class AbilitiesHandler
    {
        // A dictionary mapping Ability types to the Ability objects
        private Dictionary<Type, IAmAbility> abilities;

        // Abilities owner
        private Entity myEntity;
        
        public AbilitiesHandler(Entity myEntity)
        {
            abilities = new Dictionary<Type, IAmAbility>();
            this.myEntity = myEntity;
        }

        public bool TryGetAbility<T>(out T ability) where T : IAmAbility
        {
            if(abilities.TryGetValue(typeof(T), out IAmAbility searchedAbilityInterface))
            {
                ability = (T)searchedAbilityInterface;
                return true;
            }

            ability = default(T);
            return false;
        }
        
        // Adds an Ability to the abilities dictionary
        public void AddAbility(IAmAbility ability, Entity abilityOwner)
        {
            abilities.Add(ability.GetType(), ability);
            ability.Initialize(abilityOwner);
        }

        // Removes an Ability from the abilities dictionary
        public void RemoveAbility<T>() where T : IAmAbility
        {
            abilities.Remove(typeof(T));
        }

        // Performs the ability with the given type, affecting the given Entity or
        // another separate Entity.
        public void PerformAbility<T>(EffectArgs effectArgs) where T: IAmAbility
        {
            if (abilities.ContainsKey(typeof(T)))
            {
                abilities[typeof(T)].TriggerPerform(effectArgs);
            }
        }

        public void PerformAbility<T>() where T : IAmAbility
        {
            if (abilities.ContainsKey(typeof(T)))
            {
                abilities[typeof(T)].TriggerPerform();
            }
        }

        public void UpdateAbilities()
        {
            foreach (IAmAbility ability in abilities.Values)
            {
                ability.UpdateContinuous();
            }
        }

        public void FixedUpdateAbilities()
        {
            foreach (IAmAbility ability in abilities.Values)
            {
                ability.FixedUpdateContinuous();
            }
        }
    }
}
