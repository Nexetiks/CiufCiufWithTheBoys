using Common.AIBase;

namespace EarthEater.EnemyAi
{
    public class AttackNode : Node
    {
        private TestRailwaySpawner player; //TODO change
        //private Enemy enemy

        public AttackNode(TestRailwaySpawner player)
        {
            this.player = player;
        }

        public override NodeState Evaluate()
        {
            //Attack
            return default;
        }
    }
}