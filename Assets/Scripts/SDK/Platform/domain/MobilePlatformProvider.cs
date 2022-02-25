namespace SDK.Platform.domain
{
    public class MobilePlatformProvider: IPlatformProvider
    {
        public Platform GetCurrentPlatform() => Platform.Mobile;
    }
}