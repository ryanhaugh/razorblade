using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Razor.Parser.SyntaxTree;
using RazorBlade.Nodes;

namespace RazorBlade.Parsers.SpanParsers
{
    public class MarkupSpanParser : ISpanParser
    {
        private static readonly Regex _commentParserRegex;

        static MarkupSpanParser()
        {
            _commentParserRegex = new Regex(@"([\w\W]+?)<!--([\w\W]+?)-->");
        }

        public IEnumerable<Node> Parse(Span span)
        {
            List<Node> nodes = new List<Node>();

            string content = span.Content;

            if (string.IsNullOrEmpty(content))
            {
                return nodes;
            }

            string remainingContent = _commentParserRegex.Replace(content, match =>
            {
                string markup = match.Groups[1].Value;
                string comment = match.Groups[2].Value;

                if (!string.IsNullOrEmpty(markup))
                {
                    nodes.Add(new MarkupNode(markup));
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    nodes.Add(new CommentNode(comment));
                }

                return string.Empty;
            });

            if (!string.IsNullOrWhiteSpace(remainingContent))
            {
                nodes.Add(new MarkupNode(remainingContent));
            }

            return nodes;
        }
    }
}