using Common.AIBase;
using Entities.Abilities;
using UnityEngine;

public class MoveNode : Node
{
    private AbilitiesHandler abilitiesHandler;
    private Vector2 destination;

    public override NodeState Evaluate()
    {
        abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(destination));
        return default;
    }
}