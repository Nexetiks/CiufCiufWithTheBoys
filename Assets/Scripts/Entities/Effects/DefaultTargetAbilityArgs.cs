namespace Entities.Effects
{
    public class DefaultTargetAbilityArgs : DefaultAbilityArgs
    {
        public Entity AbilityTarget { get; private set; }

        public DefaultTargetAbilityArgs(Entity abilityTarget)
        {
            AbilityTarget = abilityTarget;
        }
    }
}
