using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using RazorBlade.Nodes;

namespace RazorBlade.Parsers.CodeSpanParsers
{
    public interface ICodeSpanParser
    {
        IEnumerable<Node> Parse(Span span);
    }
}