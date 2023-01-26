using System.Collections.Generic;

namespace Common.AIBase
{
    /// <summary>
    /// Class that checks all its node children and return Success/Running if at last one of them is Success/Running.
    /// </summary>
    public class Selector : Node
    {
        protected List<Node> Nodes = new List<Node>();

        /// <summary>
        /// Constructor that allow you to set all required data for script to run correctly. 
        /// </summary>
        public Selector(List<Node> Nodes)
        {
            this.Nodes = Nodes;
        }

        /// <summary>
        /// Evaluating and returning node state.
        /// </summary>
        public override NodeState Evaluate()
        {
            foreach (Node node in Nodes)
            {
                if (node.Evaluate() == NodeState.Success)
                {
                    nodeState = NodeState.Success;
                    return nodeState;
                }
                else if (node.Evaluate() == NodeState.Running)
                {
                    nodeState = NodeState.Running;
                    return nodeState;
                }
            }

            nodeState = NodeState.Failure;
            return nodeState;
        }
    }
}