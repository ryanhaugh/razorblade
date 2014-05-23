using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using RazorBlade.Nodes;
using RazorBlade.Parsers.CodeSpanParsers;

namespace RazorBlade.Parsers.SpanParsers
{
    public class CodeSpanParser : ISpanParser
    {
        private static readonly Dictionary<BlockType, ICodeSpanParser> _parsers;

        static CodeSpanParser()
        {
            _parsers = new Dictionary<BlockType, ICodeSpanParser>
            {
                { BlockType.Expression, new ExpressionCodeSpanParser() }
            };
        }

        public IEnumerable<Node> Parse(Span span)
        {
            return _parsers.ContainsKey(span.Parent.Type)
                ? _parsers[span.Parent.Type].Parse(span)
                : SyntaxTreeParser.EmptyNodeCollection;
        }
    }
}