using System.Diagnostics;
using System.Threading;

namespace ByondRebuilder
{
    class Program
    {
        const string DefaultPathToByondBin = @"C:\Program Files (x86)\BYOND\bin";
        const string ByondCompiler = @"dm.exe";
        const string ByondDreamDaemon = @"DreamDaemon.exe";
        const string DefaultSecurity = @"safe";

        private static string pathToByondBin;
        private static string pathToProjectDmeFile;
        private static string pathToProjectDmb;
        private static string security;

        /// <summary>
        /// Example Short Usage: ByondRebuilder.exe "C:\MyProject\MyProject.dme" "C:\MyProject\MyProject.dmb"
        /// Example Full Usage: ByondRebuilder.exe "C:\MyProject\MyProject.dme" "C:\MyProject\MyProject.dmb" trusted "K:\BYOND\bin"
        /// </summary>
        private static void Main(string[] args)
        {
            // TODO: Improve arg handling
            pathToProjectDmeFile = args[0];
            pathToProjectDmb = args[1];

            if (args.Length > 2)
            {
                security = args[2];
            }

            if (args.Length > 3)
            {
                pathToByondBin = args[3];
            }

            if (string.IsNullOrEmpty(pathToByondBin))
            {
                pathToByondBin = DefaultPathToByondBin;
            }

            if (string.IsNullOrEmpty(security))
            {
                security = DefaultSecurity;
            }

            LaunchProcess("TaskKill", $"/IM {ByondDreamDaemon} /F");
            Thread.Sleep(2000);

            CompileDme($@"-clean {pathToProjectDmeFile}");
            Thread.Sleep(2000);

            LaunchDreamDaemon($@"{pathToProjectDmb} -{security}");
        }

        // Could be handy as a shortcut when debugging locally/offline
        private static void LaunchDmb(string filePathToDmb)
        {
            LaunchProcess(filePathToDmb);
        }

        private static void CompileDme(string args)
        {
            LaunchProcess($@"{pathToByondBin}\{ByondCompiler}", args);
        }

        private static void LaunchDreamDaemon(string pathToDmbWithArgs)
        {
            LaunchProcess($@"{pathToByondBin}\{ByondDreamDaemon}", pathToDmbWithArgs, ProcessWindowStyle.Normal);
        }

        private static void LaunchProcess(string fileName, string additionalArgs = "", ProcessWindowStyle windowStyle = ProcessWindowStyle.Hidden)
        {
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = additionalArgs;
            process.StartInfo.WindowStyle = windowStyle;
            process.Start();
        }
    }
}
