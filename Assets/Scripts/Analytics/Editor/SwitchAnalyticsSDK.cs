using System.Collections.Generic;
using UnityEditor;
using Utils.Editor;

namespace Analytics.Editor
{
    public class SwitchAnalyticsSDK : DefineSymbolsFieldController<AnalyticsType>
    {
        protected override Dictionary<AnalyticsType, string> Symbols => new()
        {
            { AnalyticsType.None, "" },
            { AnalyticsType.Debug, "DEBUG_ANALYTICS" },
            { AnalyticsType.GameAnalytics, "GAME_ANALYTICS" },
        };

        [MenuItem("AnalyticsSDK/Set Game Analytics", false)]
        public static void SetGameAnalytics() => SetSDKType(AnalyticsType.GameAnalytics);
        
        [MenuItem("AnalyticsSDK/Set Debug", false)]
        public static void SetDebug() => SetSDKType(AnalyticsType.Debug);

        [MenuItem("AnalyticsSDK/Set None", false)]
        public static void SetNone() => SetSDKType(AnalyticsType.None);

        private static void SetSDKType(AnalyticsType type) => new SwitchAnalyticsSDK().SetVariant(type);
    }
}