using Common.AIBase;
using Entities.Abilities;

public class CanAttackNode : Node
{
    private AbilitiesHandler abilitiesHandler;

    public CanAttackNode(AbilitiesHandler abilitiesHandler)
    {
        this.abilitiesHandler = abilitiesHandler;
    }

    public override NodeState Evaluate()
    {
        if (abilitiesHandler.TryGetAbility(out DefaultAttackAbility defaultAttackAbility))
        {
            if (defaultAttackAbility.CanPerform)
            {
                return NodeState.Success;
            }

            return NodeState.Failure;
        }

        return NodeState.Failure;
    }
}