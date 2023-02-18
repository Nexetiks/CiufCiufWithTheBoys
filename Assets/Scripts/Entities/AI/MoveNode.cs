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
        private IsInRangeNode isInRangeNode;

        public MoveNode(AbilitiesHandler abilitiesHandler, Rigidbody2D rb, IsInRangeNode isInRangeNode)
        {
            this.abilitiesHandler = abilitiesHandler;
            this.rb = rb;
            this.isInRangeNode = isInRangeNode;
        }

        public override NodeState Evaluate()
        {
            if (isInRangeNode == null || isInRangeNode.GetTarget() == null)
            {
                return NodeState.Failure;
            }

            Vector2 position = isInRangeNode.GetTarget().Entity.GameObject.transform.position;
            abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(position, rb));
            return NodeState.Success;
        }
    }
}