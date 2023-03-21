using Common.AIBase;
using Entities.Abilities;
using Entities.Abilities.DefaultAttack;
using UnityEngine;

namespace Entities.AI
{
    public class AttackNode : Node
    {
        private AbilitiesHandler abilitiesHandler;
        private EntityContext ai;
        private float damage;

        public AttackNode(EntityContext ai, float damage)
        {
            this.ai = ai;
            this.damage = damage;
            abilitiesHandler = ai.Entity.GetComponent<AbilitiesHandler>();
        }

        public override NodeState Evaluate()
        {
            if (abilitiesHandler.TryGetAbility(out DefaultAttackAbility defaultAttackAbility))
            {
                defaultAttackAbility.Perform(new DefaultAttackAbilityArgs(damage));
                Debug.Log("AI : AttackNode  :: Success");
                return NodeState.Success;
            }

            Debug.Log("AI : AttackNode  :: Failure");
            return NodeState.Failure;
        }
    }
}