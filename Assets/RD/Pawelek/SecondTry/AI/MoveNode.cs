using Common.AIBase;
using Entities.Abilities;
using UnityEngine;

public class MoveNode : Node
{
    private AbilitiesHandler abilitiesHandler;
    private Vector2 destination;

    public MoveNode(AbilitiesHandler abilitiesHandler, Vector2 destination)
    {
        this.abilitiesHandler = abilitiesHandler;
        this.destination = destination;
    }

    public override NodeState Evaluate()
    {
        abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(destination));
        return NodeState.Success;
    }
}