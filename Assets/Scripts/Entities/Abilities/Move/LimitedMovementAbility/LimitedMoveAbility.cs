using System;
using UnityEngine;

namespace Entities.Abilities.LimitedMove
{
    [Serializable]
    public class LimitedMoveAbility : Ability<LimitedMoveAbilityArgs>
    {
        [SerializeField]
        private Rigidbody2D rb;
        private LimitedMoveStatsComponent ability;
        private Vector2 direction;

        public LimitedMoveAbility() : base("DefaultMoveAbility")
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
            direction = abilityOwner.GameObject.transform.up;
            rb = abilityOwner.GameObject.GetComponent<Rigidbody2D>();
            ability = abilityOwner.GetComponent<LimitedMoveStatsComponent>();
        }
    }
}