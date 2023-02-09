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

            Collider[] colliders = Physics.OverlapSphere(abilityOwner.GameObject.transform.position, colliderRange);

            foreach (Collider collider in colliders)
            {
                if (!collider.gameObject.TryGetComponent(out EntityContext EntityContext))
                {
                    return;
                }

                if (!EntityContext.Entity.TryGetComponent<DamageableComponent>(out DamageableComponent damageableComponent))
                {
                    return;
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