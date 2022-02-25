using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Utils.Editor
{
    public static class FileUtils
    {
        public static string ProjectRootPath => Directory.GetParent(Application.dataPath)?.FullName;

        public static string[] ReadLinesFromFileInProjectRoot(string filenameUniquePart)
        {
            var containers = Directory
                .GetFiles(ProjectRootPath)
                .Where(file => file.Contains(filenameUniquePart))
                .ToArray();
        
            return containers.Length == 0 ? Array.Empty<string>() : File.ReadAllLines(containers.First());
        }
    }
}