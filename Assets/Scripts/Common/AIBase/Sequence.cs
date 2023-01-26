using System.Collections.Generic;

namespace Common.AIBase
{
    /// <summary>
    /// Class that checks all its node children and return Success if all of its node children are success.
    /// </summary>
    public class Sequence : Node
    {
        protected List<Node> Nodes = new List<Node>();

        /// <summary>
        /// Constructor that allow you to set all required data for script to run correctly. 
        /// </summary>
        public Sequence(List<Node> Nodes)
        {
            this.Nodes = Nodes;
        }

        /// <summary>
        /// Evaluating and returning node state.
        /// </summary>
        public override NodeState Evaluate()
        {
            bool isAnyStateRunning = false;

            foreach (Node node in Nodes)
            {
                if (node.Evaluate() == NodeState.Running)
                {
                    isAnyStateRunning = true;
                }
                else if (node.Evaluate() == NodeState.Failure)
                {
                    nodeState = NodeState.Failure;
                    return nodeState;
                }
            }

            nodeState = isAnyStateRunning ? NodeState.Running : NodeState.Success;
            return nodeState;
        }
    }
}