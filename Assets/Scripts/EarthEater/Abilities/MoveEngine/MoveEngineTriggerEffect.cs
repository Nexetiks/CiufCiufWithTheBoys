using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.MoveEngine
{
    public class MoveEngineTriggerEffect : TriggeredEffect<MoveEngineAbilityArgs>
    {
        private int lastDir;

        public MoveEngineTriggerEffect() : base("MoveEngineTriggerEffect", -1)
        {
        }

        protected override void OnPerform(MoveEngineAbilityArgs args)
        {
            base.OnPerform(args);
            
            if (args.Dir != 0 && lastDir != args.Dir) args.Rb.angularVelocity = 0;

            args.Rb.AddTorque(args.Dir * args.EngineComponent.RotationSpeed.Value, ForceMode2D.Impulse);
            args.Rb.AddForce(args.Transform.up * ( Mathf.Max(args.EngineComponent.ForwardForce.Value, 0) * Mathf.Abs(args.Dir)),
                ForceMode2D.Impulse);

            if (args.Dir != 0)
            {
                lastDir = args.Dir;
            }
        }
    }
}
