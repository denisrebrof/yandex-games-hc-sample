using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace _Editor.Editor
{
    public static class FillWgl
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target != BuildTarget.WebGL)
                return;

            var files = Directory.GetFiles(pathToBuiltProject);
            var indexFiles = files.Where(file => file.Contains("index"));
            foreach (var indexFileName in indexFiles)
            {
                EditJsCode(indexFileName);
            }
        }

        private static void EditJsCode(string filePath)
        {
            var lines = File.ReadAllLines(filePath).ToList();

            // int titleIndex = lines.FindIndex(str => str.Contains("</title>"));
            // lines.RemoveAt(titleIndex);
            // lines.Insert(titleIndex, "\t<title>Horace: First Trip</title>");

            var closureHeadIndex = lines.FindIndex(str => str.Contains("</head>"));
            lines.InsertRange(closureHeadIndex, HeadLines);

            var bodyIndex = lines.FindIndex(str => str.Contains("<body>"));
            lines.InsertRange(bodyIndex + 1, BodyLines);

            var contentStartIndex = lines.FindIndex(str => str.Contains("<div class=\"webgl-content\">"));
            lines.RemoveRange(contentStartIndex, 8);
            lines.InsertRange(contentStartIndex, ContentLines);

            File.WriteAllLines(filePath, lines.ToArray());
        }

        private static string[] HeadLines
        {
            get { return ReadLinesFromFile("insertHeadJs"); }
        }

        private static string[] BodyLines
        {
            get { return ReadLinesFromFile("insertBodyJs"); }
        }

        private static string[] ContentLines
        {
            get { return ReadLinesFromFile("insertContentJs"); }
        }

        private static string[] ReadLinesFromFile(string filenameUniquePart)
        {
            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;
            if (projectRootPath == null)
                throw new InvalidOperationException();

            var container = Directory.GetFiles(projectRootPath).First(file => file.Contains(filenameUniquePart));
            return File.ReadAllLines(container);
        }
    }
}