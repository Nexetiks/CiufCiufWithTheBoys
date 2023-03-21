using System;
using UnityEngine;

namespace Entities.Abilities.DefaultMove
{
    [Serializable]
    public class DefaultMoveAbility : Ability<DefaultMoveAbilityArgs>
    {
        [SerializeField]
        private Rigidbody2D rb;
        private DefaultMoveStatsComponent ability;

        public DefaultMoveAbility() : base("DefaultMoveAbility")
        {
        }

        public override void FixedUpdateAbility()
        {
            if (args == null)
            {
                return;
            }

            rb.velocity = args.Direction * (ability.Speed.Value * Time.fixedDeltaTime);
            rb.gameObject.transform.up = args.Direction;
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);
            rb = abilityOwner.GameObject.GetComponent<Rigidbody2D>();
            ability = abilityOwner.GetComponent<DefaultMoveStatsComponent>();
        }
    }
}