using System;
using System.IO;
using System.Linq;
using ModestTree;
using SDK;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class FillWgl
{
    [PostProcessBuild(10)]
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

        var closureHeadIndex = lines.FindIndex(str => str.Contains("</head>"));
        lines.InsertRange(closureHeadIndex, HeadLines);

        var bodyIndex = lines.FindIndex(str => str.Contains("<body>"));
        lines.InsertRange(bodyIndex + 1, BodyLines);

        File.WriteAllLines(filePath, lines.ToArray());
    }

    private static string[] HeadLines
    {
        get
        {
            string headFilePath = AppendPlatformPostfix("insertHeadJs");
            return ReadLinesFromFile(headFilePath);
        }
    }

    private static string[] BodyLines
    {
        get
        {
            string bodyFilePath = AppendPlatformPostfix("insertBodyJs");
            return ReadLinesFromFile(bodyFilePath);
        }
    }

    private static string AppendPlatformPostfix(string source)
    {
        var platform = SDKProvider.GetSDK();
        return platform switch
        {
            SDKProvider.SDKType.Yandex => source + "Y",
            SDKProvider.SDKType.Vk => source + "V",
            SDKProvider.SDKType.Poki => source + "P",
            _ => source
        };
    }

    private static string[] ReadLinesFromFile(string filenameUniquePart)
    {
        var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;
        if (projectRootPath == null)
            throw new InvalidOperationException();

        var containers = Directory
            .GetFiles(projectRootPath)
            .Where(file => file.Contains(filenameUniquePart))
            .ToArray();
        
        return containers.Length == 0 ? Array.Empty<string>() : File.ReadAllLines(containers.First());
    }
}