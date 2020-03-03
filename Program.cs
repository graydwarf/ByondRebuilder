using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ByondMeUpdater
{
    class Program
    {
        const string PathToDmExe = @"C:\Program Files (x86)\BYOND\bin\dm.exe";
        const string PathToDreamDaemonExe = @"C:\Program Files (x86)\BYOND\bin\dreamdaemon.exe";

        private static void Main()
        {
            LaunchProcess("TaskKill", "/IM DreamDaemon.exe /F");
            Thread.Sleep(2000);

            CompileDme(@"-clean E:\Dad\Projects\BYOND\Applications\ByondMe\ByondMe.dme");
            Thread.Sleep(2000);

            LaunchDreamDaemon(@"E:\Dad\Projects\BYOND\Applications\ByondMe\ByondMe.dmb -trusted");
        }

        // Could be handy as a shortcut when debugging locally/offline
        private static void LaunchDmb(string filePathToDmb)
        {
            LaunchProcess(filePathToDmb);
        }

        private static void CompileDme(string args)
        {
            LaunchProcess(PathToDmExe, args);
        }

        private static void LaunchDreamDaemon(string pathToDmbWithArgs)
        {
            LaunchProcess(PathToDreamDaemonExe, pathToDmbWithArgs, ProcessWindowStyle.Normal);
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
