using System.Collections.Generic;

namespace RazorBlade.Nodes
{
    public class BlockNode : Node
    {
        private readonly IEnumerable<Node> _children;

        public BlockNode(IEnumerable<Node> children) : base(NodeType.Block)
        {
            _children = children;
        }

        public IEnumerable<Node> Children
        {
            get { return _children; }
        }
    }
}