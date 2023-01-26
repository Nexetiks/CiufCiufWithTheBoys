using Entities.Effects;
using Pawelek.Testing.ItWillBeChangeLater;
using UnityEngine;

public class MoveUnitContinuousEffect : ContinuousEffect<MoveUnitAbilityArgs>
{
    public MoveUnitContinuousEffect() : base("MoveUnitContinuousEffect")
    {
    }

    protected override void OnPerform(MoveUnitAbilityArgs args)
    {
        if (Vector2.Distance(args.Transform.position, args.player.position) < args.DistanceToAttackPlayer)
        {
            args.Chasing = true;
            args.AbilityOwner.AbilitiesHandler.PerformAbility<MoveUnitAbility>();
            args.AbilityOwner.AbilitiesHandler.UpdateAbilities();
        }
    }
}