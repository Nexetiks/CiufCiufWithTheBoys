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

        private Node topNode;

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
            ai.Entity.TryGetComponent(out AbilitiesHandler aiHandler);
       
            
            
            IsInRangeNode isInAttackRangeNode = new IsInRangeNode(player.Entity, ai.Entity, attackRange);
            AttackNode attackNode = new AttackNode(aiHandler, damage);
            IsInRangeNode isInSightNode = new IsInRangeNode(player.Entity, ai.Entity, sightRange);
            MoveNode chasePlayerNode = new MoveNode(aiHandler, player.Entity.GameObject.transform);
            MoveNode patrolNode = new MoveNode(aiHandler, player.Entity.GameObject.transform); // TODO fix it should patrol insted of chasing all the time

            
            Sequence attackSequence = new Sequence(new List<Node> { isInAttackRangeNode, attackNode });
            Sequence chaseSequence = new Sequence(new List<Node> { isInSightNode, chasePlayerNode });
            Sequence patrolSequence = new Sequence(new List<Node> { patrolNode });

            topNode = new Selector(new List<Node> { attackSequence, chaseSequence, patrolSequence });
        }
    }
}