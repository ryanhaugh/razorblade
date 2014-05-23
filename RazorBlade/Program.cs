using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Razor;
using RazorBlade.Nodes;

namespace RazorBlade
{
    public class IndexModel<T>
    {
        public string Name { get; set; }
    }
    internal class Program
    {
        private static void Main()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"html\foreach.cshtml");

            RazorBladeHost host = new RazorBladeHost(path);

            GeneratorResults results = host.GenerateCode();

            bool success = results.Success;

            IEnumerable<Node> output = SyntaxTreeParser.Parse(results);

            StringBuilder builder = new StringBuilder();

            foreach (var node in output)
            {
                builder.Append(node);
            }

            var html = builder.ToString();
        }
    }
}