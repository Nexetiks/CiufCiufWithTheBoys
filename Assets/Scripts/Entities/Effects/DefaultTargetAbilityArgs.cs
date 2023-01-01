namespace Entities.Effects
{
    public class DefaultTargetAbilityArgs : DefaultAbilityEffectArgs
    {
        public Entity AbilityTarget { get; private set; }

        public DefaultTargetAbilityArgs(Entity abilityOwner, Entity abilityTarget) : base(abilityOwner)
        {
            AbilityTarget = abilityTarget;
        }
    }
}
