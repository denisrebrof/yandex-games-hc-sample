using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class ZipBuild
{
    [PostProcessBuild(20)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.WebGL)
            return;

#if YANDEX_SDK || POKI_SDK
        var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;
        var script = Directory.GetFiles(projectRootPath).First(file => file.Contains("zip_build"));
        var archiveName = pathToBuiltProject + "_zipped_" + DateTime.Now.ToFileTime();

        var pythonpath = GetPythonPath();
        if(pythonpath==null)
            return;
        
        RunCmd(pythonpath + "python.exe", $"{script} {pathToBuiltProject} {archiveName}");
#endif
    }

    private static void RunCmd(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = cmd;//cmd is full path to python.exe
        start.Arguments = args;//args is path to .py file and any cmd line args
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using(Process process = Process.Start(start))
        {
            using(StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.Write(result);
            }
        }
    }

    private static string GetPythonPath()
    {
        var entries = Environment.GetEnvironmentVariable("path").Split(';');
        string python_location = null;

        foreach (string entry in entries)
        {
            if (entry.ToLower().Contains("python"))
            {
                var breadcrumbs = entry.Split('\\');
                foreach (string breadcrumb in breadcrumbs)
                {
                    if (breadcrumb.ToLower().Contains("python"))
                    {
                        python_location += breadcrumb + '\\';
                        break;
                    }
                    python_location += breadcrumb + '\\';
                }
                break;
            }
        }

        return python_location;
    }
}