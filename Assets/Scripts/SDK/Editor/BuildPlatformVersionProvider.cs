using SDK.SDKType;
using UnityEngine;

namespace SDK.Editor
{
    public static class BuildPlatformVersionProvider
    {
        public static string GetBuildVersion()
        {
            var sdkPostfix = SDKProvider.GetSDK() switch
            {
                SDKProvider.SDKType.Yandex => "y",
                SDKProvider.SDKType.Vk => "v",
                SDKProvider.SDKType.Poki => "p",
                SDKProvider.SDKType.Crazy => "c",
                _ => string.Empty
            };

            var version = Application.version;
            if (!string.IsNullOrEmpty(sdkPostfix))
                version += "." + sdkPostfix;

            return version;
        }
    }
}