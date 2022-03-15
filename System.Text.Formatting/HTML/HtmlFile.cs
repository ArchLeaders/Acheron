using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace System.Text.Formatting.HTML
{
    public class HtmlFile
    {
        public string Title { get; set; } = "Document";
        public List<string> Links { get; set; } = new();
        public List<string> Fonts { get; set; } = new();
        public CssFile Style { get; set; } = new();
        public string HtmlText { get; set; } = "";

        public HtmlFile(string html)
        {
            if (File.Exists(html))
                HtmlText = File.ReadAllText(html);

            else HtmlText = html;
        }

        public async Task Write(string outputFile, params Variable[] variables)
        {
            string outString = "";
            bool inOpen = false;

            string tagName = "";

            foreach (char _char in HtmlText)
            {
                if (_char == '<') inOpen = true; // open tag

                else if (_char == '>') // close tag
                {
                    inOpen = false;

                    if (tagName == "head")
                    {

                    }
                }

                else if (inOpen) tagName += _char;

                outString += _char;
            }
        }

        public class Variable
        {
            public string Name { get; set; } = "";
            public dynamic Value { get; set; } = "";
        }
    }
}
