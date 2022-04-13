using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace System.Text.Formatting
{
    public static class Convert
    {
        public static TextBlock ToTextBlock(this string text, string fontFamily = "Calibri", double body = 16, double h1 = 28, double h2 = 24, double h3 = 20,
            string quoteBackground = "", string quoteBorder = "#2f2f2f", string codeBackground = "#1f1f1f", bool noWrap = false)
        {
            TextBlock textBlock = new()
            {
                TextWrapping = TextWrapping.WrapWithOverflow,
            };

            // Text span
            string span = "";
            char last = ' ';

            // Quote handling
            bool isQuote = false;

            // Title handling
            bool isHeaderOne = false;
            bool isHeaderTwo = false;
            bool isHeaderThree = false;
            int headerCharSpan = 0;

            // Href handling
            string hrefSpan = "";
            string href = "";

            bool inHrefSpan = false;
            bool isEndHrefSpan = false;
            bool inHref = false;

            // Code block text handling
            bool isInline = false;
            string inline = "";

            // Bold/italic text handling
            List<int> starSequenceArray = new();
            string starSequenceSpan = "";
            char lastStar = ' ';

            // Underline/strikethrough


            // Text styles
            bool italic = false;
            bool bold = false;
            bool underline = false;
            bool strikethrough = false;

            foreach (char _char in text)
            {
                // Handle quotes
                if (_char == '>')
                {
                    isQuote = true;
                }
                else if (_char == '>' && last == '\n')
                {

                }
                else if (last == '>' && _char == ' ') { }

                // Handle newlines
                else if (_char == '\n' || _char == '\r')
                {
                    textBlock.Inlines.Add(Render(ref span)); // Render the span
                    textBlock.Inlines.Add(new LineBreak()); // Render the new line
                    isHeaderOne = false; isHeaderTwo = false; isHeaderThree = false; // Revert all headers
                    headerCharSpan = 0;
                }

                // Handle headers

                else if (_char == '#' && last == '\n')
                {
                    headerCharSpan++;

                    if (headerCharSpan == 1)
                        isHeaderOne = true;
                    else if (headerCharSpan == 2)
                        isHeaderTwo = true;
                    else if (headerCharSpan == 3)
                        isHeaderThree = true;
                    else
                    {
                        headerCharSpan = 0;
                        span += _char;
                    }
                }
                else if (last == '#' && _char == ' ') headerCharSpan = 0;

                // Handle inline code block
                else if (_char == '`')
                {
                    isInline = !isInline;

                    if (!isInline)
                    {
                        textBlock.Inlines.Add(Render(ref span)); // Render the span
                        textBlock.Inlines.Add(Render(ref inline, true)); // Render the inline
                    }
                }
                else if (isInline) inline += _char;

                // Hyperlink

                else if (_char == '[' && !inHrefSpan)
                {
                    textBlock.Inlines.Add(Render(ref span));
                    span = "";
                    inHrefSpan = true;
                }

                else if (_char == ']' && inHrefSpan)
                {
                    inHrefSpan = false;
                    isEndHrefSpan = true;
                }

                else if (_char == '(' && isEndHrefSpan)
                {
                    inHref = true;
                    isEndHrefSpan = false;
                }

                else if (_char != '(' && isEndHrefSpan)
                {
                    isEndHrefSpan = false;
                    textBlock.Inlines.Add($"[{hrefSpan}]{_char}");
                    hrefSpan = "";
                }

                else if (_char == ')' && inHref)
                {
                    inHref = false;

                    Hyperlink hyperlink = new() { NavigateUri = new(href) };
                    hyperlink.RequestNavigate += Actions.OpenURL;

                    hyperlink.Inlines.Add(Render(ref hrefSpan));
                    textBlock.Inlines.Add(hyperlink);

                    hrefSpan = ""; href = "";
                }

                else if (inHrefSpan)
                {
                    hrefSpan = $"{hrefSpan}{_char}";
                }

                else if (inHref)
                {
                    href = $"{href}{_char}";
                }

                // Handle bold/italic
                else if (_char == '*')
                {
                    textBlock.Inlines.Add(Render(ref span)); // Render the span
                    starSequenceSpan += _char; // Add the char to the sequence

                    // Add/remove style
                    if (starSequenceSpan == "*")
                    {
                        italic = !italic;
                    }
                    else if (starSequenceSpan == "**")
                    {
                        italic = !italic;
                        bold = !bold;
                    }
                    else if (starSequenceSpan == "***")
                    {
                        italic = !italic;
                    }

                    if (starSequenceArray.Any())
                    {
                        if (starSequenceArray.Last() == starSequenceSpan.Length)
                        {
                            starSequenceArray.RemoveAt(starSequenceArray.Count - 1); // Remove the last item
                            starSequenceSpan = ""; // Reset the span length
                        }
                    }

                    lastStar = _char; // Set the last char
                }

                // Handle underline
                else if (_char == '_' && !isInline)
                {
                    textBlock.Inlines.Add(Render(ref span)); // Render the span

                    if (last == '_')
                    {
                        underline = !underline;
                    }
                }

                // Handle strikethrough
                else if (_char == '~')
                {
                    textBlock.Inlines.Add(Render(ref span)); // Render the span

                    if (last == '~')
                    {
                        strikethrough = !strikethrough;
                    }
                }

                // Handle defaults
                else
                {
                    if (lastStar == '*')
                    {
                        starSequenceArray.Add(starSequenceSpan.Length); // Set seqLen as seqSpan.Length
                        lastStar = _char; // Set the last char to anything but * to avoid looping
                        starSequenceSpan = ""; // Rest sequence span
                    }

                    // Handle single syntax characters
                    // if (last == '_') span += '_';
                    // if (last == '~') span += '~';

                    span += _char; // Add char to rendered span
                }

                last = _char; // Set last character after handling is complete
            }

            textBlock.Inlines.Add(Render(ref span)); // Render the last span

            return textBlock;

            SolidColorBrush? Brush(string hex) => (SolidColorBrush?)new BrushConverter().ConvertFromString(hex);

            Border Render(ref string span, bool inline = false, bool quote = false)
            {
                Border border = new()
                {
                    CornerRadius = new(2.5),
                    Padding = quote ? new(10,0,10,2.5) : inline ? new(5, 0, 5, 0) : new(0),
                    Background = quote ? Brush(quoteBackground) : inline ? Brush(codeBackground) : null,
                    BorderBrush = quote ? Brush(quoteBorder) : null,
                    BorderThickness = quote ? new(5,0,0,0) : new(0)
                };

                TextBlock child = new() // Create a new Run class
                {
                    Text = span,
                    TextWrapping = noWrap ? TextWrapping.NoWrap : TextWrapping.WrapWithOverflow,
                    FontWeight = bold ? FontWeights.Bold : isHeaderThree ? FontWeights.ExtraBold : isHeaderTwo ? FontWeights.ExtraLight : FontWeights.Normal,
                    FontStyle = italic ? FontStyles.Italic : FontStyles.Normal,
                    FontFamily = inline ? new("Consolas") : new(fontFamily),
                    FontSize = isHeaderThree ? h3 : isHeaderTwo ? h2 : isHeaderOne ? h1 : body
                };

                if (underline) // Add the strikethough decoration
                    child.TextDecorations.Add(TextDecorations.Underline);

                if (strikethrough) // Add the strikethough decoration
                    child.TextDecorations.Add(TextDecorations.Strikethrough);

                border.Child = child;

                span = ""; // Clear span

                return border;
            }
        }
    }
}
