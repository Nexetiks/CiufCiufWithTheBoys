using System;
using System.Collections.Generic;
using Entities.Effects.Statuses;
using UnityEngine;

namespace Entities.Effects
{
    public class ContinuousEffectsUpdater<TriggerArgs>where TriggerArgs : EffectArgs
    {
        public event Action<ContinuousEffect<TriggerArgs>> OnEffectRemoved;
        public event Action<ContinuousEffect<TriggerArgs>> OnEffectAdded;
        
        private Dictionary<Type, ContinuousEffect<TriggerArgs>> activeEffects = new Dictionary<Type, ContinuousEffect<TriggerArgs>> ();

        public void UpdateEffects(TriggerArgs statusArgs)
        {
            foreach (ContinuousEffect<TriggerArgs> effect in activeEffects.Values)
            {
                effect.Perform(statusArgs);
                
                if (effect.IsInfinite || !effect.ExpirationConditions)
                {
                    continue;
                }
                
                activeEffects.Remove(effect.GetType());
                OnEffectRemoved?.Invoke(effect);
            }
        }
        
        public void AddEffect(ContinuousEffect<TriggerArgs> effect)
        {
            if (activeEffects.ContainsKey(effect.GetType()))
            {
                //refresh status
                activeEffects[effect.GetType()].Duration = effect.Duration;
            }
            else
            {
                activeEffects.Add(effect.GetType(), effect);
                OnEffectAdded?.Invoke(effect);
            }
        }
        
        public void RemoveEffect<T>() where T: ContinuousEffect<TriggerArgs>
        {
            if (activeEffects.ContainsKey(typeof(T)))
            {
                ContinuousEffect<TriggerArgs> status = activeEffects[typeof(T)];
                activeEffects.Remove(typeof(T));
                OnEffectRemoved?.Invoke(status);
            }
        }
    }
}
