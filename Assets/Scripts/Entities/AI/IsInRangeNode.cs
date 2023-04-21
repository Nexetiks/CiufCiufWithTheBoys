using Common.AIBase;
using EarthEater.Components;
using UnityEngine;

namespace Entities.AI
{
    public class IsInRangeNode : Node
    {
        private float detectionRadius;
        private float detectionRadiusFallout;
        private Transform aiTransform;
        private EntityContext target;
        private EntityContext ai;
        private AIDataComponent aiDataComponent;

        public IsInRangeNode(float detectionRadius, float detectionRadiusFallout, EntityContext ai)
        {
            this.detectionRadius = detectionRadius;
            this.detectionRadiusFallout = detectionRadiusFallout;
            this.ai = ai;

            aiDataComponent = ai.Entity.GetComponent<AIDataComponent>();
            aiTransform = ai.Entity.GameObject.transform;
        }

        public override NodeState Evaluate()
        {
            if (aiDataComponent.IsEscaping)
            {
                return NodeState.Failure;
            }

            if (target != null)
            {
                if (Vector2.Distance(target.gameObject.transform.position, aiTransform.position) < detectionRadiusFallout)
                {
                    Debug.Log(Vector2.Distance(target.gameObject.transform.position, aiTransform.position));
                    Debug.Log("AI : IsInRangeNode  :: Success");
                    return NodeState.Success;
                }

                Debug.Log("AI : IsInRangeNode  :: Failure");
                target = null;
                return NodeState.Failure;
            }

            Collider2D[] allColliders = Physics2D.OverlapCircleAll(ai.transform.position, detectionRadius);

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