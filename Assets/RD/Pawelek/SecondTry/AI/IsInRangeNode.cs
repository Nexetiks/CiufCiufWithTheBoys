using Common.AIBase;
using Entities;
using UnityEngine;

public class IsInRangeNode : Node
{
    private Entity targetEntity;
    private Entity sourceEntity;
    private float range;

    public override NodeState Evaluate()
    {
        if (Vector2.Distance(targetEntity.GameObject.transform.position, sourceEntity.GameObject.transform.position) < range)
        {
            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}