using System;
using System.IO;
using System.Text;
using System.Web.Razor;

namespace RazorBlade
{
    public class RazorBladeHost : RazorEngineHost
    {
        private readonly string _fullPath;

        public RazorBladeHost(string fullPath) : base(RazorCodeLanguage.GetLanguageByExtension(".cshtml"))
        {
            if (fullPath == null)
            {
                throw new ArgumentNullException("fullPath");
            }

            _fullPath = fullPath;

            EnableLinePragmas = true;
        }

        public bool EnableLinePragmas { get; set; }

        public string FullPath
        {
            get { return _fullPath; }
        }

        public GeneratorResults GenerateCode()
        {
            // Create the engine
            RazorTemplateEngine engine = new RazorTemplateEngine(this);

            // Generate code 
            using (Stream stream = File.OpenRead(_fullPath))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.Default, detectEncodingFromByteOrderMarks: true))
                {
                    return engine.GenerateCode(reader);
                }
            }
        }
    }
}