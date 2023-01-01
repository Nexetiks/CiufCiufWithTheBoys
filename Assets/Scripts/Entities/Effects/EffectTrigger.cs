using System;
using System.Collections.Generic;

namespace Entities.Effects
{
  public class EffectTrigger<TriggerArgs> where TriggerArgs : EffectArgs
  {
    private List<Effect<TriggerArgs>> _effects;
    public EffectTrigger() => this._effects = new List<Effect<TriggerArgs>>();

    public IReadOnlyList<Effect<TriggerArgs>> Effects => (IReadOnlyList<Effect<TriggerArgs>>) this._effects.AsReadOnly();
    
    public virtual void Add(Effect<TriggerArgs> effect)
    {
      if (effect.Duration == 0)
        throw new ArgumentException("Tried to add effect " + effect.Name + " to an EffectTrigger, but it has duration 0!", nameof (effect));
      this._effects.Add(effect);
    }
    
    public virtual void Remove(Effect<TriggerArgs> effect) => this._effects.Remove(effect);
    
    
    public void TriggerEffects(TriggerArgs args)
    {
      foreach (Effect<TriggerArgs> effect in this._effects)
      {
        if (effect.Duration != 0)
        {
          effect.Trigger(args);
          if ((object) args != null)
          {
            if (args.CancelTrigger)
              break;
          }
        }
      }
      this._effects.RemoveAll((Predicate<Effect<TriggerArgs>>) (eff => eff.Duration == 0));
    }
  }
}