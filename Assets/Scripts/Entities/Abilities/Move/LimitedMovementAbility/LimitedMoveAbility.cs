using System;
using UnityEngine;

namespace Entities.Abilities.LimitedMove
{
    [Serializable]
    public class LimitedMoveAbility : Ability<LimitedMoveAbilityArgs>
    {
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
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

            Quaternion rotationAngle = Quaternion.Euler(new Vector3(args.Direction.X, args.Direction.Y, args.Direction.Z * ability.MaxAngle.Value));
            direction = rotationAngle * direction;
            abilityOwner.GameObject.transform.up = direction;
            rb.velocity = direction * ability.Speed.Value;
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