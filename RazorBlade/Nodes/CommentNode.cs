using System.Diagnostics;

namespace RazorBlade.Nodes
{
    [DebuggerDisplay("Comment: {Content}")]
    public class CommentNode : Node
    {
        private readonly string _content;

        public CommentNode(string content) : base(NodeType.Comment)
        {
            _content = content;
        }

        public string Content
        {
            get { return _content; }
        }

        public override string ToString()
        {
            return "<!--" + Content + "-->";
        }
    }
}