
using Entities.Components;

namespace Entities.Effects
{
    public class DamageTriggeredEffect : TriggeredEffect<DefaultTargetAbilityArgs>
    {
        public DamageTriggeredEffect(int startingDuration = -1) : base("normal damage", startingDuration)
        {
        }

        protected override void OnPerform(DefaultTargetAbilityArgs args)
        {
            if (!args.AbilityTarget.TryGetComponent(out DamageableComponent otherDamageableComponent)) return;
            otherDamageableComponent.Hp -= 1; //args.AbilityOwner.EntityStatsModel.Strength.Value;
        }
    }
}
