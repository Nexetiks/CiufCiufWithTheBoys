using System;

namespace Common.AIBase
{
    /// <summary>
    /// The basic and abstract class of Node, others AI script will inherit from this class.
    /// </summary>
    [Serializable]
    public abstract class Node
    {
        /// <summary>
        /// Actual state of node.
        /// </summary>
        public NodeState NodeState
        {
            get { return nodeState; }
            private set { nodeState = value; }
        }

        protected NodeState nodeState;

        /// <summary>
        /// Evaluating and returning node state.
        /// </summary>
        public abstract NodeState Evaluate();
    }
}