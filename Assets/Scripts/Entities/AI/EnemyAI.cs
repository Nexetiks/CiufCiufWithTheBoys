using System.Collections.Generic;
using Common.AIBase;
using Entities.Abilities;
using UnityEngine;

namespace Entities.AI
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private float damage = 0;
        [SerializeField]
        private float speed = 0;
        [SerializeField]
        private float attackRange = 0;
        [SerializeField]
        private float sightRange = 0;
        [SerializeField]
        private EntityContext ai;
        [SerializeField]
        private EntityContext player;

        private Selector topNode;

        private void Start()
        {
            SetUpAiTree();
        }

        private void Update()
        {
            topNode.Evaluate();
        }

        private void SetUpAiTree()
        {
            AbilitiesHandler aiHandler = ai.Entity.GetComponent<AbilitiesHandler>();

            IsInRangeNode isInAttackRangeNode = new IsInRangeNode(attackRange, ai.gameObject.transform);
            AttackNode attackNode = new AttackNode(aiHandler, damage);
            IsInRangeNode isInSightNode = new IsInRangeNode(sightRange, ai.gameObject.transform);
            Rigidbody2D rb = ai.gameObject.GetComponent<Rigidbody2D>();
            MoveNode chasePlayerNode = new MoveNode(aiHandler, rb, isInSightNode);
            MoveNode patrolNode = new MoveNode(aiHandler, rb, isInAttackRangeNode); // TODO fix it should patrol insted of chasing all the time

            Sequence attackSequence = new Sequence(new List<Node> { isInAttackRangeNode, attackNode });
            Sequence chaseSequence = new Sequence(new List<Node> { isInSightNode, chasePlayerNode });
            Sequence patrolSequence = new Sequence(new List<Node> { patrolNode });

            topNode = new Selector(new List<Node> { attackSequence, chaseSequence, patrolSequence });
        }
    }
}