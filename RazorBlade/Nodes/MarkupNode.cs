using System.Diagnostics;

namespace RazorBlade.Nodes
{
    [DebuggerDisplay("Markup: {Content}")]
    public class MarkupNode : Node
    {
        public MarkupNode(string content) : base(NodeType.Markup)
        {
            Content = content;
        }

        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}