using Entities.Effects;

namespace Entities.Abilities
{
    public class UseItemAbility : Ability<UseItemAbility.UseItemAbilityArgs>
    {
        public UseItemAbility() : base("Use Item")
        {
        }

        protected override TriggeredEffect<UseItemAbilityArgs> DefaultTriggeredEffect { get; }
        
        public class UseItemAbilityArgs: DefaultTargetAbilityArgs
        {
            public int Power { get; private set; }
            public UseItemAbilityArgs(Entity abilityOwner, Entity abilityTarget, int power) : base(abilityOwner, abilityTarget)
            {
                this.Power = power;
            }
        }
    }
}
