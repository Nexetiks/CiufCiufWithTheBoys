
using Entities.Components;

namespace Entities.Effects
{
    public class DamageEffect : Effect<DefaultTargetAbilityArgs>
    {
        public DamageEffect(int startingDuration = -1) : base("normal damage", startingDuration)
        {
        }

        protected override void OnTrigger(DefaultTargetAbilityArgs args)
        {
            if (!args.AbilityTarget.TryGetComponent(out DamageableComponent otherDamageableComponent)) return;
            otherDamageableComponent.Hp -= 1; //args.AbilityOwner.EntityStatsModel.Strength.Value;
        }
    }
}
