using EarthEater.RailwaySystem;
using Entities;
using Entities.Abilities;
using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.FollowWagon
{
    public class FollowWagonAbility : Ability<FollowWagonArgs>
    {
        private FollowWagonContinuousEffect followWagonContinuousEffect;
        
        protected override TriggeredEffect<FollowWagonArgs> DefaultTriggeredEffect { get; }
        protected override ContinuousEffect<FollowWagonArgs> DefaultEffectInUpdate { get; }
        protected override ContinuousEffect<FollowWagonArgs> DefaultEffectInFixedUpdate => followWagonContinuousEffect;

        public FollowWagonAbility() : base("FollowWagonAbility")
        {
            followWagonContinuousEffect = new FollowWagonContinuousEffect();
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);
            Args = new FollowWagonArgs(abilityOwner,
                abilityOwner.GetComponent<WagonComponent>(),
                abilityOwner.GameObject.GetComponent<Rigidbody2D>());
        }
    }
}