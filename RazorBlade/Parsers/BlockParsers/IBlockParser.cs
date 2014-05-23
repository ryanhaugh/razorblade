using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using RazorBlade.Nodes;

namespace RazorBlade.Parsers.BlockParsers
{
    public interface IBlockParser
    {
        IEnumerable<Node> Parse(Block block);
    }
}