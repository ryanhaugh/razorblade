using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using RazorBlade.Nodes;

namespace RazorBlade.Parsers.SpanParsers
{
    public interface ISpanParser
    {
        IEnumerable<Node> Parse(Span span);
    }
}