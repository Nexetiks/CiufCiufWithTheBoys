using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.MoveEngine
{
    /// <summary>
    /// This should be performed in the FixedUpdate
    /// </summary>
    public class MoveEngineContinuousEffect : ContinuousEffect<MoveEngineAbilityArgs>
    {
        public bool CanMove { get; set; } = true;

        public MoveEngineContinuousEffect(float startingDuration) : base("MoveEngineEffect", startingDuration)
        {
        }

        public MoveEngineContinuousEffect() : base("MoveEngineEffect")
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

            args.Rb.velocity = args.Transform.up * args.Rb.velocity.magnitude;

            if (args.Rb.velocity.magnitude > args.EngineComponent.MaxSpeed.Value)
            {
                args.Rb.velocity = args.EngineComponent.MaxSpeed.Value * (args.Rb.velocity).normalized;
            }
        }
    }
}