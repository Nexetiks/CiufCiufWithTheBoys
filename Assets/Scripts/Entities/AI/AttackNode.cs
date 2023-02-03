using Common.AIBase;
using Entities.Abilities;
using Entities.Abilities.DefaultAttack;

namespace Entities.AI
{
    public class AttackNode : Node
    {
        private AbilitiesHandler abilitiesHandler;
        private float damage;

        public AttackNode(AbilitiesHandler abilitiesHandler, float damage)
        {
            this.abilitiesHandler = abilitiesHandler;
            this.damage = damage;
        }

        public override NodeState Evaluate()
        {
            if (abilitiesHandler.TryGetAbility(out DefaultAttackAbility defaultAttackAbility))
            {
                defaultAttackAbility.Perform(new DefaultAttackAbilityArgs(damage));
                return NodeState.Success;
            }

            return NodeState.Failure;
        }
    }
}