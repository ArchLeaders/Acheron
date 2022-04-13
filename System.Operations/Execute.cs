using System.Diagnostics;

namespace System.Operations
{
    /// <summary>
    /// Class holding simplified <c>Process.Start()</c> funtions.
    /// </summary>
    public static class Execute
    {
        /// <summary>
        /// Opens the <paramref name="uri"/> in the default browser.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task Url(Uri uri) => await App("explorer.exe", uri.ToString());

        /// <summary>
        /// Opens the <paramref name="directory"/> in file explorer.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static async Task Explorer(string directory) => await App("explorer.exe", directory);

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <param name="awaitExit">Waits for the program to exit before proceeding.</param>
        /// <param name="shellExecute">Opens the file with Windows Shell</param>
        /// <param name="workingDirectory">Directory in whcih the process starts in</param>
        /// <param name="output">Process output stream</param>
        public static void App(string filename, string args, bool hidden = true, bool awaitExit, bool shellExecute, string workingDirectory, ref StreamReader output)
        {
            Process process = new();

            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = shellExecute;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.CreateNoWindow = hidden;
            process.StartInfo.WindowStyle = hidden ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal;

            if (output != null)
            {
                process.StartInfo.RedirectStandardOutput = true;
                output = process.StandardOutput;
            }

            process.Start();

            if (awaitExit)
            {
                process.WaitForExit();
            }
        }

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <param name="awaitExit">Waits for the program to exit before proceeding.</param>
        /// <param name="shellExecute">Opens the file with Windows Shell</param>
        /// <param name="workingDirectory">Directory in whcih the process starts in</param>
        public static async Task App(string filename, string args = "", bool hidden = true, bool awaitExit = true, bool shellExecute = true, string workingDirectory = ".\\")
        {
            Process process = new();

            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = shellExecute;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.CreateNoWindow = hidden;
            process.StartInfo.WindowStyle = hidden ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal;

            process.Start();

            if (awaitExit)
                await process.WaitForExitAsync();
        }
    }
}
