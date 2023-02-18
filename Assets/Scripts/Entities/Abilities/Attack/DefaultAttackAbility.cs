using System;
using Entities.Components;
using UnityEngine;

namespace Entities.Abilities.DefaultAttack
{
    [Serializable]
    public class DefaultAttackAbility : Ability<DefaultAttackAbilityArgs>
    {
        [SerializeField]
        private float colliderRange;
        [SerializeField]
        private float cooldown;

        private float nextAttackTime = 0;
        private float lastAttackTime = 0;

        public override bool CanPerform
        {
            get
            {
                if (nextAttackTime < Time.time + lastAttackTime)
                {
                    return true;
                }

                return false;
            }
        }

        public DefaultAttackAbility() : base("DefaultAttackAbility")
        {
        }

        protected override void OnPerform()
        {
            base.OnPerform();

            if (!CanPerform)
            {
                return;
            }

            nextAttackTime = Time.time + cooldown;
            lastAttackTime = Time.time;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(abilityOwner.GameObject.transform.position, colliderRange);

            foreach (Collider2D collider in colliders)
            {
                if (!collider.gameObject.TryGetComponent(out EntityContext EntityContext))
                {
                    continue;
                }

                if (!EntityContext.Entity.TryGetComponent<DamageableComponent>(out DamageableComponent damageableComponent))
                {
                    continue;
                }

                damageableComponent.Hp -= args.damage;
            }
        }

        public override object Clone()
        {
            DefaultAttackAbility defaultAttackAbility = (DefaultAttackAbility)base.Clone();
            defaultAttackAbility.colliderRange = colliderRange;
            defaultAttackAbility.cooldown = cooldown;

            return defaultAttackAbility;
        }
    }
}