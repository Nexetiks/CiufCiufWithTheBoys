using EarthEater.Components;
using Entities;
using Entities.Abilities;
using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.MoveEngine
{
    public class MoveEngineAbility : Ability<MoveEngineAbilityArgs>
    {
        private MoveEngineContinuousEffect moveEngineContinuousEffect;
        private MoveEngineTriggerEffect moveEngineTriggerEffect;

        protected override TriggeredEffect<MoveEngineAbilityArgs> DefaultTriggeredEffect => moveEngineTriggerEffect;
        protected override ContinuousEffect<MoveEngineAbilityArgs> DefaultEffectInUpdate { get; }
        protected override ContinuousEffect<MoveEngineAbilityArgs> DefaultEffectInFixedUpdate => moveEngineContinuousEffect;

        public MoveEngineAbility() : base("Move Engine Ability")
        {
            moveEngineContinuousEffect = new MoveEngineContinuousEffect();
            moveEngineTriggerEffect = new MoveEngineTriggerEffect();
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);

            Args = new MoveEngineAbilityArgs(abilityOwner, abilityOwner.GameObject.GetComponent<Rigidbody2D>(),
                abilityOwner.GetComponent<EngineComponent>(), abilityOwner.GameObject.transform);
        }
    }
}