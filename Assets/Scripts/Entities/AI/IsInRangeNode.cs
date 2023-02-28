using Common.AIBase;
using UnityEngine;

namespace Entities.AI
{
    public class IsInRangeNode : Node
    {
        private float detectionRadius;
        private float detectionRadiusFallout;
        private Transform ai;
        private EntityContext target;

        public IsInRangeNode(float detectionRadius, float detectionRadiusFallout, Transform ai)
        {
            this.detectionRadius = detectionRadius;
            this.detectionRadiusFallout = detectionRadiusFallout;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            if (target != null)
            {
                if (Vector2.Distance(target.gameObject.transform.position, ai.position) < detectionRadiusFallout)
                {
                    Debug.Log(Vector2.Distance(target.gameObject.transform.position, ai.position));
                    Debug.Log("AI : IsInRangeNode  :: Success");
                    return NodeState.Success;
                }

                Debug.Log("AI : IsInRangeNode  :: Failure");
                target = null;
                return NodeState.Failure;
            }

            Collider2D[] allColliders = Physics2D.OverlapCircleAll(ai.position, detectionRadius);

            foreach (Collider2D collider in allColliders)
            {
                if (!collider.transform.gameObject.TryGetComponent(out EntityContext entityContext)) continue;

                if (entityContext.EntityTag != EntityTag.Player) continue;

                target = entityContext;

                Debug.Log("AI : IsInRangeNode  :: Success");
                return NodeState.Success;
            }

            Debug.Log("AI : IsInRangeNode  :: Failure");
            target = null;
            return NodeState.Failure;
        }

        public EntityContext GetTarget()
        {
            return target;
        }
    }
}