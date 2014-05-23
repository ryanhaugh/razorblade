using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using RazorBlade.Nodes;

namespace RazorBlade.Parsers.BlockParsers
{
    public class MarkupBlockParser : IBlockParser
    {
        public IEnumerable<Node> Parse(Block block)
        {
            Node[] nodes = SyntaxTreeParser.Traverse(block).ToArray();

            if (nodes.Length == 0)
            {
                return SyntaxTreeParser.EmptyNodeCollection;
            }

            return nodes.All(node => node.Type == NodeType.Markup || node.Type == NodeType.Expression) 
                ? nodes 
                : new[] { new BlockNode(nodes) };
        }
    }
}