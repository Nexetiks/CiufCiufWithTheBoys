using UnityEngine;

namespace Entities.Abilities.DefaultMove
{
    public class DefaultMoveAbility : Ability<DefaultMoveAbilityArgs>
    {
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private DefaultMovementStatsComponent ability;

        public DefaultMoveAbility() : base("DefaultMoveAbility")
        {
        }

        public override void FixedUpdateAbility()
        {
            if (args == null)
            {
                return;
            }
            
            Vector2 direction = rb.position - args.PositionToMoveAt;
            rb.velocity = direction * ability.Speed.Value * Time.fixedDeltaTime;
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);
            rb = abilityOwner.GameObject.GetComponent<Rigidbody2D>();
            ability = abilityOwner.GetComponent<DefaultMovementStatsComponent>();
        }
    }
}