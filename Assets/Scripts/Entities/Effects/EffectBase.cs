using System;

namespace Entities.Effects
{
    public abstract class EffectBase<TriggerArgs> where TriggerArgs : EffectArgs
    {
        public event Action Expired;

        public virtual bool ExpirationConditions { get; }
        /// <summary>The name of the effect.</summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Effect should be triggered using this method, it handles effect expiration. The actual Effect functionality
        /// should be a part of the OnPerform method
        /// </summary>
        /// <param name="args"></param>
        public virtual void Perform(TriggerArgs args)
        {
            OnPerform(args);

            if (ExpirationConditions)
            {
                Expired?.Invoke();
            }
        }

        /// <summary>
        /// The actual Effect functionality
        /// </summary>
        /// <param name="e"></param>
        protected abstract void OnPerform(TriggerArgs e);
    }
}
