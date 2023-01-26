namespace Common.AIBase
{
    /// <summary>
    /// Class that inverts outcome of node, it is useful if you would like to check opposite condition without creating new script.
    /// </summary>
    public class Inverter : Node
    {
        protected Node Node;

        /// <summary>
        /// Constructor that allow you to set all required data for script to run correctly. 
        /// </summary>
        public Inverter(Node Node)
        {
            this.Node = Node;
        }

        /// <summary>
        /// Evaluating and returning node state.
        /// </summary>
        public override NodeState Evaluate()
        {
            if (Node.Evaluate() == NodeState.Success)
            {
                nodeState = NodeState.Failure;
                return NodeState.Failure;
            }
            else if (Node.Evaluate() == NodeState.Failure)
            {
                nodeState = NodeState.Success;
                return nodeState;
            }

            nodeState = NodeState.Running;
            return nodeState;
        }
    }
}