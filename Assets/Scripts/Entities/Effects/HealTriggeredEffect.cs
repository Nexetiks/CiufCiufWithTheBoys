using Entities.Abilities;
using Entities.Components;
using UnityEngine;

namespace Entities.Effects
{
    public class HealTriggeredEffect : TriggeredEffect<UseItemAbility.UseItemAbilityArgs>
    {
        public HealTriggeredEffect() : base("Heal", -1)
        {
        }

        protected override void OnPerform(UseItemAbility.UseItemAbilityArgs args)
        {
            if (args.AbilityTarget.TryGetComponent(out DamageableComponent damageableComponent))
            {
                damageableComponent.Hp += args.Power;
                Debug.Log("Healed!");
            }
        }
    }
}
