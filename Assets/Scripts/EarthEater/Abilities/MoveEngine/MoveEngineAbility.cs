using EarthEater.Components;
using Entities;
using Entities.Abilities;
using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.MoveEngine
{
    public class MoveEngineAbility : Ability<MoveEngineAbilityArgs>
    {
        private Rigidbody2D rb;
        private EngineComponent engineComponent;
        private Transform transform;
        public bool CanMove { get; set; } = true;
        private int lastDir;

        public MoveEngineAbility() : base("Move Engine Ability")
        {
        }

        protected override void OnPerform()
        {
            base.OnPerform();

            if (Args.Dir != 0 && lastDir != Args.Dir) rb.angularVelocity = 0;

            rb.AddTorque(Args.Dir * engineComponent.RotationSpeed.Value, ForceMode2D.Impulse);
            rb.AddForce(transform.up * (Mathf.Max(engineComponent.ForwardForce.Value, 0) * Mathf.Abs(Args.Dir)),
                ForceMode2D.Impulse);

            if (Args.Dir != 0)
            {
                lastDir = Args.Dir;
            }
        }

        public override void FixedUpdateAbility()
        {
            base.FixedUpdateAbility();
            if (!CanMove)
            {
                rb.velocity = Vector3.zero;
                return;
            }

            rb.velocity = transform.up * rb.velocity.magnitude;

            if (rb.velocity.magnitude > engineComponent.MaxSpeed.Value)
            {
                rb.velocity = engineComponent.MaxSpeed.Value * (rb.velocity).normalized;
            }
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);

            rb = abilityOwner.GameObject.GetComponent<Rigidbody2D>();
            engineComponent = abilityOwner.GetComponent<EngineComponent>();
            transform = abilityOwner.GameObject.transform;
        }
    }
}