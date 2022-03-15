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

        #region App | Async/Void Functions

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <param name="awaitExit">Waits for the program to exit before proceeding.</param>
        /// <param name="shellExecute">Opens the file with Windows Shell</param>
        /// <param name="workingDirectory">Directory in whcih the process starts in</param>
        /// <param name="output">Process output stream</param>
        public static void App(string filename, string args, bool awaitExit, bool shellExecute, string workingDirectory, ref StreamReader output)
        {
            Process process = new();

            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = shellExecute;
            process.StartInfo.WorkingDirectory = workingDirectory;

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
        public static async Task App(string filename, string args = "", bool awaitExit = true, bool shellExecute = true, string workingDirectory = ".\\")
        {
            Process process = new();

            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = shellExecute;
            process.StartInfo.WorkingDirectory = workingDirectory;

            process.Start();

            if (awaitExit)
            {
                await process.WaitForExitAsync();
            }
        }

        #endregion

        #region App | Overloads

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <param name="awaitExit">Waits for the program to exit before proceeding.</param>
        /// <param name="shellExecute">Opens the file with Windows Shell</param>
        public static async Task App(string filename, string args, bool awaitExit, bool shellExecute) => await App(filename, args, awaitExit, shellExecute, ".\\");

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <param name="awaitExit">Waits for the program to exit before proceeding.</param>
        public static async Task App(string filename, string args, bool awaitExit) => await App(filename, args, awaitExit, true, ".\\");

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        public static async Task App(string filename, string args) => await App(filename, args, true, true, ".\\");

        /// <summary>
        /// Opens <paramref name="filename"/>.
        /// </summary>
        /// <param name="filename"></param>
        public static async Task App(string filename) => await App(filename, "", true, true, ".\\");

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <param name="awaitExit">Waits for the program to exit before proceeding.</param>
        /// <param name="shellExecute">Opens the file with Windows Shell</param>
        /// <param name="workingDirectory">Directory in whcih the process starts in</param>
        public static async Task App(string filename, bool awaitExit, bool shellExecute, string workingDirectory, params string[] args) => await App(filename, string.Join(' ', args), awaitExit, shellExecute, workingDirectory);

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <param name="awaitExit">Waits for the program to exit before proceeding.</param>
        /// <param name="shellExecute">Opens the file with Windows Shell</param>
        public static async Task App(string filename, bool awaitExit, bool shellExecute, params string[] args) => await App(filename, string.Join(' ', args), awaitExit, shellExecute, ".\\");

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <param name="awaitExit">Waits for the program to exit before proceeding.</param>
        public static async Task App(string filename, bool awaitExit, params string[] args) => await App(filename, string.Join(' ', args), awaitExit, true, ".\\");

        /// <summary>
        /// Opens <paramref name="filename"/> and passes <paramref name="args"/> to the newly created process.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        public static async Task App(string filename, params string[] args) => await App(filename, string.Join(' ', args), true, true, ".\\");

        #endregion
    }
}
