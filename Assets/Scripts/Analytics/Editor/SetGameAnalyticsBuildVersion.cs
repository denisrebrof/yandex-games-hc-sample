#if GAME_ANALYTICS
using GameAnalyticsSDK.Setup;
using SDK;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Analytics.Editor
{
    public class SetGameAnalyticsBuildVersion : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var result = AssetDatabase.FindAssets("Settings", new[] { "Assets/Resources/GameAnalytics" });
            var path = AssetDatabase.GUIDToAssetPath(result[0]);
            var settingsObject = (Settings)AssetDatabase.LoadAssetAtPath(path, typeof(Settings));
            var version = BuildPlatformVersionProvider.GetBuildVersion();
            Debug.Log("Analytics Version: " + version);
            for (var i = 0; i < settingsObject.Build.Count; i++)
            {
                settingsObject.Build[i] = version;
            }

            EditorUtility.SetDirty(settingsObject);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif