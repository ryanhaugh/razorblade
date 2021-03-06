using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using RazorBlade.Nodes;

namespace RazorBlade.Parsers.CodeSpanParsers
{
    public class ExpressionCodeSpanParser : ICodeSpanParser
    {
        public IEnumerable<Node> Parse(Span span)
        {
            return new[]
            {
                new ExpressionNode(span.Content)
            };
        }
    }
}