namespace SDK
{
    public static class SDKProvider
    {
        public static SDKType GetSDK()
        {
#if YANDEX_SDK
            return SDKType.Yandex;
#elif VK_SDK
        return SDKType.Vk;
#elif POKI_SDK
        return SDKType.Poki;
#endif
            return SDKType.None;
        }

        public enum SDKType
        {
            None,
            Yandex,
            Vk,
            Poki
        }
    }
}