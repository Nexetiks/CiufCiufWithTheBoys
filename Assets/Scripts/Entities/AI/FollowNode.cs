using Common.AIBase;
using Entities.Abilities;
using Entities.Abilities.DefaultMove;
using UnityEngine;

namespace Entities.AI
{
    public class FollowNode : Node
    {
        private AbilitiesHandler abilitiesHandler;
        private Rigidbody2D rb;
        private IsInRangeNode isInRangeNode;

        public FollowNode(AbilitiesHandler abilitiesHandler, Rigidbody2D rb, IsInRangeNode isInRangeNode)
        {
            this.abilitiesHandler = abilitiesHandler;
            this.rb = rb;
            this.isInRangeNode = isInRangeNode;
        }

        public override NodeState Evaluate()
        {
            if (isInRangeNode == null || isInRangeNode.GetTarget() == null)
            {
                Debug.Log("AI : FollowNode  :: Failure");
                return NodeState.Failure;
            }

            Vector2 playerPosition = isInRangeNode.GetTarget().Entity.GameObject.transform.position;
            Vector2 direction = (playerPosition - (Vector2)rb.gameObject.transform.position).normalized;
            abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(direction));
            Debug.Log("AI : FollowNode  :: Success");
            return NodeState.Success;
        }
    }
}