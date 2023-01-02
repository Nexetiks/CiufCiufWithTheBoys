using EarthEater.Components;
using Entities;
using Entities.Abilities;
using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.MoveEngine
{
    public class MoveEngineAbility : Ability<MoveEngineAbilityArgs>
    {
        private MoveEngineEffect moveEngineEffect;

        protected override TriggeredEffect<MoveEngineAbilityArgs> DefaultTriggeredEffect { get; }
        protected override ContinuousEffect<MoveEngineAbilityArgs> DefaultEffectInUpdate { get; }
        protected override ContinuousEffect<MoveEngineAbilityArgs> DefaultEffectInFixedUpdate => moveEngineEffect;


        public MoveEngineAbility() : base("Move Engine Ability")
        {
            moveEngineEffect = new MoveEngineEffect();
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);
            args = new MoveEngineAbilityArgs(abilityOwner, abilityOwner.GameObject.GetComponent<Rigidbody2D>(),
                abilityOwner.GetComponent<EngineComponent>(), abilityOwner.GameObject.transform);
        }
    }
}