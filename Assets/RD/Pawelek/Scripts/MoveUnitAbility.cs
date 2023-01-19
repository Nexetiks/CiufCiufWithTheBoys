using Entities;
using Entities.Abilities;
using Entities.Effects;
using UnityEngine;

namespace Pawelek.Testing.ItWillBeChangeLater
{
    public class MoveUnitAbility : Ability<MoveUnitAbilityArgs>
    {
        private MoveUnitTriggerEffect moveUnitTriggerEffect;
        protected override TriggeredEffect<MoveUnitAbilityArgs> DefaultTriggeredEffect => moveUnitTriggerEffect;
        protected override ContinuousEffect<MoveUnitAbilityArgs> DefaultEffectInUpdate { get; }
        protected override ContinuousEffect<MoveUnitAbilityArgs> DefaultEffectInFixedUpdate { get; }

        public MoveUnitAbility(string name) : base("Move Unit Ability")
        {
            moveUnitTriggerEffect = new MoveUnitTriggerEffect();
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);

            Args = new MoveUnitAbilityArgs(abilityOwner, abilityOwner.GameObject.GetComponent<Rigidbody2D>(), abilityOwner.GameObject.transform);
        }
    }
}