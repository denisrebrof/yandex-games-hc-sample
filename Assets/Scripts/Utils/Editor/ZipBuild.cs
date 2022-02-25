using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Utils.Editor
{
    public static class ZipBuild
    {
        [PostProcessBuild(20)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target != BuildTarget.WebGL)
                return;

#if YANDEX_SDK || POKI_SDK || CRAZY_SDK
            var script = Directory.GetFiles(FileUtils.ProjectRootPath).First(file => file.Contains("zip_build"));
            var archiveName = pathToBuiltProject + "_zipped_" + DateTime.Now.ToFileTime();

            var pythonPath = GetPythonPath();
            if(pythonPath==null)
                return;
        
            RunCmd(pythonPath + "python.exe", $"{script} {pathToBuiltProject} {archiveName}");
#endif
        }

        private static void RunCmd(string cmd, string args)
        {
            var start = new ProcessStartInfo
            {
                FileName = cmd, //cmd is full path to python.exe
                Arguments = args, //args is path to .py file and any cmd line args
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            using var process = Process.Start(start);
            if (process == null) return;
            using var reader = process.StandardOutput;
            var result = reader.ReadToEnd();
            Console.Write(result);
        }

        private static string GetPythonPath()
        {
            var entries = Environment.GetEnvironmentVariable("path")?.Split(';');
            string pythonLocation = null;

            if (entries == null) return null;
        
            foreach (var entry in entries)
            {
                if (!entry.ToLower().Contains("python")) continue;
            
                var breadcrumbs = entry.Split('\\');
                foreach (var breadcrumb in breadcrumbs)
                {
                    if (breadcrumb.ToLower().Contains("python"))
                    {
                        pythonLocation += breadcrumb + '\\';
                        break;
                    }
                    pythonLocation += breadcrumb + '\\';
                }
                break;
            }

            return pythonLocation;
        }
    }
}