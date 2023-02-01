using Entities;
using Entities.Abilities;
using UnityEngine;

public class DefaultMoveAbility : Ability<DefaultMoveAbilityArgs>
{
    private Rigidbody2D rb;

    public DefaultMoveAbility() : base("DefaultMoveAbility")
    {
    }

    protected override void OnPerform()
    {
        base.OnPerform();
    }

    public override void FixedUpdateAbility()
    {
        //
    }

    public override void Initialize(Entity abilityOwner)
    {
        base.Initialize(abilityOwner);
        rb = abilityOwner.GameObject.GetComponent<Rigidbody2D>();
    }
}