using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace SDK.Editor
{
    public class SelectFirstScene : IPreprocessBuildWithReport
    {
        public int callbackOrder => 2;

        public void OnPreprocessBuild(BuildReport report)
        {
            var crazyScenePath = Path.Combine("Assets", "Plugins");
            crazyScenePath = Path.Combine(crazyScenePath, "CrazySDK");
            crazyScenePath = Path.Combine(crazyScenePath, "CrazyLogo");
            crazyScenePath = Path.Combine(crazyScenePath, "CrazyLogo.unity");
#if CRAZY_SDK
            SetSceneAsFirst(true, crazyScenePath);
#else
        SetSceneAsFirst(false, crazyScenePath);
#endif
        }

        private static void SetSceneAsFirst(bool enabled, string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
                return;
            // Find valid Scene paths and make a list of EditorBuildSettingsScene
            var editorBuildSettingsScenes = EditorBuildSettings.scenes.ToList();
            var containsScene = editorBuildSettingsScenes.Any(scene => scene.path == scenePath);
            if (enabled == containsScene)
                return;

            if (enabled)
            {
                editorBuildSettingsScenes = editorBuildSettingsScenes.Prepend(new EditorBuildSettingsScene(scenePath, true)).ToList();
            }
            else
            {
                editorBuildSettingsScenes.RemoveAll(scene => scene.path == scenePath);
            }
            // Set the Build Settings window Scene list
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
        }
    }
}