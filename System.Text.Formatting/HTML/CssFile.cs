using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Formatting.HTML
{
    public class CssFile
    {
        public List<StyleDefinition> Definitions { get; set; } = new();

        public class StyleDefinition
        {
            public string Name { get; set; } = "";

            public List<StyleItem> Styles = new();
        }

        public class StyleItem
        {
            public string Key { get; set; } = "";
            public string Value { get; set; } = "";
        }

        public CssFile(params StyleDefinition[] definitions)
        {
            foreach (var def in definitions)
                Definitions.Add(def);
        }

        /// <summary>
        /// Writes the CSSFile class to a string.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Write()
        {
            string str = "";

            foreach (var def in Definitions)
            {
                str += $"{def.Name} {{\n";

                foreach (var style in def.Styles)
                    str += $"\t{style.Key}: {style.Value};\n";

                str += "}\n";
            }

            return str;
        }

        /// <summary>
        /// Writes the CSSFile class to a file.
        /// </summary>
        /// <param name="outputCss">Output file</param>
        /// <returns></returns>
        public async Task Write(string outputCss)
        {
            await File.WriteAllTextAsync(outputCss, await Write());
        }

        /// <summary>
        /// Embedds the CSSFile into a HTML file
        /// </summary>
        /// <param name="htmlFile">Output HTML file</param>
        /// <returns></returns>
        public async Task Embed(string htmlFile)
        {

        }

        /// <summary>
        /// Embedds the CSSFile into a HTML class
        /// </summary>
        /// <param name="htmlFile">Input HTMLFile Class</param>
        /// <returns></returns>
        public async Task<HtmlFile> Embed(HtmlFile htmlFile)
        {
            htmlFile.Style = this;
            return htmlFile;
        }

        /// <summary>
        /// Parses a CSS file into a CSSFile class
        /// </summary>
        /// <param name="cssFile"></param>
        /// <returns></returns>
        public async Task Open(string cssFile)
        {

        }
    }
}
