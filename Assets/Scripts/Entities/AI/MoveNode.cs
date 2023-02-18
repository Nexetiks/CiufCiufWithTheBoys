using Common.AIBase;
using Entities.Abilities;
using Entities.Abilities.DefaultMove;
using UnityEngine;

namespace Entities.AI
{
    public class MoveNode : Node
    {
        private AbilitiesHandler abilitiesHandler;
        private Rigidbody2D rb;
        private EntityContext target;

        public MoveNode(AbilitiesHandler abilitiesHandler, Rigidbody2D rb, EntityContext target)
        {
            this.abilitiesHandler = abilitiesHandler;
            this.rb = rb;
            this.target = target;
        }

        public override NodeState Evaluate()
        {
            if (target == null)
            {
                return NodeState.Failure;
            }

            abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(target.gameObject.transform.position, rb));
            return NodeState.Success;
        }
    }
}