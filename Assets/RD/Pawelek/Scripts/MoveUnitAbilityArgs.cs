using Entities;
using Entities.Effects;
using UnityEngine;

public class MoveUnitAbilityArgs : DefaultAbilityEffectArgs
{
    public Rigidbody2D Rb { get; private set; }
    public Transform Transform { get; private set; }

    public MoveUnitAbilityArgs(Entity abilityOwner, Rigidbody2D rb, Transform transform) : base(abilityOwner)
    {
        Rb = rb;
        Transform = transform;
    }
}