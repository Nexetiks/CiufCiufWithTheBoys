using Entities.Abilities;
using Entities.Components;
using UnityEngine;

namespace Entities.Effects
{
    public class HealEffect : Effect<UseItemAbility.UseItemAbilityArgs>
    {
        public HealEffect() : base("Heal", -1)
        {
        }

        protected override void OnTrigger(UseItemAbility.UseItemAbilityArgs args)
        {
            if (args.AbilityTarget.TryGetComponent(out DamageableComponent damageableComponent))
            {
                damageableComponent.Hp += args.Power;
                Debug.Log("Healed!");
            }
        }
    }
}
