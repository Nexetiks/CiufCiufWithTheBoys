using Common.AIBase;
using Entities.Abilities;
using Entities.Abilities.DefaultAttack;
using UnityEngine;

namespace Entities.AI
{
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
                    Debug.Log("AI : CanAttackNode  :: Success");
                    return NodeState.Success;
                }
            }

            Debug.Log("AI : CanAttackNode  :: Failure");
            return NodeState.Failure;
        }
    }
}