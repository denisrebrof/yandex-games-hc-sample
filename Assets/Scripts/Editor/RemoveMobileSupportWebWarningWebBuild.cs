#if !UNITY_2020_1_OR_NEWER //Not needed anymore in 2020 and above
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

/// <summary>
/// removes a warning popup for mobile builds, that this platform might not be supported:
/// "Please note that Unity WebGL is not currently supported on mobiles. Press OK if you wish to continue anyway."
/// </summary>
public class RemoveMobileSupportWarningWebBuild
{
	[PostProcessBuild]
	public static void OnPostProcessBuild(BuildTarget target, string targetPath)
	{
		if (target != BuildTarget.WebGL)
			return;

		var buildFolderPath = Path.Combine(targetPath, "Build");
		var info = new DirectoryInfo(buildFolderPath);
		var files = info.GetFiles("*.js");
		foreach (var file in files)
		{
			var filePath = file.FullName;
			var text = File.ReadAllText(filePath);
			text = text.Replace("UnityLoader.SystemInfo.hasWebGL?UnityLoader.SystemInfo.mobile?e.popup(\"Please note that Unity WebGL is not currently supported on mobiles. Press OK if you wish to continue anyway.\",[{text:\"OK\",callback:t}]):[\"Edge\",\"Firefox\",\"Chrome\",\"Safari\"].indexOf(UnityLoader.SystemInfo.browser)==-1?e.popup(\"Please note that your browser is not currently supported for this Unity WebGL content. Press OK if you wish to continue anyway.\",[{text:\"OK\",callback:t}]):t():e.popup(\"Your browser does not support WebGL\",[{text:\"OK\",callback:r}])", "t()");

			Debug.Log("Removing all webgl compatibility warnings from " + filePath);
			File.WriteAllText(filePath, text);
		}
	}
}
#endif