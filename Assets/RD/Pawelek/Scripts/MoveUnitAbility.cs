using Entities.Abilities;
using Entities.Effects;

public class MoveUnitAbility : Ability<MoveUnitAbilityArgs>
{
    protected override TriggeredEffect<MoveUnitAbilityArgs> DefaultTriggeredEffect { get; }
    protected override ContinuousEffect<MoveUnitAbilityArgs> DefaultEffectInUpdate { get; }
    protected override ContinuousEffect<MoveUnitAbilityArgs> DefaultEffectInFixedUpdate { get; }

    public MoveUnitAbility(string name) : base(name)
    {
    }
}