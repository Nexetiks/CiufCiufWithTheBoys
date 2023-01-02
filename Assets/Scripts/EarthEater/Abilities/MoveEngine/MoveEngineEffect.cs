using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.MoveEngine
{
    /// <summary>
    /// This should be performed in the FixedUpdate
    /// </summary>
    public class MoveEngineEffect : ContinuousEffect<MoveEngineAbilityArgs>
    {
        public bool CanMove { get; set; } = true;

        private int dirPrev;
        private int lastDir;

        public MoveEngineEffect(float startingDuration) : base("MoveEngineEffect", startingDuration)
        {
        }

        public MoveEngineEffect() : base("MoveEngineEffect")
        {
        }

        protected override void OnPerform(MoveEngineAbilityArgs args)
        {
            base.OnPerform(args);
            if (!CanMove)
            {
                args.Rb.velocity = Vector3.zero;
                return;
            }

            //TODO: handle input passing better, pass it as a part of MoveEngineAbilityArgs arguments
            int dir = -(int) Input.GetAxisRaw("Horizontal");
            args.Rb.velocity = args.Transform.up * args.Rb.velocity.magnitude;

            if (dir != dirPrev)
            {
                if (dir != 0 && lastDir != dir) args.Rb.angularVelocity = 0;

                args.Rb.AddTorque(dir * args.EngineComponent.RotationSpeed.Value, ForceMode2D.Impulse);
                args.Rb.AddForce(args.Transform.up * (args.EngineComponent.ForwardForce.Value * Mathf.Abs(dir)),
                    ForceMode2D.Impulse);
            }

            if (args.Rb.velocity.magnitude > args.EngineComponent.MaxSpeed.Value)
            {
                args.Rb.velocity = args.EngineComponent.MaxSpeed.Value * (args.Rb.velocity).normalized;
            }

            dirPrev = dir;

            if (dirPrev != 0)
            {
                lastDir = dirPrev;
            }
        }
    }
}