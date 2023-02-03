using Common.AIBase;
using UnityEngine;

namespace Entities.AI
{
    public class IsInRangeNode : Node
    {
        private Entity targetEntity;
        private Entity sourceEntity;
        private float range;

        public IsInRangeNode(Entity targetEntity, Entity sourceEntity, float range)
        {
            this.targetEntity = targetEntity;
            this.sourceEntity = sourceEntity;
            this.range = range;
        }

        public override NodeState Evaluate()
        {
            if (Vector2.Distance(targetEntity.GameObject.transform.position, sourceEntity.GameObject.transform.position) < range)
            {
                return NodeState.Success;
            }

            return NodeState.Failure;
        }
    }
}