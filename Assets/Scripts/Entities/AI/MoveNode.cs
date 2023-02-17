using Common.AIBase;
using Entities.Abilities;
using Entities.Abilities.DefaultMove;
using UnityEngine;

namespace Entities.AI
{
    public class MoveNode : Node
    {
        private AbilitiesHandler abilitiesHandler;
        private Transform destination;

        public MoveNode(AbilitiesHandler abilitiesHandler, Transform destination)
        {
            this.abilitiesHandler = abilitiesHandler;
            this.destination = destination;
        }

        public override NodeState Evaluate()
        {
            abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(destination.position));
            return NodeState.Success;
        }
    }
}