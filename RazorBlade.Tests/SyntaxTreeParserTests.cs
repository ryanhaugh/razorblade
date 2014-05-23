using System;
using System.IO;
using System.Web.Razor;
using Xunit;
using Xunit.Extensions;

namespace RazorBlade.Tests
{
    public class SyntaxTreeParserTests
    {
        [Theory]
        public void this_is_a_test()
        {
            string path = GetAbsolutePath(@"html\simple.cshtml");

            string expected = File.ReadAllText(path);

            string actual = GetHtml(path);

            Assert.Equal(expected, actual);
        }

        private static string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        private static string GetHtml(string path)
        {
            SyntaxTreeParserResults result = GetSyntaxTreeParserResults(path);

            return result.ToString();
        }

        private static SyntaxTreeParserResults GetSyntaxTreeParserResults(string path)
        {
            RazorBladeHost host = new RazorBladeHost(path);

            GeneratorResults results = host.GenerateCode();

            return SyntaxTreeParser.Parse(results);
        }
    }
}