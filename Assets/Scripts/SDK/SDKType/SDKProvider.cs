namespace SDK.SDKType
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
#elif CRAZY_SDK
        return SDKType.Crazy;
#endif
        return SDKType.None;
        }

        public enum SDKType
        {
            None,
            Yandex,
            Vk,
            Poki,
            Crazy
        }
    }
}