using SDK;
using SDK.SDKType;

namespace Analytics.ads
{
    public static class AdProviderProvider
    {
        public static AdProvider CurrentProvider
        {
            get
            {
                var sdk = SDKProvider.GetSDK();
                switch (sdk)
                {
                    case SDKProvider.SDKType.Yandex:
                        return AdProvider.Yandex;
                    case SDKProvider.SDKType.Vk:
                        return AdProvider.VK;
                    case SDKProvider.SDKType.Poki:
                        return AdProvider.Poki;
                    default:
                        return AdProvider.None;
                }
            }
        }
    }
}