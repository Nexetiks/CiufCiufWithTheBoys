using Common.AIBase;

namespace EarthEater.EnemyAi
{
    public class ChaseNode : Node
    {
        private float chaseSpeed;

        public ChaseNode(float chaseSpeed)
        {
            this.chaseSpeed = chaseSpeed;
        }

        public override NodeState Evaluate()
        {
            //chase
            return default;
        }
    }
}