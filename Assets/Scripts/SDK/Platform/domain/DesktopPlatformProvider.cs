namespace SDK.Platform.domain
{
    public class DesktopPlatformProvider: IPlatformProvider
    {
        public Platform GetCurrentPlatform() => Platform.Desktop;
    }
}