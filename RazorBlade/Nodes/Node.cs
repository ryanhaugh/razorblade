namespace RazorBlade.Nodes
{
    public abstract class Node
    {
        private readonly NodeType _type;

        protected Node(NodeType type)
        {
            _type = type;
        }

        public NodeType Type
        {
            get { return _type; }
        }
    }
}