using Entities.Effects;

namespace Entities.Abilities
{
    public class AttackAbility : Ability<DefaultTargetAbilityArgs>
    {
        public AttackAbility() : base("attack")
        {
        }

        protected override TriggeredEffect<DefaultTargetAbilityArgs> DefaultTriggeredEffect => new DamageTriggeredEffect();
        protected override ContinuousEffect<DefaultTargetAbilityArgs> DefaultEffectInUpdate { get; }
        protected override ContinuousEffect<DefaultTargetAbilityArgs> DefaultEffectInFixedUpdate { get; }
    }
}
