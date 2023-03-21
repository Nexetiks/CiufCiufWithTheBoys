using Common.AIBase;
using EarthEater.Components;
using Entities.Abilities;
using Entities.Abilities.DefaultMove;
using UnityEngine;

namespace Entities.AI
{
    public class FollowNode : Node
    {
        private EntityContext myEntity;
        private Rigidbody2D rb;
        private AbilitiesHandler abilitiesHandler;

        public FollowNode(EntityContext myEntity)
        {
            this.myEntity = myEntity;
            rb = myEntity.Entity.GameObject.GetComponent<Rigidbody2D>();
            abilitiesHandler = myEntity.Entity.GetComponent<AbilitiesHandler>();
        }

        public override NodeState Evaluate()
        {
            EntityContext target = myEntity.Entity.GetComponent<AIDataComponent>().Target;

            if (target == null)
            {
                Debug.Log("AI : FollowNode  :: Failure");
                return NodeState.Failure;
            }

            Vector2 playerPosition = target.Entity.GameObject.transform.position;
            Vector2 direction = (playerPosition - (Vector2)rb.gameObject.transform.position).normalized;
            abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(direction));
            Debug.Log("AI : FollowNode  :: Success");
            return NodeState.Success;
        }
    }
}