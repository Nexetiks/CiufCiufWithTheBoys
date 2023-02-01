using Entities;
using Entities.Abilities;
using Entities.Effects;
using UnityEngine;

namespace Pawelek.Testing.ItWillBeChangeLater
{
    public class MoveUnitAbility : Ability<MoveUnitAbilityArgs>
    {
        public MoveUnitAbility(string name) : base("Move Unit Ability")
        {
        }

        protected override void OnPerform()
        {
            base.OnPerform();
            Debug.Log("test - dupa");
            //move unit.
        }

        public override void FixedUpdateAbility()
        {
            base.FixedUpdateAbility();
            if (Vector2.Distance(args.Transform.position, args.player.position) < args.DistanceToAttackPlayer)
            {
                args.Chasing = true;
                if (abilityOwner.TryGetComponent(out AbilitiesHandler abilitiesHandler))
                {
                    abilitiesHandler.PerformAbility<MoveUnitAbility>(null);
                    abilitiesHandler.UpdateComponent();
                }
            }
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);

            args = new MoveUnitAbilityArgs(abilityOwner, abilityOwner.GameObject.GetComponent<Rigidbody2D>(), abilityOwner.GameObject.transform, Vector2.zero, 100f, default); // TODO change
        }
    }
}