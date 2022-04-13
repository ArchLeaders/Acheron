using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Operations
{
    public static class StringOps
    {
        /// <summary>
        /// Removes <paramref name="x"/> sub folders from the passes Windows Path string.
        /// <para><c>"C:\\ProgramFiles\\App\\SubFolder".EditPath(2) -> "C:\\ProgramFiles"</c></para>
        /// </summary>
        /// <param name="winPath">Generic Windows Path string</param>
        /// <param name="x">Amount of sub folders to be removed from the Windows Path string</param>
        public static string EditPath(this string winPath, int x = 1)
        {
            string results = "";
            string[] exploded = winPath.Split('\\');
            int pos = 0;

            foreach (var expItem in exploded)
            {
                string append = pos == exploded.Length ? "" : "\\";

                if (pos < exploded.Length - x)
                    results = $"{results}{expItem}{append}";

                pos++;
            }

            return results;
        }

        /// <summary>
        /// Reads a string and returns an array of arguments from it.
        /// <para><c>"cd \"C:\\ProgramFiles\\App\"".ParseArgs() -> [ "cd", "C:\\ProgramFiles\\App" ]</c></para>
        /// </summary>
        /// <param name="argumentString"></param>
        /// <returns></returns>
        public static string[] ParseArgs(this string argumentString)
        {
            List<string> results = new();
            string currentArgument = "";
            bool isWrappedString = false;

            // Iterate every character in the passed argumentString
            foreach (char character in argumentString)
            {
                if (character == ' ' & !isWrappedString)
                {
                    // Add argument
                    results.Add(currentArgument);

                    // Reset current argument
                    currentArgument = "";
                }
                else if (character == '\"' | character == '\'')
                {
                    // Enter/exit wrapped argument
                    isWrappedString = !isWrappedString;
                }
                else
                {
                    // Update current argument
                    currentArgument = $"{currentArgument}{character}";
                }
            }

            // Add last argument to list
            results.Add(currentArgument);

            return results.ToArray();
        }
    }
}
