using Common.AIBase;
using UnityEngine;

namespace Entities.AI
{
    public class IsInRangeNode : Node
    {
        private float radius;
        private Transform ai;
        private EntityContext target;

        public IsInRangeNode(float radius, Transform ai)
        {
            this.radius = radius;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            if (target != null)
            {
                if (Vector2.Distance(target.gameObject.transform.position, ai.position) < radius)
                {
                    Debug.Log("succ");
                    return NodeState.Success;
                }

                Debug.Log("f");
                return NodeState.Failure;
            }

            RaycastHit2D[] allColliders = Physics2D.CircleCastAll(ai.position, 360, Vector2.zero, radius);

            foreach (RaycastHit2D collider in allColliders)
            {
                if (collider.transform.gameObject.TryGetComponent(out EntityContext entityContext))
                {
                    if (entityContext.EntityTag == EntityTag.Player)
                    {
                        target = entityContext;

                        Debug.Log("suc");
                        return NodeState.Success;
                    }
                }
            }

            Debug.Log("f");
            return NodeState.Failure;
        }

        public EntityContext GetTarget()
        {
            return target;
        }
    }
}