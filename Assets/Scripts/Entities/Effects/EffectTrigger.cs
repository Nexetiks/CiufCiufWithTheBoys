using System;
using System.Collections.Generic;

namespace Entities.Effects
{
    public class EffectTrigger<TriggerArgs> where TriggerArgs : EffectArgs
    {
        private List<TriggeredEffect<TriggerArgs>> effects;

        public EffectTrigger()
        {
            effects = new List<TriggeredEffect<TriggerArgs>>();
        }

        public virtual void Add(TriggeredEffect<TriggerArgs> triggeredEffect)
        {
            if (triggeredEffect.Duration == 0)
            {
                throw new ArgumentException(
                    "Tried to add effect " + triggeredEffect.Name + " to an EffectTrigger, but it has duration 0!",
                    nameof(triggeredEffect));
            }

            effects.Add(triggeredEffect);
        }

        public virtual void Remove(TriggeredEffect<TriggerArgs> triggeredEffect)
        {
            effects.Remove(triggeredEffect);
        }

        public void TriggerEffects(TriggerArgs args)
        {
            List<int> effectIndexesToRemove = new List<int>();
            
            for (int i = 0; i < effects.Count; i++)
            {
                TriggeredEffect<TriggerArgs> triggeredEffect = effects[i];
                if (triggeredEffect.Duration != 0)
                {
                    triggeredEffect.Perform(args);
                    if (args != null)
                    {
                        if (args.CancelTrigger)
                            break;
                    }
                }
                else if (triggeredEffect.Duration == 0)
                {
                    effectIndexesToRemove.Add(i);
                }
            }

            for (int i = effectIndexesToRemove.Count-1; i >= 0; i--)
            {
                effects.RemoveAt(effectIndexesToRemove[i]);
            }
        }
    }
}