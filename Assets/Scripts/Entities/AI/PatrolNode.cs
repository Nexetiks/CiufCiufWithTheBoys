using System;
using System.Collections.Generic;
using Common.AIBase;
using Entities.Abilities;
using Entities.Abilities.DefaultMove;
using UnityEngine;

namespace Entities.AI
{
    public class PatrolNode : Node
    {
        private const float Tolerance = 1f;
        private AbilitiesHandler abilitiesHandler;
        private Rigidbody2D rb;
        private List<Vector2> localPositionToMoveAt = new List<Vector2>();
        private int index = 0;

        public PatrolNode(AbilitiesHandler abilitiesHandler, Rigidbody2D rb, List<Vector2> localPositionToMoveAt)
        {
            this.abilitiesHandler = abilitiesHandler;
            this.rb = rb;
            this.localPositionToMoveAt = localPositionToMoveAt;

            for (int i = 0; i < localPositionToMoveAt.Count; i++)
            {
                localPositionToMoveAt[i] += rb.position;
            }
        }

        public override NodeState Evaluate()
        {
            if (Math.Abs(rb.transform.position.x - localPositionToMoveAt[index].x) > Tolerance || Math.Abs(rb.transform.position.y - localPositionToMoveAt[index].y) > Tolerance)
            {
                abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(localPositionToMoveAt[index], rb));
                Debug.Log("AI : MoveNode  :: Running");
            }
            else
            {
                index++;

                if (index >= localPositionToMoveAt.Count)
                {
                    index = 0;
                }

                abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(localPositionToMoveAt[index], rb));

                Debug.Log("AI : MoveNode  :: Success");
            }

            return NodeState.Running;
        }
    }
}