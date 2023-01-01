namespace Entities.Effects
{
    public class DefaultAbilityEffectArgs : EffectArgs
    {
        public Entity AbilityOwner { get; private set; }

        public DefaultAbilityEffectArgs(Entity abilityOwner)
        {
            AbilityOwner = abilityOwner;
        }
    }
}
