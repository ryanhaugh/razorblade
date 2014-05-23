using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RazorBlade.Nodes;

namespace RazorBlade
{
    public class SyntaxTreeParserResults
    {
        private readonly ReadOnlyCollection<Node> _nodes;

        internal SyntaxTreeParserResults(ReadOnlyCollection<Node> nodes)
        {
            _nodes = nodes;
        }

        public IEnumerable<Node> Nodes
        {
            get { return _nodes; }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (Node node in _nodes)
            {
                builder.Append(node);
            }

            return builder.ToString();
        }
    }
}