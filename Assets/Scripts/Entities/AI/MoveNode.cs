using System;
using Common.AIBase;
using Entities.Abilities;
using Entities.Abilities.DefaultMove;
using UnityEngine;

namespace Entities.AI
{
    public class MoveNode : Node
    {
        private const float Tolerance = 1f;
        private AbilitiesHandler abilitiesHandler;
        private Rigidbody2D rb;
        private Vector2 positionToMoveAt;

        public MoveNode(AbilitiesHandler abilitiesHandler, Rigidbody2D rb,ref Vector2 positionToMoveAt)
        {
            this.abilitiesHandler = abilitiesHandler;
            this.rb = rb;
            this.positionToMoveAt = positionToMoveAt;
        }

        public override NodeState Evaluate()
        {
            if (Math.Abs(rb.transform.position.x - positionToMoveAt.x) > Tolerance || Math.Abs(rb.transform.position.y - positionToMoveAt.y) > Tolerance)
            {
                abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(positionToMoveAt, rb));
                return NodeState.Running;
            }
            else
            {
                return NodeState.Success;
            }
        }
    }
}