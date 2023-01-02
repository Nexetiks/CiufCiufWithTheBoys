using Entities.Effects;

namespace Entities.Abilities
{
    public class MoveAbility : Ability<MoveEffectArgs>
    {
        public MoveAbility() : base("Default Move")
        {
        }

        protected override TriggeredEffect<MoveEffectArgs> DefaultTriggeredEffect => new MoveTriggeredEffect();
    }
}
