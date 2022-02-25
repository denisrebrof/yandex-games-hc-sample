using SDK.SDKType;

namespace Utils.Editor.SetupWizard
{
    public static class SDKUtils
    {
        public static string AppendPlatformPostfix(string source)
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
    }
}