using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Razor;
using System.Web.Razor.Parser.SyntaxTree;
using RazorBlade.Nodes;
using RazorBlade.Parsers.BlockParsers;
using RazorBlade.Parsers.SpanParsers;

namespace RazorBlade
{
    public static class SyntaxTreeParser
    {
        private static readonly IDictionary<BlockType, IBlockParser> _blockParsers;

        private static readonly ReadOnlyCollection<Node> _emptyNodeCollection;

        private static readonly IDictionary<SpanKind, ISpanParser> _spanParsers;

        static SyntaxTreeParser()
        {
            _emptyNodeCollection = new ReadOnlyCollection<Node>(new Node[0]);

            _blockParsers = new Dictionary<BlockType, IBlockParser>
            {
                { BlockType.Markup, new MarkupBlockParser() },
                { BlockType.Expression, new ExpressionBlockParser() }
            };

            _spanParsers = new Dictionary<SpanKind, ISpanParser>
            {
                { SpanKind.Markup, new MarkupSpanParser() },
                { SpanKind.Code, new CodeSpanParser() }
            };
        }

        internal static ReadOnlyCollection<Node> EmptyNodeCollection
        {
            get { return _emptyNodeCollection; }
        }

        public static SyntaxTreeParserResults Parse(GeneratorResults results)
        {
            Node[] nodes = Traverse(results.Document).ToArray();

            return new SyntaxTreeParserResults(new ReadOnlyCollection<Node>(nodes));
        }

        public static IEnumerable<Node> Traverse(Block block)
        {
            Node previousNode = null;

            foreach (SyntaxTreeNode child in block.Children)
            {
                IEnumerable<Node> nodes;

                if (child is Span)
                {
                    nodes = ParseSpan(child as Span);
                }
                else
                {
                    nodes = ParseBlock(child as Block);
                }

                foreach (Node node in nodes)
                {
                    if (node.Type == NodeType.Markup)
                    {
                        if (previousNode != null)
                        {
                            ((MarkupNode)previousNode).Content += ((MarkupNode)node).Content;
                        }
                        else
                        {
                            previousNode = node;
                        }
                    }
                    else
                    {
                        if (previousNode != null)
                        {
                            yield return previousNode;
                        }

                        previousNode = null;

                        yield return node;
                    }
                }
            }

            if (previousNode != null)
            {
                yield return previousNode;
            }
        }

        private static IEnumerable<Node> ParseBlock(Block block)
        {
            return _blockParsers.ContainsKey(block.Type)
                ? _blockParsers[block.Type].Parse(block)
                : _emptyNodeCollection;
        }

        private static IEnumerable<Node> ParseSpan(Span span)
        {
            return _spanParsers.ContainsKey(span.Kind)
                ? _spanParsers[span.Kind].Parse(span)
                : _emptyNodeCollection;
        }

        //private static void TraverseOld(List<Node> nodes, Block node)
        //{
        //    foreach (SyntaxTreeNode child in node.Children)
        //    {
        //        if (child is Span)
        //        {
        //            Span span = child as Span;

        //            //IEnumerable<Node> nodes1 = ProcessSpan(child as Span);

        //            SpanKind kind = span.Kind;

        //            switch (kind)
        //            {
        //                case SpanKind.Code:
        //                {
        //                    if (node.Type == BlockType.Expression)
        //                    {
        //                        //Regex regex = new Regex(@"^\w+\.");
        //                        //string expression = regex.Replace(span.Content, string.Empty);
        //                        //builder
        //                        //    .Append("{{")
        //                        //    .Append(expression)
        //                        //    .Append("}}");
        //                    }

        //                    break;
        //                }
        //                case SpanKind.Comment:
        //                {
        //                    break;
        //                }
        //                case SpanKind.Markup:
        //                {
        //                    if (!string.IsNullOrWhiteSpace(span.Content))
        //                    {
        //                        nodes.Add(new MarkupNode(span.Content));
        //                    }

        //                    break;
        //                }
        //                case SpanKind.MetaCode:
        //                {
        //                    break;
        //                }
        //                case SpanKind.Transition:
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //        else if (child is Block)
        //        {
        //            Block block = child as Block;

        //            BlockType type = block.Type;

        //            bool traverse = true;

        //            switch (type)
        //            {
        //                case BlockType.Comment:
        //                {
        //                    traverse = false;
        //                    break;
        //                }
        //                case BlockType.Directive:
        //                {
        //                    traverse = false;
        //                    break;
        //                }
        //                case BlockType.Expression:
        //                {
        //                    break;
        //                }
        //                case BlockType.Functions:
        //                {
        //                    break;
        //                }
        //                case BlockType.Helper:
        //                {
        //                    break;
        //                }
        //                case BlockType.Markup:
        //                {
        //                    break;
        //                }
        //                case BlockType.Section:
        //                {
        //                    break;
        //                }
        //                case BlockType.Statement:
        //                {
        //                    break;
        //                }
        //                case BlockType.Template:
        //                {
        //                    break;
        //                }
        //            }
        //            if (traverse)
        //            {
        //                TraverseOld(nodes, block);
        //            }
        //        }
        //        else
        //        {
        //            // ReSharper disable once UnusedVariable.Compiler
        //            int x = 1;
        //        }
        //    }
        //}
    }
}