using Zenject;

namespace SDK.Platform.domain
{
    public class YandexPlatformProvider: IPlatformProvider
    {
        [Inject] private Plugins.YSDK.YandexSDK sdk; 
        
        public SDK.Platform.domain.Platform GetCurrentPlatform()
        {
#if YANDEX_SDK
            return sdk.GetIsOnDesktop() ? SDK.Platform.domain.Platform.Desktop : SDK.Platform.domain.Platform.Mobile;
#endif
            return SDK.Platform.domain.Platform.Desktop;
        }
    }
}