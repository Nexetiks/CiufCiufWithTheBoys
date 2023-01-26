using Common.AIBase;

namespace EarthEater.EnemyAi
{
    public class IdleWalkNode : Node
    {
        private float speed;

        public IdleWalkNode(float speed)
        {
            this.speed = speed;
        }

        public override NodeState Evaluate()
        {
            //move unit
            return default;
        }
    }
}