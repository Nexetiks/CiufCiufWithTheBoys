using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.FollowWagon
{
    public class FollowWagonContinuousEffect : ContinuousEffect<FollowWagonArgs>
    {
        public FollowWagonContinuousEffect(string name, float startingDuration) : base(name, startingDuration)
        {
        }

        public FollowWagonContinuousEffect() : base("FollowWagonContinuousEffect")
        {
        }

        protected override void OnPerform(FollowWagonArgs args)
        {
            base.OnPerform(args);
            if(args.WagonComponent.NextWagon == null) return;

            Transform target = args.WagonComponent.NextWagon.MyEntity.GameObject.transform;
            Vector2 forwardDir = (Vector2)target.position - args.Rb.position;
            args.Rb.transform.up = forwardDir.normalized;
            args.Rb.MovePosition((Vector2)target.position - forwardDir.normalized);
        }
    }
}
