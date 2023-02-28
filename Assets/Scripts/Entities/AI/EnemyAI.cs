using System.Collections.Generic;
using Common.AIBase;
using Entities.Abilities;
using UnityEngine;

namespace Entities.AI
{
    public class EnemyAI : MonoBehaviour
    {
        //TODO add longer range to escape.
        [SerializeField]
        private float damage = 0;
        [SerializeField]
        private float attackRange = 0;
        [SerializeField]
        private float attackRangeFallout = 0;
        [SerializeField]
        private float sightRange = 0;
        [SerializeField]
        private float sightRangeFallout = 1;
        [SerializeField]
        private EntityContext ai;

        [SerializeField]
        private float noiseScrollSpeed;
        [SerializeField]
        private float maxAngle = 10f;
        [SerializeField]
        private float movementSpeed = 1;
        [SerializeField]
        private float minDistanceToStartGoingBack;
        [SerializeField]
        private float maxDistanceFromStartingPoint;

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

            IsInRangeNode isInAttackRangeNode = new IsInRangeNode(attackRange, attackRangeFallout, ai.gameObject.transform);
            AttackNode attackNode = new AttackNode(aiHandler, damage);
            IsInRangeNode isInSightNode = new IsInRangeNode(sightRange, sightRangeFallout, ai.gameObject.transform);
            Rigidbody2D rb = ai.gameObject.GetComponent<Rigidbody2D>();
            FollowNode chasePlayerNode = new FollowNode(aiHandler, rb, isInSightNode);
            PatrolNode patrolNode = new PatrolNode(ai, rb, noiseScrollSpeed, maxAngle, movementSpeed, minDistanceToStartGoingBack, maxDistanceFromStartingPoint, aiHandler);

            Sequence attackSequence = new Sequence(new List<Node> { isInAttackRangeNode, attackNode });
            Sequence chaseSequence = new Sequence(new List<Node> { isInSightNode, chasePlayerNode });
            Sequence patrolSequence = new Sequence(new List<Node> { patrolNode });

            topNode = new Selector(new List<Node> { attackSequence, chaseSequence, patrolSequence });
        }
    }
}