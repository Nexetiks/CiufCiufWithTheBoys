using Entities.Effects;

namespace Entities.Abilities
{
    public class AttackAbility : Ability<DefaultTargetAbilityArgs>
    {
        public AttackAbility() : base("attack")
        {
        }

        protected override Effect<DefaultTargetAbilityArgs> DefaultEffect => new DamageEffect();
    }
}
