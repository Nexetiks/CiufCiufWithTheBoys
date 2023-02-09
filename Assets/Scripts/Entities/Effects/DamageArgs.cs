using Entities.Components;

namespace Entities.Effects
{
    public class DamageArgs : EffectArgs
    {
        public DamageableComponent Target { get; set; }

        public DamageArgs(DamageableComponent target)
        {
            Target = target;
        }
    }
}