using System.Diagnostics;

namespace RazorBlade.Nodes
{
    [DebuggerDisplay("Expression: {Content}")]
    public class ExpressionNode : Node
    {
        public ExpressionNode(string content) : base(NodeType.Expression)
        {
            Content = content;
        }

        public string Content { get; set; }

        public override string ToString()
        {
            return "@" + Content;
        }
    }
}