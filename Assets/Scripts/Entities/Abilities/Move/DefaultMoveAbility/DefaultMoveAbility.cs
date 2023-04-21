using System;
using EarthEater.Components;
using UnityEngine;

namespace Entities.Abilities.DefaultMove
{
    [Serializable]
    public class DefaultMoveAbility : Ability<DefaultMoveAbilityArgs>
    {
        [SerializeField]
        private Rigidbody2D rb;
        private DefaultMoveStatsComponent moveStatsComponent;
        private AIDataComponent aiDataComponent;

        public DefaultMoveAbility() : base("DefaultMoveAbility")
        {
        }

        public override void FixedUpdateAbility()
        {
            if (args == null)
            {
                return;
            }

            if (aiDataComponent.IsEscaping)
            {
                moveStatsComponent.Speed.AddModifier(new StatModifier(10, nameof(DefaultMoveAbility)));
            }

            rb.velocity = args.Direction * (moveStatsComponent.Speed.Value * Time.fixedDeltaTime);
            rb.gameObject.transform.up = args.Direction;
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);
            rb = abilityOwner.GameObject.GetComponent<Rigidbody2D>();
            moveStatsComponent = abilityOwner.GetComponent<DefaultMoveStatsComponent>();
        }
    }
}