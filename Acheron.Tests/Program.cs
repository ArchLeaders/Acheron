using System.Operations;
using System.Text.Formatting.HTML;

Console.WriteLine("Debugging . . .");

HtmlFile html = new HtmlFile(@"D:\Visual Studio\Projects\Practice\HTML\Syntax\index.html");

await html.Write(@"D:\Visual Studio\Projects\Practice\HTML\Syntax\index2.html");