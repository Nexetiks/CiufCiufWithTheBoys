using Entities.Effects;

namespace Entities.Abilities
{
    public class MoveAbility : Ability<MoveEffectArgs>
    {
        public MoveAbility() : base("Default Move")
        {
        }

        protected override Effect<MoveEffectArgs> DefaultEffect => new MoveEffect();
    }
}
